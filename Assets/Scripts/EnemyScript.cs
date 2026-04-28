using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private float enemyInterval = 3.5f;

    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, EnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject Enemy)
    {
        yield return new WaitForSeconds(interval);

        Camera cam = Camera.main;

        // Pick a side: 0 = left, 1 = right, 2 = top, 3 = bottom
        int side = Random.Range(0, 4);
        float offset = 0.05f; // how far outside the screen


        float x = 0f;
        float y = 0f;

        switch (side)
        {
            case 0: // Left
                x = 0f - offset;
                y = Random.Range(0f, 1f);
                break;

            case 1: // Right
                x = 1f+ offset;
                y = Random.Range(0f, 1f);
                break;

            case 2: // Top
                x = Random.Range(0f, 1f);
                y = 1f+ offset;
                break;

            case 3: // Bottom
                x = Random.Range(0f, 1f);
                y = 0f- offset;
                break;
        }

        // Convert viewport → world
        Vector3 spawnPos = cam.ViewportToWorldPoint(
            new Vector3(x, y, cam.nearClipPlane + 5f)
        );

        Instantiate(Enemy, spawnPos, Quaternion.identity);

        StartCoroutine(spawnEnemy(interval, Enemy));
    }
}