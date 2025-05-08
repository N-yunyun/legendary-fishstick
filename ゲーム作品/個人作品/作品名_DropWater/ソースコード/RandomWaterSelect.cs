using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// ���ɏo�Ă��鐅�������_���őI�сA���̃v���n�u�����o���ăX�|�i�[�Ɉ����n��
/// </summary>
public class RandomWaterSelect : MonoBehaviour
{
    #region �ϐ�
    /// <summary>
    /// �o�ꂳ�������I�u�W�F�N�g�̃��X�g(WaterCollision�^)
    /// </summary>
    /// <param name=""></param>
    [Header("�o�ꂳ�������I�u�W�F�N�g���Z�b�g")]
    [SerializeField] private GameObject[] _waterPrefabs;
    /// <summary>
    /// _WaterPrefabs�̗v�f����擾����WaterCollision���i�[
    /// </summary>
    private WaterCollision[] _waterCollisions;
    /// <summary>
    /// _waterPrefabs�̗v�f�ƑΉ�����v�[�������i�[�����z��
    /// </summary>
    private ObjectPool<WaterCollision>[] _waterPrefabPools;
    /// <summary>
    /// �����������I�u�W�F�N�g�̃v�[�����
    /// </summary>
    private ObjectPool<WaterCollision> _reservedObjectPool;
    public GameObject ReservedObject { get; private set; }

    #endregion
    void OnEnable()
    {
        //_waterCollisions�̒�����_waterPrefabs�̒����ɍ��킹��
        _waterCollisions = new WaterCollision[_waterPrefabs.Length];

        //_waterPrefabPools�̒�����_waterCollisions�ɍ��킹��
        _waterPrefabPools = new ObjectPool<WaterCollision>[_waterPrefabs.Length];

        //_WaterPrefabs���X�g�̃I�u�W�F�N�g����WaterCollision���擾���ă��X�g�Ɋi�[
        for (int i = 0; i < _waterPrefabs.Length; i++)
        {
            _waterCollisions[i] = _waterPrefabs[i].GetComponent<WaterCollision>();
            
            if (!_waterPrefabs[i].TryGetComponent(out _waterCollisions[i]))
            {
                Debug.LogError($"{_waterPrefabs[i].name} �� WaterCollision ���A�^�b�`����Ă��܂���I");
            }
        }

        //_waterPrefabs�ɑΉ����Ă���v�[����WaterCollision���o�R���Ď擾
        for (int i = 0; i < _waterCollisions.Length; i++)
        {
            _waterPrefabPools[i] =
                ObjectPoolManager.Instance.GetPoolByType(_waterCollisions[i]._waterVariousObjectData.MyWaterType);

        }

        //���ɏo���ׂ��v���t�@�u�ƃv�[������I�o
        SelectNextWaterObject();

    }
    /// <summary>
    /// ���ɏo���I�u�W�F�N�g�������_���Ō��߂āA���̃I�u�W�F�N�g�̃v�[������n��
    /// </summary>
    /// <returns>���ɏo���I�u�W�F�N�g�̃v�[�����</returns>
    public ObjectPool<WaterCollision> SelectNextWaterObject()
    {
        //�Ԃ����Ɏg���ϐ����킩��₷������
        int index = Random.Range(0, _waterPrefabs.Length);//�����_�������W�֐��Ń����_���Ȓl���C���f�b�N�X�w���ɑ��

        //�Ԃ��\���̂ɃI�u�W�F�N�g���ƃv�[��������
        _reservedObjectPool = _waterPrefabPools[index];

        //�ق��ŎQ�Ƃł���悤�ɂ��邽�߂ɎQ�Ɨp�̕ϐ��Ɏ��ɏo���I�u�W�F�N�g�̏�������
        ReservedObject = _waterPrefabs[index];

        return _reservedObjectPool;
    }

}
