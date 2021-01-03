using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    //玩家text
    public GameObject Playertext;
    //殺手text
    public GameObject Killertext;
    //殺手text是否觸發過
    public bool Killertextbool = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayertextCD());
    }

    // Update is called once per frame
    void Update()
    {
        if(Killerappear.Camerabool == true && Killertextbool == false)
        {
            Killertextbool = true;
            Playertext.SetActive(false);
            Killertext.SetActive(true);
            StartCoroutine(KillertextCD());
        }   
    }

    IEnumerator PlayertextCD()
    {
        yield return new WaitForSeconds(3f);
        Playertext.SetActive(true);
        yield return new WaitForSeconds(3f);
        Playertext.SetActive(false);
    }

    IEnumerator KillertextCD()
    {
        yield return new WaitForSeconds(2.5f);
        Killertext.SetActive(false);
    }
}
