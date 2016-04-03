using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Transform player;                    // 따라갈 플레이어

    // 여백, 카메라가 쫓아오는 속도
    public Vector2 margin, smoothing;

    [HideInInspector]
    public Vector3 min, max;                    // 맵의 최소(왼쪽 하단), 최대 좌표(오른쪽 상단)
    private const float mulFloat = 0.64f;       // 블록 한 개의 사이즈.
    private int row, col;                       // 스테이지 매니저로부터 받아올 행과 열 수
    private float mapWidth, mapHeight;          // 맵의 가로세로 길이

    public Camera mainCam;                      // 메인 카메라

    public void Start()
    {
        int rowOffset, colOffset;                           // 보정값
        player = GameObject.Find("Enbot(Clone)").transform;
        row = StageManager.instance.stLevelData.row;
        col = StageManager.instance.stLevelData.column;

        if (row >= 6 && row < 12)
        {
            rowOffset = 13 - row;
        }
        else
        {
            rowOffset = 1;
        }

        if (col >= 6 && col < 8)
        {
            colOffset = 2;
        }
        else
        {
            colOffset = 1;
        }

        mapWidth = (row + rowOffset) * mulFloat;
        mapHeight = (col + colOffset) * mulFloat;

        max = new Vector3(mapWidth, mapHeight, 1.0f);
        min = new Vector3(-mulFloat * rowOffset, -mulFloat * colOffset, 1.0f);
    }

    public void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (Mathf.Abs(x - player.position.x) > margin.x)
            x = Mathf.Lerp(x, player.position.x, smoothing.x * Time.deltaTime);
        if (Mathf.Abs(y - player.position.y) > margin.y)
            y = Mathf.Lerp(y, player.position.y, smoothing.y * Time.deltaTime);
        
        // 카메라 화면 가로 길이의 절반
        float cameraHalfWidth = mainCam.orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + mainCam.orthographicSize, max.y - mainCam.orthographicSize);

        transform.position = new Vector3(x, y, 0);
    }
}
