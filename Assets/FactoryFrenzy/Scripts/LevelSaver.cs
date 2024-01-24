using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelSaver : MonoBehaviour
{
    public string layerName = "Grabbable";

    [System.Serializable]
    public class GameObjectInfo
    {
        public string id;
        public string name;
        public string position;
        public string rotation;
        public string scale;
    }

    [System.Serializable]
    public class GameObjectInfoList
    {
        public List<GameObjectInfo> objects = new List<GameObjectInfo>();
    }

    void Start()
    {
        Debug.Log("Start of LevelSaver script");
        var allObjects = FindObjectsOfType<GameObject>();
        GameObjectInfoList infoList;

        if (File.Exists("levelObjects.json"))
        {
            var jsonString = File.ReadAllText("levelObjects.json");
            infoList = JsonUtility.FromJson<GameObjectInfoList>(jsonString);
        }
        else
        {
            infoList = new GameObjectInfoList();
        }

        Debug.Log("Number of all objects: " + allObjects.Length);
        foreach (var obj in allObjects)
        {
            if (obj.layer == LayerMask.NameToLayer(layerName) && obj.activeInHierarchy)
            {
                AddGameObjectInfo(obj, infoList.objects);
            }
        }

        var jsonStringOut = JsonUtility.ToJson(infoList);
        File.WriteAllText("levelObjects.json", jsonStringOut);
        Debug.Log("JSON file written with " + infoList.objects.Count + " objects");
    }

    void AddGameObjectInfo(GameObject obj, List<GameObjectInfo> infoList)
    {
        Debug.Log("Adding info for GameObject: " + obj.name);
        var info = new GameObjectInfo
        {
            id = obj.GetInstanceID().ToString(),
            name = obj.name,
            position = obj.transform.position.ToString(),
            rotation = obj.transform.rotation.ToString(),
            scale = obj.transform.localScale.ToString(),
        };

        infoList.Add(info);

        foreach (Transform child in obj.transform)
        {
            if (child.gameObject.layer == LayerMask.NameToLayer(layerName) && child.gameObject.activeInHierarchy)
            {
                AddGameObjectInfo(child.gameObject, infoList);
            }
        }
    }
}
