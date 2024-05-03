using UnityEngine;
using YG;

public class ShopEggManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeSlot;
    [SerializeField] private Transform parent;
    public EggUpgradeSO[] eggs;

    public static ShopEggManager inst;

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        Populate();
    }

    private void Populate()
    {
        for (int i = 0; i < eggs.Length; i++)
        {
            if (i == 0 || YandexGame.savesData.Upgrades[i + 1] > 1 || YandexGame.savesData.Upgrades[i] > 0)
            {
                CheckUpgrades(i);
            }
        }
    }

    public void CheckUpgrades(int id)
    {
        if (id >= eggs.Length) return;

        var obj = Instantiate(upgradeSlot, parent, false);
        obj.GetComponent<UpgradeSlot>().Init(eggs[id]);
    }

    public void Transcend()
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

        CheckUpgrades(0);
    }
}
