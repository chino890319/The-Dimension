using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTrack : MonoBehaviour
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

    void Start()
    {
        //AI 取得NavMeshAgent、Animator
        agent = GetComponent<NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        //跟著角色暫停
        if(PlayerControl.stop == true)
        {
            agent.isStopped = true;
        }
        else if (PlayerControl.stop == false)
        {
            agent.isStopped = false;
        }
        
        //AI攻擊CD達到3秒
        if(attacktimer == 3)
        {
            CancelInvoke("Hurt");
            CancelInvoke("attacktimer4");
            attacktimer = 0;
            PlayerControl.god = false;
        }  

        //角色進入靈界後出現AI的SKIN
        if(PlayerControl.Shinetimer == 2)
        {
            skin.SetActive(true);
        }
        //角色不在靈界後隱藏AI的SKIN
        if(PlayerControl.magic == false)
        {
            skin.SetActive(false);
        }

        //AI朝著目標移動 (播放跑步動畫)
        agent.destination = target_obj.transform.position;
        m_Animator.SetBool("Run",true);
    }

    //進到主角的Trigger
    void OnTriggerStay(Collider hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            if(PlayerControl.god == false)
            {
                HP.currentHP = HP.currentHP - 100;
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
}