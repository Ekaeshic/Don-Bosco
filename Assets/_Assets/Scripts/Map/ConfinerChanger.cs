using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class ConfinerChanger : MonoBehaviour
    {
        private PolygonCollider2D polygonCollider2D;

        private void Awake()
        {
            polygonCollider2D = GetComponent<PolygonCollider2D>();
        }

        void Start()
        {
            ChangeConfinerOnLoad();
        }



        public void ChangeConfinerOnLoad()
        {
            GameplayPlayer.Instance.SetConfiner(polygonCollider2D);
        }
    }
}
