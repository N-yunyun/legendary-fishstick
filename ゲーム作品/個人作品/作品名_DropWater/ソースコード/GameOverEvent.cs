using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// �Q�[���I�[�o�[�̃C�x���g�̓��e�������ǂ�
/// </summary>
public class GameOverEvent : MonoBehaviour
{
    /// <summary>
    /// ��ʂ��Â����邽�߂ɕK�v�ȉ摜(���̔�����)
    /// </summary>
    [Header("GameOverPanel������")]
    [SerializeField] private Image _gameOverPanel;
    /// <summary>
    /// �Q�[���I�[�o�[�̕���
    /// </summary>
    [Header("GameOverText������")]
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private SceneRoadManager _sceneRoadManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameOverPanel.enabled = false;

        #region Action�@����

        //�����̌^���w�肵�Ă��Ȃ�Action�^�̂��̂ɒǉ�����Ƃ���
        //(Action���\�b�h)���O�@+= () => {�@�ǉ��������֐���(�����Ȃ�);�@};
        //�K���u;�v�����ʓ��Ɗ��ʊO�ɂ���(�v2��)
        #endregion

    }
    /// <summary>
    /// �Q�[���I�[�o�[�ɂȂ����Ƃ��ɉ�ʂ̑�����ł��Ȃ����ĕ�����\������
    /// </summary>
    public void GameOverCall()
    {
        Debug.Log("�Q�[���I�[�o�[�I");
        _gameOverPanel.enabled = true;
        _gameOverText.enabled = true;
        _sceneRoadManager.RoadResultScene();
            }
}
