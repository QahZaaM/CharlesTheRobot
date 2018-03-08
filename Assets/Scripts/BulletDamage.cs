using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {
	
	public int damage = 10;
	public GameObject explosion;
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Charles"){
			Instantiate(explosion, other.transform.position, Quaternion.identity);
			other.gameObject.GetComponent<Player>().DamagePlayer(damage);
			Destroy(this.gameObject);
		} else if (other.gameObject.tag == "Ground"){
			Instantiate(explosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}else {
			Destroy(this.gameObject, 5f);
		}
	}
}
