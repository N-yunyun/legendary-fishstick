using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class N_PlayerCarry : MonoBehaviour
{/// <summary>
 /// �ω������C�k��͂�ł��邩
 /// </summary>
    private bool _isCatch = false;
    /// <summary>
    /// ���݂����I�u�W�F�N�g
    /// </summary>
    private GameObject _catchObject;
    private BoxCollider2D _objectCollider = default;
    [SerializeField] private N_SubMove n_SubMove;
    /// <summary>
    /// �ω��������ƐڐG���Ă��邩�t���O(�O���ʒm�p)
    /// </summary>
    private bool _isContact;
    /// <summary>
    /// �ω��������ƐڐG���Ă��邩�̃t���O
    /// </summary>
    /// <returns></returns>
    public bool GetIsContact()
    {
        return _isContact;
    }

    public bool GetIsCatch()
    {
        return _isCatch;
    }
    private FindObj_T _findObj;
    private int _putOffset = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Carryable" && !_isCatch)
        {
            //Carryable�^�O�̂����v���C���[�ɐG���ƁA���̃v���C���[���^�ԃ��[�h�Ɉڍs����
            _catchObject = collision.gameObject;
            _isContact = true;
            Debug.Log("�ڐG����");
        }
        //�Ԃ�������Ԃ������p�̃^�O��N_SubMove�ɒm�点��
    }
    /// <summary>
    /// �������ޏ���
    /// </summary>
    public void CatchMainPlayer()
    {
        //�͂݃t���O�I��
        _isCatch = true;
        _objectCollider = _catchObject.GetComponent<BoxCollider2D>();
        _findObj = _catchObject.GetComponent<FindObj_T>();
        
        //�ϐg������s�\�ɂ���
        _findObj.enabled = false;
        //�u�ꎞ�I�ɕω������C�k�v���q�I�u�W�F�N�g�ɂ��ĉ^�ׂ�悤�ɂ���
        _catchObject.transform.parent = this.gameObject.transform;
        
        //�v���C���[��Trigger�����ɖ߂�
        _objectCollider.isTrigger = true;
        Debug.Log("�v���C���[��͂�");
    }
    /// <summary>
    /// ���𗣂�����
    /// </summary>
    public void MainPlayerThrowAway()
    {
        //�e�q�֌W���������ĉ^������߂�
        _catchObject.transform.parent = null;

        if (n_SubMove.GetIsRight())
        {
            //�ω����������������ꂽ�ꏊ�ɂ��낷
            _catchObject.transform.position = new Vector3(this.gameObject.transform.position.x - _putOffset,
            this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else
        {
            //�ω����������������ꂽ�ꏊ�ɂ��낷
            _catchObject.transform.position = new Vector3(this.gameObject.transform.position.x + _putOffset,
            this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }

        //�ϐg�������\�ɂ���
        _findObj.enabled = true;

        //�v���C���[��Trigger�����ɖ߂�
        _objectCollider.isTrigger = false;

        //�ڐG���Ă��Ȃ����Ƃɂ���
        _isContact = false;
        //����ł��Ȃ����Ƃɂ���
        _isCatch = false;
    }

}

