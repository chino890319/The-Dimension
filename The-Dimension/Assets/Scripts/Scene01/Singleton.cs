using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    private static Singleton _singleton = null;
    public static Singleton singleton
    {
        get
        {
            if(_singleton == null)
            {
                _singleton = new Singleton();
            }
            return _singleton;
        }
    }

    void Awake()
    {
        if(_singleton != null)
        {
            return;
        }

        _singleton = this;
        GameObject.DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
