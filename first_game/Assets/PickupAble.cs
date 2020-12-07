using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAble : MonoBehaviour
{
    public float HeightAbovePlayer = 2.1f;      //zmienna determinujaca wysokosc obiektu nad graczem
    public float ThrowRangeHorizontal = 3f;     // zmienna determinujaca odleglosc rzutu obietku w X
    public float ThrowRangeVertical = 3f;       // zmienna determinujaca odleglosc rzutu obietku w Y

    public float Get(string what)
    {
        if (what == "Height")
        {
            return HeightAbovePlayer;
        }
        if (what == "RangeH")
        {
            return ThrowRangeHorizontal;
        }
        if (what == "RangeV")
        {
            return ThrowRangeVertical;
        }
        else
        {
            return 0f;
        }

    }
    
}