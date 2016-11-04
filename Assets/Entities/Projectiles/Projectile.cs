using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed = 10f;
    public float damage = 1f;
    public enum Type { playerSmall, enemySmall }
    public Type projectileType;
    public GameObject enemyShotExplode;
    public GameObject playerShotExplode;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, speed, 0);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (projectileType == Type.enemySmall)
        {
            Instantiate(enemyShotExplode, transform.position, Quaternion.identity);
        }
        else if (projectileType == Type.playerSmall)
        {
            Instantiate(playerShotExplode, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
