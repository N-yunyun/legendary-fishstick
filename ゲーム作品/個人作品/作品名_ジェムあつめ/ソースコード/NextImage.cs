using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ɏo�Ă���\��̃I�u�W�F�N�g�̉摜��`�悷��(��ʉE��ɂ���image�ɃA�^�b�`����)
/// </summary>
public class NextImage : MonoBehaviour
{
    [Header("�o�ꂷ��I�u�W�F�N�g��\����������image���Z�b�g")]
    [SerializeField] private Image _nextImage;
    [Header("RandomWaterSelector���Z�b�g")]
    [SerializeField] private RandomObjectsSelect _randomObjectsSelecter;

    /// <summary>
    /// ���ɏo�Ă���I�u�W�F�N�g�̃X�v���C�g��image�ɓ����
    /// </summary>
    public void NextImageInsert()
    {
        _nextImage.sprite = _randomObjectsSelecter.ReservedObject.GetComponent<SpriteRenderer>().sprite;
    }


}
