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
            Debug.Log("HealthUI enabled, game mode: " + GameManager.GameMode);
            if(GameManager.GameMode != GameMode.Battle)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
