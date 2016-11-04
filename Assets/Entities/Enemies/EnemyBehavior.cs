using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float health = 3;
    public float fireRate = 0.5f;

    public GameObject smallShot;
    public AudioClip explode;
    public AudioClip hit;
    public GameObject explodeParticles;

    private ScoreKeeper scoreKeeper;

    private string entryType;

    void Start()
    {
        //find score keeper
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        Position parentSpawner = transform.parent.gameObject.GetComponent(typeof (Position)) as Position;
        entryType = parentSpawner.GetEntryType();
        PlayEntryAnim();
    }

    void Update()
    {
        float probability = Time.deltaTime * fireRate;
        if (Random.value <= probability)
        {
            Instantiate(smallShot, transform.position, Quaternion.identity);
        }
    }

    void PlayEntryAnim()
    {
        if(entryType == "left")
        {
            GetComponent<Animator>().Play("ArriveLeft");
        }
        else if (entryType == "right")
        {
            GetComponent<Animator>().Play("ArriveRight");
        }
        else if (entryType == "center")
        {
            GetComponent<Animator>().Play("ArriveCenter");
        }
        else
        {
            GetComponent<Animator>().Play("Idle");
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Projectile projectile = coll.gameObject.GetComponent<Projectile>();
        missleControl missile = coll.gameObject.GetComponent<missleControl>();
        if (projectile)
        {
            health -= projectile.GetDamage();
            projectile.Hit();
        }
        else if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
        }
        if (health <= 0)
        {
            Instantiate(explodeParticles, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explode, transform.position);
            scoreKeeper.Score(120);
            Destroy(gameObject);
        }
        else if (projectile || missile){ AudioSource.PlayClipAtPoint(hit, transform.position, 0.5f); }
    }
	
}
