using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public AssetReference defaultPage;
    private List<MenuPage> pageList;

    void Awake()
    {
        if(MenuManager.instance != null)
        {
            Debug.LogWarning("Multiple MenuManager detected : instance destroyed");
            Destroy(this.gameObject);
        }
        MenuManager.instance = this;

        pageList = new List<MenuPage>();
    }

    void Start()
    {
        Addressables.LoadAssetAsync<GameObject>(defaultPage).Completed += OnDefaultLoaded;
    }

    public void Return()
    {
        pageList.Remove(GetCurrentPage());        
    }

    public void SwapToPage(MenuPage page)
    {
        pageList.Add(page);
    }

    public MenuPage GetCurrentPage()
    {
        if(pageList.Count > 0)
            return pageList[pageList.Count -1];
        
        return null;
    }

    private void OnDefaultLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> gameObjectOperationHandle)
    {
        GameObject g = gameObjectOperationHandle.Result;
        GameObject o = Instantiate(g, FindObjectOfType<Canvas>().transform);
        this.SwapToPage(o.GetComponent<MenuPage>());
    }

    void Update(){
        GetCurrentPage()?.gameObject.SetActive(true);
    }
}
