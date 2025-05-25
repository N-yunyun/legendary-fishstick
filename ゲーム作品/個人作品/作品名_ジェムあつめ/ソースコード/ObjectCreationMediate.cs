using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// �I�u�W�F�N�g�̐����𒇉��N���X
/// </summary>
public class ObjectCreationMediate : MonoBehaviour
{
    [SerializeField]
    private MaximumEvolutionData _waterMaximumEvolutionData;
    /// <summary>
    /// �󂯎�����I�u�W�F�N�g�v�[������I�u�W�F�N�g���Q�b�g���Ă���
    /// </summary>
    /// <param name="getObjectPool">�擾�������I�u�W�F�N�g</param>
    /// <returns>�v�[������擾�����I�u�W�F�N�g</returns>
    public ObjectsCollision SpawnObject(ObjectPool<ObjectsCollision> getObjectPool)
    {
        return getObjectPool.Get();
    }
    /// <summary>
    /// �v�[���ɕԋp�������I�u�W�F�N�g���v�[���ɕԋp����
    /// </summary>
    /// <param name="releaseObjectPool">�ԋp�������I�u�W�F�N�g�̃v�[��</param>
    /// <param name="releaseObject">�ԋp�������I�u�W�F�N�g(ObjectsCollisionn�^)</param>
    public void ReleaseObject(ObjectPool<ObjectsCollision> releaseObjectPool,ObjectsCollision releaseObject)
    {
        releaseObjectPool.Release(releaseObject);
    }
}
