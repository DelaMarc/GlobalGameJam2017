using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    public Vector3 newScale;
    public float damage = 1f;

	// Use this for initialization
	void Start () {
	}
	
   

    public void setSize(float size)
    {
        newScale = new Vector3(size, size, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("explosion");
        if (other.tag == "Player")
        {
            print("exploded");
            other.gameObject.GetComponent<PlayerScript>().TakeDamage(damage);
        }
    }

    void changeScale()
    {
        if (transform.localScale.x >= newScale.x - 0.3)
        {
            Destroy(gameObject);
        }
        else {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * 2.5f);
        }
        
    }

	// Update is called once per frame
	void Update () {
        changeScale();
    }
}
