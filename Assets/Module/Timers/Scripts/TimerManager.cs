using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {
	private static ArrayList timers = new ArrayList (); 
	public static bool isPlaying = true; 
    private static GameObject instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple TimerManager detected : instance destroyed");
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {
        foreach (Timer t in timers.ToArray())
            t.Update(Time.deltaTime);
    }

    private static void CreateInstance() {
        instance = new GameObject("Timer Manager");
        instance.AddComponent<TimerManager>();
        DontDestroyOnLoad(instance);
    }

	public static void SetupTimer(Timer t){
        if (instance == null) CreateInstance();
		timers.Add (t);
	}
}