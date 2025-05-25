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
    private Dictionary<VariousObjectData.ObjectType, ObjectPool<ObjectsCollision>> _poolByObjectsType = default;
    /// <summary>
    /// 各オブジェクトデータや定数データをすべて格納したデータファイル
    /// </summary>
    [Header("オブジェクトデータ(ScriptableObject)をセット")]
    [SerializeField]
    private ObjectsDataBase _dataArray = default;
    #endregion
    protected override void Awake()
    {
        // 基底クラスの Awake() の処理を最初に実行する
        base.Awake();

        //初期化処理
        _poolByObjectsType =
                new Dictionary<VariousObjectData.ObjectType, ObjectPool<ObjectsCollision>>();
        //オブジェクトプールを初期化
        InitializePools();

    }
    /// <summary>
    /// データファイルを基に各オブジェクトプールを初期化する
    /// </summary>
    private void InitializePools()
    {
        //_waterDataArraysの要素(登場させたいオブジェクトのデータの種類)数だけ行う
        foreach (VariousObjectData data in _dataArray._dataList)
        {
            //要素のプレハブ
            GameObject indexPrefab = data.MyObjectPrefab;

            //要素のオブジェクトタイプ
            VariousObjectData.ObjectType indexType = data.MyType;

            ObjectPool<ObjectsCollision> objectPool = new ObjectPool<ObjectsCollision>(
                createFunc: () =>
                {
                    //要素のプレハブと同じプレハブが生成されるようにする
                    GameObject instance = Instantiate(indexPrefab);

                    //WaterCollision型なのでコンポーネントを取得
                    ObjectsCollision prefabObjectsCollision = instance.GetComponent<ObjectsCollision>();

                    //コンポーネントのnullチェック
                    if (prefabObjectsCollision == null)
                    {
                        Debug.LogError("WaterCollision がアタッチされていません：" + instance.name);
                    }

                    return prefabObjectsCollision;
                },
                actionOnGet: obj =>
                {
                    //オブジェクトのコンポーネントのみ有効化
                    obj.EnableVisuals();
                },
                actionOnRelease: obj =>
                {
                    //オブジェクトのコンポーネントのみ無効化
                    obj.DisableVisuals();
                    //物理挙動の値をリセット
                    obj.ResetPhysicsState();
                },
                collectionCheck: true,
                defaultCapacity: _dataArray._constData.InitialCapacity,
                maxSize: _dataArray._constData.MaximumCapacity
            );
            //初期容量までオブジェクトを生成して格納する
            CreateObjectsUpToCapacity(objectPool, _dataArray._constData.InitialCapacity);
            _poolByObjectsType[indexType] = objectPool;
        }
    }
    /// <summary>
    /// 初期容量のサイズまでオブジェクトを生成してプールに格納する
    /// </summary>
    /// <param name="pool"></param>
    /// <param name="capacity"></param>
    public void CreateObjectsUpToCapacity(ObjectPool<ObjectsCollision> pool, int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            ObjectsCollision objectsCollision = pool.Get();
            pool.Release(objectsCollision);
        }
    }
    /// <summary>
    /// 対応するオブジェクトプールを取得する (ObjectsCollision から呼び出す用)
    /// </summary>
    /// <param name="type">プールを取得したいオブジェクトのMyType</param>
    /// <returns>対応する ObjectPool<GameObject>。見つからない場合は null。</returns>
    public ObjectPool<ObjectsCollision> GetPoolByType(VariousObjectData.ObjectType type)
    {
        _poolByObjectsType.TryGetValue(type, out ObjectPool<ObjectsCollision> pool);
        return pool;
    }

}

