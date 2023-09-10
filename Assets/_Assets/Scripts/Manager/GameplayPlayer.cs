using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace DonBosco
{
    public class GameplayPlayer : MonoBehaviour
    {
        private static GameplayPlayer _instance;
        public static GameplayPlayer Instance;
        [SerializeField] private CinemachineConfiner2D _confiner;


        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                Instance = _instance;
            }
            else
            {
                Destroy(gameObject);
            }
        }




        public void SetConfiner(PolygonCollider2D collider)
        {
            // Nullify the current confiner
            _confiner.m_BoundingShape2D = null;
            
            // Wait for a frame
            //yield return new WaitForEndOfFrame();

            _confiner.m_BoundingShape2D = collider;
            _confiner.InvalidateCache();
        }

        public void ResetConfiner()
        {
            _confiner.m_BoundingShape2D = null;
        }
    }
}
