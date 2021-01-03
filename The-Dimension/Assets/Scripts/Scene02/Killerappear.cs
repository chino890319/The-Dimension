using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killerappear : MonoBehaviour
{
    //殺手
    public GameObject Killer;
    //殺手Camera
    public GameObject KillerCamera;
    //玩家Camera
    public GameObject PlayerCamera;
    //是否觸發玩家殺手視角切換
    public static bool Camerabool = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CameraCD()
    {
        yield return new WaitForSeconds(2.5f);
        PlayerCamera.SetActive(true);
        KillerCamera.SetActive(false);
        PlayerMove.movebool = true;
        PlayerControl.PlayerActive = true;
        MouseLook.mouselookbool = true;
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player" && Camerabool == false)
        {
            PlayerMove.movebool = false;
            PlayerControl.PlayerActive = false;
            MouseLook.mouselookbool = false;
            Camerabool = true;
            Killer.SetActive(true);
            PlayerCamera.SetActive(false);
            KillerCamera.SetActive(true);
            StartCoroutine(CameraCD());
        }
    }
}
