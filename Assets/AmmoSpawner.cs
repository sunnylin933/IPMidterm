using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform spawnPoint;
    bool spawned = false;
    float spawnTimer = 0f;
    float maxSpawnTime = 7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!spawned)
        {
            if (spawnTimer >= maxSpawnTime)
            {
                GameObject ammobox =  Instantiate(ammoPrefab);
                ammobox.transform.position = spawnPoint.position;
                ammobox.transform.rotation = transform.rotation;
                spawned = true;
                spawnTimer = 0;
            }
            else
            {
                spawnTimer += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            spawned = false;
        }
    }
}
