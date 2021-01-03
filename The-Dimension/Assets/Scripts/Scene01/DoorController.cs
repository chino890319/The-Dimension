using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    //取得Animation
    private Animation anim;
    //上鎖UI
    public GameObject LockedUI;
    //手電筒UI
    public GameObject FlashLight;
    //鑰匙UI
    public GameObject KeyUI;
    //是否觸發門上鎖UI
    public bool Lockedused = false;
    //CD
    public bool CD = false;
    //兩扇門整合是否開過
    public bool door = false;
    //門01是否開過
    public bool door01 = false;
    //門02是否開過
    public bool door02 = false;
    //zb01是否已觸發
    public static bool zb01 = false;
    //房子特殊鑰匙是否已拾取
    public static bool spicalkey = false;
    //房子鑰匙是否已拾取
    public static bool key = false;
    //迷宮鑰匙是否已拾取
    public static bool key2 = false;
    //片段Animation
    public AnimationClip[] animclip;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();
    }
    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.name == "Wardrobe_Door2")
        {
            if(DoorOpenTrigger.zb01 == true && zb01 == false && door02 == false)
            {
                door02 = true;
                anim.clip = animclip[1];
                anim.Play();
                StartCoroutine(timeCD());
            }
        }
        if(this.gameObject.name == "Wardrobe_Door1")
        {
            if(DoorOpenTrigger.zb01 == true && zb01 == false && door01 == false)
            {
                door01 = true;
                anim.clip = animclip[1];
                anim.Play();
                StartCoroutine(timeCD());
            }
        }
    }

    IEnumerator LockedUICD()
    {
        yield return new WaitForSeconds(2.5f);
        LockedUI.SetActive(false);
        Lockedused = false;
    }

    void PressEE()
    {
        //播放Animation
        if(Input.GetKeyDown(KeyCode.E) && anim.clip == animclip[0] && CD == false)
        {
            CD = true;
            anim.clip = animclip[1];
            anim.Play();
            StartCoroutine(timeCD());
        }
        else if(Input.GetKeyDown(KeyCode.E) && anim.clip == animclip[1] && CD == false)
        {
            CD = true;
            anim.clip = animclip[0];
            anim.Play();
            StartCoroutine(timeCD());
        }
    }

    //鎖住的門
    void Lock()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(Lockedused == false && DoorOpenTrigger.textuiappear == false && ExploreUI.Exploreused == false && Textdisappear.flashkey == false)
            {
                Lockedused = true;
                LockedUI.SetActive(true);
                StartCoroutine(LockedUICD());
            }
        }
    }

    void UnLocker()
    {
        //播放Animation
        if(Input.GetKeyDown(KeyCode.E) && anim.clip == animclip[0] && CD == false)
        {
            CD = true;
            anim.clip = animclip[1];
            anim.Play();
            StartCoroutine(timeCD());
        }
        else if(Input.GetKeyDown(KeyCode.E) && anim.clip == animclip[1] && CD == false)
        {
            CD = true;
            anim.clip = animclip[0];
            anim.Play();
            StartCoroutine(timeCD());
        }
    }

    void flashlight()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Destroy(this.gameObject);
            FlashLight.SetActive(true);
            PlayerControl.flashlight = true;
        }
    }

    void mapBappearkey()
    {
        if(this.gameObject.name == "box_top" && Input.GetKeyDown(KeyCode.E))
        {
            anim.Play();
        }
        else if(this.gameObject.name == "key" && Input.GetKeyDown(KeyCode.E))
        {
            KeyUI.SetActive(true);
            Destroy(this.gameObject);
            key2 = true;
        }
    }

    void Housekey()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            key = true;
            KeyUI.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    void Spicalkey()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            spicalkey = true;
            KeyUI.SetActive(true);
            Destroy(this.gameObject);         
        }
    }

    void SupriseDoor()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(spicalkey == true)
            {
                //播放Animation
                if(Input.GetKeyDown(KeyCode.E) && anim.clip == animclip[0] && CD == false)
                {
                    CD = true;
                    anim.clip = animclip[1];
                    anim.Play();
                    StartCoroutine(timeCD());
                }
                else if(Input.GetKeyDown(KeyCode.E) && anim.clip == animclip[1] && CD == false)
                {
                    CD = true;
                    anim.clip = animclip[0];
                    anim.Play();
                    StartCoroutine(timeCD());
                }
            }
            else
            {
                if(Lockedused == false && DoorOpenTrigger.textuiappear == false && ExploreUI.Exploreused == false)
                {
                    Lockedused = true;
                    LockedUI.SetActive(true);
                    StartCoroutine(LockedUICD());
                }
            }
        }
    }

    void EscapeHouse()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(key == true)
            {
                SceneManager.LoadSceneAsync(3);
            }
            else if(Lockedused == false && DoorOpenTrigger.textuiappear == false && ExploreUI.Exploreused == false && Textdisappear.flashkey == false)
            {
                Lockedused = true;
                LockedUI.SetActive(true);
                StartCoroutine(LockedUICD());
            }
        }
    }

    IEnumerator timeCD()
    {
        yield return new WaitForSeconds(1f);
        CD = false;
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Killer")
        {
            if(anim.clip == animclip[0] && CD == false)
            {
                anim.clip = animclip[1];
                anim.Play();
            }
            if(this.gameObject.tag == "DoorLock")
            {
                this.gameObject.tag = "Door";
                this.gameObject.transform.GetChild(0).tag = "Door";
            }
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.tag == "Killer")
        {
            if(anim.clip == animclip[0] && CD == false)
            {
                anim.clip = animclip[1];
                anim.Play();
            }
        }
    }
}