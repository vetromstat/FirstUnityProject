using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using static UnityEditor.PlayerSettings;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float InstantiationTimer;
    private float CopyInstantiationtimer;
    [SerializeField]
    private GameObject Enemy;
    private float x;
    private float y;
    private float z;
    private Vector3 pos;



    public void Start()
    {
       CopyInstantiationtimer =  InstantiationTimer;
    }
    void Update()
    {
        CreateEnemy();
        
    }

    void CreateEnemy()
    {
        InstantiationTimer -= Time.deltaTime;
        if (InstantiationTimer <= 0)

        { // To do next 
            int choice = UnityEngine.Random.Range(0, 2);

            if (choice == 0)
            {
                x = UnityEngine.Random.Range(-20, -10);
            }
            else
            {
                x = UnityEngine.Random.Range(10, 20);
            
            }
            
            choice = UnityEngine.Random.Range(0, 2);

            if (choice == 0)
            {
                z = UnityEngine.Random.Range(-40,-30);
            }
            else
            {
                z = UnityEngine.Random.Range(30, 40);

            }
            
            y = 1.5f;
            pos = new Vector3(x, y, z);
            Instantiate(Enemy, transform.position + pos  , Quaternion.identity);
            InstantiationTimer = CopyInstantiationtimer;
            
        }
    }
}
