using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーを管理する(PC)
/// </summary>
public class AndroidController : CarMovement {
    /// <summary>
    /// ジャイロ機能が有効かどうか
    /// </summary>
    [SerializeField] private bool _isGyroEnabled = false;
    /// <summary>
    /// 入力を管理するコンポーネント
    /// </summary>
    [SerializeField] private InputManager _inputManager;
    /// <summary>
    /// シーン上に存在するボタンのイベントを管理する
    /// </summary>
    [SerializeField] private ButtonManager _buttonManager;
    /// <summary>
    ///　前進力の加速度にかける一時的な値
    /// </summary>
    [SerializeField] private float _forwardMovePowerAccelerationValue = default;
    /// <summary>
    /// 現在の速度
    /// </summary>
    [SerializeField] private float _currentSpeed = default;
    /// <summary>
    /// 現在の加速度
    /// </summary>
    [SerializeField] private float _currentBackAcceleration;
    /// <summary>
    /// デバイスの現在の横方向の角速度
    /// </summary>
    private float _currentAngularVelocityYOfDevice = default;
    /// <summary>
    /// 滑らかに回転させるために線形補間された値
    /// </summary>
    private float _smoothedAngularVelocityY = default;
    [SerializeField] private Rigidbody _playerRigidbody;
    //セッターでボタンコントローラーから値を受け取ってその値でFixedUpdateの条件判定に使う
    // Start is called before the first frame update
    void Start() {

        InitializingEachVariable();
        ResetForwardForceAcceleration();

    }
    /// <summary>
    /// テスト用にアップデート(キーボード入力を受け付けないのはGame画面をsimulatorにしているから
    /// →simulatorをクリックしてGameにすれば受け付けるようになる)
    /// </summary>
    void FixedUpdate() {


        if (!_isGyroEnabled) {
            return;

        }
        // ジャイロデータの取得
        _currentAngularVelocityYOfDevice = Input.gyro.rotationRateUnbiased.y; // 端末の横方向の回転速度を取得

        GyroControlCurves();

        //現在の速度を取得
        _currentSpeed = _playerRigidbody.velocity.sqrMagnitude;

        if (IsSpeedMaxOver() || IsNotMoveButtonPressed()) {
            return;
        }

        if (_buttonManager.IsMoveForward) {

            //加速度を加算して、押しっぱなしでスピードが上がるようにする
            _forwardMovePowerAccelerationValue = Addition(_forwardMovePowerAccelerationValue, _carConfig.AmountAddSpeed);

            //前進する
            MoveForward(_playerRigidbody, _forwardMovePowerAccelerationValue);

        } else if (_buttonManager.IsMoveBack) {
            //バック入力したときに前進力をリセット
            ResetForwardForceAcceleration();

            //制限速度超過しているとリターン
            if (IsSpeedMaxOver()) {

                return;
            }

            //調整された加速力で力を加える
            MoveBackward(_playerRigidbody,
                /*速度の制限値から現在の速度を引いて加速力を調整*/
                Subtract(_carConfig.BackMaxSpeedLimit, _currentSpeed));


        } else {

            //何も押されてなければ加速度をリセットする
            ResetForwardForceAcceleration();
        }
    }
    /// <summary>
    /// 前進力の加速度の値を初期値にリセットする
    /// </summary>
    private void ResetForwardForceAcceleration() {

        _forwardMovePowerAccelerationValue = _carConfig.ForwardMovePower;
    }
    //#if UNITY_ANDROID

    /// <summary>
    /// カーブをジャイロで制御する(Android限定)
    /// </summary>
    private void GyroControlCurves() {

        //回転を滑らかにするために計画上の角速度まで線形補完する
        _smoothedAngularVelocityY = Mathf.Lerp(
        _playerRigidbody.angularVelocity.y,
        Multiply(_currentAngularVelocityYOfDevice, _carConfig.TurnMovePower),
        _carConfig.SmoothTurnValue);

        // 回転速度に基づいて車に回転をかける
        Turn(_playerRigidbody, Multiply(_smoothedAngularVelocityY, _carConfig.TurnMovePower));

        //角速度を制限する
        AssignToPlayerRigidbodyAngularVelocity();

    }
    //#endif
    /// <summary>
    /// 各変数を初期化
    /// </summary>
    private void InitializingEachVariable() {

        _currentAngularVelocityYOfDevice = default;
        _currentSpeed = default;
        _smoothedAngularVelocityY = default;

    }

    /// <summary>
    /// 現在のスピードをリセットする
    /// </summary>
    public void ResetCurrentSpeed() {
        _currentSpeed = default;
    }
    /// <summary>
    /// 制限された値をプレイヤーの角速度に代入する
    /// </summary>
    private void AssignToPlayerRigidbodyAngularVelocity() {
        _playerRigidbody.angularVelocity = LimitingAngularVelocity(_playerRigidbody.angularVelocity);

    }
    /// <summary>
    /// ジャイロ機能を起動する
    /// </summary>
    public void StartUpGyroscope() {

        // ジャイロセンサーの有効化
        //SystemInfo.supportsGyroscopeとは、ジャイロスコープを使えるどうか
        if (SystemInfo.supportsGyroscope) {
            Input.gyro.enabled = true;
            _isGyroEnabled = true;

        } else {
            Debug.LogWarning("ジャイロセンサーがサポートされていません");
        }

    }
    /// <summary>
    /// 速度を超過しているかのフラグ
    /// </summary>
    /// <returns>速度を超過していれば真を返す</returns>
    private bool IsSpeedMaxOver() {
        return _currentSpeed >= _carConfig.ForwardMaxSpeedLimit;
    }
    /// <summary>
    /// 移動系のボタンが押されていないかのフラグ
    /// </summary>
    /// <returns>移動ボタンが押されてなければ真を返す</returns>
    private bool IsNotMoveButtonPressed() {
        return !_buttonManager.IsMoveForward && !_buttonManager.IsMoveBack;
    }

}
