using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���X�|�i�[�̃X�N���v�g
/// </summary>
public class WaterRain : MonoBehaviour //ObjectPoolManager
{
    //[SerializeField]
    //private ObjectPoolManager _objectPoolManager = null;

    //�C���X�y�N�^�[�ŘA�g
    [SerializeField]
    private RadomWaterSelect _randomWaterSelector = null;
    /// <summary>
    /// �����_���ɑI�o�����v���n�u(WaterCollision�^)
    /// </summary>
    [SerializeField]
    private GameObject _poppedPrefab = null;
    /// <summary>
    /// ���Ƃ��\��̃v���n�u(�ŏI���蕪)
    /// </summary>
    [SerializeField]
    private GameObject _droppingPrefab = null;
    /// <summary>
    /// ���ɗ����Ă���I�u�W�F�N�g�̉摜�C���[�W
    /// </summary>
    [SerializeField]
    private NextImage _nextImage = null;
    /// <summary>
    /// ���̐��I�u�W�F�N�g�����������܂ł̃N�[���^�C��
    /// </summary>
    private float _nextObjectGeneratedCoolTime = 1f;
    
    private float _SpawnerMoveSpeed = 5f;
    /// <summary>
    /// �X�|�i�[�ړ��̍����̌��E�l
    /// </summary>
    private float _SpawnerLeftLimit = -2.45f;
    /// <summary>
    /// �X�|�i�[�ړ��̉E���̌��E�l
    /// </summary>
    private float _SpawnerRightLimit = 2.45f;
    private WaitForSeconds _waitForSeconds = null;

    // Start is called before the first frame update
    void Start()
    {
        //�K�[�x�W�R���N�V������h�����߁A�L���b�V������
        if (_randomWaterSelector != null)
        _waitForSeconds = new WaitForSeconds(_nextObjectGeneratedCoolTime);
        //���𐶐�����X�N���v�g������1�b�ŃR���[�`������
        StartCoroutine(HandleWater());
        
    }
    /// <summary>
    /// �����_���ɑI�o���ꂽ���̃I�u�W�F�N�g�𐶐����āA����ɗ����Ȃ��悤�Ɏq�I�u�W�F�N�g�ɂ���q�I�u�W�F�N�g
    /// </summary>
    /// <param name="delay">������x�点��������</param>
    /// <returns></returns>
    private IEnumerator HandleWater()
    {
        _poppedPrefab = _randomWaterSelector.Pop();

        //�w�莞�ԑ҂�
        yield return _waitForSeconds;
        _droppingPrefab = Instantiate(_poppedPrefab.gameObject);
        #region �v�I�u�W�F�N�g�v�[��
        //switch (_poppedPrefab._waterVariousObjectData.MyWaterType)
        //{
        //    case WaterVariousObjectData._waterType.Drop:

        //        MediatingObjectCreation(_poppedPrefab.gameObject);
        //        _droppingPrefab = _dropObjectPool.Get(); //���Ƃ��\��̐��I�u�W�F�N�g�𐶐�
        //        break;

        //    case WaterVariousObjectData._waterType.Water:

        //        MediatingObjectCreation(_poppedPrefab.gameObject);
        //        _droppingPrefab = _waterObjectPool.Get(); //���Ƃ��\��̐��I�u�W�F�N�g�𐶐�

        //        break;

        //    case WaterVariousObjectData._waterType.Puddle:

        //        MediatingObjectCreation(_poppedPrefab.gameObject);
        //        _droppingPrefab = _puddleObjectPool.Get(); //���Ƃ��\��̐��I�u�W�F�N�g�𐶐�

        //        break;
        //}
        #endregion
        //�󂯎���������I�u�W�F�N�g�������̎q���ɂ���
        MatchPositionAndRotationIsGeneratedObjectWithMyself();
        MakeGeneratedObjectOwnChild();
        _nextImage.NextImageInsert();
        //Rigidbody2D��Kinematic��L���ɂ��邱�ƂŎ��R������h��
        _droppingPrefab.GetComponent<Rigidbody2D>().isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        //������������Ă��Ȃ��Ƃ��͐؂藣���Ȃ��悤�ɂ���
        if (Input.GetKeyDown(KeyCode.Space) && _droppingPrefab != null)
        {
            _droppingPrefab.GetComponent<Rigidbody2D>().isKinematic = false;//����؂藣��
            _droppingPrefab.transform.SetParent(null);

            //���Ɏ��o���I�u�W�F�N�g�̘g���J���Ă������߂ɕϐ�������������
            InitializeVariables();

            //�N�[���^�C����L���ɂ��āA���̐������ł���悤�ɂ���
            StartCoroutine(HandleWater());

        }
        //���E�L�[�ňړ�
        float horizontal = Input.GetAxisRaw("Horizontal") * _SpawnerMoveSpeed * Time.deltaTime;

        float x = Mathf.Clamp(transform.position.x + horizontal, _SpawnerLeftLimit, _SpawnerRightLimit);//�ŏ��ƍő��ݒ肵���͂��ꂽ�l��͈͂𒴂��Ȃ��悤�ɐ���
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    /// <summary>
    /// �󂯎���������I�u�W�F�N�g�������̎q���ɂ���
    /// </summary>
    private void MakeGeneratedObjectOwnChild()
    {
        _droppingPrefab.transform.SetParent(this.gameObject.transform);
    }
    /// <summary>
    /// �ϐ��̏�����
    /// </summary>
    private void InitializeVariables()
    {
        _poppedPrefab = null;
        _droppingPrefab = null;
    }
    /// <summary>
    /// �������ꂽ�I�u�W�F�N�g�̈ʒu�Ɖ�]������(�X�|�i�[)�ɍ��킹��
    /// </summary>
    private void MatchPositionAndRotationIsGeneratedObjectWithMyself()
    {
        //���������I�u�W�F�N�g�̈ʒu�Ɖ�]�����g�̃I�u�W�F�N�g�ɍ��킹��
        _droppingPrefab.transform.SetLocalPositionAndRotation(this.transform.position, Quaternion.identity);

    }
}
