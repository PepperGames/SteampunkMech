using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


namespace SaveLoadSystem
{
    public interface ISaveComponent
    {
        int GetSaveComponentID();
        string SaveData();
        void LoadData(string data);
    }

    public static class SaveLoadManager
    {
        
        public static IEnumerator SaveGameCoroutine(Action<float> onProgress)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string encryptedScene = EncryptDecrypt(currentScene);
            File.WriteAllText(ConstantsHolder.SCENE_SAVEFILE_PATH, encryptedScene);
            yield return null;

            List<string> saveStrings = new List<string>();
            var saveComponents = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveComponent>().ToList();

            for (int i = 0; i < saveComponents.Count; i++)
            {
                ISaveComponent saveComponent = saveComponents[i];
                int componentID = saveComponent.GetSaveComponentID();
                string dataString = saveComponent.SaveData();
                saveStrings.Add(componentID + ConstantsHolder.SAVEFILE_DEVIDER + dataString);

                if (onProgress != null)
                    onProgress(i / (float)saveComponents.Count);
                yield return null;
            }

            List<string> encryptedSaveStrings = saveStrings.Select(s => EncryptDecrypt(s)).ToList();
            File.WriteAllLines(ConstantsHolder.SAVEFILE_PATH, encryptedSaveStrings);

            if (onProgress != null)
                onProgress(1f);
        }

        public static IEnumerator LoadGameCoroutine(Action<float> onProgress)
        {
            if (File.Exists(ConstantsHolder.SCENE_SAVEFILE_PATH))
            {
                string encryptedScene = File.ReadAllText(ConstantsHolder.SCENE_SAVEFILE_PATH);
                string savedScene = EncryptDecrypt(encryptedScene);

                if (SceneManager.GetActiveScene().name != savedScene)
                {
                    yield return SceneManager.LoadSceneAsync(savedScene);
                }
            }
            
            if (File.Exists(ConstantsHolder.SAVEFILE_PATH))
            {
                string[] encryptedSaveStrings = File.ReadAllLines(ConstantsHolder.SAVEFILE_PATH);
                string[] saveStrings = encryptedSaveStrings.Select(s => EncryptDecrypt(s)).ToArray();

                for (int i = 0; i < saveStrings.Length; i++)
                {
                    string[] parts = saveStrings[i].Split(ConstantsHolder.SAVEFILE_DEVIDER);
                    if (parts.Length != 2) continue;

                    int componentID = int.Parse(parts[0]);
                    string dataString = parts[1];

                    foreach (ISaveComponent saveComponent in Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveComponent>())
                    {
                        if (saveComponent.GetSaveComponentID() == componentID)
                        {
                            saveComponent.LoadData(dataString);
                            break;
                        }
                    }

                    if (onProgress != null)
                        onProgress(i / (float)saveStrings.Length);
                    yield return null;
                }
            }
            
            if (onProgress != null)
                onProgress(1f);
            
        }
        
        private static string EncryptDecrypt(string data, int key = 128)
        {
            char[] buffer = data.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] ^= (char)key;
            }
            return new string(buffer);
        }
    }
}
