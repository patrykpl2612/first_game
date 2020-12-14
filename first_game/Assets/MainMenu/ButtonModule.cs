using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonModule : MonoBehaviour
{
    private int numberofbuttons;
    private int firstbutton;
    private Scene scene;
    private GameObject Player;
    

    void Start()
    {
        numberofbuttons = this.gameObject.transform.childCount;
        scene = SceneManager.GetActiveScene();
        Player = GameObject.Find("/Player");
        firstbutton = 0;
        EscapePressed();
    }
    
    void OnEnable()
    {
        EscapePressed();
       
    }
    
    void Update()
    {

        if (Input.GetButtonDown("Right") || Input.GetButtonDown("Down"))
        {
            if (firstbutton==numberofbuttons-1)
            {
                firstbutton = 0;
                NextButton(firstbutton);
            }
            else
            {
                NextButton(++firstbutton);
            }
        }

        if (Input.GetButtonDown("Left") || Input.GetButtonDown("Up"))
        {
            if (firstbutton==0)
            {
                firstbutton = numberofbuttons-1;
                NextButton(firstbutton);
            }
            else
            {
                NextButton(--firstbutton);
            }
            
        }
        if (Input.GetButtonDown("Talk"))
        {
            if (firstbutton==0)
            {
                if (UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0) == scene)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                }
                else
                {
                    Player.GetComponent<PlayerMenu>().ShowMenu();
                }
            }
            if (firstbutton == 2) 
            {
               
                if (UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0) == scene)
                {
                    Application.Quit();
                }
                else
                {
                    Player.GetComponent<PlayerMenu>().togglePause();
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                    
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
}
