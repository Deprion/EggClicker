using UnityEngine;
using YG;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private float remainTime = 3.5f;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        remainTime -= Time.deltaTime;

        if (remainTime < 0)
        {
            SaveData();
        }
    }

    private void SaveData()
    {
        if (remainTime < 0)
        {
            remainTime = 3.5f;

            YandexGame.SaveProgress();
        }
    }

    public void SoftReset()
    {
        YandexGame.savesData.EggCurrency = 0;

        for (int i = 1; i <= 20; i++)
        {
            YandexGame.savesData.Upgrades[i] = 0;
        }

        SaveData();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
