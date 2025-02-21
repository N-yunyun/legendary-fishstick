using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// 最終スコアを表示する
/// </summary>
public class ResultScore : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _resultScoreText;
    // Start is called before the first frame update
    void Start()
    {
        _resultScoreText = GetComponent<TextMeshProUGUI>();// TextMeshProコンポーネントを取得
        _resultScoreText.text = ScoreDisplay.GetTotalScore.ToString();
    }
    
}
