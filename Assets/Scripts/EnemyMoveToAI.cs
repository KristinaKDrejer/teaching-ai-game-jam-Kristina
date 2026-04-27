using UnityEngine;

public class EnemyMoveToAI : MonoBehaviour
{

    [SerializeField] private GameObject aiCore;
    [SerializeField] private float speed = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, aiCore.transform.position, speed * Time.deltaTime);
        
    }
}
