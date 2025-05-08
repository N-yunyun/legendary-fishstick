using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
///�I�u�W�F�N�g�X�|�i�[�̃X�N���v�g
/// </summary>
public class WaterRain : MonoBehaviour
{
    //�C���X�y�N�^�[�ŘA�g
    [SerializeField]
    private RandomWaterSelect _randomWaterSelector = null;
    /// <summary>
    /// �����_���ɑI�o�����I�u�W�F�N�g�̃v�[�����
    /// </summary>
    [SerializeField]
    private ObjectPool<WaterCollision> _poppedWatersPool;
    /// <summary>
    /// ���Ƃ��\��̃v���n�u(�ŏI���蕪)
    /// </summary>
    [SerializeField]
    private WaterCollision _droppingPrefab = null;
    /// <summary>
    /// ���ɗ����Ă���I�u�W�F�N�g�̉摜�C���[�W
    /// </summary>
    [SerializeField]
    private NextImage _nextImage = null;
    /// <summary>
    /// �萔�f�[�^�t�@�C��
    /// </summary>
    [Header("�萔�̃f�[�^�t�@�C��(ScriptableObject)���Z�b�g")]
    [SerializeField] private WaterObjectConstData _waterObjectCanstConstBase;    
    /// <summary>
    /// �L���b�V���p
    /// </summary>
    private WaitForSeconds _waitForSeconds = null;
    /// <summary>
    /// ���Ƃ��\��̃I�u�W�F�N�g�̃��W�b�h�{�f�B
    /// </summary>
    private Rigidbody2D _droppingPrefabsRb2d;

    private float _horizontalInputValue = default;
    private float _tranformX = default;


    //����������
    private void Awake()
    {
        InitializeVariables();
    }
    void Start()
    {
        //�K�[�x�W�R���N�V������h�����߁A�L���b�V������
        if (_randomWaterSelector != null)
            _waitForSeconds = new WaitForSeconds(_waterObjectCanstConstBase.NextObjectGeneratedCoolTime);

        //���𐶐�����X�N���v�g������1�b�ŃR���[�`������
        StartCoroutine(HandleWater());

    }
    /// <summary>
    /// �����_���ɑI�o���ꂽ���̃I�u�W�F�N�g�𐶐����āA�q�I�u�W�F�N�g�ɂ���
    /// </summary>
    private IEnumerator HandleWater()
    {
        _poppedWatersPool = _randomWaterSelector.SelectNextWaterObject();

        //�w�莞�ԑ҂�
        yield return _waitForSeconds;

        //�v�[������w��̃I�u�W�F�N�g�𐶐�
        _droppingPrefab = _poppedWatersPool.Get();

        //���������I�u�W�F�N�g�̕��������𐧌䂷�邽�߂Ɏ擾����
        _droppingPrefabsRb2d = _droppingPrefab.gameObject.GetComponent<Rigidbody2D>();

        //�����㏈��(���g��transform�ɐ��������I�u�W�F�N�g�����킹��A���̂܂܎��g�̎q�ɂ���)
        MatchPositionAndRotationIsGeneratedObjectWithMyself();
        MakeGeneratedObjectOwnChild();

        //�摜�����ɏo�Ă�I�u�W�F�N�g�ɍ����ւ���
        _nextImage.NextImageInsert();


        //���R�������Ȃ��悤�ɂ���
        _droppingPrefabsRb2d.isKinematic = true;
    }

    void Update()
    {
        //�I�u�W�F�N�g�̈ʒu�������l���Ɏ��܂�悤�ɐ�������
        if(!Input.GetKeyDown(KeyCode.RightArrow)&&!Input.GetKeyDown(KeyCode.LeftArrow)&&!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        //���E�L�[�ňړ�
        _horizontalInputValue = Input.GetAxisRaw("Horizontal") * _waterObjectCanstConstBase.SpawnerMoveSpeed * Time.deltaTime;
        _tranformX = Mathf.Clamp(transform.position.x + _horizontalInputValue, _waterObjectCanstConstBase.SpawnerLeftLimit, _waterObjectCanstConstBase.SpawnerRightLimit);//�ŏ��ƍő��ݒ肵���͂��ꂽ�l��͈͂𒴂��Ȃ��悤�ɐ���
        transform.position = new Vector3(_tranformX, transform.position.y, transform.position.z);

        //������������Ă��Ȃ��Ƃ��͐؂藣���Ȃ��悤�ɂ���
        if (Input.GetKeyDown(KeyCode.Space) && _droppingPrefab != null)
        {
            //���R�������I���ɂ��ăI�u�W�F�N�g��؂藣��
            _droppingPrefabsRb2d.isKinematic = false;
            _droppingPrefab.transform.SetParent(null);

            //���Ɏ��o���I�u�W�F�N�g�̘g���J���Ă������߂ɕϐ�������������
            InitializeVariables();

            //�N�[���^�C����L���ɂ��āA���̐������ł���悤�ɂ���
            StartCoroutine(HandleWater());

        }

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
        _droppingPrefabsRb2d = null;
    }
    /// <summary>
    /// �������ꂽ�I�u�W�F�N�g�̈ʒu�Ɖ�]������(�X�|�i�[)�ɍ��킹��
    /// </summary>
    private void MatchPositionAndRotationIsGeneratedObjectWithMyself()
    {
        //���������I�u�W�F�N�g�̈ʒu�Ɖ�]�����g�̃I�u�W�F�N�g�ɍ��킹��
        _droppingPrefab.gameObject.transform.SetLocalPositionAndRotation(this.transform.position, Quaternion.identity);

    }
}
