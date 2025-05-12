using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
/// <summary>
/// オブジェクトプールを管理する(シングルトン)
/// </summary>
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    #region 変数
    /// <summary>
    /// WaterType に対応する WaterCollision のプールを格納した Dictionary
    /// </summary>
    private Dictionary<WaterVariousObjectData.WaterType, ObjectPool<WaterCollision>> _poolByWaterType = default;
    /// <summary>
    /// 各オブジェクトデータや定数データをすべて格納したデータファイル
    /// </summary>
    [Header("オブジェクトデータ(ScriptableObject)をセット")]
    [SerializeField]
    private WaterDataBase _waterDataArray = default;
    #endregion
    protected override void Awake()
    {
        // 基底クラスの Awake() の処理を最初に実行する
        base.Awake();

        //初期化処理
        _poolByWaterType =
                new Dictionary<WaterVariousObjectData.WaterType, ObjectPool<WaterCollision>>();
        //オブジェクトプールを初期化
        InitializePools();

    }
    /// <summary>
    /// データファイルを基に各オブジェクトプールを初期化する
    /// </summary>
    private void InitializePools()
    {
        //_waterDataArraysの要素(登場させたいオブジェクトのデータの種類)数だけ行う
        foreach (WaterVariousObjectData data in _waterDataArray._waterDataList)
        {
            //要素のプレハブ
            GameObject indexPrefab = data.MyObjectPrefab;

            //要素のオブジェクトタイプ
            WaterVariousObjectData.WaterType indexType = data.MyWaterType;

            //
            ObjectPool<WaterCollision> objectPool = new ObjectPool<WaterCollision>(
                createFunc: () =>
                {
                    //要素のプレハブと同じプレハブが生成されるようにする
                    GameObject instance = Instantiate(indexPrefab);

                    //WaterCollision型なのでコンポーネントを取得
                    WaterCollision prefabWaterCollision = instance.GetComponent<WaterCollision>();

                    //コンポーネントのnullチェック
                    if (prefabWaterCollision == null)
                    {
                        Debug.LogError("WaterCollision がアタッチされていません：" + instance.name);
                    }

                    return prefabWaterCollision;
                },
                actionOnGet: obj =>
                {
                    obj.EnableVisuals();
                },
                actionOnRelease: obj =>
                {
                    obj.DisableVisuals();
                    obj.ResetPhysicsState();
                },
                collectionCheck: true,
                defaultCapacity: _waterDataArray._waterObjectConstBase.InitialCapacity,
                maxSize: _waterDataArray._waterObjectConstBase.MaximumCapacity
            );

            //初期容量までオブジェクトを生成して格納する
            CreateObjectsUpToCapacity(objectPool, _waterDataArray._waterObjectConstBase.InitialCapacity);
            _poolByWaterType[indexType] = objectPool;
        }
    }
    /// <summary>
    /// 初期容量のサイズまでオブジェクトを生成してプールに格納する
    /// </summary>
    /// <param name="pool"></param>
    /// <param name="capacity"></param>
    public void CreateObjectsUpToCapacity(ObjectPool<WaterCollision> pool, int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            WaterCollision waterObj = pool.Get();
            pool.Release(waterObj);
        }
    }
    /// <summary>
    /// 対応するオブジェクトプールを取得する (WaterCollision から呼び出す用)
    /// </summary>
    /// <param name="type">プールを取得したいオブジェクトのMyWaterType</param>
    /// <returns>対応する ObjectPool<GameObject>。見つからない場合は null。</returns>
    public ObjectPool<WaterCollision> GetPoolByType(WaterVariousObjectData.WaterType type)
    {
        _poolByWaterType.TryGetValue(type, out ObjectPool<WaterCollision> pool);
        return pool;
    }

}

