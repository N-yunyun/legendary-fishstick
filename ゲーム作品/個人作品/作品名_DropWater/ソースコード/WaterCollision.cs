using UnityEngine;
using System;
using UnityEngine.Pool;
/// <summary>
/// �I�u�W�F�N�g���g�̐i�������Ȃǂ��s��
/// </summary>

//Rigidbody2D�K�{
[RequireComponent(typeof(Rigidbody2D))]
public class WaterCollision : ObjectCreationMediate
{
    #region �ϐ�
    /// <summary>
    /// ���g�̃^�C�v�ɑΉ�����I�u�W�F�N�g�v�[��
    /// </summary>
    private ObjectPool<WaterCollision> _myObjectPool = null;
    /// <summary>
    /// ���g�̐i����̃I�u�W�F�N�g�̃^�C�v�ɑΉ�����v�[��
    /// </summary>
    private ObjectPool<WaterCollision> _evolvedObjectPool = null;
    /// <summary>
    /// �萔�f�[�^�t�@�C��(�v�Z�̒l�ȂǂɎg�p)
    /// </summary>
    [Header("�萔�f�[�^�t�@�C��(ScriptableObject)���Z�b�g")]
    [SerializeField] private WaterObjectConstData _waterObjectConstData = null;
    /// <summary>
    /// �I�u�W�F�N�g�e��ނ̃f�[�^�t�@�C��(�I�u�W�F�N�g�̌ŗL�̏����擾����̂Ɏg�p)
    /// </summary>
    [Header("�I�u�W�F�N�g�f�[�^(ScriptableObject)���Z�b�g")]
    [SerializeField] public WaterVariousObjectData _waterVariousObjectData = null;
    /// <summary>
    /// �ڐG��������̃Q�[���I�u�W�F�N�g(WaterCollision�^)
    /// </summary>
    private WaterCollision _evolvedGameObject = null;
    /// <summary>
    /// ��̐����ڐG�����ڐG�����̒��S�̈ʒu
    /// </summary>
    private Vector3 _thisAndOthersCenter = default;
    /// <summary>
    /// ��̐��̊Ԃ����炩�Ɉړ������邽�߂̕��
    /// </summary>
    private Quaternion _thisAndOthersRotation = default;
    /// <summary>
    /// �i����(��������I�u�W�F�N�g)�̑��x�̓��ꕨ
    /// </summary>
    private Vector3 _evolvedGameObjectVelocity = default;
    /// <summary>
    /// �ڐG���������WaterCollision
    /// </summary>
    private WaterCollision _othersWaterCollision = null;
    /// <summary>
    /// �i����̃Q�[���I�u�W�F�N�g��Rigidbody2D
    /// </summary>
    private Rigidbody2D _evolvedRigidbody = null;
    /// <summary>
    /// ���g��Rigidbody2D
    /// </summary>
    private Rigidbody2D _thisRigidbody = null;
    /// <summary>
    /// �ڐG���������Rigidbody2D
    /// </summary>
    private Rigidbody2D _othersRigidbody = null;
    /// <summary>
    /// angularVelocity�̒l��ύX����Ƃ��Ɏg��
    /// </summary>
    private float _myselfAndOtherAngularVelocity = default;
    /// <summary>
    /// �v�[���ɕԋp���邩�̃t���O
    /// </summary>
    private bool _shouldRelease = false;
    
    /// <summary>
    /// ���g�̃C���X�^���X�̈�ӂ̎��ʎq
    /// </summary>
    [SerializeField]
    public int InstanceID
    {
        get { return GetInstanceID(); }
    }

    

