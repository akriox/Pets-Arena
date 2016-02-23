using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour {

    public Dropdown dropDownResolution;
    public Dropdown dropDownQuality;
    public Toggle toggleFullscreen;

    int i;
    private Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions;
    
        for(i = 0; i < resolutions.Length; i++)
        {
            dropDownResolution.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
        }

        for(i = 0; i < QualitySettings.names.Length; i++)
        {
            dropDownQuality.options.Add(new Dropdown.OptionData(QualitySettings.names[i]));
        }

        toggleFullscreen.isOn = Screen.fullScreen;
    }

    public void ApplySettings()
    {
        Screen.SetResolution(resolutions[dropDownResolution.value].width, resolutions[dropDownResolution.value].height, toggleFullscreen.isOn);
        QualitySettings.SetQualityLevel(dropDownQuality.value);
    }

    private string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}