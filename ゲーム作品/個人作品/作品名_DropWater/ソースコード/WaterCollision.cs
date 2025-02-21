using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// ��ɐ������ꂽ���Ƒ���̃I�u�W�F�N�g�ƂԂ������Ƃ��ɓ���()
/// </summary>
public class WaterCollision : MonoBehaviour //ObjectPoolManager
{
    #region �ϐ��ꗗ
    //[SerializeField]
    //private ObjectPoolManager _objectPoolManager;
    /// <summary>
    /// �萔�f�[�^�t�@�C��(ScriptableObject)
    /// </summary>
    [SerializeField]private WaterObjectConstData _waterObjectConstData;
    /// <summary>
    /// �I�u�W�F�N�g�e��ނ̃f�[�^�t�@�C��(ScriptableObject)
    /// </summary>
    [SerializeField] public WaterVariousObjectData _waterVariousObjectData;
    /// <summary>
    /// Awake���_�ŃR���|�[�l���g�̎擾�����̂ݍs�����߂̃t���O
    /// </summary>
    private bool _isAwakeSeconce = false;
    /// <summary>
    /// �o�����鐅�̔ԍ�(�^�C�v�֌W�Ȃ�)
    /// </summary>
    private static int _waterSerialNumber = 0;
    /// <summary>
    /// �o�����鐅�̔ԍ�(�^�C�v�֌W�Ȃ�)
    /// </summary>
    [SerializeField]
    private int _mySerialNumber = 0;
    /// <summary>
    /// �Ԃ���������̃Q�[���I�u�W�F�N�g
    /// </summary>
    private GameObject _collisionGameObject;
    /// <summary>
    /// �Ԃ���������̃Q�[���I�u�W�F�N�g
    /// </summary>
    private GameObject _nextGameObject;

