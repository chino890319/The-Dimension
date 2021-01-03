using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //死亡畫面
    public GameObject over;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //主角死亡
        if(HP.currentHP <= 0)
        {
            over.SetActive(true);
            PlayerMove.movebool = false;
        }
    }
}
