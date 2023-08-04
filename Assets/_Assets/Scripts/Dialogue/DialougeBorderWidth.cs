using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DonBosco.Dialogue
{
    /// <summary>
    /// Handle the border width according to the text length
    /// </summary>
    public class DialougeBorderWidth : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image border;
        [Space(5)]
        [SerializeField] private float offset = 20f;
        [SerializeField] private float maxWidth = 600f;

        
        private void OnEnable()
        {
            if(DialogueManager.GetInstance() != null)
                DialogueManager.GetInstance().OnDialogueLineDisplay += UpdateBorderWidth;
        }

        private void OnDisable() 
        {
            if(DialogueManager.GetInstance() != null)
                DialogueManager.GetInstance().OnDialogueLineDisplay -= UpdateBorderWidth;
        }

        private void UpdateBorderWidth()
        {
            border.rectTransform.sizeDelta = new Vector2(text.preferredWidth + offset, border.rectTransform.sizeDelta.y);
            if(border.rectTransform.sizeDelta.x > maxWidth)
            {
                border.rectTransform.sizeDelta = new Vector2(maxWidth, border.rectTransform.sizeDelta.y);
            }
        }
    }
}
