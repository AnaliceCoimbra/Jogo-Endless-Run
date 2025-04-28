using TMPro;
using UnityEngine;

public class GameOverTxt : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0, 600, 0);
    public Vector3 endPosition = new Vector3(0, 0, 0);
    public float duration = 2.0f;
    public float startDelay = 1.0f; // Wait time before starting the animation

    private TextMeshProUGUI tmpText;
    private float timer = 0f;
    private float delayTimer = 0f;
    private bool animationStarted = false;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchoredPosition = startPosition;

        Color c = tmpText.color;
        c.a = 0;
        tmpText.color = c;
    }

    void Update()
    {
        if (!animationStarted)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= startDelay)
            {
                animationStarted = true;
            }
            return;
        }

        if (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);

            // Slide
            RectTransform rt = GetComponent<RectTransform>();
            rt.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);

            // Fade in
            Color c = tmpText.color;
            c.a = t;
            tmpText.color = c;
        }
    }
}
