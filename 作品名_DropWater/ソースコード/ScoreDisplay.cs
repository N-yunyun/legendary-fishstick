using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// ScoreFramesにアタッチするスコアを加算してスコア表示をするスクリプト
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreTexts;
    private static int _totalScore = 0;
    public static int GetTotalScore 
    {
        get { return _totalScore; }
    }
    // Start is called before the first frame update
    void Start()
    {
        #region 自分用のメモ

        //WaterCollision(別スクリプト)で定義したActionに
        //他のスクリプトの関数や処理を追加したいときはこういう書き方をする

        #endregion

        //スコアを加算する関数を追加
        WaterCollision._onScoreAdded += AddScore;
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
        //Debug.Log(score);
        //Debug.Log(_totalScore);
        //スコアを文字型で表記(画面にスコアが表示される)
        scoreTexts.text = _totalScore.ToString();
    }
}
