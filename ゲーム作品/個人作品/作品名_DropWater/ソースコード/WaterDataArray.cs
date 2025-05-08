using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// データファイルを集めたデータベース
/// </summary>
[CreateAssetMenu(menuName = "CreateData/DataBase")]
public class WaterDataArray : ScriptableObject
{
    /// <summary>
    /// 各オブジェクトのデータの配列
    /// </summary>
    [Header("データファイルを追加する場合は\r\n必ずスクリプト上に追加するデータの名前を記入する")]
    public List<WaterVariousObjectData> _waterDataArrays = new List<WaterVariousObjectData>();
    /// <summary>
    /// 定数データファイル
    /// </summary>
    [Header("定数のデータファイル(ScriptableObject)をセット")]
    [SerializeField]private WaterObjectConstData _waterObjectConstBase;
}
