using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //public variables for tweaking
    public float speed = 5f;
    public float maxHealth = 3;
    public float maxEnergy = 100;
    public int maxOrdinance = 6;
    public float fireRate = 0.2f;
    public float xpadding;
    public float ypadding;
    public int maxEngine;
    public int cruiseEngine;
    public int minEngine;
    public float evadeCost;
    public float energyChargeRate;

    //other variables
    public int ordinance;
    public float health;
    public float energy;

    //variables for defining movement limits 
    float xMax;
    float xMin;
    float yMax;
    float yMin;

    //variables for tracking key presses
    bool leftKeyDown = false;
    bool rightKeyDown = false;
    bool upKeyDown = false;
    bool downKeyDown = false;
    bool fireKeyDown = false;
    bool missileLeft = false;

    //For linking child
    public GameObject playerShip;
    public Sprite left;
    public Sprite right;
    public Sprite stationary;
    public ParticleSystem engine;

    //projectile Links
    public GameObject standardProjectile;
    public GameObject missile;
    public AudioClip explode;
    public AudioClip shipHit;
    public GameObject explodeParticles;

    //UI links
    private Lives lives;
    private evasionMessage evadeDisplay;

    // Use this for initialization
    void Start ()
    {
        //set stats
        health = maxHealth;
        energy = maxEnergy;
        ordinance = maxOrdinance;
        
        //Read screen size and define movement limits
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
        Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.65f, distance));
        Vector3 bottomMost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
        xMin = leftMost.x + xpadding;
        xMax = rightMost.x - xpadding;
        yMin = bottomMost.y + ypadding;
        yMax = topMost.y - ypadding;

        //find and update UI Elements
        lives = FindObjectOfType<Lives>();
        evadeDisplay = FindObjectOfType<evasionMessage>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckKeys();
        CheckFire();
        IncrementMove();
        ConstrainMove();
        EnergyCharge();
    }

    void EnergyCharge()
    {
        if (energy < maxEnergy)
        {
            energy += (energyChargeRate * Time.deltaTime);
        }
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }

    void CheckKeys ()
    {
        //if a given key is pressed, return true for corresponding bool value. Otherwise, return false
        if (Input.GetKeyDown(KeyCode.A)) {
            leftKeyDown = true;
            InvulnerabilityPasser invul = FindObjectOfType<InvulnerabilityPasser>();
            if (!invul.invulnerable)
            {
                playerShip.GetComponent<Animator>().Play("TurnLeft");
            }
        }
        else if (Input.GetKeyUp(KeyCode.A)) {
            leftKeyDown = false;
            InvulnerabilityPasser invul = FindObjectOfType<InvulnerabilityPasser>();
            if (!invul.invulnerable && !rightKeyDown)
            {
                playerShip.GetComponent<Animator>().Play("SettleLeft");
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rightKeyDown = true;
            InvulnerabilityPasser invul = FindObjectOfType<InvulnerabilityPasser>();
            if (!invul.invulnerable)
            {
                playerShip.GetComponent<Animator>().Play("TurnRight");
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rightKeyDown = false;
            InvulnerabilityPasser invul = FindObjectOfType<InvulnerabilityPasser>();
            if (!invul.invulnerable && !leftKeyDown)
            {
                playerShip.GetComponent<Animator>().Play("SettleRight");
            }
        }
        if (Input.GetKey(KeyCode.W)) { upKeyDown = true; }
        else { upKeyDown = false; }
        if (Input.GetKey(KeyCode.S)) { downKeyDown = true; }
        else { downKeyDown = false; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (energy>=evadeCost) {
                fireKeyDown = false;
                evadeDisplay.Evade();
                playerShip.GetComponent<Animator>().Play("Evasion");
                energy -= evadeCost;
            }
        }
    }

    void CheckFire ()
    {
        if (Input.GetMouseButtonDown(0)) {
            fireKeyDown = true;
            InvokeRepeating("StandardShot", 0.0001f, fireRate);
        }
        if (Input.GetMouseButtonUp(0) || !fireKeyDown) { CancelInvoke("StandardShot"); }
        if (Input.GetMouseButtonDown(1)) { MissileShot(); }
    }

    void StandardShot()
    {
        Vector3 offset = new Vector3(0f, 0.5f, 0f);
        Instantiate(standardProjectile, transform.position + offset, Quaternion.identity);
    }

    void MissileShot()
    {
        Vector3 offset1 = new Vector3(-0.3f, 0f, 0f);
        Vector3 offset2 = new Vector3(0.3f, 0f, 0f);
        if (ordinance >= 1)
        {
            if (missileLeft)
            {
                Instantiate(missile, transform.position + offset1, Quaternion.identity);
                ordinance--;
                missileLeft = false;
            }
            else
            {
                Instantiate(missile, transform.position + offset2, Quaternion.identity);
                ordinance--;
                missileLeft = true;
            }
        }
    }

    void IncrementMove ()
    {
        //if movement keys pressed, increment ship movement by speed amount
        var em = engine.emission;

        if (leftKeyDown)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            playerShip.GetComponent<SpriteRenderer>().sprite = left;
        }
        else if (rightKeyDown)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            playerShip.GetComponent<SpriteRenderer>().sprite = right;
        }
        else
        {
            playerShip.GetComponent<SpriteRenderer>().sprite = stationary;
        }
        if (upKeyDown)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            em.rate = maxEngine;
        }
        else if (downKeyDown)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
            em.rate = minEngine;
        }
        else
        {
            em.rate = cruiseEngine;
        }

    }
    void ConstrainMove()
    {
        //clamp x and y positions to values defined in start.
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Projectile projectile = coll.gameObject.GetComponent<Projectile>();
        InvulnerabilityPasser invul = FindObjectOfType<InvulnerabilityPasser>();
        if (projectile && (invul.invulnerable == false))
        {
            health -= projectile.GetDamage();
            projectile.Hit();
            if (health <= 0)
            {
                Instantiate(explodeParticles, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(explode, transform.position);
                lives.LostLife();
                Destroy(gameObject);
            }
            else { AudioSource.PlayClipAtPoint(shipHit, transform.position); }
        }
    }

}
