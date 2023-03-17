using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnvironmentLight : MonoBehaviour
{
    [SerializeField]
    public Light2D WallLights;
    [SerializeField]
    public Light2D FloorLights;

    public void SetLights(int level)
    {
        SetWallsLight(level);
        SetFloorLight(level);
    }

    public void SetWallsLight(int level)
    {
        switch (level)
        {
            case 0: WallLights.color = new Color32(37, 62, 214,255);break;
            case 1: WallLights.color = new Color32(171, 83, 43,255); break;
        }
        
    }

    public void SetFloorLight(int level)
    {
        switch (level)
        {
            case 0: FloorLights.color = new Color32(31, 31, 31,255); break;
            case 1: FloorLights.color = new Color32(52, 31, 5,255); break;
        }
        
    }

}
