using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfobulleSpawner : MonoBehaviour
{
    private RectTransform rtr;
    private Canvas myCanvas;
    private Coroutine fillerCoroutine;


    public static InfobulleSpawner instance;

    public Image fillerImage;

    private Timer.TimerFunction display;
    private Timer.TimerFunction hide;

    private bool infobulleOn = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        rtr = GetComponent<RectTransform>();
        myCanvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        rtr.localPosition = Input.mousePosition;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }

    public void StartFillerTimer(float duration, Timer.TimerFunction display, Timer.TimerFunction hide)
    {
        if (fillerCoroutine != null)
        {
            StopFillerTimer();
        }
        this.display = display;
        this.hide = hide;
        fillerCoroutine = StartCoroutine(FillerTimer(duration));
    }

    private IEnumerator FillerTimer(float duration)
    {
        float timer = 0.0f;
        while (timer <= duration)
        {
            fillerImage.fillAmount = timer / duration;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        fillerImage.fillAmount = 1.0f;
        yield return new WaitForSeconds(0.2f);
        fillerImage.fillAmount = 0.0f;
        display();
        infobulleOn = true;
    }

    public void RemoveInfobulleIfAny()
    {
        if (infobulleOn)
        {
            Debug.Log("Removing infobulle");
            Timer t = new Timer(0.5f, hide);
            t.Play();
            //hide();
            infobulleOn = false;
        }
    }

    public void StopFillerTimer()
    {
        if (fillerCoroutine != null)
        {
            StopCoroutine(fillerCoroutine);
            fillerImage.fillAmount = 0.0f;
        }
    }


}
