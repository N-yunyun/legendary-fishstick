using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
/// <summary>
/// オブジェクトプールを管理する(シングルトン)
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    #region 変数
    public static ObjectPoolManager Instance { get; private set; }
    /// <summary>
    /// WaterType に対応する WaterCollision のプールを格納した Dictionary
    /// </summary>
    private Dictionary<WaterVariousObjectData.WaterType, ObjectPool<WaterCollision>> _poolByWaterType;
    /// <summary>
    /// 各オブジェクトデータや定数データをすべて格納したデータファイル
    /// </summary>
    [Header("オブジェクトデータ(ScriptableObject)をセット")]
    [SerializeField]
    private WaterDataArray _waterDataArray;
    #endregion
    void Awake()
    {
        //シングルトンのnullチェック
        // すでにインスタンスが存在する場合（別の ObjectPoolManager がシーンに存在する場合）
        if (Instance != null && Instance != this)
        {
            Debug.LogError("ObjectPoolManager のインスタンスが複数存在します！");

            // 新しく作られた方を破棄
            Destroy(gameObject);
        }
        else
        {
            //このインスタンスを唯一のインスタンスとして保存
            Instance = this;
            //初期化処理
            _poolByWaterType = 
                new Dictionary<WaterVariousObjectData.WaterType, ObjectPool<WaterCollision>>();
            //オブジェクトプールを初期化
            InitializePools();
        }
    }
    /// <summary>
    /// データファイルを基に各オブジェクトプールを初期化する
    /// </summary>
    private void InitializePools()
    {
        //_waterDataArraysの要素(登場させたいオブジェクトのデータの種類)数だけ行う
        foreach (WaterVariousObjectData data in _waterDataArray._waterDataArrays)
        {
            GameObject indexPrefab = data.MyObjectPrefab;
            WaterVariousObjectData.WaterType indexType = data.MyWaterType;

            ObjectPool<WaterCollision> objectPool = new ObjectPool<WaterCollision>(
                createFunc: () =>
                {
                    GameObject instance = Instantiate(indexPrefab);
                    WaterCollision prefabWaterCollision = instance.GetComponent<WaterCollision>();

                    //コンポーネントのnullチェック
                    if (prefabWaterCollision == null)
                    {
                        Debug.LogError("WaterCollision がアタッチされていません：" + instance.name);
                    }
                    return prefabWaterCollision;
                },
                actionOnGet: obj => obj.gameObject.SetActive(true),
                actionOnRelease: obj => obj.gameObject.SetActive(false),
                actionOnDestroy: obj => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: 40
            );

            _poolByWaterType[indexType] = objectPool;
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

