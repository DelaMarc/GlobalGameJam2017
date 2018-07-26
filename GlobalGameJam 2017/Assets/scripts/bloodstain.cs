using UnityEngine;
using System.Collections;

public class bloodstain : MonoBehaviour {
   // private float createTime = 0;
    private float start = 5f;
    private float timer = 3;
    private float time =0;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
	}

    void vanish()
    {

        time += Time.deltaTime;
        if (time >= start)
        {
            //sr.color.a = (time - start > timer ? timer : time - start) / timer;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1 - (time - start) / timer);
            //sr.SetAlpha((time - start > timer ? timer : time - start) / timer);
        }
        if (time >= start + timer)
            Destroy(gameObject);
    }

    void Update()
    {
        //if (vanishing)
            vanish();
    }
}
