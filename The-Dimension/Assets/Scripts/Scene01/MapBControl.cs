using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBControl : MonoBehaviour
{
    public GameObject[] mapBobjappear;
    public GameObject[] mapBobjappearkey;
    public GameObject[] mapBobjdisappear;
    public GameObject[] mapBobjappearPuzzle;
    public GameObject[] mapBobjdisappearDoor1;
    void Awake()
    {
        
    }
    void Start()
    {
        mapBobjappear = GameObject.FindGameObjectsWithTag("mapBappear");
        mapBobjappearkey = GameObject.FindGameObjectsWithTag("mapBappearkey");
        mapBobjdisappear = GameObject.FindGameObjectsWithTag("mapBdisappear");
        mapBobjappearPuzzle = GameObject.FindGameObjectsWithTag("mapBPuzzle");
        mapBobjdisappearDoor1 = GameObject.FindGameObjectsWithTag("DoorLock");
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerControl.Shinetimer == 2)
        {
            for(int i = 0; i < mapBobjdisappear.Length; i++)
            {
                mapBobjdisappear[i].SetActive(false);
            }
            for(int i = 0; i < mapBobjappear.Length; i++)
            {
                mapBobjappear[i].SetActive(true);
            }
            for(int i = 0; i < mapBobjappearPuzzle.Length; i++)
            {
                mapBobjappearPuzzle[i].SetActive(true);
            }
            for(int i = 0; i < mapBobjappearkey.Length; i++)
            {
                mapBobjappearkey[i].SetActive(true);
            }
            for(int i = 0; i < mapBobjdisappearDoor1.Length; i++)
            {
                mapBobjdisappearDoor1[i].SetActive(false);
            }         
        }
        else if(PlayerControl.magic == false && PlayerControl.shine == false)
        {
            for(int i = 0; i < mapBobjdisappear.Length; i++)
            {
                mapBobjdisappear[i].SetActive(true);
            }
            for(int i = 0; i < mapBobjappear.Length; i++)
            {
                mapBobjappear[i].SetActive(false);
            }
            for(int i = 0; i < mapBobjappearPuzzle.Length; i++)
            {
                mapBobjappearPuzzle[i].SetActive(false);
            }
            for(int i = 0; i < mapBobjappearkey.Length; i++)
            {
                mapBobjappearkey[i].SetActive(false);
            }
            for(int i = 0; i < mapBobjdisappearDoor1.Length; i++)
            {
                mapBobjdisappearDoor1[i].SetActive(true);
            }     
        }
    }
}
