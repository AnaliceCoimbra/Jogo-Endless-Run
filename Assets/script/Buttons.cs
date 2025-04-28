using UnityEngine;

public class Buttons : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    public float delayBeforeFade = 4.0f; // NEW: delay before fade starts

    private CanvasGroup canvasGroup;
    private bool isFadingIn = false;
    private float timer = 0f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found on the Button.");
            return;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Start the fade-in coroutine after the delay
        Invoke(nameof(StartFadeIn), delayBeforeFade);
    }

    void StartFadeIn()
    {
        isFadingIn = true;
        timer = 0f; // reset timer for fade timing
    }

    void Update()
    {
        if (isFadingIn)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            canvasGroup.alpha = alpha;

            if (alpha >= 1f)
            {
                isFadingIn = false;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
