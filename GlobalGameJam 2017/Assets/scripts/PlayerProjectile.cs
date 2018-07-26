using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour {

    public float speed;
    public int explodeSize;
    public float explodeDamage;
    public Vector2 direction;
    public GameObject explosionPrefab;
    public PlayerScript ps;
    private Vector2 velocity;
    private GameObject o;

	// Use this for initialization
	void Start () {
        //assigner la velocité
        velocity = new Vector2(direction.x * speed, direction.y * speed);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Wall")
            return;
        print("Collision1!" + collision.gameObject.tag);
        o = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        print(o);
        //o.transform.parent = gameObject.transform;
        if (o)
        {
            o.GetComponent<explosion2>().setSize(explodeSize);
            //o.GetComponent<explosion>().newScale = new Vector3(explodeSize, explodeSize, o.transform.localScale.z);
            print("damage: " + explodeDamage.ToString());
            o.GetComponent<explosion2>().damage = explodeDamage;
            o.GetComponent<explosion2>().ps = ps;
        }
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spawnArea" || collision.gameObject.tag == "Player")
            return;
        print("Collision1!" + collision.gameObject.tag);
        o = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        print(o);
        //o.transform.parent = gameObject.transform;
        if (o) {
            o.GetComponent<explosion2>().setSize(explodeSize);
            //o.GetComponent<explosion>().newScale = new Vector3(explodeSize, explodeSize, o.transform.localScale.z);
            print("damage: " + explodeDamage.ToString());
            o.GetComponent<explosion2>().damage = explodeDamage;
            o.GetComponent<explosion2>().ps = ps;
        }
        Destroy(this.gameObject);
    }

	// Update is called once per frame
	void Update () {
    }
}
