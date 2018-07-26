using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float life = 1;
    public int score = 12;
	// Use this for initialization
	void Start () {
	
	}
	
    private void die()
    {
        float i = Random.value;
        if (i < 0.10)
            Instantiate(Resources.Load("prefabs/HealthPot"), transform.position, Quaternion.identity);
        else if (i < 0.12)
            Instantiate(Resources.Load("prefabs/BuffPot"), transform.position, Quaternion.identity);
        else if (i < 0.14)
            Instantiate(Resources.Load("prefabs/SoulOrb"), transform.position, Quaternion.identity);
        Instantiate(Resources.Load("particles/bleeding"),transform.position, Quaternion.identity);
        if (i < 0.5f)
            Instantiate(Resources.Load("textures/sprite_sang"), transform.position, Quaternion.identity);
        else
            Instantiate(Resources.Load("textures/sprite_flaque_sang"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void takeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            die();
        }
    }

    public void takeDamage(float damage, PlayerScript ps)
    {
        life -= damage;
        if (life <= 0)
        {
            ps.AddScore(score);
            ps.AddKill();
            die();
        }
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("r"))
        {
            takeDamage(1);
        }
	}
}
