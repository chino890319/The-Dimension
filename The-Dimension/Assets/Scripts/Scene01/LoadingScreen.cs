using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
        //加載%數字
        public Text text;
        //啟動Loading的物件
        public GameObject Loadingobj;
        //劇情前情提要
        public GameObject StoryBeginUI;
        //準心、血量、魔力
        public GameObject RAWHPMP;
        //教學UI
        public GameObject TriggerUI;
        //暫停選單
        public GameObject stopmenu;
        //跳過劇情前情提要
        public GameObject skip;
        //暫停選單
        public GameObject menu;
        //設定選單
        public GameObject setting;
        //開發者名單
        public GameObject DevelopersMenu;
        
        //設定選單狀況保留
        public Slider audioslider;
        public Toggle fullscreen;
        public static bool skipped = false;
        public Dropdown quality;
        public Dropdown resolutions;
        //end
        
        //異步加載
        AsyncOperation async;

        void Awake()
        {
            //Loading100%前無法跳過小提示畫面
            skipped = false;
        }

        void Start()
        {
            //Loading100%前無法跳過小提示畫面
            skipped = false;
        }

        void Update()
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                skip.SetActive(false);
                if(skipped == false)
                {
                    Load();
                }
            }
        }

        //設定選單
        public void Setting()
        {
            //暫停選單 = false
            menu.SetActive(false);
            //設定選單 = true
            setting.SetActive(true);
            
            //音量 全螢幕 畫質 解析度同步(靜態變數)
            audioslider.value = Options.audiovolume;
            fullscreen.isOn = Options.Fullscreen;
            quality.value = Options.quality;
            resolutions.value = Options.resolutionsnum;
        }

        //遊戲開發者選單
        public void Developers()
        {
            //暫停選單 = false
            menu.SetActive(false);
            //開發者名單
            DevelopersMenu.SetActive(true);
        }

        //設定選單返回menu
        public void BackMenu()
        {
            //暫停選單 = true
            menu.SetActive(true);
            //設定選單 = false
            setting.SetActive(false);
            //開發者名單
            DevelopersMenu.SetActive(false);
        }

        //載入場景
        public void Load()
        {
            //異步加載
            StartCoroutine(LoadingScreen01());
        }

        //載入劇情前情提要
        public void StoryBegin()
        {
            SceneManager.LoadSceneAsync(1);
        }

        //選單回復到遊戲中
        public void Continue()
        {
            PlayerMove.movebool = true;
            PlayerControl.PlayerActive = true;
            PlayerControl.stop = false;
            RAWHPMP.SetActive(true);
            TriggerUI.SetActive(true);
            stopmenu.SetActive(false);
            MouseLook.mouselookbool = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AIPatrols.KillerMove = true;
            Time.timeScale = 1f;
        }

        //返回遊戲主選單
        public void BackToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync(0);
        }

        //退出遊戲
        public void Quit()
        {
            Application.Quit();
        }

        //異步加載場景(Loading)
        IEnumerator LoadingScreen01()
        {
            async = SceneManager.LoadSceneAsync(2);
            async.allowSceneActivation = false;
            skipped = true;
            while (async.isDone == false)
            {
                text.text = ((int)async.progress * 100).ToString() + "%";
                if (async.progress == 0.9f)
                {
                    text.text = (1f * 100).ToString() + "%";
                    skip.SetActive(true);
                    if (Input.GetKey(KeyCode.Space) && skipped == true)
                    {
                        async.allowSceneActivation = true;   
                    }
                }
                yield return null;
            }
        }
}
