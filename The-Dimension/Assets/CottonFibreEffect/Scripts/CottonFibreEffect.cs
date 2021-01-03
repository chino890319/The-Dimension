using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonFibreEffect : MonoBehaviour {
    public int cottonFibreCount = 1000;
    public float speed = 0.1f;
    public float velocity = 0.9f;
    public float circleSize = 50f;
    public Color color = new Color(1, 1, 1, 0.5f);
    //public Color backgroundColor = new Color(0f,0f,0f);

    public Material lineMaterial;

    CottonFibre[] cottonFibres;

    bool isClear = false;

    // Use this for initialization

    void Start ()
    {

    }

    void OnEnable()
    {
        if(PlayerControl.shine == true)
        {
            init();
        }
    }

    private void init()
    {
        cottonFibres = new CottonFibre[cottonFibreCount];

        for (int i = 0; i < cottonFibreCount; i++)
        {
            cottonFibres[i] = new CottonFibre(circleSize, speed, velocity);
        }

        //isClear = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isClear == false)
        {
            isClear = true;
        }
        if (Input.GetMouseButtonUp(1) && isClear == true)
        {
            isClear = false;
        }
    }

    private void OnRenderObject()
    {
        GL.PushMatrix(); //保存当前Matirx  
        lineMaterial.SetPass(0); //刷新当前材质  
        GL.LoadProjectionMatrix(Camera.main.projectionMatrix);
        GL.LoadPixelMatrix();

        if (isClear)
        {
            isClear = false;
            GL.Clear(true, true, color);
        }

        GL.Begin(GL.LINES);
        GL.Color(color);
        for (int i = 0; i < cottonFibreCount; i++)
        {
            cottonFibres[i].Update();
        }
        GL.End();


        GL.PopMatrix();//读取之前的Matrix  
    }
}
