using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオブジェクトの定数データファイル
/// </summary>
[CreateAssetMenu(menuName = "CreateData/ConstData")]
public class WaterObjectConstData : ScriptableObject
{
    /// <summary>
    /// 2つに割るときに使う
    /// </summary>
    [SerializeField]private int _divideIntoTwo = 2;
    /// <summary>
    /// 2つに割るときに使う
    /// </summary>
    public int GetDivideIntoTwo
    {
         get{ return _divideIntoTwo; }
    }

    /// <summary>
    /// Lerp関数で指定する現在の位置
    /// </summary>
    [SerializeField]private float _myCurrentPosition = 0.5f;
    /// <summary>
    /// Lerp関数で指定する現在の位置
    /// </summary>
    public float GetMyCurrentPosition
    {
        get{ return _myCurrentPosition; }
    }
}
