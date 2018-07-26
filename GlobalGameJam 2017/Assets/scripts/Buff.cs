using UnityEngine;
using System.Collections;

public class Buff : MonoBehaviour {

    public float waitTime;

    private PlayerScript ps;

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            int i = Random.Range(0, 3);
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ps = other.gameObject.GetComponent<PlayerScript>();
            ps.AddEffect("powerUp");
            // power
            if (i == 0)
            {
                StartCoroutine("buffPower");
            }
            else if (i == 1) // speed
            {
                StartCoroutine("buffSpeed");
            }
            else // fire rate
            {
                StartCoroutine("buffFireRate");
            }
        }
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int i = Random.Range(0, 2);
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ps = other.GetComponent<PlayerScript>();
            ps.AddEffect("powerUp");
            // power
            if (i == 0)
            {
                StartCoroutine("buffPower");
            }
            else if (i == 1) // speed
            {
                StartCoroutine("buffSpeed");
            }
            else // fire rate
            {
                StartCoroutine("buffFireRate");
            }
        }
    }*/

    IEnumerator buffPower()
    {
        ps.power.bonusMult += 1;
        yield return new WaitForSeconds(waitTime);
        ps.power.bonusMult -= 1;
        Destroy(gameObject);
    }

    IEnumerator buffSpeed()
    {
        ps.speed.bonusMult += 1;
        yield return new WaitForSeconds(waitTime);
        ps.speed.bonusMult -= 1;
        Destroy(gameObject);
    }

    IEnumerator buffFireRate()
    {
        ps.fireRate.bonusMult += 1;
        yield return new WaitForSeconds(waitTime);
        ps.fireRate.bonusMult -= 1;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
