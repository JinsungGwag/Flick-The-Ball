using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerClick : MonoBehaviour {

    private Touch scrTouch;
    private Vector3 touchedPos;

    public bool inTouch;

    // Make fingerclick's singleton 
    private static FingerClick instance;
    public static FingerClick Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(FingerClick)) as FingerClick;
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start() {

        inTouch = false;

    }

    // Transform's position property
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }
    
	// Update is called once per frame
	void Update () {

        // If touch is on
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                scrTouch = Input.GetTouch(i);
                if (scrTouch.phase == TouchPhase.Began)
                {
                    inTouch = true;
                    transform.position = Camera.main.ScreenToWorldPoint(scrTouch.position); 
                    
                    break;
                }
                else
                {
                    inTouch = false;
                }
            }
        }
        else
        {
            inTouch = false;
        }
    }

}
