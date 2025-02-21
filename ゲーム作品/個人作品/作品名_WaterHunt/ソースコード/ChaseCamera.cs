using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseCamera : MonoBehaviour
{
    #region 変数一覧

    /// <summary>
    /// プレイヤーのTransform
    /// </summary>
    public Transform player;

    /// <summary>
    /// プレイヤーとカメラの相対位置
    /// </summary>
    public Vector3 offset;

    /// <summary>
    /// カメラがプレイヤーを追跡する際のスムーズさの調整用パラメータ
    /// </summary>
    public float smoothTime = 0.3f;

    /// <summary>
    /// カメラ移動時の速度ベクトル
    /// </summary>
    private Vector3 velocity = Vector3.zero; //値を大きくすると追従がゆっくりになる

    #endregion


    void LateUpdate()
    {
        // プレイヤーの位置にカメラを追従させる
        Vector3 targetPosition = player.position + offset;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


        /*「カメラを追従させるときはLateUpdate内で行うと良い」
            LateUpdateは必ずUpdateの後に呼び出されるので、Updateでキャラクターが移動する
            キャラの移動処理が完了し、座標が確定してからカメラを移動させる
            この手順で処理を行えば滑らかにカメラを動かせる*/

        //カメラをプレイヤーの子オブジェクトにすると問題が多いのでそれだけはしない
    }


}
