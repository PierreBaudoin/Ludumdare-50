using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class OptionMenuPage : MenuPage
{
    public AssetReference languageSelectionViewPreafab;

    private GameObject language;
    public void ClickLanguage()
    {
        Addressables.LoadAssetAsync<GameObject>(languageSelectionViewPreafab).Completed += OnLanguageLoaded;
    }

    public void OnSoundValueChange()
    {
        Debug.Log("Not Yet Implemented");
    }

    public void EnableDisableEffects()
    {
        Debug.Log("Not Yet Implemented");
    }

    public void OnLanguageLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> gameObjectOperationHandle)
    {
        GameObject g = gameObjectOperationHandle.Result;
        GameObject language = Instantiate(g, transform.parent);
        MenuManager.instance.SwapToPage(language.GetComponent<MenuPage>());

    }
}
