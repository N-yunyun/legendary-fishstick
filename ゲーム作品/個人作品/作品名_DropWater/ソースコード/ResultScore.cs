using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// �ŏI�X�R�A��\������
/// </summary>
public class ResultScore : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _resultScoreText;
    // Start is called before the first frame update
    void Start()
    {
        _resultScoreText = GetComponent<TextMeshProUGUI>();// TextMeshPro�R���|�[�l���g���擾
        _resultScoreText.text = ScoreDisplay.GetTotalScore.ToString();
    }
    
}
