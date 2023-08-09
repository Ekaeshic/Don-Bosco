using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DonBosco.SaveSystem;
using UnityEngine;

namespace DonBosco
{
    public class SceneLoader : MonoBehaviour, ISaveLoad
    {
        private static SceneLoader instance;
        public static SceneLoader Instance { get { return instance; } }
        
        [SerializeField] private string startingScene;

        private string currentScene;
        private LoadScene loadScene;



        private void Awake() 
        {
            instance = this;

            loadScene = GetComponent<LoadScene>();
        }

        private void OnEnable() 
        {
            SaveManager.Instance.Subscribe(this);
        }

        private void OnDisable() 
        {
            SaveManager.Instance.Unsubscribe(this);
        }



        public void LoadCurrentScene()
        {
            if(currentScene == null)
            {
                currentScene = startingScene;
            }
            AddToLoad(currentScene);
            ExecuteLoadScene();
        }

        public void AddToLoad(string sceneName)
        {
            loadScene.AddToLoad(sceneName);
        }

        public void AddToUnload(string sceneName)
        {
            loadScene.AddToUnload(sceneName);
        }

        public void ExecuteLoadScene()
        {
            loadScene.ExecuteLoadScene();
        }



        public async Task Save(SaveData saveData)
        {
            saveData.currentScene = currentScene;
            await Task.CompletedTask;
        }

        public async Task Load(SaveData saveData)
        {
            if(saveData == null)
                return;
                
            currentScene = saveData.currentScene;
            await Task.CompletedTask;
        }
    }
}