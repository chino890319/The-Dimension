using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //殺手視角 = false
    public static bool KillerCamera = false;
    //玩家視角
    public GameObject Player;
    //殺手視角
    public GameObject Killer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(KillerCamera == false)
        {
            Player.SetActive(true);
            Killer.SetActive(false);
        }
        else if (KillerCamera == true)
        {
            Player.SetActive(false);
            Killer.SetActive(true);
        }
    }
}
