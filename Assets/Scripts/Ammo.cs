using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GunSystem.instance.bulletsLeft = GunSystem.instance.magazineSize;
            Destroy(gameObject);
        }

        if(other.CompareTag("Ammo"))
        {
            Destroy(other.gameObject);
        }
    }
}
