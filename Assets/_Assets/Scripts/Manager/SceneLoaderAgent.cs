using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco
{
    public class SceneLoaderAgent : MonoBehaviour
    {
        [SerializeField] private List<string> scenesToLoad;
        [SerializeField] private List<string> scenesToUnload;


        public void ExecuteLoadScene()
        {
            if(scenesToUnload != null)
            {
                for(int i=0; i<scenesToUnload.Count; i++)
                {
                    SceneLoader.Instance.AddToUnload(scenesToUnload[i]);
                }
            }
            if(scenesToLoad != null)
            {
                for(int i=0; i<scenesToLoad.Count; i++)
                {
                    SceneLoader.Instance.AddToLoad(scenesToLoad[i]);
                }
            }
            SceneLoader.Instance.ExecuteLoadScene();
        }
    }
}
