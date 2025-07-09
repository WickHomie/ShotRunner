using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject[] truckPrefabs;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject[] enemyPrebab;

    [Header("Coin Settings")]
    [SerializeField] int maxCoinsToSpawn = 5;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeperationLength = 2f;
    [SerializeField] float enemySpawnChance = 0.5f;

    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    List<int> availableLanes = new List<int> { 0, 1, 2 };

    LevelGenerator levelGenerator;
    Wallet wallet;
    IDataProvider dataProvider;

    void Start()
    {
        SpawnTrucks();
        SpawnCoin();
        SpawnEnemy();
    }

    public void Init(LevelGenerator levelGenerator, Wallet wallet, IDataProvider dataProvider)
    {
        this.levelGenerator = levelGenerator;
        this.wallet = wallet;
        this.dataProvider = dataProvider;
    }

    void SpawnTrucks()
    {
        int truckToSpawnSide = Random.Range(0, lanes.Length);

        for (int i = 0; i < truckToSpawnSide; i++)
        {
            if (availableLanes.Count <= 0) break;

            int selectLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectLane], transform.position.y, transform.position.z);
            GameObject truckToSpawn = ChooseTruckToSpawn();
            Instantiate(truckToSpawn, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    void SpawnEnemy()
    {
        int enemyToSpawnSide = Random.Range(0, lanes.Length);

        for (int i = 0; i < enemyToSpawnSide; i++)
        {
            if (Random.value > enemySpawnChance || availableLanes.Count <= 0) return;

            int selectLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectLane], transform.position.y, transform.position.z);
            GameObject enemyToSpawn = ChooseEnemyToSpawn();
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity, this.transform);
        }

    }

    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;

        int selectedLane = SelectLane();
        int coinToSpawn = Random.Range(1, maxCoinsToSpawn);

        float topOfChunkPosZ = transform.position.z + (coinSeperationLength * 2f);

        for (int i = 0; i < coinToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkPosZ - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(wallet, dataProvider);
        }
    }

    private GameObject ChooseTruckToSpawn()
    {
        GameObject truckToSpawn;

        truckToSpawn = truckPrefabs[Random.Range(0, truckPrefabs.Length)];

        return truckToSpawn;
    }

    private GameObject ChooseEnemyToSpawn()
    {
        GameObject enemyToSpawn;

        enemyToSpawn = enemyPrebab[Random.Range(0, enemyPrebab.Length)];

        return enemyToSpawn;
    }


    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}
