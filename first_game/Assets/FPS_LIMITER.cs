using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class FPS_LIMITER : MonoBehaviour
{
    public int FPS_LIMIT = 30;
    
    void Awake()
    {
        Application.targetFrameRate = FPS_LIMIT;  
    }

}
