using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを管理する(Android版)
/// </summary>

public class WindowsController : CarMovement {
/*    /// <summary>
    /// 入力を管理するコンポーネント
    /// </summary>
    [SerializeField] private InputManager _inputManager;
*/    /// <summary>
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
    /// 滑らかに回転させるために線形補間された値
    /// </summary>
    private float _smoothedAngularVelocityY = default;
    [SerializeField] private Rigidbody _playerRigidbody;
    void Start() {

        //変数を初期化

        InitializingEachVariable();

        ResetForwardForceAcceleration();

    }

    void FixedUpdate() {

        //現在の速度を取得
        _currentSpeed = _playerRigidbody.velocity.sqrMagnitude;

        //速度が制限値を超過しているか、移動ボタンが何も押されてなければリターン
        if (IsSpeedMaxOver() || IsNotMoveButtonPressed()) {
            return;
        }

        //前進ボタンが押されたら
        if (_buttonManager.IsMoveForward) {

            //加速度を加算して、押しっぱなしでスピードが上がるようにする
            _forwardMovePowerAccelerationValue = Addition(_forwardMovePowerAccelerationValue, _carConfig.AmountAddSpeed);

            ////前進する
            MoveForward(_playerRigidbody, _forwardMovePowerAccelerationValue);

            Debug.Log("回転に入る");

            //入力をFixedUpdateを取得しているのは、デバッグ用に使うだけの要素のため

            // Aキーで左カーブ(Left)
            if (Input.GetKey(KeyCode.A)) {

                Turn(_playerRigidbody, -ControlCurve());
            }
            // Dキーで右カーブ(Right)
            else if (Input.GetKey(KeyCode.D)) {

                Turn(_playerRigidbody, ControlCurve());
            }

            AssignToPlayerRigidbodyAngularVelocity();

            //後進ボタンが押されたら
        } else if (_buttonManager.IsMoveBack) {

            //バック入力したときに前進力をリセット
            ResetForwardForceAcceleration();

            //制限速度超過しているとリターン
            if (IsSpeedMaxOver()) {

                return;
            }

            //調整された加速力で力を加える
            MoveBackward(
                _playerRigidbody,
                /*速度の制限値から現在の速度を引いて加速力を調整*/
                Subtract(_carConfig.BackMaxSpeedLimit, _currentSpeed)
                );

            //カーブを滑らかにする下準備をする
            ControlCurve();

            // Aキーで左カーブ(Left)
            if (Input.GetKey(KeyCode.A)) {

                Turn(_playerRigidbody, -_smoothedAngularVelocityY);
            }
            // Dキーで右カーブ(Right)
            else if (Input.GetKey(KeyCode.D)) {

                Turn(_playerRigidbody, _smoothedAngularVelocityY);
            }

            //角速度を制限する
            AssignToPlayerRigidbodyAngularVelocity();

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
    /// <summary>
    /// カーブを線形補完して回転が滑らかになるようにコントロールする
    /// </summary>
    /// <returns>_smoothedAngularVelocityY(線形補完された値)を返す</returns>
    private float ControlCurve() {

        //回転を滑らかにするために線形補完する
        _smoothedAngularVelocityY = Mathf.Lerp(
        _playerRigidbody.angularVelocity.y,
        _carConfig.TurnMovePower,
        _carConfig.SmoothTurnValue);

        return _smoothedAngularVelocityY;

    }
    /// <summary>
    /// 制限された値をプレイヤーの角速度に代入する
    /// </summary>
    private void AssignToPlayerRigidbodyAngularVelocity() {

        _playerRigidbody.angularVelocity = LimitingAngularVelocity(_playerRigidbody.angularVelocity);

    }
    /// <summary>
    /// 各変数を初期化
    /// </summary>
    private void InitializingEachVariable() {

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
