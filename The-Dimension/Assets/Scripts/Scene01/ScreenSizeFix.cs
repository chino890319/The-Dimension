using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 resolution = transform.root.GetComponent<CanvasScaler>().referenceResolution;
        RectTransform rect = GetComponent<RectTransform>();
        float ratio = (Screen.width * resolution.y) / (Screen.height * resolution.x);
        Debug.Log(ratio);
        rect.sizeDelta *= ratio;
    }
}
