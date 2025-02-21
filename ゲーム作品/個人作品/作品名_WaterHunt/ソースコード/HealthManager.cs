using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   //�L�����o�X�n�̃C���[�W�Ȃǂ��g���Ƃ��͐�΂��Ȃ��ƃG���[
using TMPro;                            //TMPro���Ă��Ă���������΂���
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

/// <summary>
/// �̗͂��Ǘ�����X�N���v�g
/// </summary>
public class HealthManager : MonoBehaviour
{
    #region �ϐ��ꗗ

    /// <summary>
    ///���Ԍv���p�̃J�E���g
    /// </summary>
    private float seconds;

    /// <summary>
    /// ���݂�HP
    /// </summary>
    public int NowHp;

    /// <summary>
    /// HP�̐��l���\���p
    /// </summary>
    public TMP_InputField inputTextField;

    /// <summary>
    /// HP�Q�[�W�\���p
    /// </summary>
    public Slider hpGauge;

    /// <summary>
    /// �L�����f�[�^
    /// </summary>
    [SerializeField]
    [Header("������ScriptableObject�t�@�C��������")]
    private StateManager stateManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        //�Q�[���J�n���͍ő�l����n�܂�̂Ō��݂�HP�ɂ͍ő�l��ݒ肵�Ă���
        NowHp = stateManager.MaxHp;

        hpGauge.minValue = stateManager.MinHp;
        hpGauge.maxValue = stateManager.MaxHp;
        hpGauge.value = NowHp;


        //�L�����o�X��[HP�̌���l/�ő�l]��\��
        inputTextField.text = NowHp.ToString() + "/" + stateManager.MaxHp;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //1�b���ƂɃJ�E���g��1���₵�Ă���
        seconds += Time.deltaTime;

        //3�b���Ƃ�
        if (seconds >= 3)
        {
            //�J�E���g�����Z�b�g
            seconds = 0;

            //HP��1���炷(���������Ȃ������銴�o)
            Damage(-1);

        }

        //HP��0�ɂȂ�����
        if (NowHp <= 0)
        {
            //�Q�[���I�[�o�[
            SceneManager.LoadScene("GameOver");
        }

    }
   
    /// <summary>
    /// �_���[�W�Ɖ񕜌v�Z
    /// </summary>
    /// <param name="healthValue">�_���[�W&&�񕜗�</param>
    public void Damage(int healthValue)
    {
        //���݂�HP��0����A���ő�l�ȉ��Ȃ�
        if (NowHp <= stateManager.MaxHp && NowHp > stateManager.MinHp)
        {

            NowHp = NowHp + healthValue;
            
            
            //HP���ő�l�������Ȃ��悤�ɐ���
            if (NowHp >= stateManager.MaxHp)
            {
                NowHp = stateManager.MaxHp;
            }
            
            //HP���ŏ��l�������Ȃ��悤�ɐ���
            else if (NowHp <= stateManager.MinHp)
            {
                
                NowHp = stateManager.MinHp;

            }

            //HP�Q�[�W�ɔ��f
            hpGauge.value = NowHp;

        }
   
        //�_���[�WUI��s�x�X�V
    inputTextField.text = NowHp.ToString() + "/" + stateManager.MaxHp;
        
    }

}
