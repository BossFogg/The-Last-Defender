using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

    //need variables for sparrow sprite, each UI image element, and a variable to track lives.
    public int lives;
    private GameManager manager;
    public GameObject playerController;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        lives = 3;
    }

    public void LostLife()
    {
        GameObject currentLife = transform.GetChild(lives).gameObject;
        Destroy(currentLife);
        lives--;
        if (lives<=-1)
        {
            manager.LoseGame();
        }
        else
        {
            Instantiate(playerController, new Vector3 (6f, 3.5f, 0f), Quaternion.identity);
        }
    }
}
