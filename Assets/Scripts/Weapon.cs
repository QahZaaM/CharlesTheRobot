using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public int Damage = 10;
	public LayerMask whatToHit;
	public Transform BulletTrailPrefab;
	public Transform MuzzleFlashPrefab;
	public Transform hitPrefab;

	public float effectSpawnRate = 10;
	float timeToSpawnEffect = 0;
	float timeToFire = 0;
	Transform firePoint;

	// Use this for initialization
	void Awake () {
		firePoint = transform.Find("FirePoint");
		if(firePoint == null) {
			Debug.LogError("No FirePoint? WHAT?!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(fireRate == 0){
			if(Input.GetButtonDown("Fire1"))	{
				Shoot();
			}
		} else {
			if(Input.GetButton("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				Shoot();
			}
		}
    }

	void Shoot() {
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPos = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPos, (mousePosition - firePointPos) * 100, 100, whatToHit);

		if(Time.time >= timeToSpawnEffect) {
			Vector3 hitPos;
			Vector3 hitNormal;

			if(hit.collider == null){
				hitPos = (mousePosition - firePointPos) * 30;
				hitNormal = new Vector3 (9999, 9999, 9999);
			} else {	
				hitPos = hit.point;
				hitNormal = hit.normal;
                if (hit.collider.gameObject.tag == "Enemy")
                {   
                    hit.collider.gameObject.GetComponent<Enemy>().DamageEnemy(Damage);
                } else if(hit.collider.gameObject.tag == "bullet") {
					Destroy(hit.collider.gameObject);
				}
			}

			Effect(hitPos, hitNormal);
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}
	}

	void Effect(Vector3 hitPos, Vector3 hitNormal){
		Transform trail = Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
		LineRenderer lr = trail.GetComponent<LineRenderer>();

		if(lr != null){
			lr.SetPosition(0, firePoint.position);
			lr.SetPosition(1, hitPos);
		}

		Destroy(trail.gameObject, 0.04f);

		if(hitNormal !=  new Vector3(9999, 9999, 9999)){
			Transform hitParticle = Instantiate(hitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
			Destroy(hitParticle.gameObject, 1f);
		}
	}
}
