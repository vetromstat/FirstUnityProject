using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{

    private TMP_Text TMP;
    private GameObject Pl;
    void Start()
    {
        Pl = GameObject.FindWithTag("Player");
        TMP = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        TMP.text = Pl.GetComponent<Player>().Points.ToString() + " POINTS";
    }
}
