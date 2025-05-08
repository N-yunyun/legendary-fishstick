using UnityEngine;
using TMPro;
/// <summary>
/// ScoreFramesにアタッチするスコアを加算してスコア表示をするスクリプト
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    /// <summary>
    /// ゲーム中のスコアを表示するテキスト
    /// </summary>
    [Header("ゲーム中のスコアを表示させたいテキストをセット")]
    [SerializeField]
    private TextMeshProUGUI scoreTexts;
    private static int _totalScore = 0;
    /// <summary>
    /// ゲーム中のスコアを表示するテキスト
    /// </summary>
    public static int GetTotalScore
    {
        get { return _totalScore; }
    }
    void Start()
    {
        //スコアを加算する関数を追加
        WaterCollision.OnScoreAdded += AddScore;
        //スコア初期化
        AddScore(0);
    }
    /// <summary>
    /// 受け取ったスコア分加算して、画面にスコアを描画する
    /// </summary>
    /// <param name="score">加算したいスコア</param>

    private void AddScore(int score)
    {
        //スコアを加算する
        _totalScore += score;
        //スコアを文字型で表記(画面にスコアが表示される)
        scoreTexts.text = _totalScore.ToString();
    }
}
