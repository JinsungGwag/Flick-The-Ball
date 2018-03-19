using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardHit : MonoBehaviour {
    
    public Sprite Idle;
    public Sprite Hit;

    public Sprite Idle_Fever;
    public Sprite Hit_Fever;

    public Image FeverGage;

    public TimerMove timer;
    
    void Start()
    {
        GetComponent<Image>().sprite = Idle;
        StartCoroutine(feverEvent());
    }

    // Change by fever
    IEnumerator feverEvent()
    {

        while (true)
        {
            // Fever mode
            yield return new WaitUntil(FeverControl.Fever);
            GetComponent<Image>().sprite = Idle_Fever;
            
            // Not fever mode
            yield return new WaitWhile(FeverControl.Fever);
            GetComponent<Image>().sprite = Idle;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
        StartCoroutine(HitBoard());

        // Not Fever
        if(!FeverControl.Fever())
        {
            // Hit Right
            if (other.transform.tag == transform.tag)
            {
                FeverGage.fillAmount += 0.02f;
                StartCoroutine(FeverColorChange());
                timer.StartCoroutine(timer.TimerChange("RIGHT"));

                ScoreControl.Score += 20;
            }

            // Hit Wrong
            else
                timer.StartCoroutine(timer.TimerChange("WRONG"));
        }
        // Fever Mode
        else
        {
            ScoreControl.Score += 20;
            timer.StartCoroutine(timer.TimerChange("RIGHT"));
        }
    }

    IEnumerator HitBoard()
    {
        if (!FeverControl.Fever())
            GetComponent<Image>().sprite = Hit;
        else
            GetComponent<Image>().sprite = Hit_Fever;

        yield return new WaitForSeconds(0.2f);

        if (!FeverControl.Fever())
            GetComponent<Image>().sprite = Idle;
        else
            GetComponent<Image>().sprite = Idle_Fever;
    }

    IEnumerator FeverColorChange()
    {
        FeverGage.color = Color.grey;

        yield return new WaitForSeconds(0.2f);

        FeverGage.color = Color.white;
    }
    
}
