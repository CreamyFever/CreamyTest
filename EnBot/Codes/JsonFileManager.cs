using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;

public class LevelList
{
    public List<LevelData> levelList = new List<LevelData>();

    public LevelList()
    {
        //levelList.Add(new LevelData(1, 3, 0, 30));
    }
}

public class JsonFileManager : MonoBehaviour {

    LevelList levelDataList = new LevelList();

    TextAsset jsonTxt;

    public LevelList search;
	
	void Start ()
    {
        //SaveJsonFile();

        LoadJsonFile();

	}

    void SaveJsonFile()
    {
        string levelDataListJson = JsonWriter.Serialize(levelDataList);

        File.WriteAllText(Application.dataPath + "/Resources/Json/levelDataTest.json", levelDataListJson);
    }

    void LoadJsonFile()
    {
        jsonTxt = Resources.Load("Json/levelDataTest") as TextAsset;
        string _info = jsonTxt.text;

        search = JsonReader.Deserialize<LevelList>(_info);
    }
}
