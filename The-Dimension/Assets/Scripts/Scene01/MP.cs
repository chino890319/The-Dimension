using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP : MonoBehaviour
{
    //最大MP數值
    public float MaxMP;
    //MP數值
    public static float currentMP;
    //MPUI
    public Image Bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //同步MPUI = MP
        Bar.fillAmount = currentMP/MaxMP;
    }
}