    #endregion
    private void Awake()
    {
        InitializeVariables();

        //�����̃��W�b�h�{�f�B���擾
        _thisRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //�v�[���̎擾
        _myObjectPool = GetObjectPool(_waterVariousObjectData.MyWaterType);

        //�i����̃I�u�W�F�N�g��null�`�F�b�N(null���ƍŏI�i���n)
        if (_waterVariousObjectData.NextEvolvingObject != null)
        {
            //null����Ȃ���ΐi����̃I�u�W�F�N�g�̃v�[�����擾
            _evolvedObjectPool =
                GetObjectPool
                (_waterVariousObjectData.NextEvolvingObject._waterVariousObjectData.MyWaterType);

            //�擾��̃v�[��null�`�F�b�N
            if (_evolvedObjectPool == null)
            {
                Debug.LogError("�i����̃I�u�W�F�N�g�ɑΉ�����v�[��������܂���I");

            }
        }

        //�I�u�W�F�N�g�v�[����null�`�F�b�N
        if (_myObjectPool == null)
        {
            Debug.LogError("�����̃I�u�W�F�N�g�ɑΉ�����v�[��������܂���I");
            return;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ڐG���������WaterCollision���Ȃ��A�قȂ�I�u�W�F�N�g�̃^�C�v�ł���Ώ������Ȃ�
        if (!collision.gameObject.TryGetComponent(out _othersWaterCollision))
        {
            return;
        }
        if (_othersWaterCollision._waterVariousObjectData.MyWaterType != this._waterVariousObjectData.MyWaterType)
        {
            //�g��Ȃ��̂ŏ���������
            _othersWaterCollision = null;
            return;
        }
        //�ȉ��ڐG�����I�u�W�F�N�g�������I�u�W�F�N�g�^�C�v�������ꍇ�ɂ̂ݏ�������

        //�C���X�^���XID���������ق����i���A���������Ȃǂ�S������
        _shouldRelease = 
            (InstanceID < _othersWaterCollision.InstanceID);
        
        if (_shouldRelease)
        {
            //�ڐG���������Rigidbody2D���擾
            GetOthersRigidbody2D();

            //�i����̃I�u�W�F�N�g�����R�Ȉʒu�Ő������邽�߂ɐ��`��Ԃ��擾����
            GetCollisionCenterPositionAndLinearInterpolation();

            //��ɃX�R�A�����Z
            ScoreManager.Instance?.AddScore(_waterVariousObjectData.GetSetScore);

            //�����Ƒ�����v�[���ɖ߂�
            ReleaseObject(_myObjectPool, this);
            ReleaseObject(_myObjectPool, _othersWaterCollision);

            //�i����̃I�u�W�F�N�g�������Ă���ΐi����̃I�u�W�F�N�g�̐�������
            if (_waterVariousObjectData.NextEvolvingObject != null)
            {
                //�i����̃v�[������Get
                _evolvedGameObject = SpawnObject(_evolvedObjectPool);
                _evolvedRigidbody = _evolvedGameObject._thisRigidbody;

                //���������I�u�W�F�N�g��Transform����
                SetNextObjectTransform();
                GiveVelocityAndAngularVelocity();
            }
        }
        //����������
        InitializeVariables();
        ResetComponents();
    }
    /// <summary>
    /// �ڐG���������Rigidbody2D���擾����
    /// </summary>
    private void GetOthersRigidbody2D()
    {
        _othersRigidbody = _othersWaterCollision._thisRigidbody;
    }
    /// <summary>
    /// ���������I�u�W�F�N�g�̈ʒu�Ɖ�]��ύX
    /// </summary>
    private void SetNextObjectTransform()
    {
        //���������I�u�W�F�N�g�̈ʒu�Ɖ�]�𓯎��ɕύX����
        _evolvedGameObject.transform.SetLocalPositionAndRotation(this._thisAndOthersCenter, this._thisAndOthersRotation);
    }
    /// <summary>
    /// �V�������������I�u�W�F�N�g�ɑ��x�Ɗp���x��^����
    /// </summary>
    private void GiveVelocityAndAngularVelocity()
    {
        //�@�ڐG��������Ǝ����̑��x�̕��ς����߂�
        _evolvedGameObjectVelocity =
            (_thisRigidbody.velocity + _othersRigidbody.velocity) / _waterObjectConstData.HalfDivider;

        //�A�ڐG��������Ǝ����̊p���x�̕��ς����߂�
        _myselfAndOtherAngularVelocity =
            (_thisRigidbody.angularVelocity + _othersRigidbody.angularVelocity) / _waterObjectConstData.HalfDivider;

        //�@�ƇA�Ōv�Z�����l�����ꂼ��
        //�V�������������I�u�W�F�N�g�̑��x�Ɗp���x�ɗ^����
        _evolvedRigidbody.velocity = _evolvedGameObjectVelocity;
        _evolvedRigidbody.angularVelocity = _myselfAndOtherAngularVelocity;
    }
    /// <summary>
    /// ��̃I�u�W�F�N�g�̐ڐG�����̒��S�̈ʒu�Ɛ��`��Ԃ��擾����
    /// </summary>
    private void GetCollisionCenterPositionAndLinearInterpolation()
    {
        //��̐����Ԃ������ڐG�����̒��S�̈ʒu
        _thisAndOthersCenter = (transform.position + _othersWaterCollision.transform.position) / _waterObjectConstData.HalfDivider;
        //��̐��̊Ԃ����炩�Ɉړ������邽�߂̕��
        _thisAndOthersRotation = Quaternion.Lerp(transform.rotation, _othersWaterCollision.transform.rotation, _waterObjectConstData.MyCurrentPosition);
    }
    /// <summary>
    /// �����Ɛi����̃I�u�W�F�N�g�̃v�[�����擾����
    /// </summary>
    private ObjectPool<WaterCollision> GetObjectPool(WaterVariousObjectData.WaterType waterType)
    {
        //�����̃I�u�W�F�N�g�Ɛi����̃v���n�u�ɑΉ�����v�[�����擾
        return ObjectPoolManager.Instance.GetPoolByType(waterType);
    }
    /// <summary>
    /// �ϐ�������������
    /// </summary>
    private void InitializeVariables()
    {
        _evolvedGameObjectVelocity = default;
        _myselfAndOtherAngularVelocity = default;
        _thisAndOthersCenter = default;
        _thisAndOthersRotation = default;
    }
    /// <summary>
    /// �R���|�[�l���g�̕ϐ�������������
    /// </summary>
    private void ResetComponents()
    {
        _evolvedRigidbody = null;
        _othersRigidbody = null;
        _evolvedGameObject = null;
        _othersWaterCollision = null;

    }

}