using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PuzzleController : MonoBehaviour
{
    //謎題UI
    public GameObject puzzleobj;
    //MPUI
    public GameObject MPUI;
    //文字UI
    public GameObject TextUI;
    //ESC UI
    public GameObject ESC;
    //密碼鎖完成UI
    public GameObject Password;
    //特殊鑰匙
    public GameObject SpecialKey;
    //謎題是否觸發整合
    public static bool puzzle = false;
    //謎題01是否觸發
    public bool puzzle01 = false;
    //謎題02是否觸發
    public bool puzzle02 = false;
    //謎題03是否觸發
    public bool puzzle03 = false;
    //謎題04是否觸發
    public bool puzzle04 = false;
    //謎題文字CD是否觸發
    public bool puzzlecd = false;
    //謎題觸發次數避免疊加
    public bool times = false;
    //謎題觸發次數避免疊加
    public bool times1 = false;
    //謎題觸發次數避免疊加
    public bool times2 = false;
    //謎題觸發次數避免疊加
    public bool times3 = false;
     //謎題觸發次數避免疊加
    public bool times4 = false;
    //謎題觸發次數避免疊加
    public bool times5 = false;
    //謎題觸發次數避免疊加
    public bool times6 = false;
    //特殊鑰匙是否觸發
    public bool Paimonbool = false;
    //撞球謎題是觸發
    public static bool Rack = false;
    //撲克牌謎題是否觸發
    public static bool Playingcards = false;
    //謎題01是否觸發過
    public static bool puzzle01used = false;
    //謎題03是否觸發過
    public static bool puzzle03used = false;
    //計算謎題幾次
    public static int i = 0;
    //派蒙王計算幾次
    public static int Paimon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(puzzle01 == false || puzzle02 == false || puzzle03 == false || puzzle04 == false)
        {
            puzzle = false;
        }
        if(puzzle01 == true || puzzle02 == true || puzzle03 == true || puzzle04 == true)
        {
            puzzle = true;
        }
    }

    //文字顯示計時
    IEnumerator TextCD()
    {
        yield return new WaitForSeconds(2.5f);
        puzzleobj.SetActive(false);
        puzzlecd = false;
    }

    void PuzzleE()
    {
        //撲克牌及撞球
        if(Input.GetKeyDown(KeyCode.E) && puzzlecd == false)
        {
            if(gameObject.name == "Playing Cards")
            {
                if(times == false)
                {
                    times = true;
                    i += 1;
                }
 
                //謎題文字是否觸發
                puzzlecd = true;
                //撲克牌謎題是否觸發
                Playingcards = true;
                //謎題UI
                puzzleobj.SetActive(true);
                //文字CD
                StartCoroutine(TextCD());
            }
            else if(gameObject.name == "Rack")
            {
                if(times1 == false)
                {
                    times1 = true;
                    i += 1;
                }
                
                //謎題03是否觸發過
                puzzle03used = true;
                //謎題文字是否觸發
                puzzlecd = true;
                //撞球謎題是否觸發
                Rack = true;
                //謎題UI
                puzzleobj.SetActive(true);
                //文字CD
                StartCoroutine(TextCD());
            }
        }

        //謎題01
        if(Input.GetKeyDown(KeyCode.E) && puzzle01 == false)
        {
            if(gameObject.name == "PAGE")
            {
                if(times2 == false)
                {
                    times2 = true;
                    i += 1;
                }
                
                //暫停
                Time.timeScale = 0f;
                //謎題01觸發
                puzzle01 = true;
                //謎題01已觸發過
                puzzle01used = true;
                //謎題UI
                puzzleobj.SetActive(true);
                //關閉MPUI
                MPUI.SetActive(false);
                //關閉TextUI
                TextUI.SetActive(false);
                //開啟ESCUI
                ESC.SetActive(true);
                //主角不能動
                PlayerMove.movebool = false;
                //主角不能按任何鍵盤
                PlayerControl.PlayerActive = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && puzzle01 == true)
        {
                //暫停
                Time.timeScale = 1f;
                //謎題01觸發
                puzzle01 = false;
                //謎題UI
                puzzleobj.SetActive(false);
                MPUI.SetActive(true);
                //開啟TextUI
                TextUI.SetActive(true);
                //開啟ESCUI
                ESC.SetActive(false);
                //主角能動
                PlayerMove.movebool = true;
                //主角能按任何鍵盤
                PlayerControl.PlayerActive = true;
        }

        //謎題02老婆筆記
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(gameObject.name == "book")
            {
                if(times3 == false)
                {
                    times3 = true;
                    i += 1;
                }
                
                //暫停
                Time.timeScale = 0f;
                //謎題02觸發
                puzzle02 = true;
                //謎題UI
                puzzleobj.SetActive(true);
                //關閉MPUI
                MPUI.SetActive(false);
                //關閉TextUI
                TextUI.SetActive(false);
                //開啟ESCUI
                ESC.SetActive(true);
                //主角不能動
                PlayerMove.movebool = false;
                //主角不能按任何鍵盤
                PlayerControl.PlayerActive = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && puzzle02 == true)
        {
                //暫停
                Time.timeScale = 1f;
                //謎題02觸發
                puzzle02 = false;
                //謎題UI
                puzzleobj.SetActive(false);
                //關閉MPUI
                MPUI.SetActive(true);
                //開啟TextUI
                TextUI.SetActive(true);
                //開啟ESCUI
                ESC.SetActive(false);
                //主角能動
                PlayerMove.movebool = true;
                //主角能按任何鍵盤
                PlayerControl.PlayerActive = true;
        }

        
        //謎題03手機
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(gameObject.name == "phone")
            {
                if(times4 == false)
                {
                    times4 = true;
                    i += 1;
                }
                
                //暫停
                Time.timeScale = 0f;
                //謎題03觸發
                puzzle03 = true;
                //謎題UI
                puzzleobj.SetActive(true);
                //關閉MPUI
                MPUI.SetActive(false);
                //關閉TextUI
                TextUI.SetActive(false);
                //開啟ESCUI
                ESC.SetActive(true);
                //主角不能動
                PlayerMove.movebool = false;
                //主角不能按任何鍵盤
                PlayerControl.PlayerActive = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && puzzle03 == true)
        {
                //暫停
                Time.timeScale = 1f;
                //謎題03觸發
                puzzle03 = false;
                //謎題UI
                puzzleobj.SetActive(false);
                //關閉MPUI
                MPUI.SetActive(true);
                //開啟TextUI
                TextUI.SetActive(true);
                //開啟ESCUI
                ESC.SetActive(false);
                //主角能動
                PlayerMove.movebool = true;
                //主角能按任何鍵盤
                PlayerControl.PlayerActive = true;
        }

        //謎題04海報
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(gameObject.name == "Wallpaper")
            {
                if(times5 == false)
                {
                    times5 = true;
                    i += 1;
                }
                
                Paimon += 1;
            }
        }

        //特殊鑰匙
        if(Paimon >= 9 && Paimonbool == false)
        {
            Paimonbool = true;
            SpecialKey.SetActive(true);
        }

        //密碼鎖
        if(Input.GetKeyDown(KeyCode.E) && (this.gameObject.name == "box" || this.gameObject.name == "box_top" || this.gameObject.name == "keyword" || this.gameObject.name == "Cube") && Password.activeSelf == false) 
        {
            if(times6 == false)
            {
                times6 = true;
                i += 1;
            }
            
            //暫停
            Time.timeScale = 0f;
            //謎題01UI
            puzzleobj.SetActive(true);
            //關閉MPUI
            MPUI.SetActive(false);
            //關閉TextUI
            TextUI.SetActive(false);
            //開啟ESCUI
            ESC.SetActive(true);
            //主角不能動
            PlayerMove.movebool = false;
            //主角不能按任何鍵盤
            PlayerControl.PlayerActive = false;
            //出現游標
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
                //暫停
                Time.timeScale = 1f;
                //謎題UI
                puzzleobj.SetActive(false);
                //關閉MPUI
                MPUI.SetActive(true);
                //開啟TextUI
                TextUI.SetActive(true);
                //關閉ESCUI
                ESC.SetActive(false);
                //主角不能動
                PlayerMove.movebool = true;
                //主角不能按任何鍵盤
                PlayerControl.PlayerActive = true;
                //關閉游標
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        RayControl.raylength = 2f;
    }
    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        RayControl.raylength = 1.5f;
    }
}
