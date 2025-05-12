using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// �Q�[���I�[�o�[�̃C�x���g�̓��e�������ǂ�
/// </summary>
public class GameOverEvent : MonoBehaviour
{
    #region �ϐ�
    /// <summary>
    /// ��ʂ��Â����邽�߂ɕK�v�ȉ摜
    /// </summary>
    [Header("GameOverPanel���Z�b�g")]
    [SerializeField] private Image _gameOverPanel;
    /// <summary>
    /// �Q�[���I�[�o�[�̕���
    /// </summary>
    [Header("GameOverText���Z�b�g")]
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [Header("SceneLoadManager���Z�b�g")]
    [SerializeField]
    private SceneLoadManager _sceneLoadManager;
    #endregion
    private void Start()
    {
        _gameOverPanel.enabled = false;
    }
    /// <summary>
    /// �Q�[���I�[�o�[�ɂȂ����Ƃ��ɉ�ʂ̑�����ł��Ȃ����ĕ�����\������
    /// </summary>
    public void CallGameOver()
    {
        //�Q�[���I�[�o�[��ʂƕ�����L�������ĕ\������
        _gameOverPanel.enabled = true;
        _gameOverText.enabled = true;

        //���U���g�V�[���Ɉڍs����
        _sceneLoadManager.LoadResultScene();
    }
}
