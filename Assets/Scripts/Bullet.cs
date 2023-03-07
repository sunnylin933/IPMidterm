using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] LayerMask enemyLayer;

    [Range(0f, 1f)]
    [SerializeField] float bounciness;
    [SerializeField] bool useGravity;


    [SerializeField] int explosionDamage;
    [SerializeField] int explosionRange;

    [SerializeField] int maxCollisions;
    [SerializeField] float maxLifetime;
    [SerializeField] bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial pMat;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (collisions > maxCollisions)
        {
            Explode();
        }

        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0)
        {
            Explode();
        }

    }

    private void Explode()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, enemyLayer);
        for(int i = 0; i < enemies.Length; i++)
        {
            //take damage
        }

        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void Setup()
    {
        pMat = new PhysicMaterial();
        pMat.bounciness = bounciness;
        pMat.frictionCombine = PhysicMaterialCombine.Minimum;
        pMat.bounceCombine = PhysicMaterialCombine.Maximum;
        GetComponent<SphereCollider>().material = pMat;

        rb.useGravity = useGravity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        if(explodeOnTouch)
        {
            Explode();
        }
    }
}
