using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //滑鼠靈敏度
    public float mouseSensitivity = 100f;
    //角色座標
    public Transform playerBody;
    //滑鼠是否可以移動 (鏡頭移動)
    public static bool mouselookbool = true;
    //X座標
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //可以鏡頭移動
        if(mouselookbool == true)
        {
            //計算鏡頭移動速度
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;  

            xRotation -= mouseY;
            
            //如果進入殺手斬殺動畫
            if(AIPatrols.kill == true || AIPatrols02.kill02 == true)
            {
                xRotation = Mathf.Clamp(xRotation, 0f, 0f);
                mouselookbool = false;
            }
            else
            {
                //計算鏡頭移動範圍 (-90f最小,60f最大)
                xRotation = Mathf.Clamp(xRotation, -90f, 60f);
            }
            //鏡頭轉向 (Quaternion.Euler是轉xRotation)
            //Vector3.up = Vector3(0, 1, 0)
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX); 
        }
    }
}
