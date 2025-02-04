using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ɏo�Ă��鐅�������_���őI�сA���̃v���n�u�����o���ăX�|�i�[�Ɉ����n��
/// </summary>
public class RadomWaterSelect : MonoBehaviour
{
    /// <summary>
    /// �o�ꂳ���������̃��X�g(WaterCollision�^)
    /// </summary>
    /// <param name=""></param>
    [Header("�o�ꂳ��������")] 
    [SerializeField] private GameObject[] _WaterPrefabs;
/// <summary>
/// �o�ꂳ����I�u�W�F�N�g
/// </summary>
    private GameObject reservedWaters;
    /// <summary>
    /// �o�ꂳ����I�u�W�F�N�g(Get)
    /// </summary>
    public GameObject GetReservedWaters
    {
        get { return reservedWaters; }
    }
   
    void Start()
    {
        Pop();//Pop���\�b�h�Ŏ��ɏo���ׂ��v���t�@�u��Ԃ�
        /*
         * �����̎d�g��
         * 
         * �o�ꂳ��������ނ̐�����RandomWaterSelector�̃C���X�y�N�^�[�Ŕz��𑝂₵�APrefabVariant������
         */
    }
    /// <summary>
    /// �����_���Ŏ��ɏo��prefab��I��
    /// </summary>
    /// <returns>���ɏo��prefab</returns>
    public GameObject Pop()
    {
        //�Ԃ����Ɏg���ϐ����킩��₷������
        int index = Random.Range(0, _WaterPrefabs.Length);//�����_�������W�֐��Ń����_���Ȓl���C���f�b�N�X�w���ɑ��
        reservedWaters = _WaterPrefabs[index];//�����_���ȃv���n�u��I�o
        //Debug.Log(reservedWaters);
        //Debug.Log(reservedWaters.gameObject.name);

        return reservedWaters;
    }

    
}
