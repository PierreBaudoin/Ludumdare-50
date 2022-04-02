using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GeneralMenuPage : MenuPage
{
    public AssetReference optionPageGameObject, leavePageGameObject, gameScene, draftScene;
    private GameObject option, leave;
    public void ClickPlay()
    {
        Addressables.LoadSceneAsync(draftScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
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
}
