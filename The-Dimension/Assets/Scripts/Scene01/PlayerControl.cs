using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour
{
    //動畫控制器
    public Animator anim;
    //濾鏡
    public GameObject mode;                     
    //濾鏡前的特效
    public GameObject shining;
    //殺手
    public GameObject Killer;
    //手電筒                  
    public GameObject spotlight;
    //準心、血量、魔力
    public GameObject RAWHPMP;
    //魔力
    public GameObject MPUI;
    //暫停選單
    public GameObject stopmenu;
    //死亡UI
    public GameObject DeadUI;
    //返回主選單UI
    public GameObject backmenuUI;
    //重新遊玩UI
    public GameObject ReplayUI;
    //殭屍
    public GameObject[] zb;
    //手電筒 = false
    public bool spotlightuse = false;
    //場景編號
    public int Sceneindex;
    //MPUI是否出現過
    public static bool MPUIappear = false;
    //無敵狀態
    public static bool god = false;            
    //轉頭動畫是否開啟
    public static bool turnaround = false; 
    //濾鏡是否開啟
    public static bool magic = false;           
    //濾鏡前的特效是否開啟
    public static bool shine = false;      
    //判斷濾鏡前的特效(計時)     
    public static bool shinebool = false;         
    //暫停選單 = false  
    public static bool stop = false;
    //角色活躍 = true (是否可以使用功能)
    public static bool PlayerActive = true;    
    //是否拾取手電筒
    public static bool flashlight = false;
    //角色死亡 = false   
    public bool dead = false;
    //濾鏡前特效計時
    public static int Shinetimer = 0;           
    //濾鏡前特效關閉的計時
    public int ShineExittimer = 0;              
    //與殺手的距離
    public float distance;
    //角色音效                  
    public AudioClip[] music;
    //角色音效撥放器
    public AudioSource voice;
    //角色座標
    public static Transform PlayerTransform;
    //轉向殺手的座標
    Vector3 killedlook;
    //轉向殭屍座標
    // Start is called before the first frame update
    void Start()
    {
        //動畫控制器
        anim = GetComponent<Animator>();
        //手電筒 = false
        spotlight.SetActive(false);
        //取得角色座標
        PlayerTransform = GameObject.FindWithTag("Player").transform;
        //取得角色音效撥放器
        voice = this.GetComponent<AudioSource>();
        //音效循環 = false
        voice.loop = false;
        //獲取場景編號
        Sceneindex = SceneManager.GetActiveScene().buildIndex;
    }

    //計算技能冷卻
    IEnumerator shinetimer()
    {
        //回復魔力的冷卻
        if(magic == false && MP.currentMP < 100 && MP.currentMP >= 0)
        {
            if(MP.currentMP == 0)
            {
                yield return new WaitForSeconds(2f);
            } 
            yield return new WaitForSeconds(0.1f);
            MP.currentMP = MP.currentMP + 1f;
        }
        else if(magic == true) 
        {
            yield return new WaitForSeconds(0.1f); 
        }
        shinebool = true;
    }

    //手電筒冷卻 (ON)
    IEnumerator spotlightCD()
    {
        yield return new WaitForSeconds(0.2f);
        spotlightuse = true;
    }

    //手電筒冷卻 (OFF)
    IEnumerator spotlightCD1()
    {
        yield return new WaitForSeconds(0.2f);
        spotlightuse = false;
    }

    //轉頭動畫時間
    IEnumerator turnaroundCD()
    {
        yield return new WaitForSeconds(2f);
        PlayerActive = true;
        PlayerMove.movebool = true;
        anim.SetBool("turnaround",false);
    }

    //死亡動畫
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(3);
        backmenuUI.SetActive(true);
        ReplayUI.SetActive(true);
        DeadUI.SetActive(true);
        yield return new WaitForSeconds(3);
        if(Input.GetKeyDown("escape"))
        {
            SceneManager.LoadSceneAsync(0);
        }
        if(Input.GetKeyDown(KeyCode.R) && Sceneindex == 2)
        {
            SceneManager.LoadSceneAsync(2);
        }
        else if(Input.GetKeyDown(KeyCode.R) && Sceneindex == 4)
        {
            SceneManager.LoadSceneAsync(4);
        }
    }

    void Update()
    {
        //作弊模式
        if(Input.GetKeyDown(KeyCode.U))
        {
            Killer.SetActive(false);
            HP.currentHP = 9999;
            MP.currentMP = 9999;
            AP.currentAP = 9999;
            DoorOpenTrigger.zb01 = true;
            MPUIappear = true;
            MPUI.SetActive(true);
            DoorController.key = true;
            DoorController.spicalkey = true;
            DoorController.key2 = true;
            flashlight = true;
        }
        
        //逃脫關閉特效
        if(DriveCar.successful == true)
        {
            shining.SetActive(false);
            mode.SetActive(false);
        }

        //出現MPUI
        if(DoorOpenTrigger.zb01 == true)
        {
            MPUIappear = true;
            MPUI.SetActive(true);
        }

        //與殺手的距離
        distance = Vector3.Distance(Killer.transform.position,this.transform.position);
        //角色死亡
        if(HP.currentHP == 0 && dead == false)
        {
            voice.clip = music[0];
            voice.loop = false;
            voice.Play();
            PlayerMove.movebool = false;
            PlayerActive = false;
            dead = true;
        }
        if(dead == true)
        {
            StartCoroutine(Dead());
        }

        //死亡關閉shining
        if(AIPatrols.kill == true)
        {
            shining.SetActive(false);
        }
        //死亡關閉shining
        if(AIPatrols02.kill02 == true)
        {
            shining.SetActive(false);
        }
        
        //謎題UI觸發關閉shining
        if(PuzzleController.puzzle == true)
        {
            shining.SetActive(false);
            shine = false;
            CancelInvoke("Shiningtime");
            Shinetimer = 0;
            magic = false;     
        }

        //角色活躍 = true
        if(PlayerActive == true)
        {
            //暫停選單
            if(Input.GetKeyDown("escape") && PuzzleController.puzzle == false)
            {
                stopmenu.SetActive(true);
                RAWHPMP.SetActive(false);
                PlayerMove.movebool = false;
                PlayerActive = false;
                MouseLook.mouselookbool = false;
                stop = true;
                AIPatrols.KillerMove = false;
                AIPatrols02.KillerMove02 = false;
                voice.Stop();
                Time.timeScale = 0f;

                //出現游標
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            //開關手電筒
            if(flashlight == true)
            {
                if(Input.GetKey(KeyCode.F))
                {                  
                    voice.clip = music[1];
                    voice.loop = false;
                    voice.Play();
                    
                    if(spotlightuse == false)
                    {
                        spotlight.SetActive(true);
                        StartCoroutine(spotlightCD());
                    }
                    if(spotlightuse == true)
                    {
                        spotlight.SetActive(false);
                        StartCoroutine(spotlightCD1());
                    }
                }
            }

            //魔力控制
            if(MP.currentMP >= 100)
            {
                MP.currentMP = 100;
            }
            if(MP.currentMP <= 0)
            {
                MP.currentMP = 0;
            }
            //判斷進入靈界
            if(Input.GetMouseButton(1) && MP.currentMP > 0 && magic == false && shine == false && MPUIappear == true)
            {
                shinebool = true;
                shine = true;
                shining.SetActive(true);
                InvokeRepeating("Shiningtime",0f,1f);    
            }
            if(Input.GetMouseButtonUp(1))
            {
                shine = false;
            }
            if(!Input.GetMouseButton(1))
            {
                shine = false;
                shining.SetActive(false);
                CancelInvoke("Shiningtime");
                Shinetimer = 0;
                mode.SetActive(false);
                magic = false;  
            }
            if((Input.GetMouseButtonUp(1) && magic == true) || MP.currentMP <= 0)
            {
                shine = false;
                shining.SetActive(false);
                CancelInvoke("Shiningtime");
                Shinetimer = 0;
                mode.SetActive(false);
                magic = false;      
            }

            //進入靈界前後的判斷
            if(Shinetimer == 1)
            {
                magic = true;
            }
            if(Shinetimer == 2)
            {
                shine = false;
                shining.SetActive(false);
                mode.SetActive(true);       
            }
            if(shinebool == true && shine == false && magic == true && Shinetimer >= 3)
            {
                StartCoroutine(shinetimer());
                AIPatrols.shining = true;
                AIPatrols02.shining02 = true;
                shinebool = false;
                MP.currentMP = MP.currentMP - 0.5f;  
            }
            if((shine == false && MP.currentMP < 100 && MP.currentMP >=0 && magic == false && shinebool == true && ShineExittimer >=2) || (shine == false && MP.currentMP < 100 && MP.currentMP >=0 && magic == false && shinebool == true))
            {
                StartCoroutine(shinetimer());
                shinebool = false;
            }
            //轉頭動畫播放
            if(turnaround == true)
            {
                anim.SetBool("turnaround",true);
                PlayerActive = false;
                PlayerMove.movebool = false;
                turnaround = false;
                StartCoroutine(turnaroundCD());
            }
        }
        
        //被殺手斬殺
        if(distance < 3.5f)
        {
            if(AIPatrols.kill == true)
            {           
                killedlook = Killer.transform.position - transform.GetChild(0).position;
                //殺手角度
                Quaternion rotation = Quaternion.LookRotation(new Vector3(killedlook.x,killedlook.y + 1.5f,killedlook.z));
                //緩慢轉向殺手         
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation , 500f * Time.deltaTime);       
            } 
            else if(AIPatrols02.kill02 == true)
            {           
                killedlook = Killer.transform.position - transform.GetChild(0).position;
                //殺手角度
                Quaternion rotation = Quaternion.LookRotation(new Vector3(killedlook.x,killedlook.y + 1.5f,killedlook.z));
                //緩慢轉向殺手         
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation , 500f * Time.deltaTime);       
            } 
        } 
    }

    //待在靈界的秒數
    void Shiningtime()
    {
        Shinetimer++;
    }
}