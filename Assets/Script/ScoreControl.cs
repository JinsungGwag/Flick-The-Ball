using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour {

    public static int Score;

    void Start()
    {
        Score = 0;    
    }

    // Update is called once per frame
    void Update () {

        GetComponent<Text>().text = "SCORE: " + Score;

	}
}
