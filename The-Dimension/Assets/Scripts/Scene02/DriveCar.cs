using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DriveCar : MonoBehaviour
{
    //音效
    private AudioSource sfx;
    //取得Animation
    private Animation anim;
    //玩家攝影機
    public GameObject PlayerCamera;
    //RAW
    public GameObject RAW;
    //開車攝影機
    public GameObject CarCamera;
    //成功UI
    public GameObject SuccessfulUI;
    //未取得鑰匙UI
    public GameObject Needkey02;
    //是否切換過攝影機
    public bool Camerabool;
    //是否通關
    public static bool successful = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();
        sfx = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Escaped()
    {
        yield return new WaitForSeconds(3);
        SuccessfulUI.SetActive(true);
        yield return new WaitForSeconds(10);
        SceneManager.LoadSceneAsync(0);
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player" && Camerabool == false && DoorController.key2 == true)
        {
            sfx.Play();
            successful = true;
            PlayerMove.movebool = false;
            PlayerControl.PlayerActive = false;
            MouseLook.mouselookbool = false;
            Camerabool = true;
            PlayerCamera.SetActive(false);
            RAW.SetActive(false);
            CarCamera.SetActive(true);
            anim.Play();
            StartCoroutine(Escaped());
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && DoorController.key2 == false)
        {
            Needkey02.SetActive(true);
        }
    } 

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && DoorController.key2 == false)
        {
            Needkey02.SetActive(false);
        }
    } 
}
