using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public float volume;
    public Slider slider;

    private int numberofbuttons;
    private int firstbutton;
    private Scene scene;
    private int qualityLevel;
    private bool isFullscreened = true;
    public GameObject Player;
    public GameObject menuObject;
    public GameObject settingsObject;
    public Text fullscreenText;
    public Text qualityText;

    void Start()
    {
        numberofbuttons = this.gameObject.transform.childCount;
        scene = SceneManager.GetActiveScene();
        firstbutton = 0;
        EscapePressed();
        qualityLevel = QualitySettings.GetQualityLevel();
    }

    void OnEnable()
    {
        EscapePressed();

    }

    void Update()
    {

        if (Input.GetButtonDown("Down"))
        {
            if (firstbutton == numberofbuttons - 1)
            {
                firstbutton = 0;
                NextButton(firstbutton);
            }
            else
            {
                NextButton(++firstbutton);
            }
        }

        if (Input.GetButtonDown("Up"))
        {
            if (firstbutton == 0)
            {
                firstbutton = numberofbuttons - 1;
                NextButton(firstbutton);
            }
            else
            {
                NextButton(--firstbutton);
            }

        }

        if (firstbutton == 0)
        {
            if (Input.GetButtonDown("Talk"))
            {
                isFullscreened = !isFullscreened;
                Screen.fullScreen = isFullscreened;
                if(isFullscreened) fullscreenText.text = "Fullscreen: ON";
                else fullscreenText.text = "Fullscreen: OFF";
            }
        }

        if (firstbutton == 1)
        {
            if (Input.GetButtonDown("Talk"))
            {
                qualityLevel++;
                if (qualityLevel > 2) qualityLevel = 0;
                SetQuality(qualityLevel);
                switch (qualityLevel)
                {
                    case 0: 
                        qualityText.text = "Graphics: LOW";
                        break;
                    case 1: 
                        qualityText.text = "Graphics: MEDIUM";
                        break;
                    case 2: 
                        qualityText.text = "Graphics: HIGH";
                        break;
                }
            }
        }

        if (firstbutton == 2)
        {

            if (Input.GetButtonDown("Left"))
            {
                slider.value -= 5f;
                volume = slider.value;
                audioMixer.SetFloat("volume", volume);
            }

            if (Input.GetButtonDown("Right"))
            {
                slider.value += 5f;
                volume = slider.value;
                audioMixer.SetFloat("volume", volume);
            }
        }

        if (firstbutton == 3) //close settings
        {
            if (Input.GetButtonDown("Talk"))
            {
                settingsObject.SetActive(false);
                menuObject.SetActive(true);
            }
        }


    }




    public void NextButton(int x)
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject.transform.GetChild(x).gameObject);
    }
    public void EscapePressed()
    {
        EventSystem.current.SetSelectedGameObject(null);
        firstbutton = 0;
        NextButton(0);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


}
