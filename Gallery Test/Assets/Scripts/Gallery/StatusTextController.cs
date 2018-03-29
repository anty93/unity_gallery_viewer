using UnityEngine;
using UnityEngine.UI;

namespace GalleryTest.Gallery
{
    /// <summary>
    /// Simple text 'animation'
    /// </summary>
    public class StatusTextController : MonoBehaviour
    {
        [SerializeField] private string baseText;
        [SerializeField] private float animationStep;

        private Text text;
        private int currentDots;
        private float lastDotChange;

        /// <summary>
        /// Diisables the Status Text
        /// </summary>
        public void Disable()
        {
            text.enabled = false;
        } 

        /// <summary>
        /// Enables the Status Text with dot animation
        /// </summary>
        public void Enable()
        {
            text.enabled = true;
            lastDotChange = Time.time;
        }

        private void Start()
        {
            text = GetComponent<Text>();
            currentDots = 0;
            lastDotChange = Time.time;
            Disable();
        }
        
        private void Update()
        {
            if (text.enabled)
            {
                if (Time.time > lastDotChange + animationStep)
                {
                    AddDot();
                }
                text.text = string.Format("{0}{1}", baseText, ConstructDotsString(currentDots));
            }
        }

        private void AddDot()
        {
            lastDotChange = Time.time;
            if (currentDots >= 3)
            {
                currentDots = 0;
            }
            else
            {
                currentDots++;
            }
        }

        private string ConstructDotsString (int amount)
        {
            var result = "";
            for (int i = 0; i < amount; i++)
            {
                result += ".";
            }
            return result;
        }
    }
}