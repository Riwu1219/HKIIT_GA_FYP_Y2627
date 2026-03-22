using UnityEngine;
using System.Collections.Generic;

public class MeteorGenerator : MonoBehaviour
{
    public bool isMeteorGenerate = false;

    public GameObject[] meteorPrefab;
    public Transform[] spawnArea;

    [Header("Spawning")]
    private float timer = 0f;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;
    [SerializeField]
    private float randSpawnTime;

    [Header("MeteorSetting")]
    public Vector2 speedRange;
    public Vector2 sizeRange;

    private void Start()
    {
        SpawnMeteor();
        randSpawnTime = RandomSpawnTime();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isMeteorGenerate = !isMeteorGenerate;
        }

        if (isMeteorGenerate) 
        { 
            timer += Time.deltaTime;
            if (timer >= randSpawnTime)
            {
                randSpawnTime = RandomSpawnTime();
                SpawnMeteor();
                timer = 0f;
            }
        }
    }

    public void SpawnMeteor()
    {
        Vector3 p1 = spawnArea[0].position;
        Vector3 p2 = spawnArea[1].position;

        Vector3 spawnPos_Rand = new Vector3(Random.Range(p1.x, p2.x), Random.Range(p1.y, p2.y), Random.Range(p1.z, p2.z));

        GameObject temp = Instantiate(meteorPrefab[Random.Range(0, meteorPrefab.Length)], spawnPos_Rand, Quaternion.identity);
        temp.GetComponent<MeteorScript>().Init(Random.Range(sizeRange.x, sizeRange.y), Random.Range(speedRange.x, speedRange.y));

    }

    private float RandomSpawnTime()
    {
        float randtime = Random.Range(minSpawnTime, maxSpawnTime);
        return randtime;
    }

}
