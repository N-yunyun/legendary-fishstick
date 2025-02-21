using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �萔�̃f�[�^�t�@�C��
/// </summary>
[CreateAssetMenu(menuName = "CarConfig", fileName = "CarConfigFile", order = 1)]
public class CarConfigScript : ScriptableObject {
    /// <summary>
    /// �O�����̍ő呬�x�̐����l
    /// </summary>
    [SerializeField] private float _forwardMaxSpeedLimit = 2000f;
    /// <summary>
    /// �������̍ő呬�x�̐����l
    /// </summary>
    [SerializeField] private float _backMaxSpeedLimit = 1000f;
    /// <summary>
    /// �O�����ɂ�����͂̋���
    /// </summary>
    [SerializeField] private float _forwardMovePower = 500f;
    /// <summary>
    /// �J�[�u�ȂǂŎg���^�[���ɂ�����͂̋���
    /// </summary>
    [SerializeField] private float _turnMovePower = 650f;
    /// <summary>
    /// �����W��
    /// </summary>
    [SerializeField] private float _decelerationFactor = 0.97f;
    /// <summary>
    /// �ő�p���x�̐����l(��ɉ�]���x�̐����Ɏg��)
    /// </summary>
    [SerializeField] private float _maxAngularSpeed = 10f;
    /// <summary>
    /// ���炩�ɉ�]�����邽�߂�Mathf.Lerp�̑�O����
    /// </summary>
    [SerializeField] private float _smoothTurnValue = 1f;
    /// <summary>
    /// FixedUpdate1�񂲂ƂɎԂ̃X�s�[�h�ɉ��Z����l
    /// </summary>
    [SerializeField] private float _amountAddSpeed = 5f;
    public float AmountAddSpeed {
        get {
            return _amountAddSpeed;
        }
    }
    /// <summary>
    /// ���炩�ɉ�]�����邽�߂�Mathf.Lerp�̑�O����
    /// </summary>
    public float SmoothTurnValue {
        get {
            return _smoothTurnValue;
        }

    }
    /// <summary>
    /// �O�����̍ő呬�x�̐����l
    /// </summary>
    public float ForwardMaxSpeedLimit {
        get {
            return _forwardMaxSpeedLimit;
        }
    }
    /// <summary>
    /// �������̍ő呬�x�̐����l
    /// </summary>
    public float BackMaxSpeedLimit {
        get {
            return _backMaxSpeedLimit;
        }
    }
    /// <summary>
    /// �O�����ɂ�����͂̒l
    /// </summary>
    public float ForwardMovePower {
        get {
            return _forwardMovePower;
        }
    }
    /// <summary>
    /// �����W��
    /// </summary>
    public float DecelerationFactor {
        get {
            return _decelerationFactor;
        }
    }
    /// <summary>
    /// �J�[�u�ȂǂŎg���^�[���ɂ�����͂̋���
    /// </summary>
    public float TurnMovePower {
        get {
            return _turnMovePower;
        }
    }
    /// <summary>
    /// �ő�p���x�̐����l(��ɉ�]���x�̐����Ɏg��)
    /// </summary>
    public float MaxAngularSpeed {
        get {
            return _maxAngularSpeed;
        }
    }


}
