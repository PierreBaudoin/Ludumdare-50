using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GeneralMenuPage : MenuPage
{
    public AssetReference optionPageGameObject, leavePageGameObject, creditPageGameObject, gameScene, draftScene;
    private GameObject option, leave, credits;
    public void ClickPlay()
    {
        Addressables.LoadSceneAsync(gameScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
        SoundManager2D.StartGameMusic();
    }

    public void ClickOption()
    {
        if(option != null)
        {
            MenuManager.instance.SwapToPage(option.GetComponent<MenuPage>());
        }
        else
        {
            Addressables.LoadAssetAsync<GameObject>(optionPageGameObject).Completed += OnOptionLoaded;
        }
    }

    public void ClickCredits()
    {
        if(credits != null)
        {
            MenuManager.instance.SwapToPage(credits.GetComponent<MenuPage>());
        }
        else
        {
            Addressables.LoadAssetAsync<GameObject>(creditPageGameObject).Completed += OnCreditsLoaded;
        }
    }

    public void ClickLeave()
    {
        if(leave != null)
        {
            MenuManager.instance.SwapToPage(leave.GetComponent<MenuPage>());
        }
        else
        {
            Addressables.LoadAssetAsync<GameObject>(leavePageGameObject).Completed += OnLeaveLoaded;
        }
    }


    public void OnOptionLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> gameObjectOperationHandle)
    {
        GameObject g = gameObjectOperationHandle.Result;
        option = Instantiate(g, transform.parent);
        MenuManager.instance.SwapToPage(option.GetComponent<MenuPage>());
    }

    public void OnLeaveLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> gameObjectOperationHandle)
    {
        GameObject g = gameObjectOperationHandle.Result;
        leave = Instantiate(g, transform.parent);
        MenuManager.instance.SwapToPage(leave.GetComponent<MenuPage>());
    }

    public void OnCreditsLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> gameObjectOperationHandle)
    {
        GameObject g = gameObjectOperationHandle.Result;
        credits = Instantiate(g, transform.parent);
        MenuManager.instance.SwapToPage(credits.GetComponent<MenuPage>());
    }
}
