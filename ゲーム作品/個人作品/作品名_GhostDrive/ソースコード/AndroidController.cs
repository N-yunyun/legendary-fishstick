using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �v���C���[���Ǘ�����(PC)
/// </summary>
public class AndroidController : CarMovement {
    /// <summary>
    /// �W���C���@�\���L�����ǂ���
    /// </summary>
    [SerializeField] private bool _isGyroEnabled = false;
    /// <summary>
    /// ���͂��Ǘ�����R���|�[�l���g
    /// </summary>
    [SerializeField] private InputManager _inputManager;
    /// <summary>
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
    /// �f�o�C�X�̌��݂̉������̊p���x
    /// </summary>
    private float _currentAngularVelocityYOfDevice = default;
    /// <summary>
    /// ���炩�ɉ�]�����邽�߂ɐ��`��Ԃ��ꂽ�l
    /// </summary>
    private float _smoothedAngularVelocityY = default;
    [SerializeField] private Rigidbody _playerRigidbody;
    //�Z�b�^�[�Ń{�^���R���g���[���[����l���󂯎���Ă��̒l��FixedUpdate�̏�������Ɏg��
    // Start is called before the first frame update
    void Start() {

        InitializingEachVariable();
        ResetForwardForceAcceleration();

    }
    /// <summary>
    /// �e�X�g�p�ɃA�b�v�f�[�g(�L�[�{�[�h���͂��󂯕t���Ȃ��̂�Game��ʂ�simulator�ɂ��Ă��邩��
    /// ��simulator���N���b�N����Game�ɂ���Ύ󂯕t����悤�ɂȂ�)
    /// </summary>
    void FixedUpdate() {


        if (!_isGyroEnabled) {
            return;

        }
        // �W���C���f�[�^�̎擾
        _currentAngularVelocityYOfDevice = Input.gyro.rotationRateUnbiased.y; // �[���̉������̉�]���x���擾

        GyroControlCurves();

        //���݂̑��x���擾
        _currentSpeed = _playerRigidbody.velocity.sqrMagnitude;

        if (IsSpeedMaxOver() || IsNotMoveButtonPressed()) {
            return;
        }

        if (_buttonManager.IsMoveForward) {

            //�����x�����Z���āA�������ςȂ��ŃX�s�[�h���オ��悤�ɂ���
            _forwardMovePowerAccelerationValue = Addition(_forwardMovePowerAccelerationValue, _carConfig.AmountAddSpeed);

            //�O�i����
            MoveForward(_playerRigidbody, _forwardMovePowerAccelerationValue);

        } else if (_buttonManager.IsMoveBack) {
            //�o�b�N���͂����Ƃ��ɑO�i�͂����Z�b�g
            ResetForwardForceAcceleration();

            //�������x���߂��Ă���ƃ��^�[��
            if (IsSpeedMaxOver()) {

                return;
            }

            //�������ꂽ�����͂ŗ͂�������
            MoveBackward(_playerRigidbody,
                /*���x�̐����l���猻�݂̑��x�������ĉ����͂𒲐�*/
                Subtract(_carConfig.BackMaxSpeedLimit, _currentSpeed));


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
    //#if UNITY_ANDROID

    /// <summary>
    /// �J�[�u���W���C���Ő��䂷��(Android����)
    /// </summary>
    private void GyroControlCurves() {

        //��]�����炩�ɂ��邽�߂Ɍv���̊p���x�܂Ő��`�⊮����
        _smoothedAngularVelocityY = Mathf.Lerp(
        _playerRigidbody.angularVelocity.y,
        Multiply(_currentAngularVelocityYOfDevice, _carConfig.TurnMovePower),
        _carConfig.SmoothTurnValue);

        // ��]���x�Ɋ�Â��ĎԂɉ�]��������
        Turn(_playerRigidbody, Multiply(_smoothedAngularVelocityY, _carConfig.TurnMovePower));

        //�p���x�𐧌�����
        AssignToPlayerRigidbodyAngularVelocity();

    }
    //#endif
    /// <summary>
    /// �e�ϐ���������
    /// </summary>
    private void InitializingEachVariable() {

        _currentAngularVelocityYOfDevice = default;
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
    /// �������ꂽ�l���v���C���[�̊p���x�ɑ������
    /// </summary>
    private void AssignToPlayerRigidbodyAngularVelocity() {
        _playerRigidbody.angularVelocity = LimitingAngularVelocity(_playerRigidbody.angularVelocity);

    }
    /// <summary>
    /// �W���C���@�\���N������
    /// </summary>
    public void StartUpGyroscope() {

        // �W���C���Z���T�[�̗L����
        //SystemInfo.supportsGyroscope�Ƃ́A�W���C���X�R�[�v���g����ǂ���
        if (SystemInfo.supportsGyroscope) {
            Input.gyro.enabled = true;
            _isGyroEnabled = true;

        } else {
            Debug.LogWarning("�W���C���Z���T�[���T�|�[�g����Ă��܂���");
        }

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
