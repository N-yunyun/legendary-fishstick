using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class N_Talking_Child : N_TalkBase_Mother
{
    // �Z���t�̓C���X�y�N�^�[�ŏ�������

    /// <summary>
    /// �ʏ��b�p�̃��b�Z�[�W���X�g
    /// </summary>
    [Header("�ʏ��b�p�̃��b�Z�[�W��(Size�ŃZ���t���𑝂₷)")]
    [SerializeField]
    private List<string> talkMessageList;

    /// <summary>
    /// �ߊl��b�p�̃��b�Z�[�W���X�g
    /// </summary>
    [Header("�ߊl��b�p�̃��b�Z�[�W��(Size�ŃZ���t���𑝂₷)")]
    [SerializeField]
    private List<string> catchMessageList;
    /// <summary>
    /// ���ʉ�b�p�̃��b�Z�[�W���X�g
    /// </summary>
    [Header("���ʉ�b�p�̃��b�Z�[�W��(�ȗ�)")]
    [SerializeField]
    private List<string> specialTalkMessageList;

    //�e�N���X����Ă΂��R�[���o�b�N���\�b�h (�ڐG & �{�^���������Ƃ��Ɏ��s)
    protected override IEnumerator OnActionNormal()
    {
        //Debug.Log("�R���[�`���Ăяo���ꂽ");
        for (int i = 0; i < talkMessageList.Count; ++i)
        {
            // 1�t���[���� ������ҋ@
            yield return null;

            // �Z���t����window�ɕ\��

            if (GetIsCatch())
            {
                showMessage(catchMessageList[i]);
            }
            else
            {
                showMessage(talkMessageList[i]);
            }
            


            // �{�^�����͂�ҋ@
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0));

        }

        yield break;
    }
    protected override IEnumerator OnActionSpecial()
    {
        for (int i = 0; i < specialTalkMessageList.Count; ++i)
        {
            // 1�t���[���� ������ҋ@
            yield return null;

            // �Z���t����window�ɕ\��
            //���ʉ�b�p�̃e�L�X�g���X�g
            showMessage(specialTalkMessageList[i]);
            

            // �{�^�����͂�ҋ@
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0));

        }

        yield break;
    }

}
