using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Analytics
{
    public class PlaytimeCounter : MonoBehaviour
    {
        private void Update()
        {
            Analytic.Instance.timeSpentInGame += Time.deltaTime;
        }
    }
}
