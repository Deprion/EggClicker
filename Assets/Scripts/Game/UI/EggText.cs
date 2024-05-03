using TMPro;
using UnityEngine;
using YG;

public class EggText : MonoBehaviour
{
    private TMP_Text text;

    private float timeLeft;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft > 0) return;

        timeLeft = 0.1f;
        text.text = Global.NumToString(YandexGame.savesData.EggCurrency);
    }
}
