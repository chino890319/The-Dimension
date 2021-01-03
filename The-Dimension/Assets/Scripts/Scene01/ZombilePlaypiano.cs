using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombilePlaypiano : MonoBehaviour
{
    //角色音效                  
    public AudioClip[] music;
    //角色音效撥放器
    private AudioSource voice;
    // Start is called before the first frame update
    void Start()
    {
        voice = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(!voice.isPlaying)
            {
                voice.Play();
            }
        }
    }

        void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(voice.isPlaying)
            {
                voice.Stop();
            }
        }
    }
}
