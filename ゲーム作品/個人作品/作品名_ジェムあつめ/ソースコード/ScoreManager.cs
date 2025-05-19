using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    #region 変数
    /// <summary>
    /// スコアを表示するオブジェクトについているスクリプト
    /// </summary>
    [SerializeField] private ScoreDisplay _scoreDisplay;
    /// <summary>
    /// 定数データファイル
    /// </summary>
    [Header("定数のデータファイル(ScriptableObject)をセット")]
    [SerializeField] private ConstData _constData;
    /// <summary>
    /// 現在のスコア
    /// </summary>
    private int _score;
    /// <summary>
    ///現在のスコア
    /// </summary>
    public int CurrentScore
    {
        get { return _score; }
        private set { }
    }
    #endregion
    private void Start()
    {
        // スコアをリザルトシーンに持ち越すため、破棄しない
        DontDestroyOnLoad(gameObject);
    }
    public void AddScore(int score)
    {
        //スコアを加算する
        _score += score;

        //スコアが上限値を超えたら規制する
        if (_score >= _constData.MaxScoreLimit)
        {
            RegulatingScore();
        }

        // UIにも伝える
        _scoreDisplay?.DisplayingScore(_score); 
    }
    /// <summary>
    /// スコアに上限値を代入する
    /// </summary>
    private void RegulatingScore()
    {
        _score = _constData.MaxScoreLimit;
    }
}
