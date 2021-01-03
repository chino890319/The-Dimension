using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    //靜態設定選單變數
    public static float audiovolume;
    public static bool Fullscreen = true;
    public static int quality = 5;
    public static int resolutionsnum = 33;
    //end
    
    //audioMixer (總音量混音控制)
    public AudioMixer audioMixer;
    //解析度UI
    public Dropdown resolutionDropdown;
    //解析度陣列
    Resolution[] resolutions;

    //計算解析度陣列(長 x 寬)
    void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0 ; i<resolutions.Length ; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width &&
            resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            } 
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutionsnum;
        resolutionDropdown.RefreshShownValue();
    }

    //設置總音量
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
        audiovolume = volume;
    }

    //設置解析度
    public void SetResolution(int resolutionIndex)
    {
        resolutionsnum = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
    
    //設置畫質
    public void SetQuality(int qualityIndex)
    {
        quality = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //設置全螢幕
    public void SetFullscreen(bool isFullscreen)
    {
        Fullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
}   