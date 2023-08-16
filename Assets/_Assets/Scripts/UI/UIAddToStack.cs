using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.UI
{
    public class UIAddToStack : MonoBehaviour
    {
        public void AddToStack()
        {
            UIManager.Instance.PushUI(gameObject);
        }

        public void RemoveFromStack()
        {
            UIManager.Instance.PopUI();
        }

        public void ClearStack()
        {
            UIManager.Instance.ClearUIStack();
        }
    }
}
