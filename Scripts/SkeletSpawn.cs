using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletSpawn : MonoBehaviour
{
    [SerializeField] public GameObject skelet;
    [SerializeField] public MainCharacter MC;
    [SerializeField] public Vector2 spawnPosition;
    public float spawnDelay;
    public float nextSpawn = 0.0f;
    public int countOfEnemy = 0;
    public int rangeA = 0;
    public int rangeB = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        skelet.GetComponent<EnemyMovement>().target = MC;
    }

    // Update is called once per frame
    void Update()
    {
        spawnPosition.x = Random.Range(rangeA*10, rangeB*10) / 10;
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnDelay;
            GameObject enemy = Instantiate(skelet, spawnPosition, Quaternion.identity);
            countOfEnemy++;
        }
    }
}
