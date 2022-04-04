using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEditor;
using Modules.Translations.Settings;

namespace Modules.Translations.Controllers
{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// Translation module general manager (Singleton -> instance)
    /// </summary>
    public class TranslationManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton reference
        /// </summary>
        public static TranslationManager instance;

        /// <summary>
        /// Save every words from every languages in the cache
        /// </summary>
        public bool cacheAllTranslation = false;

        /// <summary>
        /// Save every words from languages already used in the cache
        /// </summary>
        public bool cacheUsedTranslation = true;

        /// <summary>
        /// Spawn the Languague button in the up-right corner
        /// </summary>
        public bool debugButton = false;

        /// <summary>
        /// VoidEvent delegate declaration
        /// >/summary>
        public delegate void VoidEvent();

        /// <summary>
        /// Event called when text need to be rerloaded
        /// >/summary>
        public VoidEvent OnLanguageChange;


        /// <summary>
        /// Translation Settings reference
        /// >/summary>
        [SerializeField] private TranslationSettings _settings;

        /// <summary>
        /// The current loaded lang iso
        /// </summary>
        private string _currentLoadedLangIso;

        /// <summary>
        /// Translation data collector reference
        /// >/summary>
        private TranslationDataCollector _collector;



#region Set up
        void Awake()
        {
            this.singletonSetUp();

            this._collector = GetComponent<TranslationDataCollector>();
            this._currentLoadedLangIso = this._settings.GetDefaultLanguage().isoCode;
            this.SwapLanguageTo(this._settings.GetDefaultLanguage().isoCode);

            DontDestroyOnLoad(this.gameObject);

            if(debugButton == true)
            {
                Addressables.LoadAssetAsync<GameObject>(this._settings.contants.languageSelectionMenuButton).Completed += OnPrefabLoaded;
            }
        }

        /// <summary>
        /// Set up Singleton or destroy component if already existing
        /// </summary>
        private void singletonSetUp()
        {
            if(TranslationManager.instance != null)
            {
                Debug.LogWarning("Multiple TranslationManager detected, instance destroyed");
                Destroy(this);
            }
            else
            {
                TranslationManager.instance = this;
            }
        }

        /// <summary>
        /// Spawn the language selection menu button
        /// </summary>
        private void OnPrefabLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> gameObjectOperationHandle)
        {
            Instantiate(gameObjectOperationHandle.Result, FindObjectOfType<Canvas>().transform);
        }
#endregion

        /// <summary>
        /// Return the translation value of key translation in current language
        /// </summary>
        public string GetTranslation(string key)
        {
            return GetTranslation(key, this._currentLoadedLangIso);
        }

        /// <summary>
        /// Return the translation value of key translation in specified language
        /// </summary>
        public string GetTranslation(string key, string isoCode)
        {
            if(this._settings.GetLanguageByIso(isoCode) != null)
            {
                if(this._settings.GetLanguageByIso(isoCode).languageDictionnary.ContainsKey(key))
                {
                    return this._settings.GetLanguageByIso(isoCode).languageDictionnary[key];
                }
                else
                {
                    return "Missing translation";
                }
            }
            else
            {
                if(this._settings.GetLanguageByIso(this._settings.GetDefaultLanguage().isoCode).languageDictionnary.ContainsKey(key))
                {
                    return this._settings.GetLanguageByIso(this._settings.GetDefaultLanguage().isoCode).languageDictionnary[key];
                }
                else
                {
                    return "Missing translation";
                }
            }
        }

        /// <summary>
        /// Swap language to specified if exist, otherwise to default language
        /// </summary>
        public void SwapLanguageTo(string isoCode)
        {
            if(this._settings.GetLanguageByIso(isoCode) != null)
            {
                if((!cacheAllTranslation && !cacheUsedTranslation) && this._settings.GetDefaultLanguage().isoCode != this._currentLoadedLangIso)
                {
                    this._settings.GetLanguageByIso(this._currentLoadedLangIso).languageDictionnary.Clear();
                }

                this._currentLoadedLangIso = isoCode;

                this._collector.LoadLanguage(this._currentLoadedLangIso, 
                    delegate{
                        this.OnLanguageChange?.Invoke();
                    });
            }
            else
            {
                if(this._currentLoadedLangIso != this._settings.GetDefaultLanguage().isoCode)
                {
                    this._currentLoadedLangIso = this._settings.GetDefaultLanguage().isoCode;
                    this.OnLanguageChange?.Invoke();
                }
            }
        }

        /// <summary>
        /// Return the TranslationSettings used 
        /// </summary>
        public TranslationSettings GetSettings()
        {
            return this._settings;
        }

        /// <summary>
        /// Return current language iso code 
        /// </summary>
        public string GetCurrentLanguage()
        {
            return this._currentLoadedLangIso;
        }
    }
}