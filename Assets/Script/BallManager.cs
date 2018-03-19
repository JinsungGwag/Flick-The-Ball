using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public int level;

    public BallMake normalBlack;
    public BallMake normalWhite;

    public float normalTime;
    public float normalLimit;

    public float feverTime;

    private float time;

    private int[][] seed;

    private int zenNum;
    private int ranNum;   

	// Use this for initialization
	void Start () {
        
        level = 0;

        // Save current time
        time = Time.time;

        zenNum = 0;
        ranNum = Random.Range(0, 6);

        seed = new int[6][];
        randomSeed();
        
    }

    // Set random seed
    private void randomSeed()
    {
        seed[0] = new int[] { 0, 1, 2, 3 };
        seed[1] = new int[] { 0, 2, 1, 3 };
        seed[2] = new int[] { 0, 2, 3, 1 };
        seed[3] = new int[] { 2, 3, 0, 1 };
        seed[4] = new int[] { 2, 1, 3, 0 };
        seed[5] = new int[] { 2, 1, 0, 3 };
    }

    // Update is called once per frame
    void Update () {

        // If fevermode, normalTime is so tight
        if(FeverControl.Fever())
        {
            normalTime = feverTime;
        }

        // If stage is 0, normal mode
        if (level == 0)
        {
            if (Time.time > time + normalTime)
            {
         
                // Set seed randomly
                if (zenNum >= 4)
                {
                    zenNum = 0;
                    ranNum = Random.Range(0, 6);
                }
                
                // Summon monsters
                normalBlack.MakeObject(seed[ranNum][zenNum]);
                zenNum++;

                normalWhite.MakeObject(seed[ranNum][zenNum]);
                zenNum++;

                time = Time.time;

                // Decrease summon period
                if (normalTime > normalLimit)
                    normalTime = normalTime * 0.99f;
                else
                    normalTime = normalLimit;
         
            }
        }

	}
    
}
