using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���Ǘ�����(Android��)
/// </summary>

public class WindowsController : CarMovement {
/*    /// <summary>
    /// ���͂��Ǘ�����R���|�[�l���g
    /// </summary>
    [SerializeField] private InputManager _inputManager;
*/    /// <summary>
    /// �V�[����ɑ��݂���{�^���̃C�x���g���Ǘ�����
    /// </summary>
    [SerializeField] private ButtonManager _buttonManager;
    /// <summary>
    ///�@�O�i�͂̉����x�ɂ�����ꎞ�I�Ȓl
    /// </summary>
    [SerializeField] private float _forwardMovePowerAccelerationValue = default;
    /// <summary>
    /// ���݂̑��x
    /// </summary>
    [SerializeField] private float _currentSpeed = default;
    /// <summary>
    /// ���݂̉����x
    /// </summary>
    [SerializeField] private float _currentBackAcceleration;
    /// <summary>
    /// ���炩�ɉ�]�����邽�߂ɐ��`��Ԃ��ꂽ�l
    /// </summary>
    private float _smoothedAngularVelocityY = default;
    [SerializeField] private Rigidbody _playerRigidbody;
    void Start() {

        //�ϐ���������

        InitializingEachVariable();

        ResetForwardForceAcceleration();

    }

    void FixedUpdate() {

        //���݂̑��x���擾
        _currentSpeed = _playerRigidbody.velocity.sqrMagnitude;

        //���x�������l�𒴉߂��Ă��邩�A�ړ��{�^��������������ĂȂ���΃��^�[��
        if (IsSpeedMaxOver() || IsNotMoveButtonPressed()) {
            return;
        }

        //�O�i�{�^���������ꂽ��
        if (_buttonManager.IsMoveForward) {

            //�����x�����Z���āA�������ςȂ��ŃX�s�[�h���オ��悤�ɂ���
            _forwardMovePowerAccelerationValue = Addition(_forwardMovePowerAccelerationValue, _carConfig.AmountAddSpeed);

            ////�O�i����
            MoveForward(_playerRigidbody, _forwardMovePowerAccelerationValue);

            Debug.Log("��]�ɓ���");

            //���͂�FixedUpdate���擾���Ă���̂́A�f�o�b�O�p�Ɏg�������̗v�f�̂���

            // A�L�[�ō��J�[�u(Left)
            if (Input.GetKey(KeyCode.A)) {

                Turn(_playerRigidbody, -ControlCurve());
            }
            // D�L�[�ŉE�J�[�u(Right)
            else if (Input.GetKey(KeyCode.D)) {

                Turn(_playerRigidbody, ControlCurve());
            }

            AssignToPlayerRigidbodyAngularVelocity();

            //��i�{�^���������ꂽ��
        } else if (_buttonManager.IsMoveBack) {

            //�o�b�N���͂����Ƃ��ɑO�i�͂����Z�b�g
            ResetForwardForceAcceleration();

            //�������x���߂��Ă���ƃ��^�[��
            if (IsSpeedMaxOver()) {

                return;
            }

            //�������ꂽ�����͂ŗ͂�������
            MoveBackward(
                _playerRigidbody,
                /*���x�̐����l���猻�݂̑��x�������ĉ����͂𒲐�*/
                Subtract(_carConfig.BackMaxSpeedLimit, _currentSpeed)
                );

            //�J�[�u�����炩�ɂ��鉺����������
            ControlCurve();

            // A�L�[�ō��J�[�u(Left)
            if (Input.GetKey(KeyCode.A)) {

                Turn(_playerRigidbody, -_smoothedAngularVelocityY);
            }
            // D�L�[�ŉE�J�[�u(Right)
            else if (Input.GetKey(KeyCode.D)) {

                Turn(_playerRigidbody, _smoothedAngularVelocityY);
            }

            //�p���x�𐧌�����
            AssignToPlayerRigidbodyAngularVelocity();

        } else {

            //����������ĂȂ���Ή����x�����Z�b�g����
            ResetForwardForceAcceleration();
        }
    }
    /// <summary>
    /// �O�i�͂̉����x�̒l�������l�Ƀ��Z�b�g����
    /// </summary>
    private void ResetForwardForceAcceleration() {

        _forwardMovePowerAccelerationValue = _carConfig.ForwardMovePower;
    }
    /// <summary>
    /// �J�[�u����`�⊮���ĉ�]�����炩�ɂȂ�悤�ɃR���g���[������
    /// </summary>
    /// <returns>_smoothedAngularVelocityY(���`�⊮���ꂽ�l)��Ԃ�</returns>
    private float ControlCurve() {

        //��]�����炩�ɂ��邽�߂ɐ��`�⊮����
        _smoothedAngularVelocityY = Mathf.Lerp(
        _playerRigidbody.angularVelocity.y,
        _carConfig.TurnMovePower,
        _carConfig.SmoothTurnValue);

        return _smoothedAngularVelocityY;

    }
    /// <summary>
    /// �������ꂽ�l���v���C���[�̊p���x�ɑ������
    /// </summary>
    private void AssignToPlayerRigidbodyAngularVelocity() {

        _playerRigidbody.angularVelocity = LimitingAngularVelocity(_playerRigidbody.angularVelocity);

    }
    /// <summary>
    /// �e�ϐ���������
    /// </summary>
    private void InitializingEachVariable() {

        _currentSpeed = default;
        _smoothedAngularVelocityY = default;

    }
    /// <summary>
    /// ���݂̃X�s�[�h�����Z�b�g����
    /// </summary>
    public void ResetCurrentSpeed() {

        _currentSpeed = default;
    }
    /// <summary>
    /// ���x�𒴉߂��Ă��邩�̃t���O
    /// </summary>
    /// <returns>���x�𒴉߂��Ă���ΐ^��Ԃ�</returns>
    private bool IsSpeedMaxOver() {
        return _currentSpeed >= _carConfig.ForwardMaxSpeedLimit;
    }
    /// <summary>
    /// �ړ��n�̃{�^����������Ă��Ȃ����̃t���O
    /// </summary>
    /// <returns>�ړ��{�^����������ĂȂ���ΐ^��Ԃ�</returns>
    private bool IsNotMoveButtonPressed() {
        return !_buttonManager.IsMoveForward && !_buttonManager.IsMoveBack;
    }
}
