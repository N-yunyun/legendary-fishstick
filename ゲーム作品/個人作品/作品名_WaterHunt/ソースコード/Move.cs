using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Player�̓���
/// </summary>
public class Move : MonoBehaviour
{
    [Header("rigidbody��animator���A�^�b�`���ꂽ�I�u�W�F�N�g�����Ă���")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator anime;

    /// <summary>
    /// �L�����f�[�^
    /// </summary>
    [SerializeField]
    [Header("������ScriptableObject�t�@�C��������")]
    private StateManager stateManager;

    /// <summary>
    /// �̗͂��Ǘ�����X�N���v�g
    /// </summary>
    [SerializeField]
    [Header("������HealthManager������")]
    private HealthManager healthMng;

    /// <summary>
    /// �X�e�[�^�X(swich���ƃZ�b�g�Ŏg��)
    /// </summary>
    private enum JumpState
    {
        /// <summary>
        /// �W�����v��
        /// </summary>
        Wait,

        /// <summary>
        /// �W�����v�҂�
        /// </summary>
        Jump,

        /// <summary>
        /// �W�����v�I���
        /// </summary>
        JumpEnd,

        /// <summary>
        /// �W�����v�{�^��������ĂȂ����(�ʏ탂�[�h)
        /// </summary>
        DontJump,

        //������
        /// <summary>
        /// �_�E��(���~)
        /// </summary>
        Down

    }

    /// <summary>
    /// JumpState�̏�Ԃ�
    /// ����邽�߂̕ϐ�(��Ԃ�����Ƃ��͂�����g��)
    /// </summary>
    [SerializeField] JumpState jumpState = JumpState.DontJump;

    /// <summary>
    ///��񂾋������v������
    /// </summary>
    [SerializeField] private int jumpNum = 0;



    /// <summary>
    /// �ړ��L�[��������Ă��邩
    /// </summary>
    private bool isMove = false;

    /// <summary>
    /// ���L�[��������Ă��邩
    /// </summary>
    [SerializeField]
    private bool isLeft = false;

    /// <summary>
    /// ���L�[��������Ă��邩
    /// </summary>
    [SerializeField]
    private bool isDown = false;

    /// <summary>
    /// ���L�[��������Ă��邩
    /// </summary>
    [SerializeField]
    private bool isJump = false;

    /// <summary>
    /// �ڐ�����
    /// </summary>
    private bool isWater = false;

    void Start()
    {
        //�ʏ��Ԃ̃A�j�����Đ�
        anime.Play("stand");

    }

    // Update is called once per frame
    void Update()
    {
        #region �l�I����

        //���Ȃ݂Ƀ��C�L���X�g�q�b�g�����3D������ԈႦ�Ȃ��悤�ɋC��t����
        //���C���[�}�X�N���w�肷��ƁA���C�͂����Ŏw�肵�����C���[�̂��̂ɂ���������Ȃ��Ȃ�
        //���̃I�u�W�F�N�g�̒��S����Adown�͉��ɁAup�͏�ɁAright�͉E�ɁAleft�͍��Ɍ������ă��C���΂��B

        #endregion

        #region Raycast�̏���

        //�q�b�g�ϐ��ɑ��(����1:���C�̎n�_�A����2:���C�̌����A����3:���C�̋����A����4:���C���[�}�X�N)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, stateManager.rayLong, stateManager.groundLayer);

        //���C������(����1:���C�̎n�_�A����2:���C�̌����ƒ���(�|���Z�ŏ���)�A����3:���C�̐F)
        Debug.DrawRay(transform.position, Vector2.up * stateManager.rayLong, Color.red);

        #endregion

        isJump = Input.GetKey(KeyCode.UpArrow);


        if (hit)//���C�L���X�g�������ɓ��������ꍇ���鏈��
        {
            isWater = true;

            //������������̃R���C�_�[�̖��O��\��
            //Debug.Log(hit.collider.name);

        }
        else
        {
            isWater = false;
            jumpState = JumpState.JumpEnd;
        }

        switch (jumpState)
        {

            //��ɐ����ɂ���Ƃ�
            case JumpState.DontJump:

                MoveOrJumpReady();

                break;

            case JumpState.Wait:

                //������
                jumpNum = 0;

                //�W�����v�Ɉڍs
                jumpState = JumpState.Jump;

                break;

            case JumpState.Jump:

                if (!isJump)
                {
                    //�ʏ�Ɉڍs
                    jumpState = JumpState.DontJump;

                }

                //���݂̐��l���I���̐��l�𒴂�����
                else if (jumpNum >= stateManager.jumpEndNum)
                {
                    //�W�����v�I���Ɉڍs
                    jumpState = JumpState.JumpEnd;
                }

                break;

            case JumpState.JumpEnd:

                anime.SetBool("isJump", false);
                //�n�ʂɂ�����
                if (isWater)
                {
                    jumpState = JumpState.DontJump;//�ʏ��ԂɈڍs
                }

                break;

        }

        #region �ڐ��m�F�̃f�o�b�O�p����

        //isWater�̒l���擾���ă��A���^�C���ŕ�����l�̒��g�ɕς���
        //GameObject.Find("GroundCheck").GetComponent<Text>().text = isWater.ToString();
        //�e�L�X�g�ϐ��͍ŏ���using UnityEngine.UI�������Ȃ��Ǝg���Ȃ�
        //.text = �ϐ���.ToString()�ŕϐ��̓��e���X�g�����O�^�ɕϊ�����UI�ɕ\��������
        #endregion

    }

    private void FixedUpdate()
    {
        switch (jumpState)
        {

            //��ɐ����ɂ���Ƃ�
            case JumpState.DontJump:



                if (isMove)
                {
                    //��������Ă���
                    if (isLeft)
                    {
                        //�v���C���[���������ɂ���
                        transform.localScale = new Vector3(-1, 1, 1);

                        //���ɗ͂�������
                        rb2d.AddForce(new Vector2(-stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));

                        IsDown();

                    }

                    //�E������Ă���
                    else
                    {
                        //�v���C���[���E�����ɂ���
                        transform.localScale = new Vector3(1, 1, 1);

                        //�E�ɗ͂�������
                        rb2d.AddForce(new Vector2(stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));

                        IsDown();
                    }

                }

                IsDown();

                break;

            case JumpState.Wait:


                break;

            case JumpState.Jump:

                //�������Ă邩�E�����Ă邩�ŗ͂������������ς���
                if (isLeft)
                {
                    //�W�����v����
                    rb2d.AddForce(new Vector2(rb2d.velocity.x, stateManager.jumpForce * Time.deltaTime));

                    if (isMove)
                    {
                        //���ɗ͂�������
                        rb2d.AddForce(new Vector2(-stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));

                    }
                }
                else
                {

                    rb2d.AddForce(new Vector2(rb2d.velocity.x, stateManager.jumpForce * Time.deltaTime));

                    if (isMove)
                    {
                        //�E�ɗ͂�������
                        rb2d.AddForce(new Vector2(stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));
                    }
                }


                ////��������Ă���
                //if (isLeft)
                //{
                //    //�v���C���[���������ɂ���
                //    transform.localScale = new Vector3(-1, 1, 1);



                //    IsDown();



                //    //�E������Ă���
                //    else
                //    {
                //        //�v���C���[���E�����ɂ���
                //        transform.localScale = new Vector3(1, 1, 1);

                //        //�E�ɗ͂�������
                //        rb2d.AddForce(new Vector2(stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));

                //        IsDown();
                //    }

                //}

                if (!isWater)
                {
                    //�W�����v�̍��������p�J�E���gON
                    jumpNum++;
                }



                break;

            case JumpState.JumpEnd:

                //�n�ʂɂ�����
                if (isWater)
                {
                    jumpState = JumpState.DontJump;//�ʏ��ԂɈڍs
                }

                break;

        }

    }

    /// <summary>
    /// ���{�^����������Ă���Ή��~����
    /// </summary>
    private void IsDown()
    {
        //���{�^����������ĂȂ���Ώ������S�y���p��return
        if (!isDown)
        {
            return;
        }
        else
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x, -stateManager.downForce * Time.deltaTime));
        }
    }

    /// <summary>
    /// ���Ɉړ��ł��邩�W�����v�ł��邩����
    /// </summary>
    private void MoveOrJumpReady()
    {

        //����������Ă���Ԃ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isLeft = true;
            isMove = true;
        }

        //����������Ă���Ԃ�
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            isLeft = false;
            isMove = true;
        }


        //���L�[��������Ă���Ԃ�
        else if (isJump)
        {
            isDown = false;
            isJump = true;

            jumpState = JumpState.Wait;
        }

        //���L�[��������Ă���Ԃ�
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            isJump = false;
            isDown = true;
            


        }
        //�ړ��L�[��������ĂȂ����
        else
        {
            isMove = false;
            isDown = false;

        }

    }

}



