using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UpgradeSlot : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private TMP_Text lvlTxt, descTxt, priceTxt;
    [SerializeField] private Image back;
    private long price;

    private EggUpgradeSO egg;

    private static Color clcColor = new Color(0.86f, 0.96f, 0.71f);
    private static Color secColor = new Color(0.7f, 0.96f, 0.91f);

    public void Init(EggUpgradeSO egg)
    {
        this.egg = egg;

        back.color = egg.bonusType == EggUpgradeSO.BonusType.Click ? clcColor : secColor;

        UpdateInfo();
    }

    public void Buy()
    {
        if (YandexGame.savesData.EggCurrency >= price)
        {
            YandexGame.savesData.EggCurrency -= price;

            YandexGame.savesData.Upgrades[egg.Id]++;

            UpdateInfo();

            if (egg.Level == 2)
                ShopEggManager.inst.CheckUpgrades(egg.Id);

            Events.EggUpgrade.Invoke(egg.Id);
        }
    }

    private void UpdateInfo()
    {
        lvlTxt.text = $"{egg.Level}\n{TranslateManager.inst.GetText("Level")}";

        descTxt.text = egg.GetDesc();

        price = egg.GetPrice();

        priceTxt.text = Global.NumToString(price);       
    }

    private void OnEnable()
    {
        if (egg == null) return;

        price = egg.GetPrice();

        priceTxt.text = Global.NumToString(price);
    }
}
