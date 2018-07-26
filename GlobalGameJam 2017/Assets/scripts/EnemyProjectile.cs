using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {


    public float speed;
    public int explodeSize;
    public float explodeDamage;
    public Vector2 direction;
    public GameObject explosionPrefab;
    public PlayerScript ps;
    private Vector2 velocity;
    private GameObject o;

    // Use this for initialization
    void Start()
    {
        velocity = new Vector2(direction.x * speed, direction.y * speed);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Wall")
            return;
        o = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        if (o)
        {
            o.GetComponent<explosion>().setSize(explodeSize);
            o.GetComponent<explosion>().damage = explodeDamage;
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
