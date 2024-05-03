using TMPro;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    [HideInInspector] public float AdMulti = 1;

    [SerializeField] private TMP_Text perSecText;

    private long clickEarn = 1, perSecEarn = 0;

    private void Awake()
    {
        YandexGame.GameReadyAPI();

        Events.EggClicked.AddListener(Clicked);
        Events.EggUpgrade.AddListener(Upgrade);

        CalcTranscend();

        inst = this;

        for (int i = 0; i < ShopEggManager.inst.eggs.Length; i++)
        {
            if (i % 2 == 0) // per click
            {
                clickEarn += ShopEggManager.inst.eggs[i].GetTotalBonus();
            }
            else
            {
                perSecEarn += ShopEggManager.inst.eggs[i].GetTotalBonus();
            }
        }

        UpdateText();
    }

    public void CalcTranscend()
    {
        int num = YandexGame.savesData.TranscendAmount;

        Global.UpgradesDiscount = 1 + num * 0.2f;
        Global.BossHP = 1 + num * 0.1f;
        Global.EggsMulti = 1 + num * 0.1f;
        Global.BossDamage = 1 + num * 0.15f;

        clickEarn = 1;
        perSecEarn = 0;

        UpdateText();
    }

    private float oneSec = 1;

    private void Update()
    {
        oneSec -= Time.deltaTime;

        if (oneSec > 0) return;

        oneSec = 1;

        YandexGame.savesData.EggCurrency += (long)(perSecEarn * Global.EggsMulti);
    }

    private void Clicked()
    {
        long earn = (long)(clickEarn * Global.EggsMulti * AdMulti);

        YandexGame.savesData.EggCurrency += earn;

        Events.ClickEarn.Invoke(earn);
    }

    private void Upgrade(int id)
    {
        var egg = ShopEggManager.inst.eggs[id - 1];

        if (egg.bonusType == EggUpgradeSO.BonusType.Click)
            clickEarn += egg.GetPrevBonus();
        else
        {
            perSecEarn += egg.GetPrevBonus();
            UpdateText();
        }
    }

    private void UpdateText()
    {
        perSecText.text = Global.NumToString((long)(perSecEarn * Global.EggsMulti));
    }
}
