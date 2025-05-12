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
    /// _waterPrefabs�̗v�f����擾����WaterCollision���i�[
    /// </summary>
    private WaterCollision[] _waterCollisions = null;
    /// <summary>
    /// _waterPrefabs�̗v�f�ƑΉ�����v�[�������i�[�����z��
    /// </summary>
    private ObjectPool<WaterCollision>[] _waterPrefabPools = null;
    /// <summary>
    /// �����������I�u�W�F�N�g�̃v�[�����
    /// </summary>
    public ObjectPool<WaterCollision> ReservedObjectPool { get; private set; } = null;
    public GameObject ReservedObject { get; private set; } = null;

    #endregion
    //�����������ƃR���|�[�l���g�̎擾
    private void Awake()
    {
        InitializeVariables();
    }
    private void Start()
    {
        //_waterCollisions�̒�����_waterPrefabs�̒����ɍ��킹��
        _waterCollisions = new WaterCollision[_waterPrefabs.Length];

        //_waterPrefabPools�̒�����_waterCollisions�ɍ��킹��
        _waterPrefabPools = new ObjectPool<WaterCollision>[_waterPrefabs.Length];

        //_WaterPrefabs���X�g�̃I�u�W�F�N�g����
        //WaterCollision�A�v�[�������擾���ă��X�g�Ɋi�[
        for (int i = 0; i < _waterPrefabs.Length; i++)
        {
            _waterCollisions[i] = _waterPrefabs[i].GetComponent<WaterCollision>();

            _waterPrefabPools[i] =
                ObjectPoolManager.Instance.GetPoolByType(_waterCollisions[i]._waterVariousObjectData.MyWaterType);



        }

        //���ɏo���ׂ��v���t�@�u�ƃv�[������I�o
        SelectNextDroppingGameObjectsInfo();
    }
   
    private void InitializeVariables()
    {
        ReservedObject = null;
        _waterCollisions = null;
        _waterPrefabPools = null;
    }
    /// <summary>
    /// ���ɏo���I�u�W�F�N�g�������_���Ō��߂āA���̃I�u�W�F�N�g�̃v�[������n��
    /// </summary>
    /// <returns>���ɏo���I�u�W�F�N�g�̃v�[�����</returns>
    public void SelectNextDroppingGameObjectsInfo()
    {
        //�Ԃ����Ɏg���ϐ����킩��₷������
        int index = Random.Range(0, _waterPrefabs.Length);//�����_�������W�֐��Ń����_���Ȓl���C���f�b�N�X�w���ɑ��

        //�Ԃ��\���̂ɃI�u�W�F�N�g���ƃv�[��������
        ReservedObjectPool = _waterPrefabPools[index];

        //�ق��ŎQ�Ƃł���悤�ɂ��邽�߂ɎQ�Ɨp�̕ϐ��Ɏ��ɏo���I�u�W�F�N�g�̏�������
        ReservedObject = _waterPrefabs[index];

    }
}
