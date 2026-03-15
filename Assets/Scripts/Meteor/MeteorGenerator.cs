using UnityEngine;

public class MeteorGenerator : MonoBehaviour
{
    bool isMeteorDodging = false;

    public GameObject[] meteorPrefab;
    public Sprite meteorUISpritePrefab;
    public Transform[] meteorSpawnPoints;

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
        if (isMeteorDodging) 
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
        Instantiate(meteorPrefab[Random.Range(0, meteorPrefab.Length)], meteorSpawnPoints[Random.Range(0, meteorPrefab.Length)]);
    }

    private float RandomSpawnTime()
    {
        float randtime = Random.Range(minSpawnTime, maxSpawnTime);
        return randtime;
    }

}
