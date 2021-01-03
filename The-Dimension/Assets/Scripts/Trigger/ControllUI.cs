using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllUI : MonoBehaviour
{
    //教學UI
    public GameObject UI;
    //音效
    public AudioClip music;
    //音效撥放器
    private AudioSource back;
    //通關or死亡 = false
    public bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        //音效
        back = this.GetComponent<AudioSource>();
        back.clip = music;
        back.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    //進到Trigger
    void OnTriggerEnter(Collider other)
    {
        UI.SetActive(true);
        if(UI.gameObject.tag == "Monster" && played == false)
        {
            //Debug.Log("111111");
            back.Play();
            played = true;
            PlayerControl.turnaround = true;
        }
        if(UI.gameObject.tag == "SlowTime")
        {
            Time.timeScale = 0.3f;
        }
    }

    //離開Trigger
    void OnTriggerExit(Collider other)
    {
        if(UI.gameObject.tag != "Monster")
        {
            UI.SetActive(false);
        }
        if(UI.gameObject.tag == "SlowTime")
        {
            Time.timeScale = 1f;
        }   
    }
}
