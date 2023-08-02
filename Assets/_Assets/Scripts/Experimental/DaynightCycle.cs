using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Handles the day night cycle
/// </summary>
public class DaynightCycle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimeSystem timeSystem;
    [Space((10))]
    [SerializeField] private Light2D sunLight;
    
    [Header("Settings")]
    [Tooltip("The time ratio the light is at its lowest")]
    [SerializeField] private float midnightTimeRatio = 0.1f;
    [Tooltip("The time ratio the light is at its highest")]
    [SerializeField] private float middayTimeRatio = 1f;

    
    private void Update() {
        ShiftSunLight();
    }


    private void ShiftSunLight()
    {
        sunLight.intensity = Mathf.Lerp(midnightTimeRatio, middayTimeRatio, timeSystem.DayTimeRatio);
    }
}
