using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// あらゆる計算を担当するクラス
/// </summary>
public class CalculationSpecialist : MonoBehaviour {
    /// <summary>
    /// 定数のデータファイル
    /// </summary>
    [SerializeField] protected CarConfigScript _carConfig;

    /// <summary>
    ///現在の角速度(AddTorqueで回転がかからないと値はセットされない)
    /// </summary>
    [SerializeField] private Vector3 _currentAngularVelocity = default;

    /// <summary>
    /// 渡された角速度の値を制限を指定している範囲内に制限して返す。
    /// </summary>
    /// <param name="angularVelocity">制限した角速度の値</param>
    public Vector3 LimitingAngularVelocity(Vector3 angularVelocity) {

        // 現在の角速度を取得
        _currentAngularVelocity = angularVelocity;

        // 角速度を制限
        if (_currentAngularVelocity.magnitude > _carConfig.MaxAngularSpeed) {
            Debug.Log("プレイヤーの回転速度は" + angularVelocity);
            angularVelocity = _currentAngularVelocity.normalized * _carConfig.MaxAngularSpeed;
        }
        return angularVelocity;
    }
    /// <summary>
    /// 値を乗算して結果を返す
    /// </summary>
    /// <param name="firstArgument">掛けたい値その1</param>
    /// <param name="secondArgument">掛けたい値その2</param>
    /// <returns>result(計算結果)</returns>
    public float Multiply(float firstArgument, float secondArgument) {
        float result = firstArgument * secondArgument;
        return result;
    }
    /// <summary>
    /// 第一引数に第二引数を加算した結果を返す
    /// </summary>
    /// <param name="resultAddition">加算されたい値</param>
    /// <param name="argument"></param>
    /// <returns></returns>
    public float Addition(float resultAddition, float argument) {
        return resultAddition += argument;
    }
    /// <summary>
    /// 第一引数から第二引数を減算した結果を返す
    /// </summary>
    /// <param name="firstArgument">減算されたい値</param>
    /// <param name="secondArgument">減算したい値</param>
    /// <returns></returns>
    public float Subtract(float firstArgument, float secondArgument) {

        return firstArgument - secondArgument;
    }

}
