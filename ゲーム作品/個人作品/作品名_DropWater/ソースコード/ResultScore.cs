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
    [SerializeField] private TextMeshProUGUI _resultScoreText = default;

   private void Start()
    {
        //ScoreManager�ɕۑ�����Ă���ŏI�X�R�A�𕶎��^�ɂ��ĕ\������
        _resultScoreText.text = ScoreManager.Instance?.CurrentScore.ToString();
    }

}
