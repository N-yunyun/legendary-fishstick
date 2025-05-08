using UnityEngine;
/// <summary>
/// オブジェクトデータファイル
/// </summary>
[CreateAssetMenu(menuName = "CreateData/WaterData")]
public class WaterVariousObjectData : ScriptableObject
{
    /// <summary>
    ///オブジェクトのタイプのカテゴリ
    /// </summary>
    public enum WaterType
    {
        /// <summary>
        /// 雫
        /// </summary>
        Drop = 0,
        /// <summary>
        /// 水
        /// </summary>
        Water,
        /// <summary>
        /// 水たまり
        /// </summary>
        Puddle,
        /// <summary>
        /// 池
        /// </summary>
        Pond,
        /// <summary>
        /// 湖
        /// </summary>
        Lake,
        /// <summary>
        /// 川
        /// </summary>
        River,
        /// <summary>
        /// 海
        /// </summary>
        Sea,
    }
    /// <summary>
    /// 自分のオブジェクトのタイプ
    /// </summary>
    [Header("自分のタイプを設定する")]
    [SerializeField]private WaterType _myWaterType;
    /// <summary>
    /// 自分のオブジェクトの種類(Get)
    /// </summary>
    public WaterType MyWaterType
    {
        get => _myWaterType;
        private set { }
    }
    /// <summary>
    /// 自分のオブジェクトの名前
    /// </summary>
    [Header("自分のオブジェクトの名前を設定する")]
    [SerializeField]
    private string _myObjectName = null;
    /// <summary>
    /// 自分のオブジェクトの名前(GetSet)
    /// </summary>
    public string GetSetMyObjectName
    {
        get => _myObjectName;
        set => _myObjectName = value;
    }
    /// <summary>
    /// 自分のスコア
    /// </summary>
    [Header("自分のスコアを設定する")]
    [SerializeField]
    private int _score = 0;
    /// <summary>
    /// 自分のスコア(GetSet)
    /// </summary>
    public int GetSetScore
    {
        get => _score;
        set => _score = value;
    }
    /// <summary>
    /// 自分のオブジェクト
    /// </summary>
    [Header("自分のオブジェクトを設定する")]
    [SerializeField]
    private GameObject _myObject = default;
    /// <summary>
    /// 自分のオブジェクト(GetSet)
    /// </summary>
    public GameObject MyObjectPrefab
    {
        get => _myObject;
        set => _myObject = value;
    }
    /// <summary>
    /// 自分が次に進化するオブジェクト
    /// </summary>
    [Header("自分が次に進化するオブジェクトを設定する")]
    [SerializeField]
    private WaterCollision _nextEvolvingObject = null;
    /// <summary>
    /// 自分が次に進化するオブジェクト
    /// </summary>
    public WaterCollision NextEvolvingObject
    {
        get => _nextEvolvingObject;
        set => _nextEvolvingObject = value;
    }

}
