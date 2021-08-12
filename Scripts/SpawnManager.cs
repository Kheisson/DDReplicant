using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Spawner { get; private set; }
    public float Speed { get => speed; set => speed = value; }
    public GameMode difficulty;

    [SerializeField, Range(0, 15)] private float speed;
    [SerializeField] private GameObject[] _arrowPrefabs = new GameObject[4];
    [SerializeField] private byte _spawnAmount = 4;
    [SerializeField] private float _spawnRate = 2f;

    private List<GameObject> _arrowQueue = new List<GameObject>();
    private Transform[] _spawnLocations = new Transform[4];
    private bool gameStarted;


    //Singlton instansiation
    private void Awake()
    {
        if(Spawner != null)
            Destroy(gameObject);
        else
            Spawner = this;
    }

    private void Start()
    {
        GetDifficulty();
        _spawnLocations = GetComponentsInChildren<Transform>(); //Gather spawn locations
        PrepareArrows();
        StartCoroutine("GameInProgress");
    }

    //Enables the arrow if not active in scene
    IEnumerator GameInProgress()
    {
        if(!gameStarted)
        {
            yield return new WaitForSecondsRealtime(3f);
            gameStarted = true;
        }
        while (true)
        {
            switch (difficulty)
            {
                case GameMode.EASY:
                    GetArrowToSpawn()?.SetActive(true);
                    break;
                case GameMode.NORMAL:
                    GetArrowToSpawn()?.SetActive(true);
                    if (Random.Range(0f, 1f) >= 0.5f)
                    {
                        GetArrowToSpawn()?.SetActive(true);
                        yield return new WaitForSeconds(0.25f);
                        GetArrowToSpawn()?.SetActive(true);
                    }
                    break;
                default:
                    GetArrowToSpawn()?.SetActive(true);
                    if (Random.Range(0f, 0.5f) >= 0.3f)
                    {
                        GetArrowToSpawn()?.SetActive(true);
                        yield return new WaitForSeconds(0.15f);
                        GetArrowToSpawn()?.SetActive(true);
                    }
                    break;   
            }
            yield return new WaitForSecondsRealtime(_spawnRate);
        }
    }

    //Sets spawn location based on type of arrow
    private Transform GetSpawnPosition(GameObject arrow)
    {
        Arrow switcher = arrow.GetComponent<Arrow>();
        switch (switcher.type)
        {
            case Arrow.ArrowType.DOWN:
                return _spawnLocations[1];
            case Arrow.ArrowType.RIGHT:
                return _spawnLocations[2];
            case Arrow.ArrowType.UP:
                return _spawnLocations[3];
            default:
                return _spawnLocations[4];
        }
    }

    //Instansiates arrows based on _spawnAmount at the begining of the game
    private void PrepareArrows()
    {
        GameObject objectToSpawn;
        GameObject spawned;
        Transform spawnPosition;
        for (int i = 0; i < _spawnAmount; i++)
        {
            objectToSpawn = _arrowPrefabs[Random.Range(0, _arrowPrefabs.Length)];
            spawnPosition = GetSpawnPosition(objectToSpawn);
            spawned = Instantiate(objectToSpawn, spawnPosition.position,
                objectToSpawn.transform.rotation);
            spawned.GetComponent<Arrow>().StartingPos = spawnPosition.position;
            spawned.SetActive(false);
            _arrowQueue.Add(spawned);
        }
    }

    //Sets speed and spawnrate based on difficulty
    private void GetDifficulty()
    {
        difficulty = GameManager.Instance.GetGameMode();
        if (difficulty == GameMode.EASY)
        {
            _spawnRate = 1f;
            speed = 8f;
        }
        else if (difficulty == GameMode.NORMAL)
        {
            _spawnRate = 0.75f;
            speed = 10f;
        }
        else
        {
            _spawnRate = 0.5f;
            speed = 12f;
        }
    }

    //Pools an arrow from the list
    private GameObject GetArrowToSpawn()
    {
        int randomIndex = Random.Range(0, _arrowQueue.Count);
        GameObject arrow = _arrowQueue[randomIndex];
        if (!arrow.activeInHierarchy)
        {
            return arrow;
        }
        else
        {
            return null;
        }
    }

}