using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class SoulAd : MonoBehaviour, IPointerDownHandler
{
    private long soulsAmount;

    private Vector2 hidePos = new Vector2(-1200, 0);
    private Vector2 showPos = new Vector2(-785, 0);

    private float awaitTime = 45;
    private float leftTime = 99999;

    private bool hidden = true;

    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        if (YandexGame.savesData.BossLevel > 9)
        {
            ResetTimer();
        }
        else
        {
            Events.BossDefeated.AddListener(CheckBossLevel);
        }
    }

    private void CheckBossLevel()
    {
        if (YandexGame.savesData.BossLevel > 9)
        {
            Events.BossDefeated.RemoveListener(CheckBossLevel);

            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        leftTime = awaitTime;
    }

    private void CalcSouls()
    {
        soulsAmount = (long)(YandexGame.savesData.BossLevel * 0.1f);

        if (soulsAmount < 5) soulsAmount = 5;
    }

    private void Update()
    {
        leftTime -= Time.deltaTime;

        if (leftTime > 0) return;

        leftTime = 15;

        SwitchPos(!hidden);
    }

    private void SwitchPos(bool isHide)
    {
        hidden = isHide;

        if (isHide)
        {
            transform.localPosition = hidePos;

            ResetTimer();
        }
        else
        {
            CalcSouls();

            text.text = $"+{soulsAmount}";

            StartCoroutine(MoveTo(showPos));
        }
    }

    private IEnumerator MoveTo(Vector2 dest)
    {
        while (Vector3.Distance(transform.localPosition, dest) > 5)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, dest, Time.deltaTime * 2);

            yield return null;
        }
    }

    private void WatchAd()
    {
        AdManager.inst.RewardAd.AddListener(AdResult);

        AdManager.inst.InvokeRewardAd();

        StopAllCoroutines();

        SwitchPos(true);

        ResetTimer();
    }

    private void AdResult(bool val)
    {
        AdManager.inst.RewardAd.RemoveListener(AdResult);

        if (!val) return;

        YandexGame.savesData.Souls += soulsAmount;

        Events.SoulsUpdate.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        WatchAd();
    }
}
