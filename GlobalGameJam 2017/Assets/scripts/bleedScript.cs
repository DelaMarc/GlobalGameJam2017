using UnityEngine;
using System.Collections;

public class bleedScript : MonoBehaviour {

    private ParticleSystem p;

	// Use this for initialization
	void Start () {
        p = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (p && !p.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}
