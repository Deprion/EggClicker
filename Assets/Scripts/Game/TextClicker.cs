using TMPro;
using UnityEngine;

public class TextClicker : MonoBehaviour
{
    private TMP_Text text;
    private float speed = 100, timer = 1, leftTimer;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void Restart(Vector2 pos, long val)
    { 
        transform.localPosition = pos;
        text.text = Global.NumToString(val);
        leftTimer = timer;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        leftTimer -= Time.deltaTime;
        transform.Translate(0, speed * Time.deltaTime, 0);

        if (leftTimer <= 0) gameObject.SetActive(false);
    }
}
