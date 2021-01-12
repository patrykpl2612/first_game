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
    public GameObject Player;
    public GameObject menuObject;
    public GameObject settingsObject;


    void Start()
    {
        numberofbuttons = this.gameObject.transform.childCount;
        scene = SceneManager.GetActiveScene();
        firstbutton = 0;
        EscapePressed();
    }

    void OnEnable()
    {
        EscapePressed();

    }

    void Update()
    {
        if (Input.GetButtonDown("Left")) Debug.Log("LEWO KURWA");

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
        if (Input.GetButtonDown("Talk"))
        {
            //if (firstbutton == 0)
            //{
            //    if (Input.GetButtonDown("Talk")) ;
            //}

            //if (firstbutton == 1)
            //{
            //    if (Input.GetButtonDown("Talk")) ;
            //}

            if (firstbutton == 2)
            {

                if (Input.GetButtonDown("Talk"))
                {
                    Debug.Log(slider.value);
                    slider.value -= 5f;
                    volume = slider.value;
                    audioMixer.SetFloat("volume", volume);
                }
                
                if (Input.GetButtonDown("Use"))
                {
                    Debug.Log(slider.value);
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

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

  
}
