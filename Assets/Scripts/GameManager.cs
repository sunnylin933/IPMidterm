using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform player;

    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject[] enemyTypes;

    float spawnTimer = 0;
    float spawnTimerTotal = 5f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTimer > spawnTimerTotal)
        {
            Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
            spawnTimer = 0;
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
