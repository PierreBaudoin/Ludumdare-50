using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoBulleController : MonoBehaviour
{
    private Canvas canvas;

    // Start is called before the first frame update
    void Awake()
    {
        this.canvas = GetComponent<Canvas>();
        this.canvas.worldCamera = Camera.main;
    }
}
