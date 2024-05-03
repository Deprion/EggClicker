public static class Global
{
    public const string Currency = "¤";

    public static string NumToString(long num)
    {
        int rank = 0;

        while (num > 999)
        {
            rank++;
            num /= 10;
        }

        return $"{num} {Currency}{rank}";
    }

    public static float UpgradesDiscount = 1;
    public static float BossHP = 1;
    public static float EggsMulti = 1;
    public static float BossDamage = 1;
}
