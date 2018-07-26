using UnityEngine;
using System.Collections;

public class Soul : MonoBehaviour {

    public int souls = 42;

    private PlayerScript ps;

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int i = Random.Range(1, souls);
            other.GetComponent<PlayerScript>().AddScore(i * souls);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
