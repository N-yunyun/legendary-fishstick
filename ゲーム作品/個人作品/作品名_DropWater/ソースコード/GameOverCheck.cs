using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Q�[���I�[�o�[�̏����𖞂����Ă��邩�Ď����A�������Ă�����Q�[���I�[�o�[�̃C�x���g���Ă�
/// </summary>
public class GameOverCheck : MonoBehaviour
{
    /// <summary>
    /// �ڐG�t���O
    /// </summary>
    //private bool isColision = false;
    /// <summary>
    /// ��莞�ԑ҂��A�҂��I�������Q�[���I�[�o�[�ɂ���R���[�`��
    /// </summary>
    private IEnumerator timerCoroutine;
    /// <summary>
    /// (������҂�������)�Ď�������������
    /// </summary>
    private float waitTime = 1.0f;
    /// <summary>
    /// �������[�ɂ����΂�Ȃ��悤�ɃL���b�V�����Ă�����WaitForSeconds
    /// </summary>
    private WaitForSeconds waitForSeconds;
    [SerializeField] private GameOverEvent gameOverDisplay;

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(waitTime);
        timerCoroutine = TimeMeasurement();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ڐG������Ƃ肠�����I���ɂ���(���𗎂Ƃ��Ƃ��ɐ�ΐڐG���邩��)
        //��莞�Ԍo������Ԃł܂��I���Ȃ�Q�[���I�[�o�[�ɂ���

        //�R���[�`���������Ɠ����Ă���
        if (timerCoroutine != null)
        {
            //�R���[�`�����Ăяo��
            StartCoroutine(timerCoroutine);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�ڐG���Ȃ��Ȃ�����R���[�`������߂Ăق����̂ŁA�~�߂�
        //Debug.Log("���Ԃ̌v�����~");
        StopCoroutine(timerCoroutine);

        //���f���ꂽ�R���[�`�����ĊJ�����Ƃ��ɏ������r������ɂȂ��Ă��܂��̖h�����߁A����������
        
        //�R���[�`�������Z�b�g
        timerCoroutine = null;
        //���ɓ���Ȃ���
        timerCoroutine = TimeMeasurement();
    }
    /// <summary>
    /// time�̎��Ԃ���������ҋ@���A�ҋ@���I�������Q�[���I�[�o�[���Ă�
    /// </summary>
    /// <param name="time">�Ď�������������</param>
    /// <returns></returns>
    private IEnumerator TimeMeasurement()
    {
        //Debug.Log("���Ԃ̌v���J�n");

        //�����g���ɓ����Ă��邩�Ď����邽�߁A�w�莞�ԑ҂�
        yield return waitForSeconds;
        //Debug.Log(time +"�b�҂�������Q�[���I�[�o�[�Ă�");
        gameOverDisplay.GameOverCall();
        yield break;
    }
}
