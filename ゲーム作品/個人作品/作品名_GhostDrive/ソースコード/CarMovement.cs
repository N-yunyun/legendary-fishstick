using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 車の動作
/// </summary>
public abstract class CarMovement : CalculationSpecialist {

    /// <summary>
    /// 前方向に力を加える
    /// </summary>
    /// <param name="forwardAcceleration">前方向に加えたい力の強さ</param>
    public void MoveForward(Rigidbody rigidbody, float forwardAcceleration) {
        rigidbody.AddForce(transform.forward * forwardAcceleration, ForceMode.Force);
    }
    /// <summary>
    /// 後方向に力を加える
    /// </summary>
    /// <param name="backAcceleration">後方向に加えたい力の強さ</param>
    public void MoveBackward(Rigidbody rigidbody, float backAcceleration) {
        rigidbody.AddForce(-transform.forward * backAcceleration, ForceMode.Force);
    }
    /// <summary>
    /// 受け取った値の方向に回転を加える
    /// </summary>
    /// <param name="torque"></param>
    public void Turn(Rigidbody rigidbody, float torque) {
        rigidbody.AddTorque(Vector3.up * torque, ForceMode.Force);
    }
}
