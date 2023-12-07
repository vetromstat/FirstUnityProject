using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class Toolbar : MonoBehaviour
{
    int Index;
    Player Pl;

    public RectTransform Outline;
    Color color = Color.white;

    public void Start()
    {
        Pl = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public void Update()
    {
        Index = Pl.WeaponIndex;    
        Outline.position = gameObject.transform.GetChild(Index).transform.position;

        if (Pl.WeaponCooldowns[Pl.WeaponIndex] > 0) color = Color.red;
        else color = Color.black;
        color.a = 0.5f;
        gameObject.GetComponent<Image>().color = color;
       
    }
}
