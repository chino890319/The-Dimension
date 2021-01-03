using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AP : MonoBehaviour
{
    //最大AP數值
    public float MaxAP;
    //AP數值
    public static float currentAP;
    //MPUI
    public Image Bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //同步APUI = AP
        Bar.fillAmount = currentAP/MaxAP;
    }
}
