using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 次に出てくる水をランダムで選び、そのプレハブを取り出してスポナーに引き渡す
/// </summary>
public class RandomWaterSelect : MonoBehaviour
{
    #region 変数
    /// <summary>
    /// 登場させたいオブジェクトのリスト(WaterCollision型)
    /// </summary>
    /// <param name=""></param>
    [Header("登場させたいオブジェクトをセット")]
    [SerializeField] private GameObject[] _waterPrefabs;
    /// <summary>
    /// _waterPrefabsの要素から取得したWaterCollisionを格納
    /// </summary>
    private WaterCollision[] _waterCollisions = null;
    /// <summary>
    /// _waterPrefabsの要素と対応するプール情報を格納した配列
    /// </summary>
    private ObjectPool<WaterCollision>[] _waterPrefabPools = null;
    /// <summary>
    /// 生成したいオブジェクトのプール情報
    /// </summary>
    public ObjectPool<WaterCollision> ReservedObjectPool { get; private set; } = null;
    public GameObject ReservedObject { get; private set; } = null;

    #endregion
    //初期化処理とコンポーネントの取得
    private void Awake()
    {
        InitializeVariables();
    }
    private void Start()
    {
        //_waterCollisionsの長さを_waterPrefabsの長さに合わせる
        _waterCollisions = new WaterCollision[_waterPrefabs.Length];

        //_waterPrefabPoolsの長さを_waterCollisionsに合わせる
        _waterPrefabPools = new ObjectPool<WaterCollision>[_waterPrefabs.Length];

        //_WaterPrefabsリストのオブジェクトから
        //WaterCollision、プール情報を取得してリストに格納
        for (int i = 0; i < _waterPrefabs.Length; i++)
        {
            _waterCollisions[i] = _waterPrefabs[i].GetComponent<WaterCollision>();

            _waterPrefabPools[i] =
                ObjectPoolManager.Instance.GetPoolByType(_waterCollisions[i]._waterVariousObjectData.MyWaterType);



        }

        //次に出すべきプレファブとプール情報を選出
        SelectNextDroppingGameObjectsInfo();
    }
   
    private void InitializeVariables()
    {
        ReservedObject = null;
        _waterCollisions = null;
        _waterPrefabPools = null;
    }
    /// <summary>
    /// 次に出すオブジェクトをランダムで決めて、そのオブジェクトのプール情報を渡す
    /// </summary>
    /// <returns>次に出すオブジェクトのプール情報</returns>
    public void SelectNextDroppingGameObjectsInfo()
    {
        //返す時に使う変数をわかりやすくした
        int index = Random.Range(0, _waterPrefabs.Length);//ランダムレンジ関数でランダムな値をインデックス指数に代入

        //返す構造体にオブジェクト情報とプール情報を代入
        ReservedObjectPool = _waterPrefabPools[index];

        //ほかで参照できるようにするために参照用の変数に次に出すオブジェクトの情報を入れる
        ReservedObject = _waterPrefabs[index];

    }
}
