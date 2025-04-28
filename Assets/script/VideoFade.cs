using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoFade : MonoBehaviour
{
    public Button playButton;
    public RawImage videoScreen;
    public VideoPlayer videoPlayer;
    public Image fadeImage;
    public string sceneToLoad = "Endless Runner";

    private void Start()
    {
        videoScreen.gameObject.SetActive(false);
        fadeImage.canvasRenderer.SetAlpha(0f);
        playButton.onClick.AddListener(PlayVideo);
    }

    void PlayVideo()
    {
        playButton.gameObject.SetActive(false);
        videoScreen.gameObject.SetActive(true);

        // Reset video state
        videoPlayer.Stop();         // Stops and resets
        videoPlayer.time = 0;       // Reset time
        videoPlayer.frame = 0;      // Reset frame just to be sure

        // Avoid stacking events
        videoPlayer.prepareCompleted -= OnVideoPrepared;
        videoPlayer.loopPointReached -= OnVideoFinished;

        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.loopPointReached += OnVideoFinished;

        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        videoScreen.transform.SetAsLastSibling(); // Brings it to the top of the canvas

        videoScreen.texture = vp.texture;
        videoPlayer.Play();
        

    }

    void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    System.Collections.IEnumerator FadeOutAndLoadScene()
    {
        float fadeDuration = 2f;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.canvasRenderer.SetAlpha(alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
