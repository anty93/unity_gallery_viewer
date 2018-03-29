using UnityEngine;
using UnityEngine.SceneManagement;

namespace GalleryTest.SplashScreen
{
    /// <summary>
    /// Basic logo splash screen. Implements fading in and out of a logo image, after it fades out completly the app transitions to the next screen.
    /// </summary>
    public class SplashLogoController : MonoBehaviour
    {
        private enum FadeStatus
        {
            FADE_IN,
            FADE_OUT,
            FADED
        }

        [SerializeField] private float fadeSpeed;

        private SpriteRenderer spriteRenderer;
        private FadeStatus fadeStatus;
        private float treshold;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            fadeStatus = FadeStatus.FADE_IN;
            treshold = 0.05f;
        }
        
        private void Update()
        {
            switch (fadeStatus)
            {
                case FadeStatus.FADE_IN:
                    spriteRenderer.color = FadeAlpha(spriteRenderer.color, true);
                    if (spriteRenderer.color.a >= 1f - treshold)
                    {
                        fadeStatus = FadeStatus.FADE_OUT;
                    }
                    break;
                case FadeStatus.FADE_OUT:
                    spriteRenderer.color = FadeAlpha(spriteRenderer.color, false);
                    if (spriteRenderer.color.a <= 0f + treshold)
                    {
                        fadeStatus = FadeStatus.FADED;
                    }
                    break;
                case FadeStatus.FADED:
                    SceneManager.LoadScene(1); // 1 - main menu scene
                    break;
            }
        }

        private Color FadeAlpha(Color originalColor, bool increase)
        {
            originalColor.a = Mathf.Lerp(originalColor.a, (increase) ? 1f : 0f, fadeSpeed * Time.deltaTime);
            return originalColor;
        }
    }
}