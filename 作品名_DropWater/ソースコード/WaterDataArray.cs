using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// データファイルを集めたデータベース
/// </summary>
[CreateAssetMenu(menuName = "CreateData/DataBase")]
public class WaterDataArray : ScriptableObject
{
    /// <summary>
    /// 各オブジェクトのデータを配列で集めたもの
    /// </summary>
    //登場するオブジェクトはあらかじめ数が決まっているので配列にした
    [SerializeField]public WaterVariousObjectData[] _waterDataArrays = new WaterVariousObjectData[7];
    /// <summary>
    /// 定数データファイル
    /// </summary>
    [SerializeField] private WaterObjectConstData _waterObjectCanstConstBase;
}
