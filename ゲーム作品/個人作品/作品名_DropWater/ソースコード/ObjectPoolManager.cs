using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
/// <summary>
/// �I�u�W�F�N�g�v�[�����Ǘ�����(�V���O���g��)
/// </summary>
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    #region �ϐ�
    /// <summary>
    /// WaterType �ɑΉ����� WaterCollision �̃v�[�����i�[���� Dictionary
    /// </summary>
    private Dictionary<WaterVariousObjectData.WaterType, ObjectPool<WaterCollision>> _poolByWaterType = default;
    /// <summary>
    /// �e�I�u�W�F�N�g�f�[�^��萔�f�[�^�����ׂĊi�[�����f�[�^�t�@�C��
    /// </summary>
    [Header("�I�u�W�F�N�g�f�[�^(ScriptableObject)���Z�b�g")]
    [SerializeField]
    private WaterDataBase _waterDataArray = default;
    #endregion
    protected override void Awake()
    {
        // ���N���X�� Awake() �̏������ŏ��Ɏ��s����
        base.Awake();

        //����������
        _poolByWaterType =
                new Dictionary<WaterVariousObjectData.WaterType, ObjectPool<WaterCollision>>();
        //�I�u�W�F�N�g�v�[����������
        InitializePools();

    }
    /// <summary>
    /// �f�[�^�t�@�C������Ɋe�I�u�W�F�N�g�v�[��������������
    /// </summary>
    private void InitializePools()
    {
        //_waterDataArrays�̗v�f(�o�ꂳ�������I�u�W�F�N�g�̃f�[�^�̎��)�������s��
        foreach (WaterVariousObjectData data in _waterDataArray._waterDataList)
        {
            //�v�f�̃v���n�u
            GameObject indexPrefab = data.MyObjectPrefab;

            //�v�f�̃I�u�W�F�N�g�^�C�v
            WaterVariousObjectData.WaterType indexType = data.MyWaterType;

            //
            ObjectPool<WaterCollision> objectPool = new ObjectPool<WaterCollision>(
                createFunc: () =>
                {
                    //�v�f�̃v���n�u�Ɠ����v���n�u�����������悤�ɂ���
                    GameObject instance = Instantiate(indexPrefab);

                    //WaterCollision�^�Ȃ̂ŃR���|�[�l���g���擾
                    WaterCollision prefabWaterCollision = instance.GetComponent<WaterCollision>();

                    //�R���|�[�l���g��null�`�F�b�N
                    if (prefabWaterCollision == null)
                    {
                        Debug.LogError("WaterCollision ���A�^�b�`����Ă��܂���F" + instance.name);
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

            //�����e�ʂ܂ŃI�u�W�F�N�g�𐶐����Ċi�[����
            CreateObjectsUpToCapacity(objectPool, _waterDataArray._waterObjectConstBase.InitialCapacity);
            _poolByWaterType[indexType] = objectPool;
        }
    }
    /// <summary>
    /// �����e�ʂ̃T�C�Y�܂ŃI�u�W�F�N�g�𐶐����ăv�[���Ɋi�[����
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
    /// �Ή�����I�u�W�F�N�g�v�[�����擾���� (WaterCollision ����Ăяo���p)
    /// </summary>
    /// <param name="type">�v�[�����擾�������I�u�W�F�N�g��MyWaterType</param>
    /// <returns>�Ή����� ObjectPool<GameObject>�B������Ȃ��ꍇ�� null�B</returns>
    public ObjectPool<WaterCollision> GetPoolByType(WaterVariousObjectData.WaterType type)
    {
        _poolByWaterType.TryGetValue(type, out ObjectPool<WaterCollision> pool);
        return pool;
    }

}

