using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMove : MonoBehaviour {

    // GameObjects indicating start & end 
    public GameObject Right;
    public GameObject Left;

    // Timer start X position
    private float rLimit;
    
    // Timer end X postion
    private float lLimit;

    // Timer's total length
    private float lnLength;

    // Timer's decreasing length
    private float deLength;

    // Timer's moving length by hitting board
    private float unLength;

    // Timer changing faces
    public Sprite TiIdle;
    public Sprite TiAngry;
    public Sprite TiSmile;

    // Timer's fever faces
    public Sprite TiIdle_Fever;
    public Sprite TiSmile_Fever;

    // Use this for initialization
    void Start () {

        // Save start & end position
        rLimit = Right.transform.position.x;
        lLimit = Left.transform.position.x;

        // Set timer's X position to start
        transform.position = Right.transform.position;

        // Save Timer's total length
        lnLength = rLimit - lLimit;

        // Timer Decrease Speed
        unLength = lnLength / 10000;
        deLength = unLength;

        GetComponent<SpriteRenderer>().sprite = TiIdle;

        StartCoroutine(feverEvent());
    }

    // Change by fever
    IEnumerator feverEvent()
    {

        while (true)
        {
            // Fever mode
            yield return new WaitUntil(FeverControl.Fever);
            GetComponent<SpriteRenderer>().sprite = TiIdle_Fever;

            // Not fever mode
            yield return new WaitWhile(FeverControl.Fever);
            GetComponent<SpriteRenderer>().sprite = TiIdle;
        }

    }
    
    // Timer's function according to hitting board
    public IEnumerator TimerChange(string state)
    {
        // Fever check
        if (!FeverControl.Fever())
        {
            // If hit wrong board
            if (state == "WRONG")
            {
                GetComponent<SpriteRenderer>().sprite = TiAngry;
                transform.Translate(-unLength * 100, 0, 0);
            }
            // If hit right board
            else
            {
                GetComponent<SpriteRenderer>().sprite = TiSmile;
                transform.Translate(unLength * 100, 0, 0);
            }

            yield return new WaitForSeconds(0.6f);

            GetComponent<SpriteRenderer>().sprite = TiIdle;
        }
        // If fever mode is on
        else
        {
            GetComponent<SpriteRenderer>().sprite = TiSmile_Fever;
            transform.Translate(unLength * 100, 0, 0);

            yield return new WaitForSeconds(0.6f);

            // Still fever mode is on
            if(FeverControl.Fever())
                GetComponent<SpriteRenderer>().sprite = TiIdle_Fever;
        }
    }

    // Update is called once per frame
    void Update () {

        // Timer's decreasing rate is growing
        transform.Translate(-deLength, 0, 0);
        deLength *= 1.0005f;

        // Timer can't go over start X position
        if (transform.position.x > rLimit)
            transform.position = Right.transform.position;

	}
}
