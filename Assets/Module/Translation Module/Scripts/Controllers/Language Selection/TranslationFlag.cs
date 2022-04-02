using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Translations.Controllers
{
    /// \class
    /// \author baudoin_p
    /// <summary>
    /// Manage the UI language list 
    /// </summary>
    public class TranslationFlag : MonoBehaviour
    {
        /// <summary>
        /// the Language selection manager
        /// </summary>
        [HideInInspector] public LanguageSelectionManager manager;

        /// <summary>
        /// The isoCode of the language spoken in the country 
        /// </summary>
        public string isoCode;

        /// <summary>
        /// Select the language  
        /// </summary>
        public void OnClick()
        {
            this.manager.Select(isoCode);
        }
    }
}