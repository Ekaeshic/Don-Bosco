using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

namespace DonBosco.Dialogue
{
    public class DialogueVariables
    {
        public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

        private Story globalVariablesStory;
        private const string saveVariablesKey = "INK_VARIABLES";

        public DialogueVariables(TextAsset loadGlobalsJSON) 
        {
            // create the story
            globalVariablesStory = new Story(loadGlobalsJSON.text);
            // if we have saved data, load it
            // if (PlayerPrefs.HasKey(saveVariablesKey))
            // {
            //     string jsonState = PlayerPrefs.GetString(saveVariablesKey);
            //     globalVariablesStory.state.LoadJson(jsonState);
            // }
            
            /// Load using format of binary file
            // LoadVariables();

            // initialize the dictionary
            variables = new Dictionary<string, Ink.Runtime.Object>();
            foreach (string name in globalVariablesStory.variablesState)
            {
                Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
                variables.Add(name, value);
                //Debug.Log("Initialized global dialogue variable: " + name + " = " + value +" type: "+value.GetType());
            }
        }

        public void LoadVariables()
        {
            string path = Application.persistentDataPath + "/save.dat";
            if(System.IO.File.Exists(path))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream file = System.IO.File.Open(path, System.IO.FileMode.Open);
                string jsonState = (string)formatter.Deserialize(file);
                file.Close();
                globalVariablesStory.state.LoadJson(jsonState);
            }
        }

        public void LoadVariableString(string jsonState)
        {
            globalVariablesStory.state.LoadJson(jsonState);

            // initialize the dictionary
            variables = new Dictionary<string, Ink.Runtime.Object>();
            foreach (string name in globalVariablesStory.variablesState)
            {
                Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
                variables.Add(name, value);
                //Debug.Log("Initialized global dialogue variable: " + name + " = " + value +" type: "+value.GetType());
            }
        }

        public string SaveVariableString()
        {
            VariablesToStory(globalVariablesStory);
            return globalVariablesStory.state.ToJson();
        }

        public void SaveVariables() 
        {
            if (globalVariablesStory != null) 
            {
                // Load the current state of all of our variables to the globals story
                VariablesToStory(globalVariablesStory);
                // NOTE: eventually, you'd want to replace this with an actual save/load method
                // rather than using PlayerPrefs.
                //PlayerPrefs.SetString(saveVariablesKey, globalVariablesStory.state.ToJson());

                /// Save using format of binary file
                //Save to unity persistent data path
                string path = Application.persistentDataPath + "/save.dat";

                //Create a binary formatter which can read binary files
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                
                //JSON String into binary
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                System.IO.FileStream file = System.IO.File.Create(path);
                formatter.Serialize(file, globalVariablesStory.state.ToJson());
                file.Close();
            }
        }

        public void StartListening(Story story) 
        {
            // it's important that VariablesToStory is before assigning the listener!
            VariablesToStory(story);
            story.variablesState.variableChangedEvent += VariableChanged;
        }

        public void StopListening(Story story) 
        {
            story.variablesState.variableChangedEvent -= VariableChanged;
        }

        private void VariableChanged(string name, Ink.Runtime.Object value) 
        {
            // only maintain variables that were initialized from the globals ink file
            if (variables.ContainsKey(name)) 
            {
                variables.Remove(name);
                variables.Add(name, value);
            }
        }

        private void VariablesToStory(Story story) 
        {
            foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables) 
            {
                story.variablesState.SetGlobal(variable.Key, variable.Value);
            }
        }

    }
}
