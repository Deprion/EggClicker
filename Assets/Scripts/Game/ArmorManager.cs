using UnityEngine;
using YG;

public class ArmorManager : MonoBehaviour
{
    [HideInInspector] public long Damage = 1;
    [HideInInspector] public float TimeMax = 10;

    private void Start()
    {
        UpdateStats();
    }

    public long GetNextDamage()
    {
        return CalcDamage(1);
    }

    public float GetNextTime() 
    {
        float result = 10;

        for (int i = 0; i < YandexGame.savesData.RingLvl + 1; i++)
        {
            result += 10 / result;
        }

        return result;
    }

    public long GetEggPrice(long level)
    {
        return (long)(50 * Mathf.Pow(1.41f, level));
    }

    public long GetEggPriceTime(long level)
    {
        return (long)(50 * Mathf.Pow(1.6f, level));
    }

    public long GetSoulPrice(long level)
    {
        return 5;
    }

    public void UpdateStats()
    {
        TimeMax = 10;

        for (int i = 0; i < YandexGame.savesData.RingLvl; i++)
        {
            TimeMax += 10 / TimeMax;
        }

        if (YandexGame.savesData.WeaponLvl != 0)
            Damage = CalcDamage(0);
    }

    private long CalcDamage(int extraLvl)
    {
        return (long)((5 * Mathf.Pow(1.13f, YandexGame.savesData.WeaponLvl + extraLvl) + 1) * Global.BossDamage);
    }
}
