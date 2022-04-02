using System;
using UnityEngine.AddressableAssets;


namespace Modules.Translations.Models
{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// The model class for constants.
    /// </summary>
    [Serializable]
    public class TranslationConstants
    {
        /// <summary>
        /// The languages JSON File
        /// </summary>
        public AssetReference languageAssetReference;

        /// <summary>
        /// The excel sheet name / the JSON root name
        /// </summary>
        public string excelSheetName;

        /// <summary>
        /// the text printed when translation is missing
        /// </summary>
        public string missingTranslationText;

        /// <summary>
        /// the language selection menu prefab
        /// </summary>
        public AssetReference languageSelectionMenuButton;
        
        /// <summary>
        /// the language selection menu prefab
        /// </summary>
        public AssetReference languageSelectionMenuPrefab;
    }
}