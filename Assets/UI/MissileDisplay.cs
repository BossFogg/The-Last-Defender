using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissileDisplay : MonoBehaviour {

    public PlayerController player;
    public Text text;

    // Update is called once per frame
    void Update () {
        if (!player)
        {
            player = FindObjectOfType<PlayerController>();
        }
        float currentOrdinance = player.ordinance;
        text.text = "Missles X " + currentOrdinance;
    }
}
