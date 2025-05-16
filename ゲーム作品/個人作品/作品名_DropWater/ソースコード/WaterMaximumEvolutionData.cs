using UnityEngine;

/// <summary>
/// �ő�i����̃I�u�W�F�N�g�̃f�[�^(�Q�ƂɎg��)
/// </summary>
[CreateAssetMenu(menuName = "CreateData/MaximumEvolveData")]
public class WaterMaximumEvolutionData : ScriptableObject
{
    /// <summary>
    /// �ő�i���̃I�u�W�F�N�g�̃f�[�^
    /// </summary>
    [Header("�ő�i����̃f�[�^�t�@�C�����Z�b�g")]
    [SerializeField] private WaterVariousObjectData _waterVariousObjectData;
    /// <summary>
    /// �ő�i���̃I�u�W�F�N�g�̐��̃^�C�v��int�^�ɕϊ����ĕێ�
    /// </summary>
    public int MaximumEvolveType
    {
        get { return (int)_waterVariousObjectData.MyType; }
        private set { }
    }
}
