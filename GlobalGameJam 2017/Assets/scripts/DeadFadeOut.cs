using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeadFadeOut : MonoBehaviour {

    public float start = 0f;
    public float timer = 3.0f;
    private float time = 0;
    Button b;
    CanvasRenderer cr;
	// Use this for initialization
	void Start () {
        if ((b = this.GetComponent<Button>()))
        {
            b.interactable = false;
        }
        cr = this.GetComponent<CanvasRenderer>();
        cr.SetAlpha(0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= start)
        {
            if (b && !b.interactable)
                b.interactable = true;
            cr.SetAlpha((time - start > timer ? timer : time - start) / timer);
        }
    }
}
