using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �f�[�^�t�@�C�����W�߂��f�[�^�x�[�X
/// </summary>
[CreateAssetMenu(menuName = "CreateData/DataBase")]
public class WaterDataArray : ScriptableObject
{
    /// <summary>
    /// �e�I�u�W�F�N�g�̃f�[�^�̔z��
    /// </summary>
    [Header("�f�[�^�t�@�C����ǉ�����ꍇ��\r\n�K���X�N���v�g��ɒǉ�����f�[�^�̖��O���L������")]
    public List<WaterVariousObjectData> _waterDataArrays = new List<WaterVariousObjectData>();
    /// <summary>
    /// �萔�f�[�^�t�@�C��
    /// </summary>
    [Header("�萔�̃f�[�^�t�@�C��(ScriptableObject)���Z�b�g")]
    [SerializeField]private WaterObjectConstData _waterObjectConstBase;
}
