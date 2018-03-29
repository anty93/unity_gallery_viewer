using UnityEngine.EventSystems;

namespace GalleryTest.MainMenu
{
    public class SettingsButton : MainMenuButton
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            // no settings screen implemented yet
        }
    }
}