using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{   
    //AIUI
    public NavMeshAgent agent;      
    //AI攻擊目標
    public GameObject target_obj;    
    //AI 物件
    public GameObject meshobj;
    //AI mesh
    public GameObject skin;
    //AI攻擊間隔CD 
    public int attacktimer = 0; 
    //AI的animator                 
    Animator m_Animator;
    //玩家和AI之間的距離
    float distance;
    //AI當前所處的狀態
    int nowstate;
    //能否隨機一個坐標點
    bool isRandom;
    //從移動改變為停止的狀態改變的時刻
    bool stoptime;
    //控制能否改變狀態
    bool isChangestate;
    //隨機到的兩個數
    float x ;
    float z ;
    //移動的時間
    float movetime;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();
        //敵人當前所處狀態為0（發呆動畫、停止不動）
        nowstate = 0;
        //可以隨機一個數
        isRandom = true;
        //停留時間記為0
        stoptime = false;
    }

    // Update is called once per frame
    void Update()
    {
        //玩家和敵人之間的距離，實時更新
        distance = Vector3.Distance(transform.position,target_obj.transform.position);
        //調用距離改變函數
        distancechange();
        //調用狀態改變函數
        statechange();
        if(attacktimer == 3)
        {
            CancelInvoke("Hurt");
            CancelInvoke("attacktimer4");
            attacktimer = 0;
            PlayerControl.god = false;
        }  
        if(PlayerControl.Shinetimer == 2)
        {
            skin.SetActive(true);
        }
        if(PlayerControl.magic == false)
        {
            skin.SetActive(false);
        }
    }

    void distancechange()
    {
        if (distance < 10)
        {
            nowstate = 2;
        }
        //如果距離大於30
        if (distance > 30)
        {
            //如果可以改變狀態
            if (isChangestate == true)
            {
                //狀態改變為自動巡邏的發呆狀態（播放發呆動畫）
                nowstate = 0;
                //記下狀態改變的時刻
                stoptime = false;
                //狀態不可以改變了
                isChangestate = false;
           }
        }
    }

    //計算AI的時間
    IEnumerator stoptimer()
    {
        yield return new WaitForSeconds(2);
        stoptime = true;
    }

    //進到主角的Trigger
    void OnTriggerStay(Collider hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            if(PlayerControl.god == false)
            {
                HP.currentHP = HP.currentHP - 20;
                PlayerControl.god = true;            
                InvokeRepeating("Hurt",0f,3f);
            }
        }
    }

    //計算AI攻擊CD
    void Hurt()
    {
        InvokeRepeating("attacktimer4",0f,1f);      
    }
    
    //計算AI攻擊CD
    void attacktimer4()
    {
        attacktimer++;
    }

    //AI狀態改變
    void statechange()
    {
        //狀態0(發呆狀態)
        if (nowstate == 0)
        {
            m_Animator.SetBool("Run",false);
            //導航停止
            agent.isStopped = true;
            StartCoroutine(stoptimer());
            //可以隨機數
            isRandom = true;
            if (stoptime == true)
            {
                nowstate = 1;
            }
        }
        //狀態1(跑步巡邏狀態)
        else if (nowstate == 1)
        {
            //導航恢復
            agent.isStopped = false;
            //如果可以隨機數
            if (isRandom == true)
            {
                //隨機兩個點（地形范圍內）
                x = Random.Range(transform.position.x -30 , transform.position.x + 30);
                z = Random.Range(transform.position.z -30 , transform.position.z + 30);
               
                isRandom = false;
            }
            //下一個坐標點為隨機到的點
            Vector3 nextpos = new Vector3(x,transform.position.y,z);
            //導航到下一個點
            agent.destination =nextpos; 
            m_Animator.SetBool("Run",true);
            //從當前位置到下一個點之間畫一條紅線（紅線在Scene場景中可見）
            Debug.DrawLine(transform.position, nextpos, Color.red);
            //敵人和下一個點之間的距離小於0.1
            if (agent.remainingDistance<=0.1f)
            {
                nowstate = 0;
                stoptime = false;
            }
        }
        //狀態2(追著主角狀態)
        else if (nowstate == 2)
        {
            agent.destination = target_obj.transform.position;
            isChangestate = true;
        }
    }
}
