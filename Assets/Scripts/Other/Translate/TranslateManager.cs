using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using YG;

public class TranslateManager : MonoBehaviour
{
    [SerializeField] private TextAsset[] texts;

    public static TranslateManager inst;
    private Dictionary<string, string> translate = new Dictionary<string, string>();

    private void Awake()
    {
        DontDestroyOnLoad(this);

        inst = this;

        Setup();
    }

    private void Setup()
    {
        string lng = YandexGame.EnvironmentData.language;

        if (lng == "ru" || lng == "be" || lng == "kk" || lng == "uk" || lng == "uz")
            Fill(texts[0]);
        else
            Fill(texts[1]);
    }

    private void Fill(TextAsset txt)
    {
        Regex regex = new Regex(":|;\r?\n?");
        var arr = regex.Split(txt.text);

        for (int i = 0; i < arr.Length - 1; i+=2)
        {
            translate[arr[i]] = arr[i + 1];
        }
    }

    public string GetText(string text)
    {
        return translate[text];
    }
}
