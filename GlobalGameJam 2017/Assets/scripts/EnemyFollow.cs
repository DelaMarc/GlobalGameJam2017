using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

	GameObject target;
	float	rndTime;
	float	rndTimeLimit;
	Vector2 rndVec;
	public float rndMaxTime = 3.0f;
	public GameObject map;
	public float movSpeed = 1f;
	public float distFollow = 4f;
	public float distTurnAround = 2f;
	public float distTooNear = 1.0f;
	public bool  right = true;
	public float randomDirections = 0.01f;
    public float fireRate = 0.5f;
    public float fireTimer = 0;
    public bool rangeAttack = true;
    public float damage = 1f;
    // Use this for initialization
    void Start () {
		rndTime = rndMaxTime;
		rndTimeLimit = Random.Range (0, rndMaxTime);
	}
	
	void FixedUpdate () {
		target = GameObject.Find ("Player");
		if (target) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, target.transform.position - transform.position, Mathf.Max (distFollow, distTurnAround), 1 << LayerMask.NameToLayer("Player"));
			if (hit.collider != null) {
				if (hit.collider.tag == "Player"){
					if (hit.distance > 0) {
						if (randomDirections != 0) {
							float rdm = Random.value;
							if (rdm < randomDirections)
								right = !right;
						}
                        fireTimer += Time.deltaTime;
                        if (fireTimer >= 1 / fireRate && rangeAttack)
                        {
                            fireTimer = 0;
                            GameObject obj = Instantiate(Resources.Load("prefabs/conceptProjectile"), transform.position, Quaternion.identity) as GameObject;
                            obj.GetComponent<EnemyProjectile>().explodeDamage = damage;
                            Vector2 a = target.transform.position - transform.position;
                            a.Normalize();
                            obj.GetComponent<EnemyProjectile>().direction = a;
                        }
                        moveTo (target.transform.position, hit.distance);
						return;
					}
				} else {
					randomMove ();
					return;
				}
			} else {
				randomMove ();
				return;
			}
		}
		transform.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}

	void randomMove() {
		rndTime += Time.fixedDeltaTime;
		if (rndTime >= rndTimeLimit) {
			float scX2 = map.transform.lossyScale.x / 2.0f;
			float scY2 = map.transform.lossyScale.y / 2.0f;
			rndVec = new Vector2 (Random.Range (-scX2, scX2), Random.Range (-scY2, scY2));
			rndTime = 0;
			rndTimeLimit = Random.Range (1f, rndMaxTime);
		}
		if (Vector2.Distance (rndVec, (Vector2)transform.position) > 0.1)
			moveTo (rndVec, distTurnAround + 1.0f);
		else
			transform.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}

	void moveTo(Vector2 position, float hitDistance)
	{
		Vector2 diff = position - (Vector2)transform.position;
		float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rot_z + 90);
		transform.GetComponent<Rigidbody2D> ().velocity =
			(
				hitDistance <= distTurnAround ?
				(
					hitDistance < distTooNear ?
					transform.up
					:
					(
						right ?
						transform.right
						: -transform.right
					)
				)
				: -transform.up
			) * movSpeed;
	}
}