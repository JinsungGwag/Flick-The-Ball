using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverControl : MonoBehaviour {

    static Image feverGage;
    public Image guideImage;

    public Image Hide;
    public Image Line;
    public Camera backCamera;

    // Gage sprites
    public Sprite gageIdle;
    public Sprite gageFever;

    // Timer line sprites
    public Sprite lineIdle;
    public Sprite lineFever;

    // Make static image
    void Awake()
    {
        feverGage = GetComponent<Image>();
        StartCoroutine(FeverChange());
    }

    // Checking fever static method
    public static bool Fever()
    {
        // Check fevergage is full
        bool feverCharge = (feverGage.fillAmount >= 0.763f);

        return feverCharge;
    }

    IEnumerator FeverChange()
    {
        while(true)
        {
            // Wait fever mode
            yield return new WaitUntil(Fever);

            guideImage.sprite = gageFever;
            Line.sprite = lineFever;
            feverGage.color = new Color(255f, 255f, 255f, 255f);
            backCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);
            Hide.color = new Color(0f, 0f, 0f, 255f);

            // If fever, play fever mode for 10 seconds 
            yield return new WaitForSeconds(10f);

            // Close fever mode
            feverGage.fillAmount = 0.237f;
            guideImage.sprite = gageIdle;
            Line.sprite = lineIdle;
            feverGage.color = new Color(255f, 255f, 255f, 255f);
            backCamera.backgroundColor = new Color(255f, 255f, 255f, 0f);
            Hide.color = new Color(255f, 255f, 255f, 255f);
        }
    }

}
