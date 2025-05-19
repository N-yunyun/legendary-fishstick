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
    public enum ObjectType
    {
        /// <summary>
        /// アメジスト 
        /// </summary>
        Amethyst = 0,
        /// <summary>
        /// トルマリン 
        /// </summary>
        Tourmaline,
        /// <summary>
        /// ルビー 
        /// </summary>
        Ruby,
        /// <summary>
        /// アクアマリン
        /// </summary>
        Aquamarine,
        /// <summary>
        ///カーネリアン 
        /// </summary>
        Carnelian,
        /// <summary>
        /// シトリン
        /// </summary>
        Citrine,
        /// <summary>
        /// ムーンストーン 
        /// </summary>
        Moonstone,
        /// <summary>
        /// ジェイド
        /// </summary>
        Jade,
        /// <summary>
        /// クンツァイト
        /// </summary>
        Kunzite,
        /// <summary>
        /// サファイア 
        /// </summary>
        Sapphire,
        
    }
    /// <summary>
    /// 自分のオブジェクトのタイプ
    /// </summary>
    [Header("自分のタイプを設定する")]
    [SerializeField]private ObjectType _myType;
    /// <summary>
    /// 自分のオブジェクトの種類(Get)
    /// </summary>
    public ObjectType MyType
    {
        get => _myType;
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
