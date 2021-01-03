using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrols02 : MonoBehaviour
{
    //巡邏點(座標)的陣列
    public Transform[] points;
    //玩家
    public GameObject Player;
    //殺手skin
    public GameObject skin;
    //噴血UI
    public GameObject bloodsplat;
    //關閉魔力UI
    public GameObject RAWUI;
    //關閉文字UI
    public GameObject TextUI;
    //殺手的斧頭
    public GameObject ax;
    //殺手與玩家的距離
    public float distance;
    //依序巡邏巡邏點
    public int destPoint = 0;
    //是否被AI發現
    public bool EnterKillerTrigger = false;
    //解決巡點卡住
    public bool gonextpoint = false;
    //是否被斬殺
    public static bool kill02 = false;
    //殺手活躍
    public static bool KillerMove02 = true;
    //主角進入靈界
    public static bool shining02 = false;
    //AI
    public NavMeshAgent agent ;
    //AI的animator                 
    Animator m_Animator;
    //音效
    public AudioClip[] voice;
    //音效撥放器
    private AudioSource sound;
    //射線
    NavMeshHit hit;

    IEnumerator Attacktime()
    {
        yield return new WaitForSeconds(1f);
        bloodsplat.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //主角HP = 0
        HP.currentHP = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();
        sound = this.GetComponent<AudioSource>();
        sound.loop = true;
        //靠近巡邏點減速 = false
        agent.autoBraking = false;
        GotoNextPoint();
    }

    IEnumerator PointCD()
    {
        if(EnterKillerTrigger == false)
        {
            //AI目標 = 巡邏點陣列的座標
            agent.destination = points[destPoint].position;
            //AI巡邏點+1
            destPoint = (destPoint + 1) % points.Length;
            yield return new WaitForSeconds(6f);
            gonextpoint = false;
            GotoNextPoint();
        }
        else
        {
            gonextpoint = false;
        }
    }

    void GotoNextPoint()
    {
        if(gonextpoint == false)
        {
            gonextpoint = true;
            StartCoroutine(PointCD());
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(agent.remainingDistance);
        //print(destPoint);
        if(PlayerControl.Shinetimer == 2 && kill02 == false)
        {
            shining02 = true;
            skin.SetActive(false);
            ax.SetActive(false);
        }
        if(PlayerControl.magic == false)
        {
            shining02 = false;
            skin.SetActive(true);
            ax.SetActive(true);
        }

        //主角是否已逃脫成功
        if(DriveCar.successful == true)
        {
            agent.isStopped = true;
        }

        if(KillerMove02 == true)
        {
            print(agent.remainingDistance);
            distance = Vector3.Distance(Player.transform.position,this.transform.position);
            //接近下個巡邏點 OR 離主角距離>8f
            if(agent.remainingDistance <= 1f && EnterKillerTrigger == false)
            {
                GotoNextPoint();
            }       
            else if(EnterKillerTrigger == true)
            {
                if(distance > 12f || shining02 == true) 
                {
                    EnterKillerTrigger = false;
                    GotoNextPoint();
                }
            }


            //斬殺主角
            if(distance < 2f && kill02 == false && shining02 == false)
            {
                //斬殺音效
                sound.clip = voice[0];
                sound.loop = false;
                sound.Play();
                //殺手殺人了
                kill02 = true;
                //關閉RAWUI
                RAWUI.SetActive(false);
                //關閉TextUI
                TextUI.SetActive(false);
                //主角不能動
                PlayerMove.movebool = false;
                //主角不能按任何鍵盤
                PlayerControl.PlayerActive = false;
                agent.isStopped = true;

                //面向主角
                transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));      
                transform.Rotate(0,50,0);
                //播放攻擊動畫
                m_Animator.SetBool("Attack",true);
                //攻擊動畫時間
                StartCoroutine(Attacktime());
            }
            //發現主角
            if(distance > 2f && distance < 12f && shining02 == false)
            {
                //agent.autoBraking = true;
                EnterKillerTrigger = true;
                agent.destination = Player.transform.position;
                agent.speed = 3.5f;
            }
        }
    }
}
