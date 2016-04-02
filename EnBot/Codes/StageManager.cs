using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public JsonFileManager jsonFM;

    // ステージのレベルデータ
    public LevelData stLevelData;

    // マップに配置するプレハブの集合
    public GameObject[] Block;      // 壁、電池、岩など
    public GameObject[] Floor;      // 床、出口など

    // 親オブジェクトのトランスフォーム
    public Transform stageHolder;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        jsonFM = GameObject.Find("GameManager").GetComponent<JsonFileManager>();
    }

    void StageSetup(int _level)
    {
        stageHolder = new GameObject("Stage").transform;
        GameObject floorNode;

        int lev = _level;

        // JSONを読み込み、オブジェクトを配置。
        stLevelData = jsonFM.search.levelList[lev - 1];

        GameManager.instance.leftEnergies = stLevelData.leftEnergy;
        GameManager.instance.leftTime = stLevelData.limitTime;

        // y座標を変換させるためのローカル変数。
        // ex) 6 by 6の場合、1 -> 6、2 -> 5、...、6 -> 1
        int toInverse = stLevelData.column - 1;

        for (int y = 0; y < stLevelData.column; y++)
        {
            for (int x = 0; x < stLevelData.row; x++)
            {
                // y座標を反転。
                int num = stLevelData.floorData[toInverse - y][x];
                floorNode = Instantiate(Floor[num], new Vector3(x * 0.64f + 0.32f, y * 0.64f + 0.32f, 0.0f), Quaternion.identity) as GameObject;

                //ヒエラルキーが乱れることを防ぐため、オブジェクトの親をセット。
                floorNode.transform.SetParent(stageHolder);
            }
        }
    }

    void LayoutObject(int lev, GameObject[] _objArr)
    {
        int toInverse = stLevelData.column - 1;

        for (int y = 0; y < stLevelData.column; y++)
        {
            for (int x = 0; x < stLevelData.row; x++)
            {
                int num = stLevelData.blockData[toInverse - y][x];
                Instantiate(_objArr[num], new Vector3(x * 0.64f + 0.32f, y * 0.64f + 0.32f, -0.01f), Quaternion.identity);
            }
        }
    }

    public void SetupScene(int _lev)
    {
        StageSetup(_lev);
        LayoutObject(_lev, Block);
    }
}
