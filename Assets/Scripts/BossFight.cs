using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour {

	public Transform target1;
	public float deathTimer = 2.1f;

	public float fireRate = 0;
	float timeToFire = 0;
	private bool shooting = true;
	public float bulletSpeed = 10f;

	public GameObject bullet;
	public GameObject boss;
	bool isSeventyFive = false;
	bool isFifty = false;
	bool isTwentyFive = false;
	public Transform spaceShip;

	private bool enemyWaveOne = false;
	private bool enemyWaveTwo = false;
	private bool enemyWaveThree = false;

	public float xSpawnPoint = 182f;
	public float ySpawnPoint = 12f;

	void Start() {
		target1 = GameObject.FindGameObjectWithTag("Player").transform;
	}
	void Update () {
		if(GetComponent<Enemy>().stats.currentHealth <= GetComponent<Enemy>().stats.maxHealth * 0.75 && isSeventyFive == false){
			if(enemyWaveOne == false){
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 1, 0), Quaternion.identity);
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 2, 0), Quaternion.identity);
				enemyWaveOne = true;
			}
			if(GameObject.FindGameObjectsWithTag("Enemy").Length != 1){
				shooting = false;
			} else {
				shooting = true;
				isSeventyFive = true;
			}
		}

		if(GetComponent<Enemy>().stats.currentHealth <= GetComponent<Enemy>().stats.maxHealth * 0.50 && isFifty == false){
			if(enemyWaveTwo == false){
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 1, 0), Quaternion.identity);
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 2, 0), Quaternion.identity);
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 3, 0), Quaternion.identity);
				enemyWaveTwo = true;
			}
			if(GameObject.FindGameObjectsWithTag("Enemy").Length != 1){
				shooting = false;
			} else {
				shooting = true;
				isFifty = true;
			}
		}

		if(GetComponent<Enemy>().stats.currentHealth <= GetComponent<Enemy>().stats.maxHealth * 0.25 && isTwentyFive == false){
			if(enemyWaveThree == false){
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 1, 0), Quaternion.identity);
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 2, 0), Quaternion.identity);
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 3, 0), Quaternion.identity);
				Instantiate(spaceShip, new Vector3(xSpawnPoint, ySpawnPoint, 0) + new Vector3(3f + 4, 0), Quaternion.identity);
				enemyWaveThree = true;
			}
			if(GameObject.FindGameObjectsWithTag("Enemy").Length != 1){
				shooting = false;
			} else {
				shooting = true;
				isTwentyFive = true;
			}
		}

		if(shooting){
			GetComponent<Enemy>().takeDamage = true;
			if (Time.time > timeToFire){
				timeToFire = Time.time + 1 / fireRate;
				Shoot();
			} 
		} else {
			GetComponent<Enemy>().takeDamage = false;
		}

		if(GetComponent<Enemy>().stats.currentHealth <= 10){
			boss.SetActive(false);
			Invoke("endGame", 3);
		}
    }

	void endGame(){
		SceneManager.LoadScene("GameOver");
	}
	
	void Shoot() {
		GameObject newBullet = Instantiate(bullet, new Vector3(transform.position.x - 5, transform.position.y), Quaternion.identity);
		if (target1 != null){
			Vector2 speed = (target1.position - transform.position).normalized * bulletSpeed;
			newBullet.GetComponent<Rigidbody2D>().velocity = speed;
			Destroy(newBullet, 5f);
		} else if(GameObject.FindGameObjectWithTag("Player") != null) {
			target1 = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
}
