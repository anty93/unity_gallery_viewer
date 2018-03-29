using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GalleryTest.WebAPI.Models;

namespace GalleryTest.Gallery
{
    /// <summary>
    /// Controls small photo thumbnails that are placed in ScrollView content
    /// </summary>
    public class PhotoThumbController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image photoThumbnail;
        [SerializeField] private Text photoLabel;

        public delegate void ThumbClicked(PhotoDownloaded photoDownloaded);
        public event ThumbClicked thumbClicked;

        private Vector2 imageSize;
        private PhotoDownloaded photoDownloadedData;

        /// <summary>
        /// Loads the Texture and meta data of a Photo into this thumbnail
        /// </summary>
        /// <param name="photoDownloaded">Already downloaded photo data</param>
        public void Initialize(PhotoDownloaded photoDownloaded)
        {
            photoDownloadedData = photoDownloaded;

            var photoSize = new Vector2(Mathf.Min(photoDownloaded.Texture.width, imageSize.x),
                                        Mathf.Min(photoDownloaded.Texture.height, imageSize.y));

            photoThumbnail.sprite = Sprite.Create(photoDownloadedData.Texture as Texture2D, new Rect(0, 0, photoSize.x, photoSize.y), new Vector2(0.5f, 0.5f));
            photoLabel.text = photoDownloadedData.MetaData.Title;
        }

        #region IPointerHandler intrfaces implementation
        public void OnPointerDown(PointerEventData eventData)
        {
            // open photo view panel and send him the info
            thumbClicked.Invoke(photoDownloadedData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // highlight the hovered thumbnail
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // turn off highlight
        }
        #endregion

        private void Awake()
        {
            imageSize = new Vector2(photoThumbnail.rectTransform.rect.width, photoThumbnail.rectTransform.rect.height);
        }
    }
}