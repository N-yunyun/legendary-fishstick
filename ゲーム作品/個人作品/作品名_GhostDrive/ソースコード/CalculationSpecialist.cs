using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������v�Z��S������N���X
/// </summary>
public class CalculationSpecialist : MonoBehaviour {
    /// <summary>
    /// �萔�̃f�[�^�t�@�C��
    /// </summary>
    [SerializeField] protected CarConfigScript _carConfig;

    /// <summary>
    ///���݂̊p���x(AddTorque�ŉ�]��������Ȃ��ƒl�̓Z�b�g����Ȃ�)
    /// </summary>
    [SerializeField] private Vector3 _currentAngularVelocity = default;

    /// <summary>
    /// �n���ꂽ�p���x�̒l�𐧌����w�肵�Ă���͈͓��ɐ������ĕԂ��B
    /// </summary>
    /// <param name="angularVelocity">���������p���x�̒l</param>
    public Vector3 LimitingAngularVelocity(Vector3 angularVelocity) {

        // ���݂̊p���x���擾
        _currentAngularVelocity = angularVelocity;

        // �p���x�𐧌�
        if (_currentAngularVelocity.magnitude > _carConfig.MaxAngularSpeed) {
            Debug.Log("�v���C���[�̉�]���x��" + angularVelocity);
            angularVelocity = _currentAngularVelocity.normalized * _carConfig.MaxAngularSpeed;
        }
        return angularVelocity;
    }
    /// <summary>
    /// �l����Z���Č��ʂ�Ԃ�
    /// </summary>
    /// <param name="firstArgument">�|�������l����1</param>
    /// <param name="secondArgument">�|�������l����2</param>
    /// <returns>result(�v�Z����)</returns>
    public float Multiply(float firstArgument, float secondArgument) {
        float result = firstArgument * secondArgument;
        return result;
    }
    /// <summary>
    /// �������ɑ����������Z�������ʂ�Ԃ�
    /// </summary>
    /// <param name="resultAddition">���Z���ꂽ���l</param>
    /// <param name="argument"></param>
    /// <returns></returns>
    public float Addition(float resultAddition, float argument) {
        return resultAddition += argument;
    }
    /// <summary>
    /// ��������������������Z�������ʂ�Ԃ�
    /// </summary>
    /// <param name="firstArgument">���Z���ꂽ���l</param>
    /// <param name="secondArgument">���Z�������l</param>
    /// <returns></returns>
    public float Subtract(float firstArgument, float secondArgument) {

        return firstArgument - secondArgument;
    }

}
