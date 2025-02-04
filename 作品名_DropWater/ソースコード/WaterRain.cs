using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 水スポナーのスクリプト
/// </summary>
public class WaterRain : MonoBehaviour //ObjectPoolManager
{
    //[SerializeField]
    //private ObjectPoolManager _objectPoolManager = null;

    //インスペクターで連携
    [SerializeField]
    private RadomWaterSelect _randomWaterSelector = null;
    /// <summary>
    /// ランダムに選出したプレハブ(WaterCollision型)
    /// </summary>
    [SerializeField]
    private GameObject _poppedPrefab = null;
    /// <summary>
    /// 落とす予定のプレハブ(最終決定分)
    /// </summary>
    [SerializeField]
    private GameObject _droppingPrefab = null;
    /// <summary>
    /// 次に落ちてくるオブジェクトの画像イメージ
    /// </summary>
    [SerializeField]
    private NextImage _nextImage = null;
    /// <summary>
    /// 次の水オブジェクトが生成されるまでのクールタイム
    /// </summary>
    private float _nextObjectGeneratedCoolTime = 1f;
    
    private float _SpawnerMoveSpeed = 5f;
    /// <summary>
    /// スポナー移動の左側の限界値
    /// </summary>
    private float _SpawnerLeftLimit = -2.45f;
    /// <summary>
    /// スポナー移動の右側の限界値
    /// </summary>
    private float _SpawnerRightLimit = 2.45f;
    private WaitForSeconds _waitForSeconds = null;

    // Start is called before the first frame update
    void Start()
    {
        //ガーベジコレクションを防ぐため、キャッシュする
        if (_randomWaterSelector != null)
        _waitForSeconds = new WaitForSeconds(_nextObjectGeneratedCoolTime);
        //水を生成するスクリプトを引数1秒でコルーチン発動
        StartCoroutine(HandleWater());
        
    }
    /// <summary>
    /// ランダムに選出された水のオブジェクトを生成して、勝手に落ちないように子オブジェクトにする子オブジェクト
    /// </summary>
    /// <param name="delay">処理を遅らせたい時間</param>
    /// <returns></returns>
    private IEnumerator HandleWater()
    {
        _poppedPrefab = _randomWaterSelector.Pop();

        //指定時間待つ
        yield return _waitForSeconds;
        _droppingPrefab = Instantiate(_poppedPrefab.gameObject);
        #region 没オブジェクトプール
        //switch (_poppedPrefab._waterVariousObjectData.MyWaterType)
        //{
        //    case WaterVariousObjectData._waterType.Drop:

        //        MediatingObjectCreation(_poppedPrefab.gameObject);
        //        _droppingPrefab = _dropObjectPool.Get(); //落とす予定の水オブジェクトを生成
        //        break;

        //    case WaterVariousObjectData._waterType.Water:

        //        MediatingObjectCreation(_poppedPrefab.gameObject);
        //        _droppingPrefab = _waterObjectPool.Get(); //落とす予定の水オブジェクトを生成

        //        break;

        //    case WaterVariousObjectData._waterType.Puddle:

        //        MediatingObjectCreation(_poppedPrefab.gameObject);
        //        _droppingPrefab = _puddleObjectPool.Get(); //落とす予定の水オブジェクトを生成

        //        break;
        //}
        #endregion
        //受け取った生成オブジェクトを自分の子供にする
        MatchPositionAndRotationIsGeneratedObjectWithMyself();
        MakeGeneratedObjectOwnChild();
        _nextImage.NextImageInsert();
        //Rigidbody2DのKinematicを有効にすることで自由落下を防ぐ
        _droppingPrefab.GetComponent<Rigidbody2D>().isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        //水が生成されていないときは切り離せないようにする
        if (Input.GetKeyDown(KeyCode.Space) && _droppingPrefab != null)
        {
            _droppingPrefab.GetComponent<Rigidbody2D>().isKinematic = false;//水を切り離す
            _droppingPrefab.transform.SetParent(null);

            //次に取り出すオブジェクトの枠を開けておくために変数を初期化する
            InitializeVariables();

            //クールタイムを有効にして、次の生成ができるようにする
            StartCoroutine(HandleWater());

        }
        //左右キーで移動
        float horizontal = Input.GetAxisRaw("Horizontal") * _SpawnerMoveSpeed * Time.deltaTime;

        float x = Mathf.Clamp(transform.position.x + horizontal, _SpawnerLeftLimit, _SpawnerRightLimit);//最小と最大を設定し入力された値を範囲を超えないように制限
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    /// <summary>
    /// 受け取った生成オブジェクトを自分の子供にする
    /// </summary>
    private void MakeGeneratedObjectOwnChild()
    {
        _droppingPrefab.transform.SetParent(this.gameObject.transform);
    }
    /// <summary>
    /// 変数の初期化
    /// </summary>
    private void InitializeVariables()
    {
        _poppedPrefab = null;
        _droppingPrefab = null;
    }
    /// <summary>
    /// 生成されたオブジェクトの位置と回転を自分(スポナー)に合わせる
    /// </summary>
    private void MatchPositionAndRotationIsGeneratedObjectWithMyself()
    {
        //生成したオブジェクトの位置と回転を自身のオブジェクトに合わせる
        _droppingPrefab.transform.SetLocalPositionAndRotation(this.transform.position, Quaternion.identity);

    }
}
