using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] private Button upgradesTab, transcendTab, armorTab;
    [SerializeField] private GameObject upgradesMenu, transcendMenu, armorMenu;

    private GameObject curObj;

    private void Awake()
    {
        upgradesTab.onClick.AddListener(() => OpenMenu(upgradesMenu));
        transcendTab.onClick.AddListener(() => OpenMenu(transcendMenu));
        armorTab.onClick.AddListener(() => OpenMenu(armorMenu));

        curObj = upgradesMenu;

        upgradesMenu.SetActive(true);
    }

    private void OpenMenu(GameObject obj)
    { 
        curObj.SetActive(false);

        obj.SetActive(true);

        curObj = obj;
    }
}
