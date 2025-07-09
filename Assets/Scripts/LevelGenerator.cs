using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public enum SpeedLevel
    {
        FirstSpeed,
        SecondSpeed,
        ThirdSpeed,
        FourthSpeed,
        FiveSpeed,
        SixthSpeed,
        SevenSpeed,
        EighthSpeed,
        NinthSpeed
    }

    [Header("Referendes")]
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private int startFrom = 5;

    [Header("Level Settings")]
    public int startingChunksAmount = 10;
    public int defaultChunksCount = 2;
    public float chunkLength = 10f;
    public float speedOfScoring = 10f;

    public float moveSpeed = 30f;
    public float upperSpeedMultiply = 10f;

    public bool readyToStart;

    private int defaultStartChunks = 0;

    private Movement player;

    private ParticleSystem timer;

    public Wallet wallet;
    private IDataProvider dataProvider;

    List<GameObject> chunks = new List<GameObject>();

    public void InitWallet(Wallet wallet, IDataProvider dataProvider)
    {
        this.wallet = wallet;
        this.dataProvider = dataProvider;
    }

    private void Start()
    {
        player = FindFirstObjectByType<Movement>();
        readyToStart = false;

        SpawnStartingChunks();
        StartCoroutine(WaitToStart());
    }

    private void Update()
    {
        if (!player.isDead && readyToStart)
            StartGame();
        else if (player.isDead)
            scoreManager.SaveScore();
    }

    private void StartGame()
    {
        MoveChunks();
        ScoreCalculate();
        SpeedCalculate();
    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject chunkToSpawn = ChooseChunkToSpawn();
        GameObject newChunkGO = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, wallet, dataProvider);
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;

        if (defaultStartChunks < defaultChunksCount)
        {
            chunkToSpawn = chunkPrefabs[0];
            defaultStartChunks++;
        }
        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(1, chunkPrefabs.Length)];
        }

        return chunkToSpawn;

    }

    private float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }
        return spawnPositionZ;
    }

    private void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }

    private void ScoreCalculate()
    {
        if (Time.timeScale == 0) return;

        float score = moveSpeed * speedOfScoring * Time.deltaTime;
        scoreManager.AddScore(score);
    }

    private SpeedLevel currentSpeedLevel = SpeedLevel.FirstSpeed;

    private void SpeedCalculate()
    {
        SpeedLevel newSpeedLevel = GetSpeedLevel(scoreManager.score);

        if (newSpeedLevel != currentSpeedLevel)
        {
            int levelDiff = (int)newSpeedLevel - (int)currentSpeedLevel;
            moveSpeed += levelDiff * upperSpeedMultiply;
            currentSpeedLevel = newSpeedLevel;

            Debug.Log("Уровень увеличен! След уровень: " + newSpeedLevel + ", Скорость: " + moveSpeed);
        }
    }

    private SpeedLevel GetSpeedLevel(float score)
    {
        if (score > 10_000_000) return SpeedLevel.NinthSpeed;
        if (score > 1_000_000) return SpeedLevel.EighthSpeed;
        if (score > 500_000) return SpeedLevel.SevenSpeed;
        if (score > 250_000) return SpeedLevel.SixthSpeed;
        if (score > 100_000) return SpeedLevel.FiveSpeed;
        if (score > 50_000) return SpeedLevel.FourthSpeed;
        if (score > 30_000) return SpeedLevel.ThirdSpeed;
        if (score > 10_000) return SpeedLevel.SecondSpeed;
        if (score > 5_000) return SpeedLevel.FirstSpeed;

        return SpeedLevel.FirstSpeed;
    }

    private IEnumerator WaitToStart()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = startFrom; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.text = "GO!";
        readyToStart = true;

        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.gameObject.SetActive(false);
    }
}
