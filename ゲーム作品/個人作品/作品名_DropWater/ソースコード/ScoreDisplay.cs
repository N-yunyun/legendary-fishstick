using UnityEngine;
using TMPro;
/// <summary>
/// ScoreFrames�ɃA�^�b�`����X�R�A�����Z���ăX�R�A�\��������X�N���v�g
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    /// <summary>
    /// �Q�[�����̃X�R�A��\������e�L�X�g
    /// </summary>
    [Header("�Q�[�����̃X�R�A��\�����������e�L�X�g���Z�b�g")]
    [SerializeField]
    private TextMeshProUGUI scoreTexts;
    private static int _totalScore = 0;
    /// <summary>
    /// �Q�[�����̃X�R�A��\������e�L�X�g
    /// </summary>
    public static int GetTotalScore
    {
        get { return _totalScore; }
    }
    void Start()
    {
        //�X�R�A�����Z����֐���ǉ�
        WaterCollision.OnScoreAdded += AddScore;
        //�X�R�A������
        AddScore(0);
    }
    /// <summary>
    /// �󂯎�����X�R�A�����Z���āA��ʂɃX�R�A��`�悷��
    /// </summary>
    /// <param name="score">���Z�������X�R�A</param>

    private void AddScore(int score)
    {
        //�X�R�A�����Z����
        _totalScore += score;
        //�X�R�A�𕶎��^�ŕ\�L(��ʂɃX�R�A���\�������)
        scoreTexts.text = _totalScore.ToString();
    }
}
