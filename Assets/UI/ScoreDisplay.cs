using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

    Text scoreDisplay;

    // Use this for initialization
	void Start () {
        scoreDisplay = GetComponent<Text>();
        string finalScore = ScoreKeeper.score.ToString();
        scoreDisplay.text = "Your Score: " + finalScore;
	}
	
}
