using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �f�[�^�t�@�C�����W�߂��f�[�^�x�[�X
/// </summary>
[CreateAssetMenu(menuName = "CreateData/DataBase")]
public class WaterDataArray : ScriptableObject
{
    /// <summary>
    /// �e�I�u�W�F�N�g�̃f�[�^��z��ŏW�߂�����
    /// </summary>
    //�o�ꂷ��I�u�W�F�N�g�͂��炩���ߐ������܂��Ă���̂Ŕz��ɂ���
    [SerializeField]public WaterVariousObjectData[] _waterDataArrays = new WaterVariousObjectData[7];
    /// <summary>
    /// �萔�f�[�^�t�@�C��
    /// </summary>
    [SerializeField] private WaterObjectConstData _waterObjectCanstConstBase;
}
