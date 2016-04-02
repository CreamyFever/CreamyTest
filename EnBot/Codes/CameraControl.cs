using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public Transform player;                    // ターゲットとなるプレイヤー

    // マージン, カメラが追ってくるスピード
    public Vector2 margin, smoothing;

    [HideInInspector]
    public Vector3 min, max;                    // マップの左下と右上の座標
    private const float mulFloat = 0.64f;       // グリッドのサイズ
    private int row, col;                       // 行と列に配置するセルの数
    private float mapWidth, mapHeight;          // マップの横と縦の幅

    public Camera mainCam;                      // メインカメラ

    public void Start()
    {
        int rowOffset, colOffset;                           // オフセット
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
        
        // カメラビューの横の半分
        float cameraHalfWidth = mainCam.orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + mainCam.orthographicSize, max.y - mainCam.orthographicSize);

        transform.position = new Vector3(x, y, 0);
    }
}