    /// <summary>
    /// ��̐����Ԃ������ڐG�����̒��S�̈ʒu
    /// </summary>
    private Vector3 _myselfAndOtherPersonsCenter = default;
    /// <summary>
    /// ��̐��̊Ԃ����炩�Ɉړ������邽�߂̕��
    /// </summary>
    private Quaternion _myselfAndOtherPersonsRotation = default;
    /// <summary>
    /// �i����(��������I�u�W�F�N�g)�̑��x�̓��ꕨ
    /// </summary>
    private Vector3 _nextGameObjectVelocity = default;
    /// <summary>
    /// �Ԃ����������WaterCollision
    /// </summary>
    private WaterCollision _opponentsWaterCollision;
    /// <summary>
    /// �i����̃Q�[���I�u�W�F�N�g��Rigidbody2D
    /// </summary>
    private Rigidbody2D _nextRb2d;
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
    public static Action<int> _onScoreAdded;
    #endregion 
    private void Awake()
    {
        //���̒ʂ��ԍ����R�s�[
        _mySerialNumber = _waterSerialNumber;

        //�R�s�[��ɔԍ��𑝂₵�āA���ɗ��鐅�Ɣԍ������Ȃ��悤�ɂ���
        _waterSerialNumber++;
        if (_isAwakeSeconce)
        {
            return;
        }
        else
        {
            _thisRb2d = gameObject.GetComponent<Rigidbody2D>();
            _isAwakeSeconce = true;
        }

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("�����̐i���n��(OnCollision2d[0])" + _nextWaterPrefab);
        //�Ԃ����������WaterCollision�����ĂȂ�&����̐��^�C�v�������Ɠ����łȂ����
        if (!collision.gameObject.TryGetComponent(out _opponentsWaterCollision))
        {
            return;
        }
        if (_opponentsWaterCollision._waterVariousObjectData.MyWaterType != this._waterVariousObjectData.MyWaterType)
        {
            _opponentsWaterCollision = null;
            return;
        }
        //�i���Ɛi���̂��߂̏��������錴��
        //���̂Ƃ���Ԃ���������Ƃ̔ԍ����r���āA
        //�ԍ���������(�Â�)���̃I�u�W�F�N�g�������ȉ��̏����𑖂点�邱�Ƃ��o����
        if (_waterVariousObjectData.GetSetNextEvolvingObject != null && _mySerialNumber < _opponentsWaterCollision._mySerialNumber)
        {
            _collisionGameObject = collision.gameObject;
            GetOthersRigidbody2D();
            GetColisionCenterPositionAndLinearInterpolation();
            //�X�R�A�����Z
            _onScoreAdded?.Invoke(_waterVariousObjectData.GetSetScore);
            //�����Ƒ���������Đi����̃I�u�W�F�N�g�𐶐�����
            Destroy(this.gameObject);
            Destroy(_collisionGameObject);
            _nextGameObject = Instantiate(_waterVariousObjectData.GetSetNextEvolvingObject);

            #region �v�I�u�W�F�N�g�v�[��
            ////�I�u�W�F�N�g���ƂɃI�u�W�F�N�g�v�[�����قȂ�̂ŁA�����𕪂���
            //switch (_waterVariousObjectData.MyWaterType)
            //{
            //    case WaterVariousObjectData._waterType.Drop:
            //        //_dropObjectPool.Release(gameObject); //���g��
            //        //_dropObjectPool.Release(_collisionGameObject); //���������
            //        //Debug.Log(_dropObjectPool);
            //        //MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        //_nextGameObject = _dropObjectPool.Get();//�i����̃I�u�W�F�N�g�𐶐�
            //        break;
            //    case WaterVariousObjectData._waterType.Water:
            //        _waterObjectPool.Release(gameObject); //���g��
            //        _waterObjectPool.Release(_collisionGameObject); //���������                                                                       
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _waterObjectPool.Get();//�i����̃I�u�W�F�N�g�𐶐�
            //        break;

            //    case WaterVariousObjectData._waterType.Puddle:
            //        _puddleObjectPool.Release(gameObject); //���g��
            //        _puddleObjectPool.Release(_collisionGameObject); //���������                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _puddleObjectPool.Get();//�i����̃I�u�W�F�N�g�𐶐�
            //        break;
            //    case WaterVariousObjectData._waterType.Pond:
            //        _pondObjectPool.Release(gameObject); //���g��
            //        _pondObjectPool.Release(_collisionGameObject); //���������                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _pondObjectPool.Get();//�i����̃I�u�W�F�N�g�𐶐�
            //        break;
            //    case WaterVariousObjectData._waterType.Lake:
            //        _lakeObjectPool.Release(gameObject); //���g��
            //        _lakeObjectPool.Release(_collisionGameObject); //���������                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _lakeObjectPool.Get();//�i����̃I�u�W�F�N�g�𐶐�

            //        break;
            //    case WaterVariousObjectData._waterType.River:
            //        _riverObjectPool.Release(gameObject); //���g��
            //        _riverObjectPool.Release(_collisionGameObject); //���������                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _riverObjectPool.Get();//�i����̃I�u�W�F�N�g�𐶐�

            //        break;
            //    case WaterVariousObjectData._waterType.Sea:
            //        _seaObjectPool.Release(gameObject); //���g��
            //        _seaObjectPool.Release(_collisionGameObject); //���������                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _seaObjectPool.Get();//�i����̃I�u�W�F�N�g�𐶐�
            //        break;
            //}
            #endregion

            //�I�u�W�F�N�g�������ɃX�R�A���Z
            //_onScoreAdded.Invoke(WaterVariousObjectData._score);
            ChangeNextObjectPositionAndRotation();
            _nextRb2d = _nextGameObject.GetComponent<Rigidbody2D>();
            TakeVelocityAndAngularvelocity();
            GiveVelocityAndAngularVelocity();
            InitializeVariables();
            //Debug.Log("���̃I�u�W�F�N�g��" + _nextGameObject.gameObject.name);
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
    private void ChangeNextObjectPositionAndRotation()
    {
        //���������I�u�W�F�N�g�̈ʒu�Ɖ�]�𓯎��ɕύX����
        _nextGameObject.transform.SetLocalPositionAndRotation(this._myselfAndOtherPersonsCenter, this._myselfAndOtherPersonsRotation);
    }
    /// <summary>
    /// �Ԃ���������Ǝ����̑��x�Ɗp���x���擾���đ����Ĕ����Ɋ���
    /// </summary>
    private void TakeVelocityAndAngularvelocity()
    {
        //�Ԃ���������Ǝ����̑��x�Ɗp���x�𑫂��Ċ���
        _nextGameObjectVelocity = (_thisRb2d.velocity + _collidedRb2d.velocity) / _waterObjectConstData.GetDivideIntoTwo;
        _myselfAndOtherPersonsAngularVelocity = (_thisRb2d.angularVelocity + _collidedRb2d.angularVelocity) / _waterObjectConstData.GetDivideIntoTwo;
    }
    /// <summary>
    /// �V�������������I�u�W�F�N�g�ɑ��x�Ɗp���x��^����
    /// </summary>
    private void GiveVelocityAndAngularVelocity()
    {
        //�Ԃ���������Ǝ����̑��x�Ɗp���x�̕��ς�V�������������I�u�W�F�N�g�ɗ^����
        _nextRb2d.velocity = _nextGameObjectVelocity;
        _nextRb2d.angularVelocity = _myselfAndOtherPersonsAngularVelocity;
    }
    /// <summary>
    /// ��̃I�u�W�F�N�g�̐ڐG�����̒��S�̈ʒu�Ɛ��`��Ԃ��擾����
    /// </summary>
    private void GetColisionCenterPositionAndLinearInterpolation()
    {
        //��̐����Ԃ������ڐG�����̒��S�̈ʒu
        _myselfAndOtherPersonsCenter = (transform.position + _opponentsWaterCollision.transform.position) / _waterObjectConstData.GetDivideIntoTwo;
        #region Quaternion.Lerp�Ƃ�

        /*���`��Ԃ����Ă����B�����͎O��
         * 
         * �u���`��ԁv
         * 
         * ���ꂽ�ꏊ�ɓ�_���������ꍇ�A���̊Ԃ𒼐��ł��邱�Ƃ�z�肵�ċߎ��I�ɕ₤���@
         * 
         * Lerp(�n�܂�̈ʒu(Vector3), �I���̈ʒu(Vector3), ���݂̈ʒu(float))
         */

        #endregion
        //��̐��̊Ԃ����炩�Ɉړ������邽�߂̕��
        _myselfAndOtherPersonsRotation = Quaternion.Lerp(transform.rotation, _opponentsWaterCollision.transform.rotation, _waterObjectConstData.GetMyCurrentPosition);
    }
    /// <summary>
    /// �ϐ�������������
    /// </summary>
    private void InitializeVariables()
    {
        _collisionGameObject = null;
        _opponentsWaterCollision = null;
        _collidedRb2d = null;
        _nextGameObjectVelocity = default;
        _myselfAndOtherPersonsAngularVelocity = default;
        _myselfAndOtherPersonsCenter = default;
        _myselfAndOtherPersonsRotation = default;
        _mySerialNumber = 0;
    }

}




