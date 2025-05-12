using UnityEngine;
/// <summary>
/// �萔�f�[�^�t�@�C��
/// </summary>
[CreateAssetMenu(menuName = "CreateData/ConstData")]
public class WaterObjectConstData : ScriptableObject
{
    [Header("2�Ɋ���Ƃ��Ɏg��")]
    [SerializeField] private int _halfDivide = 2;
    /// <summary>
    /// 2�Ɋ���Ƃ��Ɏg��
    /// </summary>
    public int HalfDivider
    {
        get { return _halfDivide; }
        private set { }
    }
    [Header("���̃I�u�W�F�N�g�����������܂ł̃N�[���^�C��")]
    [SerializeField]private float _nextObjectGeneratedCoolTime = 1f;
    /// <summary>
    /// ���̃I�u�W�F�N�g�����������܂ł̃N�[���^�C��
    /// </summary>
    public float NextObjectGeneratedCoolTime
    {
        get { return _nextObjectGeneratedCoolTime; }
        private set { }
    }
    [Header("�X�|�i�[�̈ړ����x")]
    [SerializeField]private float _spawnerMoveSpeed = 5f;
    /// <summary>
    /// �X�|�i�[�̈ړ����x
    /// </summary>
    public float SpawnerMoveSpeed
    {
        get { return _spawnerMoveSpeed; }
    }
    [Header("�X�|�i�[�ړ��̍����̌��E�l")]
    [SerializeField]private float _spawnerLeftLimit = -2.45f;
    /// <summary>
    /// �X�|�i�[�ړ��̍����̌��E�l
    /// </summary>
    public float SpawnerLeftLimit
    {
        get { return _spawnerLeftLimit; }
    }
    
    [Header("�X�|�i�[�ړ��̉E���̌��E�l")]
    [SerializeField] private float _spawnerRightLimit = 2.45f;
    /// <summary>
    /// �X�|�i�[�ړ��̉E���̌��E�l
    /// </summary>
    public float SpawnerRightLimit
    {
        get { return _spawnerRightLimit; }
    }
    /// <summary>
    /// Lerp�֐��Ŏw�肷�錻�݂̈ʒu
    /// </summary>
    [Header("Lerp�֐��Ŏw�肷�錻�݂̈ʒu")]
    [SerializeField] private float _myCurrentPosition = 0.5f;
    /// <summary>
    /// Lerp�֐��Ŏw�肷�錻�݂̈ʒu
    /// </summary>
    public float MyCurrentPosition
    {
        get { return _myCurrentPosition; }
    }
    /// <summary>
    /// ObjectPool�̏����e��
    /// </summary>
    [Header("ObjectPool�̏����e��")]
    private int _initialCapacity = 15;
    /// <summary>
    /// ObjectPool�̏����e��
    /// </summary>
    public int InitialCapacity
    {
        get { return _initialCapacity; }
    }

}
