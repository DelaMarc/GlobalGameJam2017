using UnityEngine;
using System.Collections;

public class explosion2 : MonoBehaviour
{

    public Vector3 newScale;
    public float damage;
    public PlayerScript ps;

    // Use this for initialization
    void Start()
    {
    }

    public void setSize(float size)
    {
        newScale = new Vector3(size, size, transform.localScale.z);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().takeDamage(damage, ps);
        }
    }

    void changeScale()
    {
        if (transform.localScale.x >= newScale.x - 0.3)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * 4.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        changeScale();
    }
}
