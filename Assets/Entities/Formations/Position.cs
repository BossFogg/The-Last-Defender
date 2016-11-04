using UnityEngine;
using System.Collections;



public class Position : MonoBehaviour {

    public enum EnterFrom { left, right, center };
    public enum EnemyType { standard, heavy, spreader, speedy };
    public EnterFrom enterFrom;
    public EnemyType enemyType;
    public GameObject standardEnemy;
    public GameObject heavyEnemy;
    public GameObject spreaderEnemy;

    private GameObject currentEnemy;


    void Start()
    {
        if (enemyType == EnemyType.standard)
        {
            currentEnemy = standardEnemy;
        }
        else if (enemyType == EnemyType.heavy)
        {
            currentEnemy = heavyEnemy;
        }
        else if (enemyType == EnemyType.spreader)
        {
            currentEnemy = spreaderEnemy;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
	
    public string GetEntryType()
    {
        string entryType;
        if (enterFrom == EnterFrom.left)
        {
            entryType = "left";
            return entryType;
        }
        else if (enterFrom == EnterFrom.center)
        {
            entryType = "center";
            return entryType;
        }
        else if (enterFrom == EnterFrom.right)
        {
            entryType = "right";
            return entryType;
        }
        return null;
    }
	
    public void SpawnEnemy()
    {
        GameObject enemy1 = Instantiate(currentEnemy, transform.position, Quaternion.identity) as GameObject;
        enemy1.transform.parent = transform;
    }

}
