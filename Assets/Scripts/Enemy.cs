using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {
		public int maxHealth = 100;

		private int _currentHealth;
		public int currentHealth {
			get {return _currentHealth;}
			set {_currentHealth = Mathf.Clamp(value, 0, maxHealth);}
		}

		public int damage = 40;

		public void Init() {
			currentHealth = maxHealth;
		}
	}

	public EnemyStats stats = new EnemyStats();

	public Transform deathParticles;
    public EnemiesRemainingCalculator emrScript;

	public int fallBoundary = -20;

	public bool takeDamage = true;

	[Header("Optional: ")]
	[SerializeField]
	private StatusIndicator statusIndicator;
	
	void Start() {
		stats.Init();
        emrScript = GameObject.Find("EnemiesRemaining").GetComponent<EnemiesRemainingCalculator>();

        if (statusIndicator != null) {
			statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
		}

		if(deathParticles == null){
			Debug.LogError("no death particles on enemy");
		}
	}

	void Update() {
		if(transform.position.y <= fallBoundary) {
			GameMaster.KillEnemy(this);
			if (emrScript.EnemiesRemaining != 0){
                emrScript.EnemiesRemaining--;
            }
		}
	}

	public void DamageEnemy (int damage) {
		if(takeDamage){
			stats.currentHealth -= damage;
			if(stats.currentHealth <= 0) {
                if (emrScript.EnemiesRemaining != 0)
                {
                    emrScript.EnemiesRemaining--;
                }
				GameMaster.KillEnemy(this);
			}
			if(statusIndicator != null) {
				statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D _colInfo){
		Player _player = _colInfo.collider.GetComponent<Player>();
		if(_player != null){
			_player.DamagePlayer(stats.damage);
			DamageEnemy(101);
		}
	}
}
