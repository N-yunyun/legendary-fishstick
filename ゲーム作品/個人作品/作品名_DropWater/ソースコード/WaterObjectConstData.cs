using UnityEngine;
/// <summary>
/// �萔�f�[�^�t�@�C��
/// </summary>
[CreateAssetMenu(menuName = "CreateData/ConstData")]
public class WaterObjectConstData : ScriptableObject
{
    [Header("2�Ɋ���Ƃ��Ɏg��")]
    [SerializeField] private int _divideIntoTwo = 2;
    /// <summary>
    /// 2�Ɋ���Ƃ��Ɏg��
    /// </summary>
    public int GetDivideIntoTwo
    {
        get { return _divideIntoTwo; }
    }
    [Header("���̃I�u�W�F�N�g�����������܂ł̃N�[���^�C��")]
    [SerializeField]private float _nextObjectGeneratedCoolTime = 1f;
    /// <summary>
    /// ���̃I�u�W�F�N�g�����������܂ł̃N�[���^�C��
    /// </summary>
    public float NextObjectGeneratedCoolTime
    {
        get { return _nextObjectGeneratedCoolTime; }
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
    public float GetMyCurrentPosition
    {
        get { return _myCurrentPosition; }
    }
}
