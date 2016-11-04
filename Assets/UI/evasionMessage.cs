using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class evasionMessage : MonoBehaviour {

    public Text text;

    void Start()
    {
        text.text = "";
    }

    public void Evade()
    {
        InvokeRepeating("DisplayOn", 0.001f, 1f);
        Invoke("DisplayOff", 3f);
    }

    void DisplayOn()
    {
        text.text = "*Evading*";
        Invoke("DisplayClear", 0.75f);
    }

    void DisplayClear()
    {
        text.text = "";
    }

    void DisplayOff()
    {
        CancelInvoke();
    }
}
