public class LevelData
{
    public int level;                           //レベル
    public int row, column;                     //マップに配置するオブジェクトの行と列
    public int leftEnergy;                      //マップに残ってる電池の数
    public int bestScore;                       //最高点数
    public float limitTime;                     //制限時間

    public int[][] blockData;                   //壁、電池、岩などのデータを保持するブロックデータ
    public int[][] floorData;                   //床、出口などのデータを保持するフロアデータ

    public LevelData()
    {
    }

    public LevelData(int _level, int _leftEnergy, int _bestScore, float _limitTime)
    {
        level = _level;
        row = 8;
        column = 8;
        leftEnergy = _leftEnergy;
        bestScore = _bestScore;
        limitTime = _limitTime;
    }
}
