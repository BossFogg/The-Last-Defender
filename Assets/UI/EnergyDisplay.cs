using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyDisplay : MonoBehaviour {

    private float maxHeight;
    private float maxWidth;
    public float currentWidth;

    public PlayerController player;

    void Start()
    {
        maxWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        maxHeight = gameObject.GetComponent<RectTransform>().sizeDelta.y;
        currentWidth = maxWidth;
    }

    void Update()
    {
        if (!player)
        {
            player = FindObjectOfType<PlayerController>();
        }
        currentWidth = (player.energy * maxWidth) / player.maxEnergy;
        if (currentWidth <= 0)
        {
            currentWidth = 1;
        }
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(currentWidth, maxHeight);
    }
}
