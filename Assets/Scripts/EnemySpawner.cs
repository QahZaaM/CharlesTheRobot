using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public Transform spaceShip;
	public Transform groundEnemy;	
	public int spawnNumbers = 1;
    public EnemiesRemainingCalculator emrScript;

    void Start()
    {
        emrScript = GameObject.Find("EnemiesRemaining").GetComponent<EnemiesRemainingCalculator>();

    }


	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			numberToSpawn(spawnNumbers);
        //    emrScript.EnemiesRemaining += (spawnNumbers*2);  
   		Destroy(this.gameObject);
		}
	}

	void numberToSpawn(int num){
		for(int i = 0; i < num; i++){
			Instantiate(spaceShip, spawnPoint1.position + new Vector3(3f * i, 0), Quaternion.identity);
			Instantiate(groundEnemy, spawnPoint2.position + new Vector3(3f * i, 0), Quaternion.identity);
		}
	}
}
