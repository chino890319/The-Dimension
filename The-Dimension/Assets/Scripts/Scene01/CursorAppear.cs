﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAppear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //顯示鼠標
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        LoadingScreen.skipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
