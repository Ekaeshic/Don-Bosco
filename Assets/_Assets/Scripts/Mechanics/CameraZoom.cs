using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace DonBosco
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] CinemachineConfiner2D confiner;
        CinemachineVirtualCamera vcam;


        void Start()
        {
            vcam = GetComponent<CinemachineVirtualCamera>();
        }



        void Update()
        {
            float scroll = InputManager.Instance.GetScrollWheelValue();
            if(scroll != 0)
            {
                // check if current camera size is within confiner bounds
                if(confiner.m_BoundingShape2D.OverlapPoint(vcam.transform.position))
                {
                    vcam.m_Lens.OrthographicSize += scroll;
                    confiner.InvalidateCache();
                }
                else
                {
                    // if not, check if the new size is within bounds
                    float newSize = vcam.m_Lens.OrthographicSize + scroll;
                    if(confiner.m_BoundingShape2D.OverlapPoint(vcam.transform.position))
                    {
                        vcam.m_Lens.OrthographicSize = newSize;
                        confiner.InvalidateCache();
                    }
                }
            }
        }
    }
}