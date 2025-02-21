using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Food : MonoBehaviour
{
    [Tooltip("�H�ו���H�ׂ�Ƃ��ɏo����������")]
    public AudioClip eatSound;
    [Tooltip("�_���[�W���󂯂鎞�ɏo����������")]
    public AudioClip damegeSound;

    /// <summary>
    /// �u�H�ׂ�Ƒ̗͂��񕜂���H�ו��v���i�[
    /// </summary>
    GameObject[] foods1;

    /// <summary>
    /// �u�H�ׂ�ƃ_���[�W���󂯂�H�ו��v���i�[
    /// </summary>
    GameObject[] dameges;

    /// <summary>
    /// �u�H�ׂ�Ƒ̗͂��������񕜂���H�ו��v���i�[
    /// </summary>
    GameObject[] foods2;

    /// <summary>
    /// �̗͂��Ǘ�����X�N���v�g
    /// </summary>
    [SerializeField] private HealthManager healthMng;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()

    {
        //�I�[�f�B�I�\�[�X�擾
        audioSource = GetComponent<AudioSource>();

        //�^�O�������I�u�W�F�N�g���擾���Ă��ꂼ��ɑΉ�������ꕨ�Ɋi�[
        foods1 = GameObject.FindGameObjectsWithTag("Foods");
        dameges = GameObject.FindGameObjectsWithTag("damege");  
        foods2 = GameObject.FindGameObjectsWithTag("Foods2");

    }

    // Update is called once per frame

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);�f�o�b�O�p

        //�Ԃ������^�O���񕜂���H�ו��Ȃ�
        if (collision.gameObject.tag == "Foods")
        {
            //���o��
            audioSource.PlayOneShot(eatSound);

            //�Ԃ������H�ו����\���ɂ���
            collision.gameObject.SetActive(false);

            //��
            healthMng.Damage(1);
        }

        //�Ԃ������A�C�e�����_���[�W���󂯂�H�ו��Ȃ�
        else if (collision.gameObject.tag == "damege")
        {
            //�ȗ�
            audioSource.PlayOneShot(damegeSound);
            collision.gameObject.SetActive(false);

            //�_���[�W�󂯂�
            healthMng.Damage(-2);

            //�_���[�W���󂯂����̐F��ԂɕύX����
            this.GetComponent<SpriteRenderer>().color = Color.red;

            //0.2�b��ɔ��̐F�ɖ߂����\�b�h���Ăяo��      
            Invoke("ColorBack", 0.2f);

        }

        //�Ԃ������A�C�e���������񕜂���H�ו��Ȃ�Ȃ�
        else if (collision.gameObject.tag == "Foods2")
        {

            //�ȗ�
            audioSource.PlayOneShot(eatSound);
            collision.gameObject.SetActive(false);

            //�����߂ɉ�
            healthMng.Damage(3);

        }
    }
    /// <summary>
    /// Player�̐F�𔒂ɖ߂�(�_���[�W���󂯂��Ƃ��ɐԂ����鏈���ƃZ�b�g�Ŏg��)
    /// </summary>
    private void ColorBack()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }



}
