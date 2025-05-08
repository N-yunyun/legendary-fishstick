using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
///オブジェクトスポナーのスクリプト
/// </summary>
public class WaterRain : MonoBehaviour
{
    //インスペクターで連携
    [SerializeField]
    private RandomWaterSelect _randomWaterSelector = null;
    /// <summary>
    /// ランダムに選出したオブジェクトのプール情報
    /// </summary>
    [SerializeField]
    private ObjectPool<WaterCollision> _poppedWatersPool;
    /// <summary>
    /// 落とす予定のプレハブ(最終決定分)
    /// </summary>
    [SerializeField]
    private WaterCollision _droppingPrefab = null;
    /// <summary>
    /// 次に落ちてくるオブジェクトの画像イメージ
    /// </summary>
    [SerializeField]
    private NextImage _nextImage = null;
    /// <summary>
    /// 定数データファイル
    /// </summary>
    [Header("定数のデータファイル(ScriptableObject)をセット")]
    [SerializeField] private WaterObjectConstData _waterObjectCanstConstBase;    
    /// <summary>
    /// キャッシュ用
    /// </summary>
    private WaitForSeconds _waitForSeconds = null;
    /// <summary>
    /// 落とす予定のオブジェクトのリジッドボディ
    /// </summary>
    private Rigidbody2D _droppingPrefabsRb2d;

    private float _horizontalInputValue = default;
    private float _tranformX = default;


    //初期化処理
    private void Awake()
    {
        InitializeVariables();
    }
    void Start()
    {
        //ガーベジコレクションを防ぐため、キャッシュする
        if (_randomWaterSelector != null)
            _waitForSeconds = new WaitForSeconds(_waterObjectCanstConstBase.NextObjectGeneratedCoolTime);

        //水を生成するスクリプトを引数1秒でコルーチン発動
        StartCoroutine(HandleWater());

    }
    /// <summary>
    /// ランダムに選出された水のオブジェクトを生成して、子オブジェクトにする
    /// </summary>
    private IEnumerator HandleWater()
    {
        _poppedWatersPool = _randomWaterSelector.SelectNextWaterObject();

        //指定時間待つ
        yield return _waitForSeconds;

        //プールから指定のオブジェクトを生成
        _droppingPrefab = _poppedWatersPool.Get();

        //生成したオブジェクトの物理挙動を制御するために取得する
        _droppingPrefabsRb2d = _droppingPrefab.gameObject.GetComponent<Rigidbody2D>();

        //生成後処理(自身のtransformに生成したオブジェクトを合わせ後、そのまま自身の子にする)
        MatchPositionAndRotationIsGeneratedObjectWithMyself();
        MakeGeneratedObjectOwnChild();

        //画像を次に出てるオブジェクトに差し替える
        _nextImage.NextImageInsert();


        //自由落下しないようにする
        _droppingPrefabsRb2d.isKinematic = true;
    }

    void Update()
    {
        //オブジェクトの位置が制限値内に収まるように制限する
        if(!Input.GetKeyDown(KeyCode.RightArrow)&&!Input.GetKeyDown(KeyCode.LeftArrow)&&!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        //左右キーで移動
        _horizontalInputValue = Input.GetAxisRaw("Horizontal") * _waterObjectCanstConstBase.SpawnerMoveSpeed * Time.deltaTime;
        _tranformX = Mathf.Clamp(transform.position.x + _horizontalInputValue, _waterObjectCanstConstBase.SpawnerLeftLimit, _waterObjectCanstConstBase.SpawnerRightLimit);//最小と最大を設定し入力された値を範囲を超えないように制限
        transform.position = new Vector3(_tranformX, transform.position.y, transform.position.z);

        //水が生成されていないときは切り離せないようにする
        if (Input.GetKeyDown(KeyCode.Space) && _droppingPrefab != null)
        {
            //自由落下をオンにしてオブジェクトを切り離す
            _droppingPrefabsRb2d.isKinematic = false;
            _droppingPrefab.transform.SetParent(null);

            //次に取り出すオブジェクトの枠を開けておくために変数を初期化する
            InitializeVariables();

            //クールタイムを有効にして、次の生成ができるようにする
            StartCoroutine(HandleWater());

        }

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
        _droppingPrefabsRb2d = null;
    }
    /// <summary>
    /// 生成されたオブジェクトの位置と回転を自分(スポナー)に合わせる
    /// </summary>
    private void MatchPositionAndRotationIsGeneratedObjectWithMyself()
    {
        //生成したオブジェクトの位置と回転を自身のオブジェクトに合わせる
        _droppingPrefab.gameObject.transform.SetLocalPositionAndRotation(this.transform.position, Quaternion.identity);

    }
}
