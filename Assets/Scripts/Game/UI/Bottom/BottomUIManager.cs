using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomUIManager : MonoBehaviour
{
    [SerializeField] private Button adBtn;
    [SerializeField] private TMP_Text adText;
    [SerializeField] private GameObject adExtraImage;
    private string translateText, secText;

    [SerializeField] private GameObject eggMenu, dungeon;

    private float adTimer = 30;
    private bool isAd = false;

    private void Awake()
    {
        adBtn.onClick.AddListener(WatchAd);

        translateText = "2X " + TranslateManager.inst.GetText("Click");
        secText = TranslateManager.inst.GetText("Sec");

        adText.text = translateText;
    }

    public void SwitchMenu()
    {
        eggMenu.SetActive(!eggMenu.activeSelf);
        dungeon.SetActive(!dungeon.activeSelf);
    }

    private void WatchAd()
    {
        if (isAd) return;

        AdManager.inst.RewardAd.AddListener(AdResult);

        AdManager.inst.InvokeRewardAd();
    }

    private void AdResult(bool val)
    {
        AdManager.inst.RewardAd.RemoveListener(AdResult);

        if (!val) return;

        adExtraImage.SetActive(false);
        isAd = true;

        GameManager.inst.AdMulti += 1;
    }

    private void Update()
    {
        if (!isAd) return;

        adTimer -= Time.deltaTime;

        adText.text = $"2X {adTimer:f0} {secText}";

        if (adTimer > 0) return;

        isAd = false;
        adTimer = 30;
        adExtraImage.SetActive(true);
        adText.text = translateText;

        GameManager.inst.AdMulti -= 1;
    }
}
