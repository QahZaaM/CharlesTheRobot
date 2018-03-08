using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerOn : MonoBehaviour {

	public GameObject wall;
	public Transform spawnPoint1;
	public GameObject boss;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			wall.SetActive(true);
			Instantiate(boss, spawnPoint1.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
