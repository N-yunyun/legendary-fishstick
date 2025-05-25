using UnityEngine;
using TMPro;
/// <summary>
/// ScoreFrames�ɃA�^�b�`����X�R�A�����Z���ăX�R�A�\��������X�N���v�g
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    #region �ϐ�
    /// <summary>
    /// �Q�[�����̃X�R�A��\������e�L�X�g
    /// </summary>
    [Header("�Q�[�����̃X�R�A��\�����������e�L�X�g���Z�b�g")]
    [SerializeField]
    private TextMeshProUGUI _scoreTexts = null;
    #endregion
    private void Awake()
    {
        _scoreTexts.text = null;
    }
    /// <summary>
    /// �󂯎�����X�R�A�����Z���āA��ʂɃX�R�A��`�悷��
    /// </summary>
    /// <param name="score">���Z�������X�R�A</param>
    public void DisplayingScore(int score)
    {
        //�X�R�A�𕶎��^�ŕ\�L(��ʂɃX�R�A���\�������)
        _scoreTexts.text = score.ToString();
    }
}
