using UnityEngine;
using TMPro;
/// <summary>
/// ScoreFramesにアタッチするスコアを加算してスコア表示をするスクリプト
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    #region 変数
    /// <summary>
    /// ゲーム中のスコアを表示するテキスト
    /// </summary>
    [Header("ゲーム中のスコアを表示させたいテキストをセット")]
    [SerializeField]
    private TextMeshProUGUI _scoreTexts = null;
    #endregion
    private void Awake()
    {
        _scoreTexts.text = null;
    }
    /// <summary>
    /// 受け取ったスコア分加算して、画面にスコアを描画する
    /// </summary>
    /// <param name="score">加算したいスコア</param>
    public void DisplayingScore(int score)
    {
        //スコアを文字型で表記(画面にスコアが表示される)
        _scoreTexts.text = score.ToString();
    }
}
