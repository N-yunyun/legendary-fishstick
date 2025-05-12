using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
///�I�u�W�F�N�g�X�|�i�[�̃X�N���v�g
/// </summary>
public class WaterRain : MonoBehaviour
{
    #region �ϐ�
    //�C���X�y�N�^�[�ŘA�g
    [SerializeField]
    private RandomWaterSelect _randomWaterSelector;
    /// <summary>
    /// �����_���ɑI�o�����I�u�W�F�N�g�̃v�[�����
    /// </summary>
    private ObjectPool<WaterCollision> _poppedObjectPool = default;
    /// <summary>
    /// ���Ƃ��\��̃v���n�u
    /// </summary>
    private WaterCollision _droppingPrefab = default;
    /// <summary>
    /// ���ɗ����Ă���I�u�W�F�N�g�̉摜�C���[�W
    /// </summary>
    [SerializeField]
    private NextImage _nextImage = default;
    /// <summary>
    /// �萔�f�[�^�t�@�C��
    /// </summary>
    [Header("�萔�̃f�[�^�t�@�C��(ScriptableObject)���Z�b�g")]
    [SerializeField] private WaterObjectConstData _waterObjectConstBase = default;
    /// <summary>
    /// �L���b�V���p
    /// </summary>
    private WaitForSeconds _specifiedWaitForSeconds = null;
    /// <summary>
    /// ���Ƃ��\��̃I�u�W�F�N�g�̃��W�b�h�{�f�B
    /// </summary>
    private Rigidbody2D _droppingPrefabsRigidbody;
    /// <summary>
    /// �v���C���[�̉��̓��͒l
    /// </summary>
    private float _horizontalInputValue = default;
    /// <summary>
    /// �������ꂽ�X�|�i�[��position��x�̒l
    /// </summary>
    private float _ControlledTransformPositionX = default;
    #endregion

    //����������
    private void Awake()
    {
        InitializeVariables();
    }
    private void Start()
    {
        //�K�[�x�W�R���N�V������h�����߁A�L���b�V������
        _specifiedWaitForSeconds = new WaitForSeconds(_waterObjectConstBase.NextObjectGeneratedCoolTime);

        //���𐶐�����X�N���v�g������1�b�ŃR���[�`������
        StartCoroutine(HandleWater());

    }
    /// <summary>
    /// �����_���ɑI�o���ꂽ���̃I�u�W�F�N�g�𐶐����āA�q�I�u�W�F�N�g�ɂ���
    /// </summary>
    private IEnumerator HandleWater()
    {
        //�I�o�����I�u�W�F�N�g�̃v�[�������󂯎��
        ReceiveNextPoppedObjectPool();

        //�w�莞�ԑ҂�
        yield return _specifiedWaitForSeconds;

        //�v�[������w��̃I�u�W�F�N�g�𐶐�
        _droppingPrefab = _poppedObjectPool.Get();

        //���R�����𐧌䂷�邽�߁ARigidbody2D���擾����
        _droppingPrefabsRigidbody = _droppingPrefab.GetComponent<Rigidbody2D>();

        //�����㏈��(���g��transform�ɐ��������I�u�W�F�N�g�����킹��A���̂܂܎��g�̎q�ɂ���)
        SetTransformToSelf();
        MakeGeneratedObjectOwnChild();

        //�摜�����ɏo�Ă�I�u�W�F�N�g�ɍ����ւ���
        _nextImage.NextImageInsert();

        //���R�������Ȃ��悤�ɂ���
        _droppingPrefabsRigidbody.isKinematic = true;
    }

    private void Update()
    {
        //���E�L�[�ŉ������Ɉړ�
        MoveHorizontall();
        //�ړ����w�肵���͈͂𒴂��Ȃ��悤�ɐ�������
        MoveAreaRestricted();

        //������������Ă��Ȃ��Ƃ��͐؂藣���Ȃ��悤�ɂ���
        if (Input.GetKey(KeyCode.Space) && _droppingPrefab != null)
        {
            //�I�u�W�F�N�g��؂藣��
            DetachObject();

            //���Ɏ��o���I�u�W�F�N�g�̘g���J���Ă������߂ɕϐ�������������
            InitializeVariables();

            //�N�[���^�C����L���ɂ��āA���̐������ł���悤�ɂ���
            StartCoroutine(HandleWater());

        }

    }
    /// <summary>
    /// �I�o���ꂽ�I�u�W�F�N�g�̃v�[�������󂯎��
    /// </summary>
    private void ReceiveNextPoppedObjectPool()
    {
        _randomWaterSelector.SelectNextDroppingGameObjectsInfo();
        _poppedObjectPool = _randomWaterSelector.ReservedObjectPool;
    }
    /// <summary>
    /// �󂯎���������I�u�W�F�N�g�������̎q���ɂ���
    /// </summary>
    private void MakeGeneratedObjectOwnChild()
    {
        _droppingPrefab.gameObject.transform.SetParent(this.gameObject.transform);
    }
    /// <summary>
    /// �ϐ��̏�����
    /// </summary>
    private void InitializeVariables()
    {
        _droppingPrefab = null;
        _droppingPrefabsRigidbody = null;
    }
    /// <summary>
    /// �������ꂽ�I�u�W�F�N�g�̈ʒu�Ɖ�]������(�X�|�i�[)�ɍ��킹��
    /// </summary>
    private void SetTransformToSelf()
    {
        //���������I�u�W�F�N�g�̈ʒu�Ɖ�]�����g�̃I�u�W�F�N�g�ɍ��킹��
        _droppingPrefab.gameObject.transform.SetLocalPositionAndRotation(this.transform.position, Quaternion.identity);
    }
    /// <summary>
    ///�I�u�W�F�N�g��؂藣��
    /// </summary>
    private void DetachObject()
    {
        //���R�������I���ɂ��ăI�u�W�F�N�g��؂藣��
        _droppingPrefabsRigidbody.isKinematic = false;
        _droppingPrefab.transform.SetParent(null);

    }
    /// <summary>
    /// ���E�L�[�̓��͒l�ŉ��Ɉړ�����
    /// </summary>
    private void MoveHorizontall()
    {
        //���E�L�[�ňړ�
        _horizontalInputValue = 
            Input.GetAxisRaw("Horizontal") * _waterObjectConstBase.SpawnerMoveSpeed * Time.deltaTime;
    }
    /// <summary>
    /// �ړ��͈͂𐧌�����
    /// </summary>
    private void MoveAreaRestricted()
    {
        //�ŏ��ƍő��ݒ肵���͂��ꂽ�l��͈͂𒴂��Ȃ��悤�ɐ���
        _ControlledTransformPositionX = Mathf.Clamp(transform.position.x + _horizontalInputValue, _waterObjectConstBase.SpawnerLeftLimit, _waterObjectConstBase.SpawnerRightLimit);
        transform.position = new Vector3(_ControlledTransformPositionX, transform.position.y, transform.position.z);

    }
}
