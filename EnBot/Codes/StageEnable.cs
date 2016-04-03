using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;                               //UGUI를 사용하기 위해 임포트

public class StageEnable : MonoBehaviour
{
    private const int maxPanels = 2;                // 현재 존재하는 스테이지 패널 수
    private const int maxButtons = 20;              // 패널에 있는 버튼 수

    [HideInInspector]
    public Transform canvasChild;
    [HideInInspector]
    public List<Button> stageBtn;
    private Button button;
    private GameObject canvas;

    private string str;                             // 유니티 계층 구조 경로를 저장하는 문자열

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        for (int j = 0; j < maxPanels; j++)
        {
            for (int i = 0; i < maxButtons; i++)
            {
                int num = (maxButtons * j) + (i + 1);       // 버튼 번호

                if (num >= 1 && num < 10)
                    str = "StagePanel_0" + (j + 1) + "/StageBtn_00" + num;
                else if (num >= 10 && num < 100)
                    str = "StagePanel_0" + (j + 1) + "/StageBtn_0" + num;
                else if (num >= 100 && num < 1000)
                    str = "StagePanel_0" + (j + 1) + "/StageBtn_" + num;
                canvasChild = canvas.transform.FindChild(str);
                button = canvasChild.gameObject.GetComponent<Button>();
                stageBtn.Add(button);

                stageBtn[num - 1].interactable = false;
            }
        }

        EnableButton();
    }

    void EnableButton()
    {
        int stage = GameManager.instance.openedStages;
        
        for (int i = 0; i < stage; i++)
        {
            stageBtn[i].interactable = true;
        }
    }
}
