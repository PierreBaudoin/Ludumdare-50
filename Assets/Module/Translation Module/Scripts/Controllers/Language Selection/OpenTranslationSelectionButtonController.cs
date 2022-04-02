using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;



namespace Modules.Translations.Controllers{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// Open/close the language selection menu
    /// </summary>
    public class OpenTranslationSelectionButtonController : MonoBehaviour
    {

        /// <summary>
        /// The parent of the new Translation Selection Menu
        /// </summary>
        private Transform _translationSelectionMenuParent;

        /// <summary>
        /// The button image renderer
        /// </summary>
        private Image _buttonImage;

        /// <summary>
        /// the language selection menu instance
        /// </summary>
        private LanguageSelectionManager _menuInstance;
        
        /// <summary>
        /// Unity Start event
        /// </summary>
        void Start()
        {
            this._buttonImage = GetComponent<Image>();
            this._translationSelectionMenuParent = FindObjectOfType<Canvas>().transform;
            OnLanguageChange();

            TranslationManager.instance.OnLanguageChange += OnLanguageChange;
        }

        /// <summary>
        /// On click button event
        /// </summary>
        public void OnClick()
        {
            if(this._menuInstance != null)
            {
                if(this._menuInstance.gameObject.activeInHierarchy)
                {
                    this._menuInstance.gameObject.SetActive(false);
                }
                else
                {
                    this._menuInstance.gameObject.SetActive(true);
                }
            }
            else
            {
                Addressables.LoadAssetAsync<GameObject>(TranslationManager.instance.GetSettings().contants.languageSelectionMenuPrefab).Completed += SpawnLanguageSelectionMenuPrefab;
            }
        }

        /// <summary>
        /// Update the button sprite when language change
        /// </summary>
        public void OnLanguageChange()
        {
            this._buttonImage.sprite = TranslationManager.instance.GetSettings().GetLanguageByIso(TranslationManager.instance.GetCurrentLanguage()).flagSpite;
        }

        /// <summary>
        /// Spawn the language selection menu
        /// </summary>
        private void SpawnLanguageSelectionMenuPrefab(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> gameObjectOperationHandle)
        {
            this._menuInstance = Instantiate(gameObjectOperationHandle.Result, this._translationSelectionMenuParent).GetComponent<LanguageSelectionManager>();
        }

        /// <summary>
        /// OnDestroy Unity event
        /// </summary>
        void OnDestroy()
        {
            TranslationManager.instance.OnLanguageChange -= OnLanguageChange;
        }
    }
}