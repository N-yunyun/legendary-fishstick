using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseCamera : MonoBehaviour
{
    #region �ϐ��ꗗ

    /// <summary>
    /// �v���C���[��Transform
    /// </summary>
    public Transform player;

    /// <summary>
    /// �v���C���[�ƃJ�����̑��Έʒu
    /// </summary>
    public Vector3 offset;

    /// <summary>
    /// �J�������v���C���[��ǐՂ���ۂ̃X���[�Y���̒����p�p�����[�^
    /// </summary>
    public float smoothTime = 0.3f;

    /// <summary>
    /// �J�����ړ����̑��x�x�N�g��
    /// </summary>
    private Vector3 velocity = Vector3.zero; //�l��傫������ƒǏ]���������ɂȂ�

    #endregion


    void LateUpdate()
    {
        // �v���C���[�̈ʒu�ɃJ������Ǐ]������
        Vector3 targetPosition = player.position + offset;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


        /*�u�J������Ǐ]������Ƃ���LateUpdate���ōs���Ɨǂ��v
            LateUpdate�͕K��Update�̌�ɌĂяo�����̂ŁAUpdate�ŃL�����N�^�[���ړ�����
            �L�����̈ړ��������������A���W���m�肵�Ă���J�������ړ�������
            ���̎菇�ŏ������s���Ί��炩�ɃJ�����𓮂�����*/

        //�J�������v���C���[�̎q�I�u�W�F�N�g�ɂ���Ɩ�肪�����̂ł��ꂾ���͂��Ȃ�
    }


}
