using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Translations.Models
{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// The model class for language settings.
    /// </summary>
    [Serializable]
    public class Language
    {
        /// <summary>
        /// The language name
        /// </summary>
        public string name;

        /// <summary>
        /// The language iso code.
        /// </summary>
        public string isoCode;

        /// <summary>
        /// Define if it's the default language.
        /// </summary>
        public bool isDefault;

        /// <summary>
        /// The language XML File
        /// </summary>
        public Sprite flagSpite;

        /// <summary>
        /// The cached translation
        /// </summary>
        public Dictionary<string,string> languageDictionnary;
    }
}
