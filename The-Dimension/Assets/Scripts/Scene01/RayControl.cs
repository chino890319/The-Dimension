using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayControl : MonoBehaviour
{
    //預設準心
    Ray ray;
    //預設準心距離
    public static float raylength = 1.5f;
    //準心UI
    public RaycastHit hit;
    //ButtonE UI
    public GameObject ButtonE;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //預設準心
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            
        if (Physics.Raycast(ray, out hit, raylength))
        {
            if((hit.collider.gameObject.tag == "Door" || hit.collider.gameObject.tag == "Cabinet" || hit.collider.gameObject.tag == "Floor"))
            {
                ButtonE.SetActive(true);
                hit.transform.parent.SendMessage("PressEE", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
                
                if(Time.timeScale == 0f)
                {
                    ButtonE.SetActive(false);
                }
            }
            else if(hit.collider.gameObject.tag == "DoorLock")
            {
                if(DoorOpenTrigger.textuiappear == false && ExploreUI.Exploreused == false && Textdisappear.flashkey == false)
                {
                    ButtonE.SetActive(true);
                }       
                hit.transform.parent.SendMessage("Lock", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
                if(Time.timeScale == 0f)
                {
                    ButtonE.SetActive(false);
                }
            }
            else if(hit.collider.gameObject.tag == "SupriseDoor")
            {
                if(DoorOpenTrigger.textuiappear == false && ExploreUI.Exploreused == false && Textdisappear.flashkey == false)
                {
                    ButtonE.SetActive(true);
                } 
                hit.transform.parent.SendMessage("SupriseDoor", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
                if(Time.timeScale == 0f)
                {
                    ButtonE.SetActive(false);
                }
            }
            else if(hit.collider.gameObject.tag == "Sofa" && PuzzleController.puzzle03used == true)
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("SofaMove", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
            }
            else if(hit.collider.gameObject.tag == "Puzzle" || hit.collider.gameObject.tag == "mapBPuzzle")
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("PuzzleE", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
            }
            else if(hit.collider.gameObject.tag == "Escape")
            {
                if(DoorOpenTrigger.textuiappear == false && ExploreUI.Exploreused == false && Textdisappear.flashkey == false)
                {
                    ButtonE.SetActive(true);
                } 
                hit.transform.parent.SendMessage("EscapeHouse", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
                if(Time.timeScale == 0f)
                {
                    ButtonE.SetActive(false);
                }
            }
            else if(hit.collider.gameObject.tag == "LockerGameObject" && PuzzleController.puzzle01used == true)
            {
                ButtonE.SetActive(true);
                hit.transform.parent.SendMessage("UnLocker", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
            }
            else if(hit.collider.gameObject.tag == "Flashlight")
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("flashlight", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
            }
            else if(hit.collider.gameObject.tag == "mapBappearkey" && (hit.collider.gameObject.name == "box_top" || hit.collider.gameObject.name == "key"))
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("mapBappearkey", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
            }
            else if(hit.collider.gameObject.name == "Housekey" && hit.collider.gameObject.tag == "Key")
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("Housekey", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
            }
            else if(hit.collider.gameObject.name == "Spicalkey" && hit.collider.gameObject.tag == "Key")
            {
                ButtonE.SetActive(true);
                hit.transform.SendMessage("Spicalkey", gameObject, SendMessageOptions.DontRequireReceiver);
                print(hit.transform.name);
            }
            Debug.DrawLine(ray.origin, hit.point, Color.yellow);
            //print(hit.transform.name);
        }
        else
        {
            ButtonE.SetActive(false);
        }
    }
}