using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {

    public Animator anim;
    public Sprite[] monsters;
    public ParticleSystem[] effects;

    private void Start()
    {

        // Set animation according to random number
        int rand = Random.Range(0, 2);
        
        if (rand == 0)
            anim.SetBool("IsWhite", true);
        
        // number 0 : white, number 1 : black
        anim.gameObject.GetComponent<Image>().sprite = monsters[rand];
        effects[rand].Play();

        anim.SetBool("IsAnim", true);
    }

}
