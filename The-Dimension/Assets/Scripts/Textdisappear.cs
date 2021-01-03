using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textdisappear : MonoBehaviour
{
    public bool used = false;
    public static bool flashkey = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator CD()
    {
        yield return new WaitForSeconds(2.5f);
        this.gameObject.SetActive(false);
        used = false;
        flashkey = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf == true && used == false)
        {
            flashkey = true;
            used = true;
            StartCoroutine(CD());
        }
    }
}
