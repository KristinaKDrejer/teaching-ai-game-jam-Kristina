using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
     [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private float enemyInterval = 3.5f; // Time interval between enemy spawns
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, EnemyPrefab));
        
    }

   private IEnumerator spawnEnemy( float interval, GameObject Enemy)
    
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(Enemy, new Vector3(Random.Range(-5, 5), Random.Range(-6, 6), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, Enemy));
     
     
    }
}
