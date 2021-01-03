using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour
{
    //對話觸發
    public static bool textuiappear = false;
    //殭屍01Trigger是否觸發
    public static bool zb01 = false;
    //文字UI
    public GameObject TextUI;
    //文字UI2
    public GameObject TextUI2;
     //音效撥放器
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TextCD()
    {
        yield return new WaitForSeconds(5f);
        TextUI.SetActive(false);
        if(ExploreUI.Exploreused == false)
        {
            TextUI2.SetActive(true);
        }
        yield return new WaitForSeconds(2.5f);
        TextUI2.SetActive(false);
        textuiappear = false;
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player" && PuzzleController.puzzle01used == true && zb01 == false)
        {
            textuiappear = true;
            zb01 = true;
            TextUI.SetActive(true);
            sound.Play();
            StartCoroutine(TextCD());
        }
    }
}
