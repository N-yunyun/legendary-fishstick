using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ɏo�Ă���\��̃I�u�W�F�N�g�̉摜��`�悷��(��ʉE��ɂ���image�ɃA�^�b�`����)
/// </summary>
public class NextImage : MonoBehaviour
{
    [Header("�o�ꂷ��I�u�W�F�N�g��\����������image���Z�b�g")]
    [SerializeField] private Image NextWaterImage;
    [Header("RandomWaterSelector���Z�b�g")]
    [SerializeField] private RandomWaterSelect randomWaterSelect;

    /// <summary>
    /// ���ɏo�Ă���I�u�W�F�N�g�̃X�v���C�g��image�ɓ����
    /// </summary>
    public void NextImageInsert()
    {
        NextWaterImage.sprite = randomWaterSelect.ReservedObject.GetComponent<SpriteRenderer>().sprite;
    }


}
