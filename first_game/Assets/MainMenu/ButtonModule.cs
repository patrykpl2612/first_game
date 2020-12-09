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
                EventSystem.current.SetSelectedGameObject(this.gameObject.transform.GetChild(firstbutton).gameObject);
            }
            else
            {
                firstbutton += 1;
                EventSystem.current.SetSelectedGameObject(this.gameObject.transform.GetChild(firstbutton).gameObject);
            }
        }
        if (Input.GetButtonDown("Up"))
        {
            if (firstbutton==0)
            {
                firstbutton = numberofbuttons-1;
                EventSystem.current.SetSelectedGameObject(this.gameObject.transform.GetChild(firstbutton).gameObject);
            }
            else
            {
                firstbutton -= 1;
                EventSystem.current.SetSelectedGameObject(this.gameObject.transform.GetChild(firstbutton).gameObject);
            }
            
        }
        if (Input.GetButton("Talk"))
        {
            if (firstbutton==0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }
    }
}
