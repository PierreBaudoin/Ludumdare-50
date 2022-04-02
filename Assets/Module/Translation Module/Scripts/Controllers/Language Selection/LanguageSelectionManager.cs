using System.Collections;
using System.Collections.Generic;
using Modules.Translations.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Modules.Translations.Controllers
{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// Manage the UI language list 
    /// </summary>
    public class LanguageSelectionManager : MenuPage
    {
        /// <summary>
        /// OnDestroy Event of Unity Lifecycle 
        /// </summary>
        public AssetReference languageUIPrefab;

        /// <summary>
        /// The parent of the TranslationFlag list 
        /// </summary>
        [SerializeField] private Transform _languageListParent;

        /// <summary>
        /// Awake Event of Unity Lifecycle 
        /// </summary>
        void Start()
        {
            if(TranslationManager.instance.GetSettings().languages.Length > 1)
            {
                Addressables.LoadAssetAsync<GameObject>(languageUIPrefab).Completed += onPrefabLoaded;
            }
            else
            {
                if(TranslationManager.instance.GetSettings().languages.Length == 1)
                {
                    TranslationManager.instance.SwapLanguageTo(TranslationManager.instance.GetSettings().GetDefaultLanguage().isoCode);
                }
                else
                {
                    Debug.LogError(TranslationManager.instance.GetSettings().contants.missingTranslationText);
                }
            }
        }

        /// <summary>
        /// Initialize the selection flags 
        /// </summary>
        private void onPrefabLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> gameObjectOperationHandle)
        {
            TranslationFlag t;
            foreach(Language l in TranslationManager.instance.GetSettings().languages)
            {
                GameObject o = Instantiate(gameObjectOperationHandle.Result);
                o.transform.SetParent(this._languageListParent);
                t = o.GetComponent<TranslationFlag>();
                t.manager = this;
                t.isoCode = l.isoCode;
                o.GetComponent<Image>().sprite = l.flagSpite;
            }
        }

        /// <summary>
        /// Select a language and destroy the language selection menu 
        /// </summary>
        public void Select(string isoCode)
        {
            TranslationManager.instance.SwapLanguageTo(isoCode);
            this.Return();
        }
    }
}