using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DonBosco.Utility
{
    public class ExecuteOnAwake : MonoBehaviour
    {
        [SerializeField] UnityEvent awakeEvent;

        private void Awake() {
            awakeEvent?.Invoke();
        }
    }
}
