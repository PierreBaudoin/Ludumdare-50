using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;


public class GameOverScreen : MonoBehaviour
{
    private Animator animator;
    public AssetReference menuScene;

    private float score;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartGameOverSequence(float score)
    {
        this.score = score;
        animator.SetBool("start", true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartGameOverSequence(score);
        }
    }

    public void OpenLinkToGamePage()
    {
        Application.OpenURL("facebook.com");
    }

    public void BackToMenu()
    {
        ///TODO
        Addressables.LoadSceneAsync(menuScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
