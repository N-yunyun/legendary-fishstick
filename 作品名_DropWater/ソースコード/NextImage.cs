using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// ���ɏo�Ă���\��̃I�u�W�F�N�g�̉摜��`�悷��(��ʉE��ɂ���image�ɃA�^�b�`����)
/// </summary>
public class NextImage : MonoBehaviour
{
    [SerializeField] private Image NextWaterImage;
    [SerializeField] private RadomWaterSelect randomWaterSelect;
    //�C���X�y�N�^�[�ŉ摜���X�V������Image��RandomWaterSelector(�I�u�W�F�N�g)���Z�b�g

    /// <summary>
    /// ���ɏo�Ă��鐅�̃X�v���C�g��image�ɓ����
    /// </summary>
    public void NextImageInsert()
    {
        NextWaterImage.sprite = randomWaterSelect.GetReservedWaters.GetComponent<SpriteRenderer>().sprite;
    }


}
