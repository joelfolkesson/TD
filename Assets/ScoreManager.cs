using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {
	public int lives = 20;
	public int money = 100;

	public Text moneyText;
	public Text livesText;

	public void loseLife(int l = 1){
		lives -= l;
		if (lives <= 0) {
			GameOver();
		}
	}
	public void GameOver(){
		Debug.Log ("Game Over");
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
	void Update(){
		moneyText.text = "Money: $" + money.ToString();
		livesText.text = "Lives: " + lives.ToString();
	}
}
