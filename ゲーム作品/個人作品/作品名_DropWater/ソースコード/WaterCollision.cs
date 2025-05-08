using UnityEngine;
using System;
using UnityEngine.Pool;
/// <summary>
/// �I�u�W�F�N�g���g�̐i�������Ȃǂ��s��
/// </summary>

//Rigidbody2D�K�{
[RequireComponent(typeof(Rigidbody2D))]
public class WaterCollision : MonoBehaviour
{
    #region �ϐ�
    /// <summary>
    /// ���g�̃^�C�v�ɑΉ�����I�u�W�F�N�g�v�[��
    /// </summary>
    private ObjectPool<WaterCollision> _myObjectPool;
    /// <summary>
    /// ���g�̐i����̃I�u�W�F�N�g�̃^�C�v�ɑΉ�����v�[��
    /// </summary>
    private ObjectPool<WaterCollision> _evolvedObjectPool;
    /// <summary>
    /// �萔�f�[�^�t�@�C��(�v�Z�̒l�ȂǂɎg�p)
    /// </summary>
    [Header("�萔�f�[�^�t�@�C��(ScriptableObject)���Z�b�g")]
    [SerializeField] private WaterObjectConstData _waterObjectConstData;
    /// <summary>
    /// �I�u�W�F�N�g�e��ނ̃f�[�^�t�@�C��(�I�u�W�F�N�g�̌ŗL�̏����擾����̂Ɏg�p)
    /// </summary>
    [Header("�I�u�W�F�N�g�f�[�^(ScriptableObject)���Z�b�g")]
    [SerializeField] public WaterVariousObjectData _waterVariousObjectData;
    /// <summary>
    /// �o������I�u�W�F�N�g�ɔԍ�������U�邽�߂̕ϐ�
    /// </summary>
    private static int _serialNumber = 0;
    /// <summary>
    /// �o������I�u�W�F�N�g�̔ԍ�
    /// </summary>
    [Header("�o������I�u�W�F�N�g�̔ԍ�")]
    [SerializeField]
    private int _mySerialNumber = 0;
    /// <summary>
    /// �Ԃ���������̃Q�[���I�u�W�F�N�g(WaterCollision�^)
    /// </summary>
    private WaterCollision _evolvedGameObject;
    /// <summary>
    /// ��̐����Ԃ������ڐG�����̒��S�̈ʒu
    /// </summary>
    private Vector3 _myselfAndOthersCenter = default;
    /// <summary>
    /// ��̐��̊Ԃ����炩�Ɉړ������邽�߂̕��
    /// </summary>
    private Quaternion _myselfAndOthersRotation = default;
    /// <summary>
    /// �i����(��������I�u�W�F�N�g)�̑��x�̓��ꕨ
    /// </summary>
    private Vector3 _evolvedGameObjectVelocity = default;
    /// <summary>
    /// �Ԃ����������WaterCollision
    /// </summary>
    private WaterCollision _opponentsWaterCollision;
    /// <summary>
    /// �i����̃Q�[���I�u�W�F�N�g��Rigidbody2D
    /// </summary>
    private Rigidbody2D _evolvedRb2d;
    /// <summary>
    /// ���g��Rigidbody2D
    /// </summary>
    private Rigidbody2D _thisRb2d;
    /// <summary>
    /// �Ԃ����������Rigidbody2D
    /// </summary>
    private Rigidbody2D _collidedRb2d;
    /// <summary>
    /// angularVelocity�̒l��ύX����Ƃ��Ɏg��
    /// </summary>
    private float _myselfAndOtherPersonsAngularVelocity = default;
    /// <summary>
    /// �X�R�A���Z�������s�����߂̃C�x���g(�V�[�����܂����ŃX�R�A��ێ�����)
    /// </summary>
    //�����p�̃���
    //�֐����Q�ƌ^�Ƃ��Ĉ����A��R�ێ����Ĉ�ĂɎ��s�ł���Q�ƌ^
    public static Action<int> OnScoreAdded;
    #endregion 
    private void Awake()
    {
            //�����̃��W�b�h�{�f�B���擾
            _thisRb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        //���̒ʂ��ԍ����R�s�[
        _mySerialNumber = _serialNumber;
        //�R�s�[��ɔԍ��𑝂₵�āA���ɗ���I�u�W�F�N�g�Ɣԍ������Ȃ��悤�ɂ���
        _serialNumber++;

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
        //�Ԃ����������WaterCollision�����ĂȂ��āA����̐��^�C�v�������ƈႤ�̂ł���΃��^�[��
        if (!collision.gameObject.TryGetComponent(out _opponentsWaterCollision))
        {
            return;
        }
        if (_opponentsWaterCollision._waterVariousObjectData.MyWaterType != this._waterVariousObjectData.MyWaterType)
        {
            //�g��Ȃ��̂ŏ���������
            _opponentsWaterCollision = null;
            return;
        }

        //�����Ɠ�����ނ̃I�u�W�F�N�g�ŁA�ԍ���������(�Â�)���̃I�u�W�F�N�g�����ȉ��̏���������
        if (_mySerialNumber < _opponentsWaterCollision._mySerialNumber)
        {
            GetOthersRigidbody2D();

            //�i����̃I�u�W�F�N�g�����R�Ȉʒu�Ő������邽�߂ɐ��`��Ԃ��擾����
            GetCollisionCenterPositionAndLinearInterpolation();

            //��ɃX�R�A�����Z
            OnScoreAdded?.Invoke(_waterVariousObjectData.GetSetScore);

            //�v�[���ɖ߂��O�Ɏ��g�̔ԍ�������������
            GameObjectsInitializeNumbers();

            // �����Ƒ�����v�[���ɖ߂�
            _myObjectPool.Release(this);
            _myObjectPool.Release(_opponentsWaterCollision);

            //�i����̃I�u�W�F�N�g�������Ă���ΐi����̃I�u�W�F�N�g�̐�������
            if (_waterVariousObjectData.NextEvolvingObject != null)
            {
                //�i����̃v�[������Get
                _evolvedGameObject = _evolvedObjectPool.Get();
                _evolvedRb2d = _evolvedGameObject._thisRb2d;

                //���������I�u�W�F�N�g��Transform����
                SetNextObjectTransform();
                //TakeVelocityAndAngularvelocity();
                GiveVelocityAndAngularVelocity();
            }
            //����������
            InitializeVariables();
            ResetComponents();
        }

    }
    /// <summary>
    /// �Ԃ����������Rigidbody2D���擾����
    /// </summary>
    private void GetOthersRigidbody2D()
    {
        _collidedRb2d = _opponentsWaterCollision._thisRb2d;
    }
    /// <summary>
    /// ���������I�u�W�F�N�g�̈ʒu�Ɖ�]��ύX
    /// </summary>
    private void SetNextObjectTransform()
    {
        //���������I�u�W�F�N�g�̈ʒu�Ɖ�]�𓯎��ɕύX����
        _evolvedGameObject.transform.SetLocalPositionAndRotation(this._myselfAndOthersCenter, this._myselfAndOthersRotation);
    }
    ///// <summary>
    ///// �Ԃ���������Ǝ����̑��x�Ɗp���x���擾���đ����Ĕ����Ɋ���
    ///// </summary>
    //private void TakeVelocityAndAngularvelocity()
    //{
    //    //�Ԃ���������Ǝ����̑��x�Ɗp���x�𑫂��Ċ���
    //    _evolvedGameObjectVelocity =
    //        (_thisRb2d.velocity + _collidedRb2d.velocity) / _waterObjectConstData.GetDivideIntoTwo;

