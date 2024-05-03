using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "EggUp", menuName = "Egg upgrade")]
public class EggUpgradeSO : ScriptableObject
{
    enum CalcType
    { 
        Alg = 0,
        Geom = 1
    }

    public enum BonusType
    {
        Click = 0,
        PerSec = 1
    }

    public int Id;
    public string Name;
    public int Level { get { return YandexGame.savesData.Upgrades.GetValueOrDefault(Id); } }
    public long Price, Bonus;
    public float DifPrice, DifBonus;
    [SerializeField] private CalcType priceCalcType;
    [SerializeField] private CalcType bonusCalcType;
    public BonusType bonusType;

    private float finalDifPrice { get { return DifPrice / Global.UpgradesDiscount + 1; } }

    public long GetPrice()
    {
        switch (priceCalcType)
        { 
            case CalcType.Alg:
                return (long)(Price + Level * finalDifPrice);
            case CalcType.Geom:
                return (long)(Price * Mathf.Pow(finalDifPrice, Level));
            default: return Price;
        }
    }

    public long GetPrevBonus()
    {
        switch (bonusCalcType)
        {
            case CalcType.Alg:
                return (long)(Bonus + (Level - 1) * DifBonus);
            case CalcType.Geom:
                return (long)(Bonus * Mathf.Pow(DifBonus, Level - 1));
            default: return Bonus;
        }
    }

    public long GetBonus()
    {
        switch (bonusCalcType)
        {
            case CalcType.Alg:
                return (long)(Bonus + Level * DifBonus);
            case CalcType.Geom:
                return (long)(Bonus * Mathf.Pow(DifBonus, Level));
            default: return Bonus;
        }
    }

    public long GetTotalBonus()
    {
        switch (bonusCalcType)
        {
            case CalcType.Alg:
                return (long)((Bonus + GetPrevBonus()) / 2f * Level);
            case CalcType.Geom:
                return (long)(Bonus * Mathf.Pow(DifBonus, Level) / DifBonus);
            default: return Bonus;
        }
    }

    public string GetDesc()
    {
        string result = "";

        switch (bonusType)
        {
            case BonusType.Click:
                result = $"+{GetBonus()} {TranslateManager.inst.GetText("PerClick")}";
                break;
            case BonusType.PerSec:
                result = $"+{GetBonus()} {TranslateManager.inst.GetText("PerSec")}";
                break;
        }

        return result;
    }
}
