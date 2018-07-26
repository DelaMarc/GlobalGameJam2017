using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {

    public float healPercentage = 0.3f;

    private PlayerScript ps;

    // Use this for initialization
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            PlayerScript p = other.gameObject.GetComponent<PlayerScript>();
            if (p.getMaxHp() > p.hp) {
                other.collider.GetComponent<PlayerScript>().AddHpPercentage(healPercentage);
                Destroy(gameObject);
            }
        }
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerScript>().AddHpPercentage(healPercentage);
            Destroy(gameObject);
        }
    }*/

    // Update is called once per frame
    void Update()
    {

    }
}
