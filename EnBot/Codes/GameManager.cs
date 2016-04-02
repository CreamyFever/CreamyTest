using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int level;
    public int leftEnergies;
    public float leftTime;
    public int openedStages = 1;                    // 最初に開いてるレベル。レベル1以外のボタンは押せない。

    public bool canControlPlayer = true;            // プレイヤーのコントロール可能か不可能か。

    private float timeSwitch = 0.0f;                // 制限時間の流れを止める時は0.0、減らす時は1.0。


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
        DontDestroyOnLoad(gameObject);              // オブジェクトが破壊されないようにする。
    }

    void Update ()
    {
        if(leftTime > 0.0f)
            leftTime -= timeSwitch * Time.deltaTime;
    }

    void OnLevelWasLoaded (int index)
    {
        level += 1;
        if (index == 1)                     // ステージ選択のシーンで
            timeSwitch = 0.0f;

        else if (index == 2)                // ゲームプレイのシーンで
            InitGame();
    }

    void InitGame ()
    {
        timeSwitch = 1.0f;
        StageManager.instance.SetupScene(level);
    }
}
