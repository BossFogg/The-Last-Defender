using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyStandard;
    public float speed = 5;
    public float width = 5;
    public float height = 5;
    public float spawnDelay = 1f;

    //private float xMax;
    //private float xMin;
    
    // Use this for initialization
	void Start () {
        Populate();

    }

    void OnDrawGizmos ()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
	
	// Update is called once per frame
	void Update () {

        if (EverybodyDead())
        {
            Populate();
        }
	}

    //void IncrementMove()
    //{
    //    if (moveLeft)
    //    {
    //        transform.position += Vector3.left * speed * Time.deltaTime;
    //    }
    //    else
    //    {
    //        transform.position += Vector3.right * speed * Time.deltaTime;
    //    }
    //    float rightEdge = transform.position.x + (0.5f * width);
    //    float leftEdge = transform.position.x - (0.5f * width);
    //    if (rightEdge >= xMax)
    //    {
    //        moveLeft = true;
    //    }
    //    else if (leftEdge <= xMin)
    //    {
    //        moveLeft = false;
    //    }
    //}

    bool EverybodyDead()
    {
        foreach (Transform childPosition in transform)
        {
            if (childPosition.childCount >= 1)
            {
                return false;
            }
        }
        return true;
    }

    void Populate()
    {
        Transform freePosition = NextPosition();
        if (freePosition)
        {
            Position pos = freePosition.gameObject.GetComponent<Position>();
            pos.SpawnEnemy();
        }
        if (NextPosition())
        {
            Invoke("Populate", spawnDelay);
        }
    }

    Transform NextPosition()
    {
        foreach (Transform childPos in transform)
        {
            if (childPos.childCount == 0)
            {
                return childPos;
            }
        }
        return null;
    }

    //void SetBoundary ()
    //{
    //    float distance = transform.position.z - Camera.main.transform.position.z;
    //    Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
    //    Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
    //    xMax = rightMost.x;
    //    xMin = leftMost.x;
    //}
}
