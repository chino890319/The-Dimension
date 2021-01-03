using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombileappear : MonoBehaviour
{
    public GameObject[] zb;
    //計算出現時間
    public bool cd = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DoorOpenTrigger.zb01 == true && cd == false)
        {
            cd = true;
            zb[0].SetActive(true);
            StartCoroutine(disappear());
        }
    }

    IEnumerator disappear()
    {
        yield return new WaitForSeconds(2f);
        zb[0].SetActive(false);
    }
}
