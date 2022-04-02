using UnityEngine;
using Modules.Translations.Models;

namespace Modules.Translations.Settings
{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// This class is use to gather all translations settings with constant values.
    /// </summary>
    [CreateAssetMenu(fileName = "TranslationsSettings", menuName = "Settings/TranslationsSettings")]
    public class TranslationSettings : ScriptableObject
    {
        /// <summary>
        /// The translation module constants
        /// </summary>
        public TranslationConstants contants;

        /// <summary>
        /// The list of differents languages in the application
        /// </summary>
        public Language[] languages;


        /// <summary>
        /// Return a language with it's iso code
        /// </summary>
        /// <param name="iso"></param>
        /// <returns></returns>
        public Language GetLanguageByIso(string iso)
        {
            for (int i = 0; i < this.languages.Length; i++)
            {
                if (this.languages[i].isoCode == iso)
                {
                    return this.languages[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Return the default language
        /// </summary>
        /// <returns></returns>
        public Language GetDefaultLanguage()
        {
            for (int i = 0; i < this.languages.Length; i++)
            {
                if (this.languages[i].isDefault == true)
                {
                    return this.languages[i];
                }
            }

            return null;
        }
    }
}