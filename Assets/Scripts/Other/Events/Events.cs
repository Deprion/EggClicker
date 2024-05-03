public static class Events
{
    public static SimpleEvent EggClicked = new SimpleEvent();
    public static SimpleEvent<long> ClickEarn = new SimpleEvent<long>();

    public static SimpleEvent BossDefeated = new SimpleEvent();
    public static SimpleEvent BossClicked = new SimpleEvent();
    public static SimpleEvent<long, long> BossHP = new SimpleEvent<long, long>();
    public static SimpleEvent<long> BossDamaged = new SimpleEvent<long>();
    public static SimpleEvent<float, float> BossTime = new SimpleEvent<float, float>();

    public static SimpleEvent<int> EggUpgrade = new SimpleEvent<int>();

    public static SimpleEvent SoulsUpdate = new SimpleEvent();
}
