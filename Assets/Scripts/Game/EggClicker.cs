using UnityEngine;
using UnityEngine.EventSystems;

public class EggClicker : MonoBehaviour, IPointerDownHandler
{
    private Vector3 smallScale = new Vector3(0.8f, 0.8f, 0.8f);
    private float time = 0;

    private void FixedUpdate()
    {
        if (transform.localScale.x >= 1) return;

        time += Time.fixedDeltaTime;

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, time);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = smallScale;
        time = 0;

        Events.EggClicked.Invoke();
    }
}
