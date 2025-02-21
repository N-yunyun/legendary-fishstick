using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���I�u�W�F�N�g�̒萔�f�[�^�t�@�C��
/// </summary>
[CreateAssetMenu(menuName = "CreateData/ConstData")]
public class WaterObjectConstData : ScriptableObject
{
    /// <summary>
    /// 2�Ɋ���Ƃ��Ɏg��
    /// </summary>
    [SerializeField]private int _divideIntoTwo = 2;
    /// <summary>
    /// 2�Ɋ���Ƃ��Ɏg��
    /// </summary>
    public int GetDivideIntoTwo
    {
         get{ return _divideIntoTwo; }
    }

    /// <summary>
    /// Lerp�֐��Ŏw�肷�錻�݂̈ʒu
    /// </summary>
    [SerializeField]private float _myCurrentPosition = 0.5f;
    /// <summary>
    /// Lerp�֐��Ŏw�肷�錻�݂̈ʒu
    /// </summary>
    public float GetMyCurrentPosition
    {
        get{ return _myCurrentPosition; }
    }
}
