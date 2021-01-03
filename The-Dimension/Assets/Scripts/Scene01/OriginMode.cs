using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OriginMode : MonoBehaviour
{
    private AudioSource voice;
    //玩家視角
    public GameObject PlayerCam;
    //殺手視角
    public GameObject KillerCam;
    //殺手
    public GameObject Killer;
    //玩家text
    public GameObject Playertext;
    //殺手text
    public GameObject Killertext;
    //RAWHPMP
    public GameObject RAW;
    //PressE
    public GameObject pressE;
    public bool pressEused = false;
    //殺手視角 = false
    public bool KillerCamera = false;
    public bool killer = false;
    // Start is called before the first frame update
    void Start()
    {
        voice = this.gameObject.GetComponent<AudioSource>();

        //正長時間速度
        Time.timeScale = 1f;        
        //隱藏滑鼠        
        Cursor.visible = false;
        //把滑鼠鎖定到螢幕中間
        Cursor.lockState = CursorLockMode.Locked;
        //無敵模式
        PlayerControl.god = false;   
        //進入靈界       
        PlayerControl.magic = false; 
        //進入靈界前的特效       
        PlayerControl.shine = false;   
        //判斷濾鏡前的特效(計時)     
        PlayerControl.shinebool = false;   
        //原始血量 
        HP.currentHP = 100;      
        //原始魔力           
        MP.currentMP = 100;
        //原始體力   
        AP.currentAP =100;          
        //施放魔力的時間    
        PlayerControl.Shinetimer = 0;    
        //角色是可以移動   
        PlayerMove.movebool = true;         
        //角色是否活躍(動畫 音效等)  
        PlayerControl.PlayerActive = true;  
        //視角是否可以轉動
        MouseLook.mouselookbool = true;     
        //暫停遊戲
        PlayerControl.stop = false;         
        //轉頭動畫是否開啟
        PlayerControl.turnaround = false;
        //殺手是否斬殺
        AIPatrols.kill = false;
        //殺手是否活躍
        AIPatrols.KillerMove = true;
        //殺手是否斬殺
        AIPatrols02.kill02 = false;
        //殺手是否活躍
        AIPatrols02.KillerMove02 = true;
        //殺手視角主角是否進入靈界
        AIPatrols.shining = false;
        //殺手視角主角是否進入靈界
        AIPatrols02.shining02 = false;
        //謎題重製
        PuzzleController.puzzle = false;
        //謎題01已觸發過
        PuzzleController.puzzle01used = false;
        //殭屍01Trigger是否觸發
        DoorOpenTrigger.zb01 = false;
        //殭屍01是否出現
        DoorController.zb01 = false;
        //是否拾取手電筒
        PlayerControl.flashlight = false;
        //MPUI是否出現
        PlayerControl.MPUIappear = false;
        //角色是否疲憊
        PlayerMove.tired = false;
        //撲克牌謎題是否觸發
        PuzzleController.Playingcards = false;
        //撞球謎題是否觸發
        PuzzleController.Rack = false;
        //Scene02殺手是否已出現
        Killerappear.Camerabool = false;
        //是否已通關
        DriveCar.successful = false;
        //房子特殊鑰匙是否已拾取
        DoorController.spicalkey = false;
        //房子鑰匙是否已拾取
        DoorController.key = false;
        //迷宮鑰匙是否已拾取
        DoorController.key2 = false;
        //UI是否觸發
        DoorOpenTrigger.textuiappear = false;
        //UI是否觸發
        ExploreUI.Exploreused = false;
        //計算謎題幾次
        PuzzleController.i = 0;
        //派蒙王計算幾次
        PuzzleController.Paimon = 0;
        //手機謎題是否觸發
        PuzzleController.puzzle03used = false;
        //預設準心長度
        RayControl.raylength = 1.5f;
        //手電筒及鑰匙UI出現
        Textdisappear.flashkey = false;
    }

    IEnumerator CamCD()
    {
        yield return new WaitForSeconds(3.5f);
        PlayerCam.SetActive(true);
        KillerCam.SetActive(false);
        RAW.SetActive(true);
        if(pressEused == true)
        {
            pressE.SetActive(true);
            pressEused = false;
        }
        Playertext.SetActive(true);
        Killertext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PuzzleController.i >= 4 && killer == false && SceneManager.GetActiveScene().buildIndex == 2 && Time.timeScale == 1f)
        {
            killer = true;
            Killer.SetActive(true);
            PlayerCam.SetActive(false);
            KillerCam.SetActive(true);
            RAW.SetActive(false);
            if(pressE.activeInHierarchy == true)
            {
                pressEused = true;
                pressE.SetActive(false);
            }
            Playertext.SetActive(false);
            Killertext.SetActive(true);
            voice.Play();
            StartCoroutine(CamCD());
        }
    }
}
