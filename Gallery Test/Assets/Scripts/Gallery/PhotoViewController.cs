using UnityEngine;
using UnityEngine.UI;
using GalleryTest.WebAPI.Models;

namespace GalleryTest.Gallery
{
    public class PhotoViewController : MonoBehaviour
    {
        [SerializeField] private Image photoImage;
        [SerializeField] private Text photoTitle;
        [SerializeField] private Text photoDescription;
        [SerializeField] private Button closeButton;

        public bool PanelOpen { get { return gameObject.activeInHierarchy; } }

        private Vector2 imageSize;

        public void OpenPanel(PhotoDownloaded photoDownloaded)
        {
            // if the panel is already open we don't consider the click on another photo underneath it
            if (PanelOpen)
                return;

            var photoSize = new Vector2(Mathf.Min(photoDownloaded.Texture.width, imageSize.x), 
                                        Mathf.Min(photoDownloaded.Texture.height, imageSize.y));

            photoImage.sprite = Sprite.Create(photoDownloaded.Texture as Texture2D, new Rect(0, 0, photoSize.x, photoSize.y), new Vector2(0.5f, 0.5f));
            photoTitle.text = photoDownloaded.MetaData.Title;
            photoDescription.text = string.Format("ID: {0}\nServer: {1}\nFarm: {2}\nSecret: {3}", 
                photoDownloaded.MetaData.Id, photoDownloaded.MetaData.ServerId, photoDownloaded.MetaData.Farm, photoDownloaded.MetaData.Secret);

            gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            imageSize = new Vector2(photoImage.rectTransform.rect.width, photoImage.rectTransform.rect.height);
            closeButton.onClick.AddListener(CloseButtonClick);
            ClosePanel();
        }

        private void CloseButtonClick()
        {
            ClosePanel();
        }
    }
}