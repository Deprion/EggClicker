using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ArmorShop : MonoBehaviour
{
    [SerializeField] private ArmorManager armor;
    [SerializeField] private GameObject soulPriceGO, mainTab, previewTab;

    [SerializeField] private Button weaponBtn, ringBtn;

    [SerializeField] private TMP_Text infoTxt, eggPriceTxt, soulPriceTxt;

    [SerializeField] private Button upgradeBtn, adBtn;

    private bool isWeaponOpened = false;

    private void Awake()
    {
        weaponBtn.onClick.AddListener(WeaponTab);

        ringBtn.onClick.AddListener(RingTab);

        upgradeBtn.onClick.AddListener(Upgrade);
        adBtn.onClick.AddListener(AdBuy);
    }

    private void OnEnable()
    {
        if (isWeaponOpened)
            infoTxt.text = $"{TranslateManager.inst.GetText("Damage")} {armor.Damage} => {armor.GetNextDamage()}";
    }

    private void Upgrade()
    {
        long eggPrice;
        long soulPrice = 0;

        if (isWeaponOpened)
        {
            eggPrice = armor.GetEggPrice(YandexGame.savesData.WeaponLvl + 1);

            if ((YandexGame.savesData.WeaponLvl + 1) % 10 == 0)
                soulPrice = armor.GetSoulPrice(YandexGame.savesData.WeaponLvl + 1);
        }
        else
        {
            eggPrice = armor.GetEggPriceTime(YandexGame.savesData.RingLvl + 1);
            if ((YandexGame.savesData.RingLvl + 1) % 10 == 0)
                soulPrice = armor.GetSoulPrice(YandexGame.savesData.RingLvl + 1);
        }

        if (YandexGame.savesData.Souls >= soulPrice &&
            YandexGame.savesData.EggCurrency >= eggPrice)
        {
            YandexGame.savesData.Souls -= soulPrice;
            YandexGame.savesData.EggCurrency -= eggPrice;

            ProceedBuy();
        }
    }

    private void AdBuy()
    {
        AdManager.inst.RewardAd.AddListener(AdResult);

        AdManager.inst.InvokeRewardAd();
    }

    private void AdResult(bool val)
    {
        AdManager.inst.RewardAd.RemoveListener(AdResult);

        if (!val) return;

        ProceedBuy();
    }

    private void ProceedBuy()
    {
        if (isWeaponOpened)
        {
            YandexGame.savesData.WeaponLvl++;
            armor.UpdateStats();
            WeaponTab();
        }
        else
        {
            YandexGame.savesData.RingLvl++;
            armor.UpdateStats();
            RingTab();
        }
    }

    private void WeaponTab()
    {
        mainTab.SetActive(true);
        previewTab.SetActive(false);

        isWeaponOpened = true;

        infoTxt.text = $"{TranslateManager.inst.GetText("Damage")} {armor.Damage} => {armor.GetNextDamage()}";

        eggPriceTxt.text = $"{Global.NumToString(armor.GetEggPrice(YandexGame.savesData.WeaponLvl + 1))}";

        if ((YandexGame.savesData.WeaponLvl + 1) % 10 == 0)
        { 
            soulPriceGO.SetActive(true);

            soulPriceTxt.text = $"{Global.NumToString(armor.GetSoulPrice(YandexGame.savesData.WeaponLvl + 1))}";

            adBtn.gameObject.SetActive(true);
        }
        else
        {
            soulPriceGO.SetActive(false);
            adBtn.gameObject.SetActive(false);
        }
    }

    private void RingTab()
    {
        mainTab.SetActive(true);
        previewTab.SetActive(false);

        isWeaponOpened = false;

        infoTxt.text = $"{TranslateManager.inst.GetText("Time")} {armor.TimeMax:f2} => {armor.GetNextTime():f2}";

        eggPriceTxt.text = $"{Global.NumToString(armor.GetEggPriceTime(YandexGame.savesData.RingLvl + 1))}";

        if ((YandexGame.savesData.RingLvl + 1) % 10 == 0)
        {
            soulPriceGO.SetActive(true);

            soulPriceTxt.text = $"{Global.NumToString(armor.GetSoulPrice(YandexGame.savesData.RingLvl + 1))}";

            adBtn.gameObject.SetActive(true);
        }
        else
        {
            soulPriceGO.SetActive(false);
            adBtn.gameObject.SetActive(false);
        }
    }
}
