using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ObjectState
{
    public string objectName;
    public bool isActive;
}

[System.Serializable]
public class SceneState
{
    public List<ObjectState> objectsState = new List<ObjectState>();
}
