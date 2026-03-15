using UnityEngine;
using System.Collections.Generic;

public class MeteorGenerator : MonoBehaviour
{
    public bool isMeteorGenerate = false;

    public GameObject[] meteorPrefab;
    public Sprite meteorUISpritePrefab;
    public Transform[] meteorSpawnPoints;

    public List<GameObject> curExistMeteor;

    private float timer = 0f;
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 10f;
    public float randSpawnTime;

    private void Start()
    {
        randSpawnTime = RandomSpawnTime();
    }

    void Update()
    {
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
        GameObject temp = Instantiate(meteorPrefab[Random.Range(0, meteorPrefab.Length)], meteorSpawnPoints[Random.Range(0, meteorPrefab.Length)]);
        curExistMeteor.Add(temp);
    }

    private float RandomSpawnTime()
    {
        float randtime = Random.Range(minSpawnTime, maxSpawnTime);
        return randtime;
    }

}
