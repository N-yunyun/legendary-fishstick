using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// オブジェクトの生成を仲介するクラス
/// </summary>
public class ObjectCreationMediate : MonoBehaviour
{
    [SerializeField]
    private MaximumEvolutionData _waterMaximumEvolutionData;
    /// <summary>
    /// 受け取ったオブジェクトプールからオブジェクトをゲットしてくる
    /// </summary>
    /// <param name="getObjectPool">取得したいオブジェクト</param>
    /// <returns>プールから取得したオブジェクト</returns>
    public ObjectsCollision SpawnObject(ObjectPool<ObjectsCollision> getObjectPool)
    {
        return getObjectPool.Get();
    }
    /// <summary>
    /// プールに返却したいオブジェクトをプールに返却する
    /// </summary>
    /// <param name="releaseObjectPool">返却したいオブジェクトのプール</param>
    /// <param name="releaseObject">返却したいオブジェクト(ObjectsCollisionn型)</param>
    public void ReleaseObject(ObjectPool<ObjectsCollision> releaseObjectPool,ObjectsCollision releaseObject)
    {
        releaseObjectPool.Release(releaseObject);
    }
}
