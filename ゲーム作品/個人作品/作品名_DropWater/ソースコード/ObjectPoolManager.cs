using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;
/// <summary>
/// �I�u�W�F�N�g�v�[�����Ǘ�����(�V���O���g��)
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    #region �ϐ�
    public static ObjectPoolManager Instance { get; private set; }
    /// <summary>
    /// WaterType �ɑΉ����� WaterCollision �̃v�[�����i�[���� Dictionary
    /// </summary>
    private Dictionary<WaterVariousObjectData.WaterType, ObjectPool<WaterCollision>> _poolByWaterType;
    /// <summary>
    /// �e�I�u�W�F�N�g�f�[�^��萔�f�[�^�����ׂĊi�[�����f�[�^�t�@�C��
    /// </summary>
    [Header("�I�u�W�F�N�g�f�[�^(ScriptableObject)���Z�b�g")]
    [SerializeField]
    private WaterDataArray _waterDataArray;
    #endregion
    void Awake()
    {
        //�V���O���g����null�`�F�b�N
        // ���łɃC���X�^���X�����݂���ꍇ�i�ʂ� ObjectPoolManager ���V�[���ɑ��݂���ꍇ�j
        if (Instance != null && Instance != this)
        {
            Debug.LogError("ObjectPoolManager �̃C���X�^���X���������݂��܂��I");

            // �V�������ꂽ����j��
            Destroy(gameObject);
        }
        else
        {
            //���̃C���X�^���X��B��̃C���X�^���X�Ƃ��ĕۑ�
            Instance = this;
            //����������
            _poolByWaterType = 
                new Dictionary<WaterVariousObjectData.WaterType, ObjectPool<WaterCollision>>();
            //�I�u�W�F�N�g�v�[����������
            InitializePools();
        }
    }
    /// <summary>
    /// �f�[�^�t�@�C������Ɋe�I�u�W�F�N�g�v�[��������������
    /// </summary>
    private void InitializePools()
    {
        //_waterDataArrays�̗v�f(�o�ꂳ�������I�u�W�F�N�g�̃f�[�^�̎��)�������s��
        foreach (WaterVariousObjectData data in _waterDataArray._waterDataArrays)
        {
            GameObject indexPrefab = data.MyObjectPrefab;
            WaterVariousObjectData.WaterType indexType = data.MyWaterType;

            ObjectPool<WaterCollision> objectPool = new ObjectPool<WaterCollision>(
                createFunc: () =>
                {
                    GameObject instance = Instantiate(indexPrefab);
                    WaterCollision prefabWaterCollision = instance.GetComponent<WaterCollision>();

                    //�R���|�[�l���g��null�`�F�b�N
                    if (prefabWaterCollision == null)
                    {
                        Debug.LogError("WaterCollision ���A�^�b�`����Ă��܂���F" + instance.name);
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

