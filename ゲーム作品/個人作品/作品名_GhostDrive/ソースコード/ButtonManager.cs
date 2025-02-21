using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �V�[����ɑ��݂���{�^���̃C�x���g���Ǘ�����
/// </summary>
public class ButtonManager : SceneChangeManager {
    /*    /// <summary>
        /// ���͂��Ǘ�����R���|�[�l���g
        /// </summary>
        [SerializeField]private InputManager _inputManager;
    */
    [SerializeField] private bool _isMoveForward = false;
    [SerializeField] private bool _isMoveBack = false;
    [SerializeField] private GameObject _pauseImages;
    private ColorBlock _autoButtonColorBlock;
    /// <summary>
    /// �I�[�g���̐F�i�ԁj
    /// </summary>
    private Color _activeColor = Color.red;
    /// <summary>
    /// �ʏ펞�̐F�i���j
    /// </summary>
    private Color _defaultColor = Color.white;

    /// <summary>
    /// �I�[�g�A�N�Z���ɂȂ��Ă��邩
    /// </summary>
    private bool _isAutoAccelerate = false;
    /// <summary>
    /// �O�i�{�^����������Ă��邩
    /// </summary>
    public bool IsMoveForward {
        get {

            return _isMoveForward;
        }
    }
    /// <summary>
    /// ��i�{�^����������Ă��邩
    /// </summary>
    public bool IsMoveBack {
        get {
            return _isMoveBack;
        }
    }
    private void Start() {

        //�|�[�Y��ʂ𖳌�������
        _pauseImages.SetActive(false);
    }
    /// <summary>
    /// �I�[�g�A�N�Z���̏�Ԃ�؂�ւ���
    /// </summary>
    public void ToggleSwitchingAutoAccelerate() {
        //�I�[�g�A�N�Z���t���O��_forwardMove�̐؂�ւ�
        if (_isAutoAccelerate) {
            _isAutoAccelerate = false;
            ForwardButtonUp();
        } else {
            ForwardButtonDown();
            _isAutoAccelerate = true;
        }

        Debug.Log("�I�[�g�A�N�Z�����[�h: " + (_isAutoAccelerate ? "�L��" : "����"));
    }

    /// <summary>
    /// �A�N�Z���{�^���������ꂽ���Ɍ�i�t���O�𖳌������đO�i�t���O��L����
    /// </summary>

    public void ForwardButtonDown() {

        BackButtonUp();
        //�O�i�{�^������������I�[�g�A�N�Z����Ԃ͉�������
        _isMoveForward = true;
        Debug.Log("�A�N�Z���{�^���������ꂽ");

    }
    /// <summary>
    /// �A�N�Z���{�^���������ꂽ���ɑO�i�t���O�𖳌���
    /// </summary>
    public void ForwardButtonUp() {
        Debug.Log("�A�N�Z���{�^���������ꂽ");
        _isMoveForward = false;

    }
    /// <summary>
    /// �o�b�N�{�^���������ꂽ���ɑO�i�t���O�𖳌������Č�i�t���O��L����
    /// </summary>
    public void BackButtonDown() {
        ForwardButtonUp();
        _isAutoAccelerate = false;
        _isMoveBack = true;
        Debug.Log("�o�b�N�{�^���������ꂽ");
    }
    /// <summary>
    /// �o�b�N�{�^���������ꂽ���Ɍ�i�t���O�𖳌���
    /// </summary>
    public void BackButtonUp() {
        _isMoveBack = false;
        Debug.Log("�o�b�N�{�^���������ꂽ");

    }
    /// <summary>
    /// ���j���[�{�^�����������烁�j���[���J��
    /// </summary>
    public void PauseGame() {
        _pauseImages.SetActive(false);
        Time.timeScale = 0;
    }
    /// <summary>
    /// �Q�[���ɖ߂�{�^������������Q�[���ɖ߂�
    /// </summary>
    public void ResumeGame() {
        _pauseImages.SetActive(false);
        Time.timeScale = 1;
    }
}
