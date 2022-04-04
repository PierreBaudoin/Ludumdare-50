using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;


public class GameOverScreen : MonoBehaviour
{
    private Animator animator;
    public AssetReference menuScene;

    public TextMeshProUGUI[] userNames;
    public TextMeshProUGUI[] userComments;
    public TextMeshProUGUI gameName;

    public string[] namesGood;
    public string[] namesAverage;
    public string[] namesBad;

    public string[] commentsGood;
    public string[] commentsAverage;
    public string[] commentsBad;
    public string[] gameNames;

    private float score;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void StartGameOverSequence(float normalizedScore)
    {
        this.score = normalizedScore;
        if (score < 0.3)
        {
            SetTextsInUI(userNames, namesBad);
            SetTextsInUI(userComments, commentsBad);
            gameName.text = gameNames[2];
        }
        else if (score >= 0.3 && score < 0.6)
        {
            SetTextsInUI(userNames, namesAverage);
            SetTextsInUI(userComments, commentsAverage);
            gameName.text = gameNames[1];
        }
        else if (score >= 0.6)
        {
            SetTextsInUI(userNames, namesGood);
            SetTextsInUI(userComments, commentsGood);
            gameName.text = gameNames[0];
        }
        animator.SetFloat("score", score);
        animator.SetBool("start", true);
    }

    private void SetTextsInUI(TextMeshProUGUI[] targets, string[] input)
    {
        for (int i = 0; i < targets.Length; ++i)
        {
            targets[i].text = input[i];
        }
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
        Addressables.LoadSceneAsync(menuScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
