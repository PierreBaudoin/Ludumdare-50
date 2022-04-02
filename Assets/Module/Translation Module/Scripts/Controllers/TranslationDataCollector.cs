using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;

namespace Modules.Translations.Controllers{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// Collect the data from file and cache it
    /// </summary>
    public class TranslationDataCollector : MonoBehaviour
    {
        /// <summary>
        /// Contains the new iso code 
        /// </summary>
        private string newIsoCode;

        /// <summary>
        /// Contains the new iso code 
        /// </summary>
        private UnityAction onLoadEnd;

        /// <summary>
        /// Load the language translation tab 
        /// </summary>
        public void LoadLanguage(string languageIsoCode, UnityAction callback)
        {
            if(TranslationManager.instance.GetSettings().GetLanguageByIso(languageIsoCode).languageDictionnary == null ||
               TranslationManager.instance.GetSettings().GetLanguageByIso(languageIsoCode).languageDictionnary.Count == 0)
            {
                newIsoCode = languageIsoCode;
                this.onLoadEnd = callback;

                AssetReference dataFile = TranslationManager.instance.GetSettings().contants.languageAssetReference;
                Addressables.LoadAssetAsync<TextAsset>(dataFile).Completed += OnLoadComplete;
            }
            else
            {
                callback?.Invoke();
            }
        }

        /// <summary>
        /// On language load finished 
        /// </summary>
        protected void OnLoadComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<TextAsset> jsonFile)
        {
            string json = jsonFile.Result.text;
            
            SaveData(json);
            newIsoCode = "";
            this.onLoadEnd?.Invoke();
        }

        /// <summary>
        /// Save the data in the cache 
        /// </summary>
        private void SaveData(string jsonData)
        {
            if(jsonData == "")
            {
                Debug.LogError("Empty JSON file");
            }
            else
            {
                var o = JSON.Parse(jsonData);

                string key, value;
                TranslationManager.instance.GetSettings().GetLanguageByIso(newIsoCode).languageDictionnary = new Dictionary<string, string>();

                for(int i = 0; i < o[TranslationManager.instance.GetSettings().contants.excelSheetName].Count; i++)
                {
                    var item = JSON.Parse(o.ToString());

                    key = item[0][i]["key"];
                    value = item[0][i][newIsoCode];

                    TranslationManager.instance.GetSettings().GetLanguageByIso(newIsoCode).languageDictionnary.Add(key, value);
                }
            }
        }
    }
}