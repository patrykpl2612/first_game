using UnityEngine;
using System.Collections;

public class MenuAppearScript : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Update()
    {
        if (Input.GetKeyDown("Pause"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}