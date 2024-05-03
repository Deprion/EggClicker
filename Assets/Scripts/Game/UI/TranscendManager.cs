using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TranscendManager : MonoBehaviour
{
    [SerializeField] private TMP_Text soulText, titleText, infoText, warningText, priceText;
    [SerializeField] private Button transcendBtn;

    [SerializeField] private ArmorManager armor;

    private long price;

    private void Start()
    {
        transcendBtn.onClick.AddListener(Transcend);

        titleText.text = TranslateManager.inst.GetText("TranscendTitle");
        warningText.text = TranslateManager.inst.GetText("TranscendWarning");

        UpdateInfo();

        Events.SoulsUpdate.AddListener(OnEnable);
    }

    private void UpdateInfo()
    {
        price = (long)Mathf.Pow(YandexGame.savesData.TranscendAmount + 1, 3) + 4;

        priceText.text = price.ToString();

        string now = TranslateManager.inst.GetText("Now");

        string txt = $"-20% ({CalcPerc(Global.UpgradesDiscount):f0}% {now}) {TranslateManager.inst.GetText("TranscendUpDiscount")}\n";
        txt += $"-10% ({CalcPerc(Global.BossHP):f0}% {now}) {TranslateManager.inst.GetText("TranscendBossHP")}\n";
        txt += $"+10% ({CalcPerc(Global.EggsMulti):f0}% {now}) {TranslateManager.inst.GetText("TranscendEarnEggs")}\n";
        txt += $"+15% ({CalcPerc(Global.BossDamage):f0}% {now}) {TranslateManager.inst.GetText("TranscendBossDmg")}\n";

        infoText.text = txt;

        soulText.text = YandexGame.savesData.Souls.ToString();
    }

    private float CalcPerc(float val)
    {
        return val * 100 - 100;
    }

    private void OnEnable()
    {
        soulText.text = YandexGame.savesData.Souls.ToString();
    }

    private void Transcend()
    {
        if (YandexGame.savesData.Souls >= price)
        {
            YandexGame.savesData.Souls -= price;
            YandexGame.savesData.TranscendAmount++;

            DataManager.instance.SoftReset();

            ShopEggManager.inst.Transcend();
            GameManager.inst.CalcTranscend();

            armor.UpdateStats();

            UpdateInfo();
        }
    }
}
