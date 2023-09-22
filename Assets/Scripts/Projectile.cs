using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Projectile : MonoBehaviour
{
    private Vector3 firingPoint;

    public bool canMakeDamage = true;

    [SerializeField]
    float projectileSpeed;

    [SerializeField]
    float maxProjectileDistance;

    private bool shouldMove = false;
    void Start()
    {

    }


    void FixedUpdate()
    {
        if (shouldMove)
        {
            MoveProjectile();
        }
    }
    public void Move()
    {
        firingPoint = transform.position;
        shouldMove = true;
    }
    void MoveProjectile()
    {

        if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        {
            ProjectilePool.Instance.ReturnToPool(this);
            shouldMove = false;
        }
        else
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        ProjectilePool.Instance.ReturnToPool(this);
    }
    public void OnTriggerEnter(Collider other)
    {
        ProjectilePool.Instance.ReturnToPool(this);
    }
}
