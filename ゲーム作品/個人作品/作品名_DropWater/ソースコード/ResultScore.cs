using UnityEngine;
using TMPro;
/// <summary>
/// �ŏI�X�R�A��\������
/// </summary>
public class ResultScore : MonoBehaviour
{
    /// <summary>
    /// �ŏI�X�R�A��\������e�L�X�g
    /// </summary>
    [Header("�ŏI�X�R�A��\�����������e�L�X�g���Z�b�g")]
    [SerializeField] private TextMeshProUGUI _resultScoreText;
    void Start()
    {
        //TextMeshPro���擾���āA�ŏI�X�R�A�𕶎��^�ɂ��ĕ\������
        _resultScoreText.text = ScoreDisplay.GetTotalScore.ToString();
    }

}
