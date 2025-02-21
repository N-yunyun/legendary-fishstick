using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ô‚Ì“®ì
/// </summary>
public abstract class CarMovement : CalculationSpecialist {

    /// <summary>
    /// ‘O•ûŒü‚É—Í‚ğ‰Á‚¦‚é
    /// </summary>
    /// <param name="forwardAcceleration">‘O•ûŒü‚É‰Á‚¦‚½‚¢—Í‚Ì‹­‚³</param>
    public void MoveForward(Rigidbody rigidbody, float forwardAcceleration) {
        rigidbody.AddForce(transform.forward * forwardAcceleration, ForceMode.Force);
    }
    /// <summary>
    /// Œã•ûŒü‚É—Í‚ğ‰Á‚¦‚é
    /// </summary>
    /// <param name="backAcceleration">Œã•ûŒü‚É‰Á‚¦‚½‚¢—Í‚Ì‹­‚³</param>
    public void MoveBackward(Rigidbody rigidbody, float backAcceleration) {
        rigidbody.AddForce(-transform.forward * backAcceleration, ForceMode.Force);
    }
    /// <summary>
    /// ó‚¯æ‚Á‚½’l‚Ì•ûŒü‚É‰ñ“]‚ğ‰Á‚¦‚é
    /// </summary>
    /// <param name="torque"></param>
    public void Turn(Rigidbody rigidbody, float torque) {
        rigidbody.AddTorque(Vector3.up * torque, ForceMode.Force);
    }
}
