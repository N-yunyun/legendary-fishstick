using UnityEngine;
using System;
using UnityEngine.Pool;
/// <summary>
/// オブジェクト自身の進化処理などを行う
/// </summary>

//Rigidbody2D必須
[RequireComponent(typeof(Rigidbody2D))]
public class WaterCollision : ObjectCreationMediate
{
    #region 変数
    /// <summary>
    /// 自身のタイプに対応するオブジェクトプール
    /// </summary>
    private ObjectPool<WaterCollision> _myObjectPool = null;
    /// <summary>
    /// 自身の進化先のオブジェクトのタイプに対応するプール
    /// </summary>
    private ObjectPool<WaterCollision> _evolvedObjectPool = null;
    /// <summary>
    /// 定数データファイル(計算の値などに使用)
    /// </summary>
    [Header("定数データファイル(ScriptableObject)をセット")]
    [SerializeField] private ConstData _constData = null;
    /// <summary>
    /// オブジェクト各種類のデータファイル(オブジェクトの固有の情報を取得するのに使用)
    /// </summary>
    [Header("オブジェクトデータ(ScriptableObject)をセット")]
    [SerializeField] public WaterVariousObjectData _waterVariousObjectData = null;
    /// <summary>
    /// 接触した相手のゲームオブジェクト(WaterCollision型)
    /// </summary>
    private WaterCollision _evolvedGameObject = null;
    /// <summary>
    /// 二つの水が接触した接触部分の中心の位置
    /// </summary>
    private Vector3 _thisAndOthersCenter = default;
    /// <summary>
    /// 二つの水の間を滑らかに移動させるための補間
    /// </summary>
    private Quaternion _thisAndOthersRotation = default;
    /// <summary>
    /// 進化先(生成するオブジェクト)の速度の入れ物
    /// </summary>
    private Vector3 _evolvedGameObjectVelocity = default;
    /// <summary>
    /// 接触した相手のWaterCollision
    /// </summary>
    private WaterCollision _othersWaterCollision = null;
    /// <summary>
    /// 進化先のゲームオブジェクトのRigidbody2D
    /// </summary>
    private Rigidbody2D _evolvedRigidbody = null;
    /// <summary>
    /// 自身のRigidbody2D
    /// </summary>
    private Rigidbody2D _thisRigidbody = null;
    /// <summary>
    /// 接触した相手のRigidbody2D
    /// </summary>
    private Rigidbody2D _othersRigidbody = null;
    /// <summary>
    /// angularVelocityの値を変更するときに使う
    /// </summary>
    private float _myselfAndOtherAngularVelocity = default;
    /// <summary>
    /// プールに返却するかのフラグ
    /// </summary>
    private bool _shouldRelease = false;
    private SpriteRenderer _sprite = null;
    private Collider2D _collider = null;
    /// <summary>
    /// 自身のインスタンスの一意の識別子
    /// </summary>
    private int _instanceID
    {
        get { return GetInstanceID(); }
    }
    #endregion
    private void Awake()
    {
        InitializeVariables();
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        //自分のリジッドボディを取得
        _thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //プールの取得
        _myObjectPool = GetObjectPool(_waterVariousObjectData.MyType);

        //進化先のオブジェクトのnullチェック(nullだと最終進化系)
        if (_waterVariousObjectData.NextEvolvingObject != null)
        {
            //nullじゃなければ進化先のオブジェクトのプールも取得
            _evolvedObjectPool =
                GetObjectPool
                (_waterVariousObjectData.NextEvolvingObject._waterVariousObjectData.MyType);

            //取得後のプールnullチェック
            if (_evolvedObjectPool == null)
            {
                Debug.LogError("自分のオブジェクト名は！" + this.name);
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
        //接触した相手にWaterCollisionがなく、異なるオブジェクトのタイプであれば処理しない
        if (!collision.gameObject.TryGetComponent(out _othersWaterCollision))
        {
            return;
        }
        if (_othersWaterCollision._waterVariousObjectData.MyType != this._waterVariousObjectData.MyType)
        {
            //使わないので初期化する
            _othersWaterCollision = null;
            return;
        }
        //以下接触したオブジェクトが同じオブジェクトタイプだった場合にのみ処理する

        //インスタンスIDが小さいほうが進化、消去処理などを担当する
        _shouldRelease = 
            _instanceID < _othersWaterCollision._instanceID;
        
        if (_shouldRelease)
        {
            //接触した相手のRigidbody2Dを取得
            GetOthersRigidbody2D();

            //進化先のオブジェクトを自然な位置で生成するために線形補間を取得する
            GetCollisionCenterPositionAndLinearInterpolation();

            //先にスコアを加算
            ScoreManager.Instance?.AddScore(_waterVariousObjectData.GetSetScore);

            //自分と相手をプールに戻す
            ReleaseObject(_myObjectPool, this);
            ReleaseObject(_myObjectPool, _othersWaterCollision);

            //進化先のオブジェクトが入っていれば進化先のオブジェクトの生成処理
            if (_waterVariousObjectData.NextEvolvingObject != null)
            {
                //進化先のプールからGet
                _evolvedGameObject = SpawnObject(_evolvedObjectPool);
                _evolvedRigidbody = _evolvedGameObject._thisRigidbody;

                //生成したオブジェクトのTransform調整
                SetNextObjectTransform();
                GiveVelocityAndAngularVelocity();
            }
        }
        //初期化処理
        InitializeVariables();
        ResetComponents();
    }
    /// <summary>
    /// 接触した相手のRigidbody2Dを取得する
    /// </summary>
    private void GetOthersRigidbody2D()
    {
        _othersRigidbody = _othersWaterCollision._thisRigidbody;
    }
    /// <summary>
    /// 生成したオブジェクトの位置と回転を変更
    /// </summary>
    private void SetNextObjectTransform()
    {
        //生成したオブジェクトの位置と回転を同時に変更する
        _evolvedGameObject.transform.SetLocalPositionAndRotation(this._thisAndOthersCenter, this._thisAndOthersRotation);
    }
    /// <summary>
    /// 新しく生成したオブジェクトに速度と角速度を与える
    /// </summary>
    private void GiveVelocityAndAngularVelocity()
    {
        //�@接触した相手と自分の速度の平均を求める
        _evolvedGameObjectVelocity =
            (_thisRigidbody.velocity + _othersRigidbody.velocity) / _constData.HalfDivider;

        //�A接触した相手と自分の角速度の平均も求める
        _myselfAndOtherAngularVelocity =
            (_thisRigidbody.angularVelocity + _othersRigidbody.angularVelocity) / _constData.HalfDivider;

        //�@と�Aで計算した値をそれぞれ
        //新しく生成したオブジェクトの速度と角速度に与える
        _evolvedRigidbody.velocity = _evolvedGameObjectVelocity;
        _evolvedRigidbody.angularVelocity = _myselfAndOtherAngularVelocity;
    }
    /// <summary>
    /// 二つのオブジェクトの接触部分の中心の位置と線形補間を取得する
    /// </summary>
    private void GetCollisionCenterPositionAndLinearInterpolation()
    {
        //二つの水がぶつかった接触部分の中心の位置
        _thisAndOthersCenter = (transform.position + _othersWaterCollision.transform.position) / _constData.HalfDivider;
        //二つの水の間を滑らかに移動させるための補間
        _thisAndOthersRotation = Quaternion.Lerp(transform.rotation, _othersWaterCollision.transform.rotation, _constData.MyCurrentPosition);
    }
    /// <summary>
    /// 自分と進化先のオブジェクトのプールを取得する
    /// </summary>
    private ObjectPool<WaterCollision> GetObjectPool(WaterVariousObjectData.ObjectType waterType)
    {
        //自分のオブジェクトと進化先のプレハブに対応するプールを取得
        return ObjectPoolManager.Instance.GetPoolByType(waterType);
    }
    /// <summary>
    /// 変数を初期化する
    /// </summary>
    private void InitializeVariables()
    {
        _evolvedGameObjectVelocity = default;
        _myselfAndOtherAngularVelocity = default;
        _thisAndOthersCenter = default;
        _thisAndOthersRotation = default;
    }
    /// <summary>
    /// コンポーネントの変数を初期化する
    /// </summary>
    private void ResetComponents()
    {
        _evolvedRigidbody = null;
        _othersRigidbody = null;
        _evolvedGameObject = null;
        _othersWaterCollision = null;

    }
    /// <summary>
    /// オブジェクト(コンポーネント)を有効化する
    /// </summary>
    public void EnableVisuals()
    {
        //スプライトとコライダーを無効化する
        _sprite.enabled = true;
        _collider.enabled = true;
        _thisRigidbody.simulated = true;
    }
    /// <summary>
    /// コンポーネントを無効化する
    /// </summary>
    public void DisableVisuals()
    {
        //スプライトとコライダーを無効化する
        _sprite.enabled = false;
        _collider.enabled = false;
        //自由落下をオフにしてオブジェクトが落ちないようにする
        _thisRigidbody.simulated = false;
    }
    /// <summary>
    /// Rigidbody2Dの値をリセットする
    /// </summary>
    public void ResetPhysicsState()
    {
        _thisRigidbody.velocity = Vector2.zero;
        _thisRigidbody.angularVelocity = default;
        _thisRigidbody.rotation = default;
    }
}