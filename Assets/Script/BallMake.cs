using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMake : MonoBehaviour {

    public GameObject Board;

    public List<GameObject> Objs;
    
    private float wid;
    private float range;
    
    private void Start()
    {

        Objs = new List<GameObject>();

        LoadObject();

        Vector3 scrToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        wid = scrToWorld.x;
        range = wid / 4;       
    }

    private void LoadObject()
    {
        foreach(Transform child in transform)
        {
            if (child != null)
                Objs.Add(child.gameObject);
        }
    }
    
    private GameObject GetPooledObject()
    {
        for (int i = 0; i < Objs.Count; i++)
        {
            if (!Objs[i].activeInHierarchy)
            {
                return Objs[i];
            }
        }
        return null;
    }

    public void MakeObject(int seedNum)
    {
        float startX = 0;
        float startY = 0;

        // Appear Location
        switch (seedNum)
        {
            // 1 quadrant
            case 0:
                startX = Random.Range(-range, 0);
                startY = Random.Range(Board.GetComponent<Transform>().position.y, range + Board.GetComponent<Transform>().position.y);
                break;

            // 2 quadrant
            case 1:
                startX = Random.Range(0, range);
                startY = Random.Range(Board.GetComponent<Transform>().position.y, range + Board.GetComponent<Transform>().position.y);
                break;

            // 3 quadrant
            case 2:
                startX = Random.Range(0, range);
                startY = Random.Range(-range + Board.GetComponent<Transform>().position.y, Board.GetComponent<Transform>().position.y);
                break;

            // 4 quadrant
            case 3:
                startX = Random.Range(-range, 0);
                startY = Random.Range(-range + Board.GetComponent<Transform>().position.y, Board.GetComponent<Transform>().position.y);
                break;

            default:
                break;
        }

        GameObject monster = GetPooledObject();
        monster.transform.position = new Vector3(startX, startY, transform.position.z);
        monster.SetActive(true);
    }

}
