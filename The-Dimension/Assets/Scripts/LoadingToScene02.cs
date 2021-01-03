using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingToScene02 : MonoBehaviour
{
        //加載%數字
        public Text text;
        //異步加載
        AsyncOperation async;
        public bool load = false;
        void Awake()
        {
      
        }

        void Start()
        {
  
        }

        void Update()
        {
            if(load == false)
            {
                load = true;
                Load();
            }
        }

        //載入場景
        public void Load()
        {
            //異步加載
            StartCoroutine(LoadingScreen01());
        }

        //異步加載場景(Loading)
        IEnumerator LoadingScreen01()
        {
            async = SceneManager.LoadSceneAsync(4);
            async.allowSceneActivation = false;
            while (async.isDone == false)
            {
                text.text = ((int)async.progress * 100).ToString() + "%";
                if (async.progress == 0.9f)
                {
                    text.text = (1f * 100).ToString() + "%";
                    async.allowSceneActivation = true;   
                }
                yield return null;
            }
        }
}
