using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// �Q�[���I�[�o�[�̃C�x���g�̓��e�������ǂ�
/// </summary>
public class GameOverEvent : MonoBehaviour
{
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
    [SerializeField]
    private SceneLoadManager _sceneRoadManager;
    private void Start()
    {
        _gameOverPanel.enabled = false;
    }
    /// <summary>
    /// �Q�[���I�[�o�[�ɂȂ����Ƃ��ɉ�ʂ̑�����ł��Ȃ����ĕ�����\������
    /// </summary>
    public void GameOverCall()
    {
        Debug.Log("�Q�[���I�[�o�[�I");
        //�Q�[���I�[�o�[��ʂƕ�����L�������ĕ\������
        _gameOverPanel.enabled = true;
        _gameOverText.enabled = true;

        //���U���g�V�[���Ɉڍs����
        _sceneRoadManager.LoadResultScene();
    }
}
