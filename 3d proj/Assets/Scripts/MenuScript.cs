using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    [SerializeField]
    private GameObject menuCanvas;
    void Start()
    {
        GameSettings.LoadSettings();
        GameObject.Find("MuteToggle").GetComponent<Toggle>().isOn = false;
        Time.timeScale = menuCanvas.activeInHierarchy ? 0.0f : 1.0f;
        GameObject.Find("BgSlider").GetComponent<Slider>().value = GameSettings.BackgroundVolume;

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = menuCanvas.activeInHierarchy ? 1.0f : 0.0f;
            menuCanvas.SetActive(!menuCanvas.activeInHierarchy);
        }
    }

    public void MuteChanged(bool state)
    {
        GameSettings.IsMuted = state;
    }

    public void BackgroundVolumeChanged(Single value)
    {
        GameSettings.BackgroundVolume = value;
    }
}
