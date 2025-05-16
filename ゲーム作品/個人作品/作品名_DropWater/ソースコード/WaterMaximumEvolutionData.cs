using UnityEngine;

/// <summary>
/// 最大進化先のオブジェクトのデータ(参照に使う)
/// </summary>
[CreateAssetMenu(menuName = "CreateData/MaximumEvolveData")]
public class WaterMaximumEvolutionData : ScriptableObject
{
    /// <summary>
    /// 最大進化のオブジェクトのデータ
    /// </summary>
    [Header("最大進化先のデータファイルをセット")]
    [SerializeField] private WaterVariousObjectData _waterVariousObjectData;
    /// <summary>
    /// 最大進化のオブジェクトの水のタイプをint型に変換して保持
    /// </summary>
    public int MaximumEvolveType
    {
        get { return (int)_waterVariousObjectData.MyType; }
        private set { }
    }
}
