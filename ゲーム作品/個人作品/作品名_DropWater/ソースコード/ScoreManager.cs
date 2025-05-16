using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    #region �ϐ�
    /// <summary>
    /// �X�R�A��\������I�u�W�F�N�g�ɂ��Ă���X�N���v�g
    /// </summary>
    [SerializeField] private ScoreDisplay _scoreDisplay;
    /// <summary>
    /// �萔�f�[�^�t�@�C��
    /// </summary>
    [Header("�萔�̃f�[�^�t�@�C��(ScriptableObject)���Z�b�g")]
    [SerializeField] private ConstData _constData;
    /// <summary>
    /// ���݂̃X�R�A
    /// </summary>
    private int _score;
    /// <summary>
    ///���݂̃X�R�A
    /// </summary>
    public int CurrentScore
    {
        get { return _score; }
        private set { }
    }
    #endregion
    private void Start()
    {
        // �X�R�A�����U���g�V�[���Ɏ����z�����߁A�j�����Ȃ�
        DontDestroyOnLoad(gameObject);
    }
    public void AddScore(int score)
    {
        //�X�R�A�����Z����
        _score += score;

        //�X�R�A������l�𒴂�����K������
        if (_score >= _constData.MaxScoreLimit)
        {
            RegulatingScore();
        }

        // UI�ɂ��`����
        _scoreDisplay?.DisplayingScore(_score); 
    }
    /// <summary>
    /// �X�R�A�ɏ���l��������
    /// </summary>
    private void RegulatingScore()
    {
        _score = _constData.MaxScoreLimit;
    }
}
