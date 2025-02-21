using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 定数のデータファイル
/// </summary>
[CreateAssetMenu(menuName = "CarConfig", fileName = "CarConfigFile", order = 1)]
public class CarConfigScript : ScriptableObject {
    /// <summary>
    /// 前方向の最大速度の制限値
    /// </summary>
    [SerializeField] private float _forwardMaxSpeedLimit = 2000f;
    /// <summary>
    /// 後ろ方向の最大速度の制限値
    /// </summary>
    [SerializeField] private float _backMaxSpeedLimit = 1000f;
    /// <summary>
    /// 前方向にかける力の強さ
    /// </summary>
    [SerializeField] private float _forwardMovePower = 500f;
    /// <summary>
    /// カーブなどで使うターンにかける力の強さ
    /// </summary>
    [SerializeField] private float _turnMovePower = 650f;
    /// <summary>
    /// 減速係数
    /// </summary>
    [SerializeField] private float _decelerationFactor = 0.97f;
    /// <summary>
    /// 最大角速度の制限値(主に回転速度の制限に使う)
    /// </summary>
    [SerializeField] private float _maxAngularSpeed = 10f;
    /// <summary>
    /// 滑らかに回転させるためのMathf.Lerpの第三引数
    /// </summary>
    [SerializeField] private float _smoothTurnValue = 1f;
    /// <summary>
    /// FixedUpdate1回ごとに車のスピードに加算する値
    /// </summary>
    [SerializeField] private float _amountAddSpeed = 5f;
    public float AmountAddSpeed {
        get {
            return _amountAddSpeed;
        }
    }
    /// <summary>
    /// 滑らかに回転させるためのMathf.Lerpの第三引数
    /// </summary>
    public float SmoothTurnValue {
        get {
            return _smoothTurnValue;
        }

    }
    /// <summary>
    /// 前方向の最大速度の制限値
    /// </summary>
    public float ForwardMaxSpeedLimit {
        get {
            return _forwardMaxSpeedLimit;
        }
    }
    /// <summary>
    /// 後ろ方向の最大速度の制限値
    /// </summary>
    public float BackMaxSpeedLimit {
        get {
            return _backMaxSpeedLimit;
        }
    }
    /// <summary>
    /// 前方向にかける力の値
    /// </summary>
    public float ForwardMovePower {
        get {
            return _forwardMovePower;
        }
    }
    /// <summary>
    /// 減速係数
    /// </summary>
    public float DecelerationFactor {
        get {
            return _decelerationFactor;
        }
    }
    /// <summary>
    /// カーブなどで使うターンにかける力の強さ
    /// </summary>
    public float TurnMovePower {
        get {
            return _turnMovePower;
        }
    }
    /// <summary>
    /// 最大角速度の制限値(主に回転速度の制限に使う)
    /// </summary>
    public float MaxAngularSpeed {
        get {
            return _maxAngularSpeed;
        }
    }


}
