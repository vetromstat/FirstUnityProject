using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    Transform firingPoint;

    [SerializeField]
    float firingSpeed;

    public static PlayerGun Instance;
    // Start is called before the first frame update

    private float lastTimeShot = 0;
    void Awake()
    {
        Instance  = GetComponent<PlayerGun>();
    }

    public void Shoot()
    {
        if (lastTimeShot + firingSpeed <= Time.time) 
        {
            Projectile _projectile = ProjectilePool.Instance.Instantiate(firingPoint.position, firingPoint.rotation);
            _projectile.Move();
            lastTimeShot = Time.time;
        }
    }
}
