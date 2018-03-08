using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats {
		public int maxHealth = 100;

		private int _curHealth;
		public int curHealth{
			get{return _curHealth;}
			set {_curHealth = Mathf.Clamp(value, 0, maxHealth);}
		}
		
		public void Init (){
			curHealth = maxHealth;
		}
	}
	[SerializeField]
	private StatusIndicator statusIndicator;

	public PlayerStats stats = new PlayerStats();

	public int fallBoundary = -20;

	void Start() {
		stats.Init();

		if(statusIndicator == null) {
			Debug.LogError("no status indicator referenced on player");
		} else {
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
	}

	void Update () {
		if(transform.position.y <= fallBoundary) {
			DamagePlayer (99999999);
		}
	}

	public void DamagePlayer (int damage) {
		stats.curHealth -= damage;
		if(stats.curHealth <= 0) {
			GameMaster.KillPlayer(this);
			SceneManager.LoadScene("YouLose");
		}

		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}
}
