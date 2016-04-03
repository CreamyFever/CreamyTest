public class LevelData
{
    public int level;                           //스테이지의 레벨
    public int row, column;                     //블록 행,열
    public int leftEnergy;                      //맵 상에 남아있는 목표물(에너지 캔)
    public int bestScore;                       //최고 점수(미구현)
    public float limitTime;                     //제한 시간

    public int[][] blockData;                   //블록 데이터
    public int[][] floorData;                   //바닥 데이터

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
