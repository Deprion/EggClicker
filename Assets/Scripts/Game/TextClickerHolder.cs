using UnityEngine;

public class TextClickerHolder : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Transform egg;

    private TextClicker[] texts = new TextClicker[20];

    private int index;
    private Vector2 pos;

    private void Awake()
    {
        for (int i = 0; i < texts.Length; i++) 
        {
            texts[i] = Instantiate
                (textPrefab, transform, false).GetComponent<TextClicker>();
        }

        Events.ClickEarn.AddListener(Clicked);
    }

    private void Clicked(long val)
    {
        pos.x = Random.Range(egg.localPosition.x - 150, egg.localPosition.x + 150);
        pos.y = Random.Range(egg.localPosition.y - 150, egg.localPosition.y + 150);

        texts[index].Restart(pos, val);

        index = index + 1 >= texts.Length ? 0 : index + 1;
    }
}
