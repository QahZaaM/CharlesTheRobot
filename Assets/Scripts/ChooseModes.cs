using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseModes : MonoBehaviour {
	public void EasyMode(){
		SceneManager.LoadScene("Easy");
		GetComponent<EnemiesRemainingCalculator>().EnemiesRemaining = 22;
	 }

	 public void IntermediateMode(){
		SceneManager.LoadScene("Intermediate");
		GetComponent<EnemiesRemainingCalculator>().EnemiesRemaining = 30;
	 }

	 public void HardMode(){
		SceneManager.LoadScene("Hard");
		GetComponent<EnemiesRemainingCalculator>().EnemiesRemaining = 32;
	 }

	 public void ImpossibleMode(){
		SceneManager.LoadScene("Impossible");
		GetComponent<EnemiesRemainingCalculator>().EnemiesRemaining = 33;
	 }
}
