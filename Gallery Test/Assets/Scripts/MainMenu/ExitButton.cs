using UnityEngine;
using UnityEngine.EventSystems;

namespace GalleryTest.MainMenu
{
    public class ExitButton : MainMenuButton
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            Application.Quit();
        }
    }
}