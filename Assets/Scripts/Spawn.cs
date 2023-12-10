using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using static UnityEditor.PlayerSettings;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject BigEnemy;
    [SerializeField]
    private GameObject SmallEnemy;
    [SerializeField]
    private float BigInstantiationTimer;
    private float BigCopyInstantiationtimer;
    [SerializeField]
    private float SmallInstantiationTimer;
    private float SmallCopyInstantiationtimer;

    [SerializeField]
    private GameObject BonusCrate;
    [SerializeField]
    private float CrateInstantiationTimer;
    private float CopyCrateInstantiationTimer;



    public void Start()
    {
       BigCopyInstantiationtimer =  BigInstantiationTimer;
       SmallCopyInstantiationtimer = SmallInstantiationTimer;
       CopyCrateInstantiationTimer = CrateInstantiationTimer;
    }
    void Update()
    {
        CreateBigEnemy();
        CreateCrate();
        CreateSmallEnemy();
    }
    void CreateSmallEnemy()
    {
        float x;
        float y;
        float z;

        SmallInstantiationTimer -= Time.deltaTime;
        if (SmallInstantiationTimer <= 0)

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
                z = UnityEngine.Random.Range(-40, -30);
            }
            else
            {
                z = UnityEngine.Random.Range(30, 40);

            }

            y = 1.5f;
            Vector3 pos = new Vector3(x, y, z);
            Instantiate(SmallEnemy, transform.position + pos, Quaternion.identity);
            SmallInstantiationTimer = SmallCopyInstantiationtimer;
        }
    }
    void CreateBigEnemy()
    {
            float x;
            float y;
            float z;

        BigInstantiationTimer -= Time.deltaTime;
        if (BigInstantiationTimer <= 0)

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
            Vector3 pos = new Vector3(x, y, z);
            Instantiate(BigEnemy, transform.position + pos  , Quaternion.identity);
            BigInstantiationTimer = BigCopyInstantiationtimer;  
        }
    }
    void CreateCrate()
    {
        CrateInstantiationTimer -= Time.deltaTime;
        if (CrateInstantiationTimer <= 0)
        {
            float x = UnityEngine.Random.Range(-15, 15);
            float y = 20;
            float z = UnityEngine.Random.Range(-15, 15);
            Vector3 pos = new Vector3(x, y, z);

            Instantiate(BonusCrate, transform.position + pos, Quaternion.identity);
            CrateInstantiationTimer = CopyCrateInstantiationTimer;
            Debug.Log(CrateInstantiationTimer);
            Debug.Log(CopyCrateInstantiationTimer);

        }
    }
}
