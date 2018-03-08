using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {
	
	public static GameMaster gm;

	void Awake() {
		if(gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public Transform spawnPrefab;

	public int spawnDelay = 2;

	public IEnumerator RespawnPlayer() {
		yield return new WaitForSeconds(spawnDelay);

		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);		
	}

	public static void KillPlayer(Player player) {
		Destroy (player.gameObject);
		gm.StartCoroutine(gm.RespawnPlayer());
	}

	public static void KillEnemy(Enemy enemy) {
		gm._KillEnemy(enemy);
	}

	public void _KillEnemy(Enemy _enemy){
		Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
		Destroy (_enemy.gameObject);
	}
}