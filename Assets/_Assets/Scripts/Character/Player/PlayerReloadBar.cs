using System.Collections;
using System.Collections.Generic;
using DonBosco.Character;
using UnityEngine;
using UnityEngine.UI;

namespace DonBosco
{
    public class PlayerReloadBar : MonoBehaviour
    {
        [SerializeField] private CharacterAttack characterAttack;
        [SerializeField] private Slider slider;



        private void Update()
        {
            if(characterAttack.FireTimer <= 0f)
            {
                slider.gameObject.SetActive(false);
                return;
            }
            else
            {
                slider.gameObject.SetActive(true);
                    
                float fillAmount = characterAttack.FireTimer / characterAttack.fireDelay;
                slider.value = fillAmount;
            }
        }
    }
}
