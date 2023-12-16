using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class BestScore : MonoBehaviour
{
    private TMP_Text TMP;
    void Start()
    {
        TMP = gameObject.GetComponent<TMP_Text>();
        float highScore = PlayerPrefs.GetFloat("highScore");
        TMP.text = "BEST SCORE IS " + highScore.ToString();
    }

}
