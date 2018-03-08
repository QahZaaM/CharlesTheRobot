using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAi : MonoBehaviour {

	public Transform target1;
	private float speed1 = 0.05f;
	public float deathTimer = 2.1f;

	public float fireRate = 0;
	float timeToFire = 0;
	private bool shooting = false;
	public float minShootingRange;
	public float maxShootingRange;
	public float bulletSpeed = 10f;

	public GameObject bullet;

	void Start() {
		target1 = GameObject.FindGameObjectWithTag("Player").transform;
	}
	void Update () {
		if(target1 != null){
			if(Mathf.Abs((target1.position - transform.position).x) > minShootingRange && Mathf.Abs((target1.position - transform.position).x) < maxShootingRange){
				shooting = true;
			} else {
				shooting = false;
			}
		}

		if(!shooting){
			if (target1 != null){
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(target1.position.x, transform.position.y), speed1);
			} else if(GameObject.FindGameObjectWithTag("Player") != null) {
				target1 = GameObject.FindGameObjectWithTag("Player").transform;
			}
		} else {
			if(Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				Shoot();
			}
		}
    }

	void Shoot() {
		GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 1), Quaternion.identity);
		if (target1 != null){
			Vector2 speed = (target1.position - transform.position).normalized * bulletSpeed;
			newBullet.GetComponent<Rigidbody2D>().velocity = speed;
			Destroy(newBullet, 5f);
		} else if(GameObject.FindGameObjectWithTag("Player") != null) {
			target1 = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
}
