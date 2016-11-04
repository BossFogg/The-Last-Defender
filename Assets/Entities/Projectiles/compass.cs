using UnityEngine;
using System.Collections;

public class compass : MonoBehaviour {

    public Transform target;
    

	// Update is called once per frame
	void Update () {
        if (target)
        {
            var dir = target.position - transform.position;
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else { target = FindObjectOfType<EnemyBehavior>().transform; }
	}
}
