using TMPro;
using UnityEngine;

public class StatsTxt: MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0, 600, 0);  // Off-screen start position
    public float endSpacing = -40f;                         // Spacing between final Y positions
    public float duration = 1.0f;
    public float startDelay = 1.0f;
    public float staggerDelay = 0.3f;

    private TextMeshProUGUI[] tmpTexts;
    private RectTransform[] rects;
    private float[] startTimes;
    private Vector3[] endPositions;

    void Start()
    {
        tmpTexts = GetComponentsInChildren<TextMeshProUGUI>();
        int count = tmpTexts.Length;

        rects = new RectTransform[count];
        startTimes = new float[count];
        endPositions = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            rects[i] = tmpTexts[i].GetComponent<RectTransform>();
            rects[i].anchoredPosition = startPosition;

            Color c = tmpTexts[i].color;
            c.a = 0;
            tmpTexts[i].color = c;

            // Each one ends 40 units lower than the one before
            endPositions[i] = new Vector3(startPosition.x, startPosition.y + (i * endSpacing), startPosition.z);
            startTimes[i] = Time.time + startDelay + (i * staggerDelay);
        }
    }

    void Update()
    {
        float globalTime = Time.time;

        for (int i = 0; i < tmpTexts.Length; i++)
        {
            float t = (globalTime - startTimes[i]) / duration;

            if (t >= 0 && t <= 1f)
            {
                // Animate position and alpha
                rects[i].anchoredPosition = Vector3.Lerp(startPosition, endPositions[i], t);

                Color c = tmpTexts[i].color;
                c.a = t;
                tmpTexts[i].color = c;
            }
            else if (t > 1f)
            {
                // Ensure final position/alpha is perfect after animation ends
                rects[i].anchoredPosition = endPositions[i];

                Color c = tmpTexts[i].color;
                c.a = 1f;
                tmpTexts[i].color = c;
            }
        }
    }
}
