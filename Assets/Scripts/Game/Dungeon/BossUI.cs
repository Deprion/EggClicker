using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class BossUI : MonoBehaviour
{
    [SerializeField] private Slider sliderHP, sliderTime;
    [SerializeField] private TMP_Text textHP, textTime, textTop;
    [SerializeField] private Animation anim;

    [SerializeField] private Sprite[] firstSpr, secondSpr, thirdSpr, fourthSpr, fifthSpr, sixthSpr;

    private Sprite[][] spr;

    private void Start()
    {
        spr = new Sprite[6][];
        spr[0] = firstSpr;
        spr[1] = secondSpr;
        spr[2] = thirdSpr;
        spr[3] = fourthSpr;
        spr[4] = fifthSpr;
        spr[5] = sixthSpr;

        Events.BossHP.AddListener(Damaged, true);
        Events.BossTime.AddListener(Time);
        Events.BossDefeated.AddListener(Defeated);

        sliderHP.maxValue = 1;
        sliderHP.value = 1;

        sliderTime.maxValue = 1;
        sliderTime.value = 1;

        textTop.text = $"{TranslateManager.inst.GetText("Level")}\n{YandexGame.savesData.BossLevel}";
    }

    private void Defeated()
    {
        anim.UpdateSprites(spr[Random.Range(0, spr.GetUpperBound(0) + 1)]);

        textTop.text = $"{TranslateManager.inst.GetText("Level")}\n{YandexGame.savesData.BossLevel}";
    }

    private void Damaged(long hp, long max)
    {
        sliderHP.value = (float)hp / max;

        textHP.text = Global.NumToString(hp);
    }

    private void Time(float cur, float max)
    { 
        sliderTime.value = (float)cur / max;

        textTime.text = $"{cur:f1}";
    }
}
