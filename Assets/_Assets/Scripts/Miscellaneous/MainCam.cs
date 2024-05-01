using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace DonBosco
{
    public class MainCam : MonoBehaviour
    {
        private static MainCam _instance;
        public static MainCam Instance { get { return _instance; } }

        private CinemachineBrain brain;


        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            brain = GetComponent<CinemachineBrain>();
        }



        public void GetCinemachineBrain(out CinemachineBrain brain)
        {
            brain = this.brain;
        }
    }

}