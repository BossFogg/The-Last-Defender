using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    public Text scoreDisplay;

    public static int score;

    // Use this for initialization
	void Start () {
        Reset();
        
	}
	
	public void Score(int points)
    {
        score += points;
        scoreDisplay.text = "Score: " + score;
    }

    public void Reset()
    {
        score = 0;
        scoreDisplay.text = "Score: " + score;
    }
}
