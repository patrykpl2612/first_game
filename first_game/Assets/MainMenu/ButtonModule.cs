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
    }

    void FixedUpdate()
    {
        if (firstbutton == 0)
        {
            NextButton(0);
        }
    }
    void Update()
    {
        if (firstbutton == 0)
        {
            NextButton(0);
        }
        if (Input.GetButtonDown("Right") || Input.GetButtonDown("Down"))
        {
            if (firstbutton==numberofbuttons-1)
            {
                firstbutton = 0;
                NextButton(firstbutton);
            }
            else
            {
                firstbutton += 1;
                NextButton(firstbutton);
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
                firstbutton -= 1;
                NextButton(firstbutton);
            }
            
        }
        if (Input.GetButton("Talk"))
        {
            if (firstbutton==0)
            {
                if (UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0) == scene)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                }
                else
                {
                    firstbutton = 0;
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

    void NextButton(int x)
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject.transform.GetChild(x).gameObject);
    }
}
