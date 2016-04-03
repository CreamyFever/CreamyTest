using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int level;
    public int leftEnergies;
    public float leftTime;
    public int openedStages = 1;                    // 초기에 열려있는 스테이지는 1개

    public bool canControlPlayer = true;            // 플레이어 컨트롤 가능여부

    private float timeSwitch = 0.0f;                // 제한 시간을 멈출 때 0, 감소시킬 때 1


    void Awake ()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update ()
    {
        if(leftTime > 0.0f)
            leftTime -= timeSwitch * Time.deltaTime;
    }

    void OnLevelWasLoaded (int index)
    {
        level += 1;
        if (index == 1)                     // 스테이지 선택 씬에서
            timeSwitch = 0.0f;

        else if (index == 2)                // 인게임 씬에서
            InitGame();
    }

    void InitGame ()
    {
        timeSwitch = 1.0f;
        StageManager.instance.SetupScene(level);
    }
}
