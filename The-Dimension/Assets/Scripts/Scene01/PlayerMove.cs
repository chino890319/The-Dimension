using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //角色控制器
    public CharacterController controller;
    //角色移動速度
    public float speed = 12f;
    //重力
    public float gravity = -9.81f;
    //角色跳躍高度
    public float JumpHeight = 3f;
    //判斷是否在地板上
    public Transform groundCheck;
    //與地板的距離
    public float groundDistance = 0.4f;
    //groundMask為掉落要碰到的圖層名稱
    public LayerMask groundMask;
    //角色能否移動 = true
    public static bool movebool = true;
    //AP是否<40(tired)
    public static bool tired = false;
    //角色是否跑步 (是否按住shift) = false
    public bool shift = false;
    //角色是否跑步狀態 = false
    public bool shiftCD;
    //狀態是否tired
    public bool tiredCD = false;
    //角色是否移動 (WASD) = false
    public bool WASD = false;
    //回復AP
    public bool ReAP = false;
    //加速度
    Vector3 velocity;
    //角色是否在地板上 (bool)
    public static bool isGrounded;
    //角色音效
    public AudioClip[] music;
    //角色音效撥放器
    public AudioSource voice;
    // Start is called before the first frame update
    void Start()
    {
        //取得角色音效撥放器
        voice = this.GetComponent<AudioSource>();
        //音效循環 = false
        voice.loop = false;
    }
 
    //跑步
    IEnumerator Runtime()
    {
        AP.currentAP = AP.currentAP - 2;
        yield return new WaitForSeconds(0.1f);
        shiftCD = false;
    }

    //回復AP
    IEnumerator ReAPtime()
    {
        if(AP.currentAP <= 0)
        {
            yield return new WaitForSeconds(3f);
        }
        if(tired == false)
        {
            speed = 3f;
        }
        AP.currentAP = AP.currentAP + 1;
        yield return new WaitForSeconds(0.1f);
        tiredCD = false;
    }

    // Update is called once per frame
    void Update()
    {
        //AP控制
        if(AP.currentAP >=100)
        {
            AP.currentAP = 100;
        }
        if(AP.currentAP <=0)
        {
            AP.currentAP = 0;
        }

        //放開Shift就停止跑步
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            shift = false;
            shiftCD = false;
            ReAP = true;
            speed = 3f;
        }

        if(movebool == true)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            
            //transform.right = X軸 , transform.forward = Z軸
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            //跳躍
            if(Input.GetButtonDown("Jump") && isGrounded && speed != 1f)
            {
                //Mathf.Sqrf = 取平方根 (根據物理的結果)
                velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
                AP.currentAP = AP.currentAP - 20;
            }

            //狀態
            if(AP.currentAP >= 100)
            {
                ReAP = false;
            }
            if(AP.currentAP < 100)
            {
                ReAP = true;
            }

            if(voice.isPlaying && AP.currentAP >= 40 && tired == true)
            {
                tired = false;
                voice.loop = false;
                voice.Stop();
            }
            if(!voice.isPlaying && AP.currentAP < 40)
            {
                tired = true;
                voice.clip = music[0];
                voice.loop = true;
                voice.Play();
            }

            //跑步
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                WASD = true;
            }
            else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                WASD = false;
            }
            if(Input.GetKey(KeyCode.LeftShift))
            {   
                shift  = true;
                speed = 6f;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                shift = false;
                shiftCD = false;
                ReAP = true;
                speed = 3f;
            }
            if(AP.currentAP <= 0)
            {
                shift = false;
                shiftCD = false;
                ReAP = true;
                speed = 1f;
            }
            if(shift == true && WASD == true && shiftCD == false)
            {   
                shiftCD = true;
                StartCoroutine(Runtime());
            }
            if(((shift == false || (shift == true && WASD == false)) && ReAP == true && isGrounded) && tiredCD == false)
            {
                tiredCD = true;
                StartCoroutine(ReAPtime());
            }
            
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);    
            //Debug.Log(AP.currentAP);
        }  
    }
}