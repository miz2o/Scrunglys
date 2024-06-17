using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionHandler : MonoBehaviour
{
   public TMP_Dropdown resolutionDropdown;

    public UnityEngine.Resolution[] resolutions;
    private List<UnityEngine.Resolution> filteredResolution;

    public float currentRefreshRate;
    private int currentResolutionIndex = 0;
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolution = new List<UnityEngine.Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        for(int i = 0; i < resolutions.Length; i++ )
        {
            if(resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolution.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for(int i = 0; i < filteredResolution.Count; i++)
        {
            string resolutionOPtion = filteredResolution[i].width + "x" + filteredResolution[i].height+ " "+ filteredResolution[i].refreshRateRatio + "Hz";
            options.Add(resolutionOPtion);
            if(filteredResolution[i].width == Screen.width && filteredResolution[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }


        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

   public void SetResolution(int resolutionIndex)
   {
        UnityEngine.Resolution resolution = filteredResolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
   }
}
