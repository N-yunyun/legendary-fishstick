using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// ScoreFrames�ɃA�^�b�`����X�R�A�����Z���ăX�R�A�\��������X�N���v�g
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreTexts;
    private static int _totalScore = 0;
    public static int GetTotalScore 
    {
        get { return _totalScore; }
    }
    // Start is called before the first frame update
    void Start()
    {
        #region �����p�̃���

        //WaterCollision(�ʃX�N���v�g)�Œ�`����Action��
        //���̃X�N���v�g�̊֐��⏈����ǉ��������Ƃ��͂�������������������

        #endregion

        //�X�R�A�����Z����֐���ǉ�
        WaterCollision._onScoreAdded += AddScore;
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
        //Debug.Log(score);
        //Debug.Log(_totalScore);
        //�X�R�A�𕶎��^�ŕ\�L(��ʂɃX�R�A���\�������)
        scoreTexts.text = _totalScore.ToString();
    }
}
