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
    [SerializeField] private TextMeshProUGUI _resultScoreText = default;

   private void Start()
    {
        //ScoreManagerに保存されている最終スコアを文字型にして表示する
        _resultScoreText.text = ScoreManager.Instance?.CurrentScore.ToString();
    }

}
