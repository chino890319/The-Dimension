using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreUI : MonoBehaviour
{
    //探索UI
    public GameObject Explore;
    public GameObject[] otherUI;
    //是否觸發過
    public static bool Exploreused = false;
    public bool used = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UICD()
    {
        yield return new WaitForSeconds(5f);
        Explore.SetActive(false);
        Exploreused = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && Exploreused == false && used == false)
        {
            used = true;
            Exploreused = true;
            Explore.SetActive(true);
            for(int i = 0 ; i < 3 ; i++)
            {
                otherUI[i].SetActive(false);
            }
            StartCoroutine(UICD());
        }
    }
}
