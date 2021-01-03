using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSofa : MonoBehaviour
{
    //取得Animation
    private Animation anim;
    public AnimationClip animclip;
    public bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SofaMove()
    {
        //播放Animation
        if(Input.GetKeyDown(KeyCode.E) && PuzzleController.puzzle03used == true && used == false)
        {
            used = true;
            RayControl.raylength = 2f;
            anim.Play();
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.tag == "Player" && PuzzleController.puzzle03used == true)
        RayControl.raylength = 2f;
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        RayControl.raylength = 1.5f;
    }
}
