using UnityEngine;
using YG;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private ParticleSystem fx1, fx2;
    [SerializeField] private ArmorManager armor;
    private long maxHP, curHP;

    private float remainTime, leadTime = 2;

    private bool wantLead = false;

    private void Awake()
    {
        remainTime = armor.TimeMax;

        Events.BossClicked.AddListener(Clicked);

        NewBoss();

        Events.BossHP.Invoke(curHP, maxHP);
    }

    private void Update()
    {
        remainTime -= Time.deltaTime;
        leadTime -= Time.deltaTime;

        Events.BossTime.Invoke(remainTime, armor.TimeMax);

        if (wantLead && leadTime <= 0)
        {
            leadTime = 2;
            wantLead = false;

            YandexGame.NewLeaderboardScores("Level", (int)YandexGame.savesData.BossLevel);
        }

        if (remainTime > 0) return;

        remainTime = armor.TimeMax;

        NewBoss();

        Events.BossHP.Invoke(curHP, maxHP);
    }

    private void NewBoss()
    {
        long extra = (long)Mathf.Floor(YandexGame.savesData.BossLevel / 10);

        if (extra == 0) extra = 1;

        maxHP = (long)(20 * Mathf.Pow(1.13f, YandexGame.savesData.BossLevel) * extra / Global.BossHP);
        curHP = maxHP;
    }

    private void Clicked()
    {
        long dmg = (long)(armor.Damage * GameManager.inst.AdMulti);

        Events.ClickEarn.Invoke(dmg);

        curHP -= dmg;

        Events.BossDamaged.Invoke(dmg);
        
        if (curHP <= 0)
        {
            remainTime = armor.TimeMax;

            YandexGame.savesData.BossLevel++;
            YandexGame.savesData.Souls++;

            NewBoss();

            fx1.Play();
            fx2.Play();

            wantLead = true;

            Events.BossDefeated.Invoke();
            Events.SoulsUpdate.Invoke();
        }

        Events.BossHP.Invoke(curHP, maxHP);
    }
}
