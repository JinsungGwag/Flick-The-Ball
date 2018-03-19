using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMove : MonoBehaviour {

    public float speed;
         
    private bool isPush;

    private float wid;
    private float range;

    private Animator Amt;

    // Normal and fever animator controller
    public RuntimeAnimatorController norAmt;
    public RuntimeAnimatorController feverAmt;

    // Previous image, idle image, fever idle image
    public Sprite preIdle;
    public Sprite Idle;
    public Sprite feverIdle;

    // Monster's idle image
    private Sprite realIdle;
    
    void Awake()
    {
        // Apply animator
        Amt = GetComponentInChildren<Animator>();
    }

    // Call at active 
    void OnEnable()
    {
        // Decide animator
        if (FeverControl.Fever())
            GetComponent<Animator>().runtimeAnimatorController = feverAmt;
        else
            GetComponent<Animator>().runtimeAnimatorController = norAmt;

        // Animator disabled
        Amt.enabled = false;

        // Set idle sprite depending on fever
        if (FeverControl.Fever())
            realIdle = feverIdle;
        else
            realIdle = preIdle;
        
        GetComponent<SpriteRenderer>().sprite = realIdle;

        // Set alpha low
        GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.2f);

        // Set first state
        speed = 0;

        // Start checking fever
        StartCoroutine(feverEvent());

        // Prepare summon
        StartCoroutine(prepareSummon());
        
    }

    // Call once
    void Start ()
    {
        
        Vector3 scrToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        wid = scrToWorld.x;
        range = wid / 3;
      
    }

    // Set rotation and speed depending on distance
    private void speedUp(float disX, float disY, float dis)
    {
        float disR = (Mathf.Atan2(disY, disX) / Mathf.PI * 180) - 90 - transform.rotation.eulerAngles.z;

        transform.Rotate(new Vector3(0, 0, disR + 180));

        if (wid / 20 / dis > wid / 4)
            speed = wid / 6;
        else
            speed = wid / 30 / dis;
    }

    IEnumerator feverEvent()
    {

        while(true)
        {
            // Fever mode
            yield return new WaitUntil(FeverControl.Fever);
            GetComponent<Animator>().runtimeAnimatorController = feverAmt;
            realIdle = feverIdle;
            
            GetComponent<SpriteRenderer>().sprite = realIdle;

            // Not fever mode
            yield return new WaitWhile(FeverControl.Fever);
            GetComponent<Animator>().runtimeAnimatorController = norAmt;
            realIdle = Idle;

            GetComponent<SpriteRenderer>().sprite = realIdle;
        }

    }

    // Hit animation
    IEnumerator hitAnim()
    {
        Amt.enabled = true;

        yield return new WaitForSeconds(0.1f);

        Amt.enabled = false;
        GetComponent<SpriteRenderer>().sprite = realIdle;
    }

    // Preparing summon function
    IEnumerator prepareSummon()
    {
        // Set ball dimly
        GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.2f);

        yield return new WaitForSeconds(1.5f);

        // Save current time
        float nowTime = Time.time;

        // Do alpha animation
        Color color;
        do
        {
            // Add alpha accoring to time
            color = GetComponent<SpriteRenderer>().color;
            color.a += Time.time - nowTime;
            nowTime = Time.time;
            GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }
        while (color.a <= 1f);

        // If not fever mode, change idle image
        if(!FeverControl.Fever())
        {
            realIdle = Idle;
            GetComponent<SpriteRenderer>().sprite = realIdle;
        }

        // Summon start
        StartCoroutine(startSummon());
    }

    // Summon and give moving ability
    IEnumerator startSummon()
    {
        while(true)
        {
            // Monster reaction
            float disX = FingerClick.Instance.Position.x - transform.position.x;
            float disY = FingerClick.Instance.Position.y - transform.position.y;
            float dis = Mathf.Sqrt(disX * disX + disY * disY);

            // Recognize touch
            if (dis < range && Input.touchCount > 0 && FingerClick.Instance.inTouch)
            {
                StartCoroutine(hitAnim());
                speedUp(disX, disY, dis);
            }

            // Monster move
            transform.Translate(new Vector3(0, speed, 0));
            if (speed > 0)
                speed *= 0.9f;
            else
                speed = 0;

            yield return null;
        }
    }

}