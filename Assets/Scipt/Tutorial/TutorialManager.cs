using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    private List<TutorialViewController> showedTutoView;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple TutorialManager detected : instance destroyed");
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        showedTutoView = new List<TutorialViewController>();
    }

    public void ShowTutoView(TutorialViewController view)
    {
        if(showedTutoView.Contains(view) == false)
        {
            view.gameObject.SetActive(true);
            showedTutoView.Add(view);
        }
    }
}
