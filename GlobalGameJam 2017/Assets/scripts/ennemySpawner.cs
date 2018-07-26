using UnityEngine;
using System.Collections;

public class ennemySpawner : MonoBehaviour {

    //ON ATTACHE LES ZONES DE SPAWN A UN GAMEOBJECT QUI LES ACTIVERA MANUELLEMENT A CHAQUE VAGUE
	public float spawnDelay;
	public float deadTime = 5;
	public int mobLimit = 20;
	public Wave[] waves;
    private bool isActive;
    private bool isGenerating;
	public Component waveMsg;
    //utiliser Ressources.load() pour charger des ennemis

	// Use this for initialization
	void Start () {
        isActive = true;
        isGenerating = false;
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isActive = true;
        }
    }

    private Vector2 getRandomPosition()
    {
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
		float x = Random.Range(circle.transform.position.x - circle.radius, circle.transform.position.x + circle.radius);
		float y = Random.Range(circle.transform.position.y - circle.radius, circle.transform.position.y + circle.radius);
        // Get a random local position
        /*float t = Random.value;
        Vector3 localPosition = Vector3.Lerp(box.center - box.size / 2, box.center + box.size / 2, t);
        // Return the world position
        return this.transform.TransformPoint(localPosition);*/

        return new Vector2(x, y);
    }

    IEnumerator generateWave()
    {
        int i = 0;
		GameObject id;
        isGenerating = true;
		while ( i < waves.Length)
		{
			waves [i].reset ();
			print ("[" + this.name + "] Begin wave " + (i + 1) + "");
			while ((id = waves[i].getNext()) != null) {
				if (isActive) {
					while (GameObject.FindGameObjectsWithTag("Enemy").Length > mobLimit)
						yield return new WaitForSeconds(1);
					yield return new WaitForSeconds(spawnDelay);
					Instantiate(id, getRandomPosition(), Quaternion.identity);
				}
				yield return new WaitForSeconds(1);
			}
			print ("[" + this.name + "] Wave " + (i + 1) + " Finished");
			while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
				yield return new WaitForSeconds(1);
			if (waveMsg)
				waveMsg.gameObject.SetActive (true);
			yield return new WaitForSeconds(deadTime);
			if (waveMsg)
				waveMsg.gameObject.SetActive (false);
			++i;
        }
        //yield return new WaitForSeconds(5);
        isGenerating = false;
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        if (isGenerating == false) { 
            StartCoroutine("generateWave");
        }
    }
}
