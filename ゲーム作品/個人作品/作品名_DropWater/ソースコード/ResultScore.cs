using UnityEngine;
using TMPro;
/// <summary>
/// 最終スコアを表示する
/// </summary>
public class ResultScore : MonoBehaviour
{
    /// <summary>
    /// 最終スコアを表示するテキスト
    /// </summary>
    [Header("最終スコアを表示させたいテキストをセット")]
    [SerializeField] private TextMeshProUGUI _resultScoreText;
    void Start()
    {
        //TextMeshProを取得して、最終スコアを文字型にして表示する
        _resultScoreText.text = ScoreDisplay.GetTotalScore.ToString();
    }

}
