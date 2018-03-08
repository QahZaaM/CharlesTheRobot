using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlobAI : MonoBehaviour {

	public Transform target1;
	private float speed1 = 0.05f;
	public float deathTimer = 2.1f;
	public Transform sprite;

	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");		
		if (player != null){
			target1 = player.transform;
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(target1.position.x, transform.position.y), speed1);
			if(target1.position.x > transform.position.x){
				sprite.localScale = new Vector3(1, 1, 1);
			} else {
				sprite.localScale = new Vector3(-1, 1, 1);
			}
		} else {
			StartCoroutine(Wait());
			player = GameObject.FindGameObjectWithTag("Player");
		}
    }
	IEnumerator Wait() {
		yield return new WaitForSeconds(deathTimer);
	}
}
