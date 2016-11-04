using UnityEngine;
using System.Collections;

public class missleControl : MonoBehaviour {

    public float damage;
    public float thrust;
    public float turnRadius;
    public float maxVelocity;

    public GameObject explode;
    public AudioClip explodeSound;

    private Transform compass;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        compass = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        RotateTowardTarget();
        Accelerate();
    }

    void RotateTowardTarget()
    {
        transform.rotation = compass.rotation;
    }

    void Accelerate ()
    {
        var direction = transform.TransformDirection(Vector3.up);
        rb.AddForce(direction * thrust * Time.deltaTime);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        AudioSource.PlayClipAtPoint(explodeSound, transform.position);
        Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
