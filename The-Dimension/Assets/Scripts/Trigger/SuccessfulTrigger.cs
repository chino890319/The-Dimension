using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessfulTrigger : MonoBehaviour
{
    //死亡黑畫面
    public GameObject UI;
    //死亡字體UI
    public GameObject UI2;
    //返回menu字體UI
    public GameObject UI3;
    //準心、血條、魔力
    public GameObject RAWHPMP;
    //音效
    public AudioClip music;
    //音效撥放器
    private AudioSource back;
    //通關 = false (防止二次進入)
    public bool played = false;
    //通關 = false
    public bool successsful = false;
    // Start is called before the first frame update
    void Start()
    {
        //播放音效
        back = this.GetComponent<AudioSource>();
        back.clip = music;
        back.loop = false;
    }

    // Update is called once per frame
    void Update()
    {   
        //通關
        if(successsful == true)
        {
            if(Input.GetKey("escape"))
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }

    //進到通關Trigger
    void OnTriggerEnter(Collider other)
    {     
        if(other.gameObject.tag == "Player" && played == false)
        {
            UI.SetActive(true);
            UI2.SetActive(true);
            UI3.SetActive(true);
            RAWHPMP.SetActive(false);
            Debug.Log("111111");
            //HP.currentHP = HP.currentHP - 100;
            back.Play();
            played = true;
            PlayerMove.movebool = false;
            PlayerControl.PlayerActive = false;
            successsful = true;
        }
    }
    
    //離開通關Trigger
    void OnTriggerExit(Collider other)
    {
        UI.SetActive(false);
        UI2.SetActive(false);
        UI3.SetActive(false);
    }
}
