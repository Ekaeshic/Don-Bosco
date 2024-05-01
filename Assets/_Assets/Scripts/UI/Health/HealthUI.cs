using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.UI
{
    public class HealthUI : MonoBehaviour
    {
        void OnEnable()
        {
            if(GameManager.GameMode != GameMode.Battle)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
