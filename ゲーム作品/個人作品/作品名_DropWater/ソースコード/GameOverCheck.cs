using System.Collections;
using UnityEngine;
/// <summary>
/// �Q�[���I�[�o�[�̏����𖞂����Ă��邩�Ď����A�������Ă�����Q�[���I�[�o�[�̃C�x���g���Ă�
/// </summary>
public class GameOverCheck : MonoBehaviour

{
    /// <summary>
    /// ��莞�ԑ҂��A�҂��I�������Q�[���I�[�o�[�ɂ���R���[�`��
    /// </summary>
    private Coroutine _runningCoroutine;
    /// <summary>
    /// (������҂�������)�Ď�������������
    /// </summary>
    private float _waitTime = 1.0f;
    /// <summary>
    /// �L���b�V�����Ă�����WaitForSeconds
    /// </summary>
    private WaitForSeconds waitForSeconds;
    [Header("GameOverEvent���A�^�b�`���ꂽ�I�u�W�F�N�g���Z�b�g")]
    [SerializeField] private GameOverEvent _gameOverDisplay;

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(_waitTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ڐG������Ƃ肠�����I���ɂ���
        //��莞�Ԍo������Ԃł܂��I���Ȃ�Q�[���I�[�o�[�ɂ���

        //�R���[�`���������Ɠ����Ă���
        if (_runningCoroutine == null)
        {
            _runningCoroutine = StartCoroutine(WaitAndGameOver());
            //�R���[�`�����Ăяo��
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�ڐG���Ȃ��Ȃ�����R���[�`������߂Ăق����̂ŁA�~�߂�
        if (_runningCoroutine != null)
        {
            StopCoroutine(_runningCoroutine);
            _runningCoroutine = null;
        }

    }
    /// <summary>
    /// time�̎��Ԃ���������ҋ@���A�ҋ@���I�������Q�[���I�[�o�[���Ă�
    /// </summary>
    /// <param name="time">�Ď�������������</param>
    /// <returns></returns>
    private IEnumerator WaitAndGameOver()
    {
        //�����g���ɓ����Ă��邩�Ď����邽�߁A�w�莞�ԑ҂�
        yield return waitForSeconds;
        //�҂��I�������Q�[���I�[�o�[�C�x���g���Ă�
        _gameOverDisplay.GameOverCall();

        _runningCoroutine = null;
    }
}
