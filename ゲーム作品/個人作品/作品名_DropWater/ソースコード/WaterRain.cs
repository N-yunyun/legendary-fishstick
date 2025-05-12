using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
///オブジェクトスポナーのスクリプト
/// </summary>
public class WaterRain : MonoBehaviour
{
    #region 変数
    //インスペクターで連携
    [SerializeField]
    private RandomWaterSelect _randomWaterSelector;
    /// <summary>
    /// ランダムに選出したオブジェクトのプール情報
    /// </summary>
    private ObjectPool<WaterCollision> _poppedObjectPool = default;
    /// <summary>
    /// 落とす予定のプレハブ
    /// </summary>
    private WaterCollision _droppingPrefab = default;
    /// <summary>
    /// 次に落ちてくるオブジェクトの画像イメージ
    /// </summary>
    [SerializeField]
    private NextImage _nextImage = default;
    /// <summary>
    /// 定数データファイル
    /// </summary>
    [Header("定数のデータファイル(ScriptableObject)をセット")]
    [SerializeField] private WaterObjectConstData _waterObjectConstBase = default;
    /// <summary>
    /// キャッシュ用
    /// </summary>
    private WaitForSeconds _specifiedWaitForSeconds = null;
    /// <summary>
    /// 落とす予定のオブジェクトのリジッドボディ
    /// </summary>
    private Rigidbody2D _droppingPrefabsRigidbody;
    /// <summary>
    /// プレイヤーの横の入力値
    /// </summary>
    private float _horizontalInputValue = default;
    /// <summary>
    /// 調整されたスポナーのpositionのxの値
    /// </summary>
    private float _ControlledTransformPositionX = default;
    #endregion

    //初期化処理
    private void Awake()
    {
        InitializeVariables();
    }
    private void Start()
    {
        //ガーベジコレクションを防ぐため、キャッシュする
        _specifiedWaitForSeconds = new WaitForSeconds(_waterObjectConstBase.NextObjectGeneratedCoolTime);

        //水を生成するスクリプトを引数1秒でコルーチン発動
        StartCoroutine(HandleWater());

    }
    /// <summary>
    /// ランダムに選出された水のオブジェクトを生成して、子オブジェクトにする
    /// </summary>
    private IEnumerator HandleWater()
    {
        //選出したオブジェクトのプール情報を受け取る
        ReceiveNextPoppedObjectPool();

        //指定時間待つ
        yield return _specifiedWaitForSeconds;

        //プールから指定のオブジェクトを生成
        _droppingPrefab = _poppedObjectPool.Get();

        //自由落下を制御するため、Rigidbody2Dを取得する
        _droppingPrefabsRigidbody = _droppingPrefab.GetComponent<Rigidbody2D>();

        //生成後処理(自身のtransformに生成したオブジェクトを合わせ後、そのまま自身の子にする)
        SetTransformToSelf();
        MakeGeneratedObjectOwnChild();

        //画像を次に出てるオブジェクトに差し替える
        _nextImage.NextImageInsert();

        //自由落下しないようにする
        _droppingPrefabsRigidbody.isKinematic = true;
    }

    private void Update()
    {
        //左右キーで横方向に移動
        MoveHorizontall();
        //移動が指定した範囲を超えないように制限する
        MoveAreaRestricted();

        //水が生成されていないときは切り離せないようにする
        if (Input.GetKey(KeyCode.Space) && _droppingPrefab != null)
        {
            //オブジェクトを切り離す
            DetachObject();

            //次に取り出すオブジェクトの枠を開けておくために変数を初期化する
            InitializeVariables();

            //クールタイムを有効にして、次の生成ができるようにする
            StartCoroutine(HandleWater());

        }

    }
    /// <summary>
    /// 選出されたオブジェクトのプール情報を受け取る
    /// </summary>
    private void ReceiveNextPoppedObjectPool()
    {
        _randomWaterSelector.SelectNextDroppingGameObjectsInfo();
        _poppedObjectPool = _randomWaterSelector.ReservedObjectPool;
    }
    /// <summary>
    /// 受け取った生成オブジェクトを自分の子供にする
    /// </summary>
    private void MakeGeneratedObjectOwnChild()
    {
        _droppingPrefab.gameObject.transform.SetParent(this.gameObject.transform);
    }
    /// <summary>
    /// 変数の初期化
    /// </summary>
    private void InitializeVariables()
    {
        _droppingPrefab = null;
        _droppingPrefabsRigidbody = null;
    }
    /// <summary>
    /// 生成されたオブジェクトの位置と回転を自分(スポナー)に合わせる
    /// </summary>
    private void SetTransformToSelf()
    {
        //生成したオブジェクトの位置と回転を自身のオブジェクトに合わせる
        _droppingPrefab.gameObject.transform.SetLocalPositionAndRotation(this.transform.position, Quaternion.identity);
    }
    /// <summary>
    ///オブジェクトを切り離す
    /// </summary>
    private void DetachObject()
    {
        //自由落下をオンにしてオブジェクトを切り離す
        _droppingPrefabsRigidbody.isKinematic = false;
        _droppingPrefab.transform.SetParent(null);

    }
    /// <summary>
    /// 左右キーの入力値で横に移動する
    /// </summary>
    private void MoveHorizontall()
    {
        //左右キーで移動
        _horizontalInputValue = 
            Input.GetAxisRaw("Horizontal") * _waterObjectConstBase.SpawnerMoveSpeed * Time.deltaTime;
    }
    /// <summary>
    /// 移動範囲を制限する
    /// </summary>
    private void MoveAreaRestricted()
    {
        //最小と最大を設定し入力された値を範囲を超えないように制限
        _ControlledTransformPositionX = Mathf.Clamp(transform.position.x + _horizontalInputValue, _waterObjectConstBase.SpawnerLeftLimit, _waterObjectConstBase.SpawnerRightLimit);
        transform.position = new Vector3(_ControlledTransformPositionX, transform.position.y, transform.position.z);

    }
}
