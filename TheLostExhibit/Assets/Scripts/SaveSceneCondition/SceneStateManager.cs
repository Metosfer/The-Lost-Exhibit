using UnityEngine;
using System.IO;

public class SceneStateManager : MonoBehaviour
{
    public GameObject[] sceneObjects;

    public void SaveSceneState()
    {
        SceneState sceneState = new SceneState();

        foreach (var obj in sceneObjects)
        {
            ObjectState objState = new ObjectState
            {
                objectName = obj.name,
                isActive = obj.activeSelf
            };

            sceneState.objectsState.Add(objState);
        }

        string json = JsonUtility.ToJson(sceneState, true);
        File.WriteAllText(Application.persistentDataPath + "/sceneState.json", json);
    }

    public void LoadSceneState()
    {
        string filePath = Application.persistentDataPath + "/sceneState.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SceneState sceneState = JsonUtility.FromJson<SceneState>(json);

            foreach (var objState in sceneState.objectsState)
            {
                GameObject obj = GameObject.Find(objState.objectName);
                if (obj != null)
                {
                    obj.SetActive(objState.isActive);
                }
            }
        }
    }
}

