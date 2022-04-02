using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Modules.Translations.Controllers
{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// Translation listener, should be placed on the Text or TextMeshProUGUI gameobject
    /// </summary>
    public class TextUGUITranslationController : MonoBehaviour
    {
        /// <summary>
        /// Translation key
        /// </summary>
        public string key;

        /// <summary>
        /// The text component 
        /// </summary>
        private TextMeshProUGUI _textComponent;

        /// <summary>
        /// Start Event of Unity Lifecycle 
        /// </summary>
        void Start()
        {
            this._textComponent = GetComponent<TextMeshProUGUI>();
            TranslationManager.instance.OnLanguageChange += this.OnUpdateText;

            this._textComponent.text = TranslationManager.instance.GetTranslation(key);
        }

        /// <summary>
        /// Event called by TranslationManager 
        /// </summary>
        void OnUpdateText()
        {
            this._textComponent.text = TranslationManager.instance.GetTranslation(key);
        }

        /// <summary>
        /// OnDestroy Event of Unity Lifecycle 
        /// </summary>
        void OnDestroy()
        {
            TranslationManager.instance.OnLanguageChange -= this.OnUpdateText;
        }
    }
}
