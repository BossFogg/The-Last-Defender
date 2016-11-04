using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

    //TODO: figure out why this is not colliding properly

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}
