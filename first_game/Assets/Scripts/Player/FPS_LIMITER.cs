using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class FPS_LIMITER : MonoBehaviour
{
    public int FPS_LIMIT = 30;
    
    void Start()
    {
        QualitySettings.vSyncCount = 0;
    }
    void Update()
    {
        if (FPS_LIMIT != Application.targetFrameRate)
        {
            Application.targetFrameRate = FPS_LIMIT;
        }
    }
}
