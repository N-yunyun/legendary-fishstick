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
    private Dictionary<VariousObjectData.ObjectType, ObjectPool<ObjectsCollision>> _poolByObjectsType = default;
    /// <summary>
    /// �e�I�u�W�F�N�g�f�[�^��萔�f�[�^�����ׂĊi�[�����f�[�^�t�@�C��
    /// </summary>
    [Header("�I�u�W�F�N�g�f�[�^(ScriptableObject)���Z�b�g")]
    [SerializeField]
    private ObjectsDataBase _dataArray = default;
    #endregion
    protected override void Awake()
    {
        // ���N���X�� Awake() �̏������ŏ��Ɏ��s����
        base.Awake();

        //����������
        _poolByObjectsType =
                new Dictionary<VariousObjectData.ObjectType, ObjectPool<ObjectsCollision>>();
        //�I�u�W�F�N�g�v�[����������
        InitializePools();

    }
    /// <summary>
    /// �f�[�^�t�@�C������Ɋe�I�u�W�F�N�g�v�[��������������
    /// </summary>
    private void InitializePools()
    {
        //_waterDataArrays�̗v�f(�o�ꂳ�������I�u�W�F�N�g�̃f�[�^�̎��)�������s��
        foreach (VariousObjectData data in _dataArray._dataList)
        {
            //�v�f�̃v���n�u
            GameObject indexPrefab = data.MyObjectPrefab;

            //�v�f�̃I�u�W�F�N�g�^�C�v
            VariousObjectData.ObjectType indexType = data.MyType;

            ObjectPool<ObjectsCollision> objectPool = new ObjectPool<ObjectsCollision>(
                createFunc: () =>
                {
                    //�v�f�̃v���n�u�Ɠ����v���n�u�����������悤�ɂ���
                    GameObject instance = Instantiate(indexPrefab);

                    //WaterCollision�^�Ȃ̂ŃR���|�[�l���g���擾
                    ObjectsCollision prefabObjectsCollision = instance.GetComponent<ObjectsCollision>();

                    //�R���|�[�l���g��null�`�F�b�N
                    if (prefabObjectsCollision == null)
                    {
                        Debug.LogError("WaterCollision ���A�^�b�`����Ă��܂���F" + instance.name);
                    }

                    return prefabObjectsCollision;
                },
                actionOnGet: obj =>
                {
                    //�I�u�W�F�N�g�̃R���|�[�l���g�̂ݗL����
                    obj.EnableVisuals();
                },
                actionOnRelease: obj =>
                {
                    //�I�u�W�F�N�g�̃R���|�[�l���g�̂ݖ�����
                    obj.DisableVisuals();
                    //���������̒l�����Z�b�g
                    obj.ResetPhysicsState();
                },
                collectionCheck: true,
                defaultCapacity: _dataArray._constData.InitialCapacity,
                maxSize: _dataArray._constData.MaximumCapacity
            );
            //�����e�ʂ܂ŃI�u�W�F�N�g�𐶐����Ċi�[����
            CreateObjectsUpToCapacity(objectPool, _dataArray._constData.InitialCapacity);
            _poolByObjectsType[indexType] = objectPool;
        }
    }
    /// <summary>
    /// �����e�ʂ̃T�C�Y�܂ŃI�u�W�F�N�g�𐶐����ăv�[���Ɋi�[����
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
    /// �Ή�����I�u�W�F�N�g�v�[�����擾���� (ObjectsCollision ����Ăяo���p)
    /// </summary>
    /// <param name="type">�v�[�����擾�������I�u�W�F�N�g��MyType</param>
    /// <returns>�Ή����� ObjectPool<GameObject>�B������Ȃ��ꍇ�� null�B</returns>
    public ObjectPool<ObjectsCollision> GetPoolByType(VariousObjectData.ObjectType type)
    {
        _poolByObjectsType.TryGetValue(type, out ObjectPool<ObjectsCollision> pool);
        return pool;
    }

}