    //    _myselfAndOtherPersonsAngularVelocity =
    //        (_thisRb2d.angularVelocity + _collidedRb2d.angularVelocity) / _waterObjectConstData.GetDivideIntoTwo;
    //}
    /// <summary>
    /// �V�������������I�u�W�F�N�g�ɑ��x�Ɗp���x��^����
    /// </summary>
    private void GiveVelocityAndAngularVelocity()
    {
        //�@�Ԃ���������Ǝ����̑��x�̕��ς����߂�
        _evolvedGameObjectVelocity =
            (_thisRb2d.velocity + _collidedRb2d.velocity) / _waterObjectConstData.GetDivideIntoTwo;
        
        //�A�Ԃ���������Ǝ����̊p���x�̕��ς����߂�
        _myselfAndOtherPersonsAngularVelocity =
            (_thisRb2d.angularVelocity + _collidedRb2d.angularVelocity) / _waterObjectConstData.GetDivideIntoTwo;

        //�@�ƇA�Ōv�Z�����l�����ꂼ��
        //�V�������������I�u�W�F�N�g�̑��x�Ɗp���x�ɗ^����
        _evolvedRb2d.velocity = _evolvedGameObjectVelocity;
        _evolvedRb2d.angularVelocity = _myselfAndOtherPersonsAngularVelocity;
    }
    /// <summary>
    /// ��̃I�u�W�F�N�g�̐ڐG�����̒��S�̈ʒu�Ɛ��`��Ԃ��擾����
    /// </summary>
    private void GetCollisionCenterPositionAndLinearInterpolation()
    {
        //��̐����Ԃ������ڐG�����̒��S�̈ʒu
        _myselfAndOthersCenter = (transform.position + _opponentsWaterCollision.transform.position) / _waterObjectConstData.GetDivideIntoTwo;
        //��̐��̊Ԃ����炩�Ɉړ������邽�߂̕��
        _myselfAndOthersRotation = Quaternion.Lerp(transform.rotation, _opponentsWaterCollision.transform.rotation, _waterObjectConstData.GetMyCurrentPosition);
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
        _evolvedObjectPool = null;
        _evolvedGameObjectVelocity = default;
        _myselfAndOtherPersonsAngularVelocity = default;
        _myselfAndOthersCenter = default;
        _myselfAndOthersRotation = default;
    }
    /// <summary>
    /// �R���|�[�l���g�̕ϐ�������������
    /// </summary>
    private void ResetComponents()
    {
        _evolvedRb2d = null;
        _collidedRb2d = null;
        _evolvedGameObject = null;
        _opponentsWaterCollision = null;

    }
    /// <summary>
    /// �Q�[���I�u�W�F�N�g�ˑ��̕ϐ�������������
    /// </summary>
    private void GameObjectsInitializeNumbers()
    {
        _mySerialNumber = default;
        _opponentsWaterCollision._mySerialNumber = default;
    }
    
}