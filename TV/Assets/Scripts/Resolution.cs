using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Dropdown res;
    UnityEngine.Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;

        res.ClearOptions();

        List<string> options = new List<string>();

        int cur = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
            {
                cur = i;
            }
        }
        res.AddOptions(options);
        res.value = cur;
        res.RefreshShownValue();

    }

    // Update is called once per frame
    public void SetResolution(int index)
    {
        UnityEngine.Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
