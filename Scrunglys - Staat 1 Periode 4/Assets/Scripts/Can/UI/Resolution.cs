using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Resolution : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    public UnityEngine.Resolution[] resolutions;
    private List<Resolution> filteredResolution;

    public float currentRefreshRate;
    private int currentResolutionIndex = 0;
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolution = new List<Resolution>();

        resolutionDropdown.ClearOptions();
    }

    
}
