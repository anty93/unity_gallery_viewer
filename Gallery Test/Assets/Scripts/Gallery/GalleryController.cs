using GalleryTest.Config;
using GalleryTest.WebAPI;
using GalleryTest.WebAPI.Models;
using GalleryTest.Misc;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GalleryTest.Gallery
{
    /// <summary>
    /// Controls the gallery screen and window
    /// </summary>
    public class GalleryController : MonoBehaviour
    {
        [SerializeField] private InputField galleryIdInput;
        [SerializeField] private InputField gallerySizeInput;
        [SerializeField] private Button galleryRefreshButton;
        [SerializeField] private GameObject galleryPanel;
        [SerializeField] private StatusTextController statusText;
        [SerializeField] private PhotoViewController photoView;

        private AppConfig appConfig;
        private IWebAPIController webAPIController;
        private ObjectPool photoPanelsPool;
        private int thumbsPerRow;

        private void Start()
        {
            appConfig = new AppConfig()
            {
                SelectedWebAPI = Config.WebAPI.FLICKR,
                APIKey = "459491681e1de93f008663073114a23d",
                APIAddress = "https://api.flickr.com/services/rest/"
            };
            webAPIController = WebAPIControllerFactory.CreateWebAPIController(appConfig);

            galleryRefreshButton.onClick.AddListener(OnRefreshClicked);

            thumbsPerRow = 5;

            photoPanelsPool = GetComponent<ObjectPool>();
            photoPanelsPool.Setup(Resources.Load("UI Elements/Image Panel") as GameObject);
        }
        
        private void OnRefreshClicked ()
        {
            photoView.ClosePanel(); //close the panel if its open

            if (galleryIdInput.text.Length > 0)
            {
                statusText.Enable();

                int maxPhotos = 10; //default value if not specified by the user
                if (gallerySizeInput.text.Length > 0)
                {
                    int.TryParse(gallerySizeInput.text, out maxPhotos);
                }
                InitializeGallery(galleryIdInput.text, maxPhotos);
            }
        }

        private void InitializeGallery(string galleryId, int maxPhotos)
        {
            webAPIController.CollectPhotosFromAGallery(galleryId)
                .Then(metaData =>
                {
                    return webAPIController.DownloadPhotosFromGallery(metaData, maxPhotos);
                })
                .Then(downloadedPhotos =>
                {
                    AddPhotosToGallery(downloadedPhotos as List<PhotoDownloaded>);
                })
                .Catch(error =>
                {
                    Debug.LogError(error.Message);
                });
        }

        private void AddPhotosToGallery (List<PhotoDownloaded> photos)
        {
            RemoveAllPhotos();

            var column = 0;
            var row = 0;
            foreach (var photo in photos)
            {
                AddPhotoToGallery(photo, column, row);
                column++;
                if (column >= thumbsPerRow)
                {
                    row++;
                    column = 0;
                }
            }

            statusText.Disable();
        }

        private PhotoThumbController AddPhotoToGallery(PhotoDownloaded photo, int column = 0, int row = 0)
        {
            var startingPosition = new Vector3(-590f, 195f, 0f);
            var pos = startingPosition + new Vector3(column * 260f, row * -260f, 0f);
            var gameObject = photoPanelsPool.BorrowObject(galleryPanel.transform);
            gameObject.GetComponent<RectTransform>().localPosition = pos;
            var photoPanel = gameObject.GetComponent<PhotoThumbController>();
            photoPanel.Initialize(photo);

            photoPanel.thumbClicked += photoView.OpenPanel;

            return photoPanel;
        }

        private void RemoveAllPhotos()
        {
            while (galleryPanel.transform.childCount > 0)
            {
                photoPanelsPool.ReturnObject(galleryPanel.transform.GetChild(0).gameObject);
            }
        }
    }
}