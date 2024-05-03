namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public long EggCurrency = 0;
        public long Souls = 0;
        public int TranscendAmount = 0;
        public long BossLevel = 1;
        public long WeaponLvl = 0, RingLvl = 0;

        public ListDictionary<int, int> Upgrades = new ListDictionary<int, int>()
        {
            [1] = 0,
            [2] = 0,
            [3] = 0,
            [4] = 0,
            [5] = 0,
            [6] = 0,
            [7] = 0,
            [8] = 0,
            [9] = 0,
            [10] = 0,
            [11] = 0,
            [12] = 0,
            [13] = 0,
            [14] = 0,
            [15] = 0,
            [16] = 0,
            [17] = 0,
            [18] = 0,
            [19] = 0,
            [20] = 0,
        };

        public SavesYG()
        { 

        }
    }
}
