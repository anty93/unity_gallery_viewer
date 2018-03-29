using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace GalleryTest.MainMenu
{
    public class FlickrGalleryButton : MainMenuButton
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            SceneManager.LoadScene(2); // gallery screen
        }
    }
}