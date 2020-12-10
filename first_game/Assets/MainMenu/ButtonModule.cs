using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonModule : MonoBehaviour
{
    private int numberofbuttons;
    public int firstbutton=0;

    void Start()
    {
        numberofbuttons = this.gameObject.transform.childCount;
        EventSystem.current.SetSelectedGameObject(this.gameObject.transform.GetChild(firstbutton).gameObject);
    }
    void Update()
    {
        if (Input.GetButtonDown("Down"))
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
        if (Input.GetButtonDown("Up"))
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
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            if (firstbutton == 2) 
            {
                Application.Quit();
            }
        }
    }

    void NextButton(int x)
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject.transform.GetChild(x).gameObject);
    }
}
