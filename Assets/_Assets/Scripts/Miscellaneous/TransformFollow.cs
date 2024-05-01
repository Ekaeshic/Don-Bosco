using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class TransformFollow : MonoBehaviour
    {
        [SerializeField] private Transform target = null;


        void Update()
        {
            transform.position = target.position;
        }
    }
}