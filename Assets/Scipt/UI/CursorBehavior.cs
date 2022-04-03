using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorBehavior : MonoBehaviour
{
    public static CursorBehavior instance;

    public Texture2D normalCursor;
    public Vector2 normalHotSpot = Vector2.zero;
    public Texture2D grabbingCursor;
    public Vector2 grabbingHotSpot = Vector2.zero;

    public Canvas loadingSymbol;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
        Cursor.visible = false;
        ActivateNormalCursor();
    }

    public static void ActivateGrabbingCursor()
    {
        Cursor.SetCursor(instance.grabbingCursor, instance.normalHotSpot, CursorMode.Auto);
    }

    public static void ActivateNormalCursor()
    {
        Cursor.SetCursor(instance.normalCursor, instance.normalHotSpot, CursorMode.Auto);
    }
}
