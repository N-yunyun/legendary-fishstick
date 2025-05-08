using UnityEngine;
using System;
using UnityEngine.Pool;
/// <summary>
/// オブジェクト自身の進化処理などを行う
/// </summary>

//Rigidbody2D必須
[RequireComponent(typeof(Rigidbody2D))]
public class WaterCollision : MonoBehaviour
{
    #region 変数
    /// <summary>
    /// 自身のタイプに対応するオブジェクトプール
    /// </summary>
    private ObjectPool<WaterCollision> _myObjectPool;
    /// <summary>
    /// 自身の進化先のオブジェクトのタイプに対応するプール
    /// </summary>
    private ObjectPool<WaterCollision> _evolvedObjectPool;
    /// <summary>
    /// 定数データファイル(計算の値などに使用)
    /// </summary>
    [Header("定数データファイル(ScriptableObject)をセット")]
    [SerializeField] private WaterObjectConstData _waterObjectConstData;
    /// <summary>
    /// オブジェクト各種類のデータファイル(オブジェクトの固有の情報を取得するのに使用)
    /// </summary>
    [Header("オブジェクトデータ(ScriptableObject)をセット")]
    [SerializeField] public WaterVariousObjectData _waterVariousObjectData;
    /// <summary>
    /// 出現するオブジェクトに番号を割り振るための変数
    /// </summary>
    private static int _serialNumber = 0;
    /// <summary>
    /// 出現するオブジェクトの番号
    /// </summary>
    [Header("出現するオブジェクトの番号")]
    [SerializeField]
    private int _mySerialNumber = 0;
    /// <summary>
    /// ぶつかった相手のゲームオブジェクト(WaterCollision型)
    /// </summary>
    private WaterCollision _evolvedGameObject;
    /// <summary>
    /// 二つの水がぶつかった接触部分の中心の位置
    /// </summary>
    private Vector3 _myselfAndOthersCenter = default;
    /// <summary>
    /// 二つの水の間を滑らかに移動させるための補間
    /// </summary>
    private Quaternion _myselfAndOthersRotation = default;
    /// <summary>
    /// 進化先(生成するオブジェクト)の速度の入れ物
    /// </summary>
    private Vector3 _evolvedGameObjectVelocity = default;
    /// <summary>
    /// ぶつかった相手のWaterCollision
    /// </summary>
    private WaterCollision _opponentsWaterCollision;
    /// <summary>
    /// 進化先のゲームオブジェクトのRigidbody2D
    /// </summary>
    private Rigidbody2D _evolvedRb2d;
    /// <summary>
    /// 自身のRigidbody2D
    /// </summary>
    private Rigidbody2D _thisRb2d;
    /// <summary>
    /// ぶつかった相手のRigidbody2D
    /// </summary>
    private Rigidbody2D _collidedRb2d;
    /// <summary>
    /// angularVelocityの値を変更するときに使う
    /// </summary>
    private float _myselfAndOtherPersonsAngularVelocity = default;
    /// <summary>
    /// スコア加算処理を行うためのイベント(シーンをまたいでスコアを保持する)
    /// </summary>
    //自分用のメモ
    //関数を参照型として扱い、沢山保持して一斉に実行できる参照型
    public static Action<int> OnScoreAdded;
    #endregion 
    private void Awake()
    {
            //自分のリジッドボディを取得
            _thisRb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        //水の通し番号をコピー
        _mySerialNumber = _serialNumber;
        //コピー後に番号を増やして、次に来るオブジェクトと番号が被らないようにする
        _serialNumber++;

        //プールの取得
        _myObjectPool = GetObjectPool(_waterVariousObjectData.MyWaterType);

        //進化先のオブジェクトのnullチェック(nullだと最終進化系)
        if (_waterVariousObjectData.NextEvolvingObject != null)
        {
            //nullじゃなければ進化先のオブジェクトのプールも取得
            _evolvedObjectPool =
                GetObjectPool
                (_waterVariousObjectData.NextEvolvingObject._waterVariousObjectData.MyWaterType);
            
            //取得後のプールnullチェック
            if (_evolvedObjectPool == null)
            {
                Debug.LogError("進化先のオブジェクトに対応するプールがありません！");

            }
        }

        //オブジェクトプールのnullチェック
        if (_myObjectPool == null)
        {
            Debug.LogError("自分のオブジェクトに対応するプールがありません！");
            return;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ぶつかった相手にWaterCollisionがついてなくて、相手の水タイプが自分と違うのであればリターン
        if (!collision.gameObject.TryGetComponent(out _opponentsWaterCollision))
        {
            return;
        }
        if (_opponentsWaterCollision._waterVariousObjectData.MyWaterType != this._waterVariousObjectData.MyWaterType)
        {
            //使わないので初期化する
            _opponentsWaterCollision = null;
            return;
        }

        //自分と同じ種類のオブジェクトで、番号が小さい(古い)方のオブジェクトだけ以下の処理が走る
        if (_mySerialNumber < _opponentsWaterCollision._mySerialNumber)
        {
            GetOthersRigidbody2D();

            //進化先のオブジェクトを自然な位置で生成するために線形補間を取得する
            GetCollisionCenterPositionAndLinearInterpolation();

            //先にスコアを加算
            OnScoreAdded?.Invoke(_waterVariousObjectData.GetSetScore);

            //プールに戻す前に自身の番号を初期化する
            GameObjectsInitializeNumbers();

            // 自分と相手をプールに戻す
            _myObjectPool.Release(this);
            _myObjectPool.Release(_opponentsWaterCollision);

            //進化先のオブジェクトが入っていれば進化先のオブジェクトの生成処理
            if (_waterVariousObjectData.NextEvolvingObject != null)
            {
                //進化先のプールからGet
                _evolvedGameObject = _evolvedObjectPool.Get();
                _evolvedRb2d = _evolvedGameObject._thisRb2d;

                //生成したオブジェクトのTransform調整
                SetNextObjectTransform();
                //TakeVelocityAndAngularvelocity();
                GiveVelocityAndAngularVelocity();
            }
            //初期化処理
            InitializeVariables();
            ResetComponents();
        }

    }
    /// <summary>
    /// ぶつかった相手のRigidbody2Dを取得する
    /// </summary>
    private void GetOthersRigidbody2D()
    {
        _collidedRb2d = _opponentsWaterCollision._thisRb2d;
    }
    /// <summary>
    /// 生成したオブジェクトの位置と回転を変更
    /// </summary>
    private void SetNextObjectTransform()
    {
        //生成したオブジェクトの位置と回転を同時に変更する
        _evolvedGameObject.transform.SetLocalPositionAndRotation(this._myselfAndOthersCenter, this._myselfAndOthersRotation);
    }
    ///// <summary>
    ///// ぶつかった相手と自分の速度と角速度を取得して足して半分に割る
    ///// </summary>
    //private void TakeVelocityAndAngularvelocity()
    //{
    //    //ぶつかった相手と自分の速度と角速度を足して割る
    //    _evolvedGameObjectVelocity =
    //        (_thisRb2d.velocity + _collidedRb2d.velocity) / _waterObjectConstData.GetDivideIntoTwo;

    //    _myselfAndOtherPersonsAngularVelocity =
    //        (_thisRb2d.angularVelocity + _collidedRb2d.angularVelocity) / _waterObjectConstData.GetDivideIntoTwo;
    //}
    /// <summary>
    /// 新しく生成したオブジェクトに速度と角速度を与える
    /// </summary>
    private void GiveVelocityAndAngularVelocity()
    {
        //①ぶつかった相手と自分の速度の平均を求める
        _evolvedGameObjectVelocity =
            (_thisRb2d.velocity + _collidedRb2d.velocity) / _waterObjectConstData.GetDivideIntoTwo;
        
        //②ぶつかった相手と自分の角速度の平均も求める
        _myselfAndOtherPersonsAngularVelocity =
            (_thisRb2d.angularVelocity + _collidedRb2d.angularVelocity) / _waterObjectConstData.GetDivideIntoTwo;

        //①と②で計算した値をそれぞれ
        //新しく生成したオブジェクトの速度と角速度に与える
        _evolvedRb2d.velocity = _evolvedGameObjectVelocity;
        _evolvedRb2d.angularVelocity = _myselfAndOtherPersonsAngularVelocity;
    }
    /// <summary>
    /// 二つのオブジェクトの接触部分の中心の位置と線形補間を取得する
    /// </summary>
    private void GetCollisionCenterPositionAndLinearInterpolation()
    {
        //二つの水がぶつかった接触部分の中心の位置
        _myselfAndOthersCenter = (transform.position + _opponentsWaterCollision.transform.position) / _waterObjectConstData.GetDivideIntoTwo;
        //二つの水の間を滑らかに移動させるための補間
        _myselfAndOthersRotation = Quaternion.Lerp(transform.rotation, _opponentsWaterCollision.transform.rotation, _waterObjectConstData.GetMyCurrentPosition);
    }
    /// <summary>
    /// 自分と進化先のオブジェクトのプールを取得する
    /// </summary>
    private ObjectPool<WaterCollision> GetObjectPool(WaterVariousObjectData.WaterType waterType)
    {
        //自分のオブジェクトと進化先のプレハブに対応するプールを取得

        return ObjectPoolManager.Instance.GetPoolByType(waterType);
    }
    /// <summary>
    /// 変数を初期化する
    /// </summary>
    private void InitializeVariables()
    {
        _evolvedObjectPool = null;
        _evolvedGameObjectVelocity = default;
        _myselfAndOtherPersonsAngularVelocity = default;
        _myselfAndOthersCenter = default;
        _myselfAndOthersRotation = default;
    }
    /// <summary>
    /// コンポーネントの変数を初期化する
    /// </summary>
    private void ResetComponents()
    {
        _evolvedRb2d = null;
        _collidedRb2d = null;
        _evolvedGameObject = null;
        _opponentsWaterCollision = null;

    }
    /// <summary>
    /// ゲームオブジェクト依存の変数を初期化する
    /// </summary>
    private void GameObjectsInitializeNumbers()
    {
        _mySerialNumber = default;
        _opponentsWaterCollision._mySerialNumber = default;
    }
    
}