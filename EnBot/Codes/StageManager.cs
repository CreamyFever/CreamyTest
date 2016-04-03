using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public JsonFileManager jsonFM;

    // 스테이지 레벨 데이터
    public LevelData stLevelData;

    // 맵을 구성하는 구조물 프리팹
    public GameObject[] Block;
    public GameObject[] Floor;

    // 구성요소들의 부모
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

        // JSON 파일로부터 읽어들인 스테이지 매니저의 레벨데이터를 배치
        stLevelData = jsonFM.search.levelList[lev - 1];

        GameManager.instance.leftEnergies = stLevelData.leftEnergy;
        GameManager.instance.leftTime = stLevelData.limitTime;

        // y 좌표를 반전하기 위한 지역 변수.
        // ex) 6 by 6 스테이지일 경우, 1 -> 6, 2 -> 5, ... 6 -> 1
        int toInverse = stLevelData.column - 1;

        for (int y = 0; y < stLevelData.column; y++)
        {
            for (int x = 0; x < stLevelData.row; x++)
            {
                // y 좌표 반전
                int num = stLevelData.floorData[toInverse - y][x];
                floorNode = Instantiate(Floor[num], new Vector3(x * 0.64f + 0.32f, y * 0.64f + 0.32f, 0.0f), Quaternion.identity) as GameObject;

                //새롭게 생성되는 오브젝트의 부모로 설정. 계층 구조가 어지러워지는 걸 막기 위함.
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
