using UnityEngine;
/// <summary>
/// �I�u�W�F�N�g�f�[�^�t�@�C��
/// </summary>
[CreateAssetMenu(menuName = "CreateData/WaterData")]
public class WaterVariousObjectData : ScriptableObject
{
    /// <summary>
    ///�I�u�W�F�N�g�̃^�C�v�̃J�e�S��
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// �A���W�X�g 
        /// </summary>
        Amethyst = 0,
        /// <summary>
        /// �g���}���� 
        /// </summary>
        Tourmaline,
        /// <summary>
        /// ���r�[ 
        /// </summary>
        Ruby,
        /// <summary>
        /// �A�N�A�}����
        /// </summary>
        Aquamarine,
        /// <summary>
        ///�J�[�l���A�� 
        /// </summary>
        Carnelian,
        /// <summary>
        /// �V�g����
        /// </summary>
        Citrine,
        /// <summary>
        /// ���[���X�g�[�� 
        /// </summary>
        Moonstone,
        /// <summary>
        /// �W�F�C�h
        /// </summary>
        Jade,
        /// <summary>
        /// �N���c�@�C�g
        /// </summary>
        Kunzite,
        /// <summary>
        /// �T�t�@�C�A 
        /// </summary>
        Sapphire,
        
    }
    /// <summary>
    /// �����̃I�u�W�F�N�g�̃^�C�v
    /// </summary>
    [Header("�����̃^�C�v��ݒ肷��")]
    [SerializeField]private ObjectType _myType;
    /// <summary>
    /// �����̃I�u�W�F�N�g�̎��(Get)
    /// </summary>
    public ObjectType MyType
    {
        get => _myType;
        private set { }
    }
    /// <summary>
    /// �����̃I�u�W�F�N�g�̖��O
    /// </summary>
    [Header("�����̃I�u�W�F�N�g�̖��O��ݒ肷��")]
    [SerializeField]
    private string _myObjectName = null;
    /// <summary>
    /// �����̃I�u�W�F�N�g�̖��O(GetSet)
    /// </summary>
    public string GetSetMyObjectName
    {
        get => _myObjectName;
        set => _myObjectName = value;
    }
    /// <summary>
    /// �����̃X�R�A
    /// </summary>
    [Header("�����̃X�R�A��ݒ肷��")]
    [SerializeField]
    private int _score = 0;
    /// <summary>
    /// �����̃X�R�A(GetSet)
    /// </summary>
    public int GetSetScore
    {
        get => _score;
        set => _score = value;
    }
    /// <summary>
    /// �����̃I�u�W�F�N�g
    /// </summary>
    [Header("�����̃I�u�W�F�N�g��ݒ肷��")]
    [SerializeField]
    private GameObject _myObject = default;
    /// <summary>
    /// �����̃I�u�W�F�N�g(GetSet)
    /// </summary>
    public GameObject MyObjectPrefab
    {
        get => _myObject;
        set => _myObject = value;
    }
    /// <summary>
    /// ���������ɐi������I�u�W�F�N�g
    /// </summary>
    [Header("���������ɐi������I�u�W�F�N�g��ݒ肷��")]
    [SerializeField]
    private WaterCollision _nextEvolvingObject = null;
    /// <summary>
    /// ���������ɐi������I�u�W�F�N�g
    /// </summary>
    public WaterCollision NextEvolvingObject
    {
        get => _nextEvolvingObject;
        set => _nextEvolvingObject = value;
    }

}
