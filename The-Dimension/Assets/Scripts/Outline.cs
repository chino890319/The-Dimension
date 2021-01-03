using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{   
    //物件renderer
    public MeshRenderer renderer;
    //物件輪廓寬度
    public float maxOutlineWidth;
    //物件輪廓顏色
    public Color OutlineColor;
    // Start is called before the first frame update
    void Start()
    {
        //取得物件MeshRenderer
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    
    //顯示物件輪廓(Outline)
    void ShowOutline()
    {
        //print("yaaaa");
        renderer.material.SetFloat("_OutlineWidth",maxOutlineWidth);
        renderer.material.SetColor("_OutlineColor",OutlineColor);
    }
    
    //關閉物件輪廓(Outline)
    void HideOutline()
    {
         renderer.material.SetFloat("_OutlineWidth",0f);
    }
}
