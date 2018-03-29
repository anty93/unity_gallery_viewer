using UnityEngine;
using UnityEngine.EventSystems;

namespace GalleryTest.MainMenu
{
    public class MainMenuButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private UnityEngine.UI.Text buttonText;
        private bool hovered;
        private int initialTextSize;
        private int highlightedTextSize;
        private float currentTextSize;
        private float lerpSpeed;
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            // here we could play a sound or something that would apply to all buttons
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EnableHighlight();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DisableHighlight();
        }

        private void Awake()
        {
            buttonText = GetComponent<UnityEngine.UI.Text>();
            hovered = false;
            initialTextSize = buttonText.fontSize;
            currentTextSize = initialTextSize;
            highlightedTextSize = initialTextSize + 8;
            lerpSpeed = 5f;
        }

        private void Update()
        {
            if (hovered && buttonText.fontSize < highlightedTextSize)
            {
                currentTextSize = Mathf.Lerp(currentTextSize, highlightedTextSize, Time.deltaTime * lerpSpeed);
            }
            else if (!hovered && buttonText.fontSize > initialTextSize)
            {
                currentTextSize = Mathf.Lerp(currentTextSize, initialTextSize, Time.deltaTime * lerpSpeed);
            }

            buttonText.fontSize = Mathf.RoundToInt(currentTextSize);
        }

        private void EnableHighlight()
        {
            hovered = true;
        }

        private void DisableHighlight()
        {
            hovered = false;
        }
    }
}