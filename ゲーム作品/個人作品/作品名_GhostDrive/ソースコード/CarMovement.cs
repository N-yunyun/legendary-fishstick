using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ԃ̓���
/// </summary>
public abstract class CarMovement : CalculationSpecialist {

    /// <summary>
    /// �O�����ɗ͂�������
    /// </summary>
    /// <param name="forwardAcceleration">�O�����ɉ��������͂̋���</param>
    public void MoveForward(Rigidbody rigidbody, float forwardAcceleration) {
        rigidbody.AddForce(transform.forward * forwardAcceleration, ForceMode.Force);
    }
    /// <summary>
    /// ������ɗ͂�������
    /// </summary>
    /// <param name="backAcceleration">������ɉ��������͂̋���</param>
    public void MoveBackward(Rigidbody rigidbody, float backAcceleration) {
        rigidbody.AddForce(-transform.forward * backAcceleration, ForceMode.Force);
    }
    /// <summary>
    /// �󂯎�����l�̕����ɉ�]��������
    /// </summary>
    /// <param name="torque"></param>
    public void Turn(Rigidbody rigidbody, float torque) {
        rigidbody.AddTorque(Vector3.up * torque, ForceMode.Force);
    }
}
