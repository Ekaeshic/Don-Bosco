using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobShadow2D : MonoBehaviour
{
    public float SunHeight = 0.5f;
    public AnimationCurve ShadowAngle = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve ShadowLength = AnimationCurve.Linear(0, 1, 1, 1);
    public List<GameObject> m_Shadows = new List<GameObject>();
    [Header("References")]
    [SerializeField] private TimeSystem timeSystem;


    private void Awake() 
    {
        //Get every child of this object to add to the list
        foreach(Transform child in transform)
        {
            m_Shadows.Add(child.gameObject);
        }
        UpdateShadow(timeSystem.DayTimeRatio);
    }

    private void Update() 
    {
        UpdateShadow(timeSystem.DayTimeRatio);
    }


    void UpdateShadow(float ratio)
    {
        float currentShadowAngle = ShadowAngle.Evaluate(ratio);
        float currentShadowLength = ShadowLength.Evaluate(ratio);

        var opposedAngle = currentShadowAngle + 0.5f;
        while(currentShadowAngle > 1f)
        {
            currentShadowAngle -= 1f;
        }

        Vector3 lightDirection = Quaternion.Euler(SunHeight*90f, 0, 0) * Vector3.up;
        lightDirection = Quaternion.Euler(0, 0, opposedAngle * 360f) * lightDirection;

        Shader.SetGlobalVector("_LightDirection", lightDirection.normalized);

        foreach(var shadow in m_Shadows)
        {
            Transform t = shadow.transform;
            //use 1.0-angle so that the angle clo
            t.eulerAngles = new Vector3(0, 0, currentShadowAngle * 360f);
            t.localScale = new Vector3(currentShadowLength, currentShadowLength, 1);
        }
    }
}
