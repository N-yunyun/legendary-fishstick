using UnityEngine;
/// <summary>
/// 定数データファイル
/// </summary>
[CreateAssetMenu(menuName = "CreateData/ConstData")]
public class ConstData : ScriptableObject
{
    [Header("2つに割るときに使う")]
    [SerializeField] private int _halfDivide = 2;
    /// <summary>
    /// 2つに割るときに使う
    /// </summary>
    public int HalfDivider
    {
        get { return _halfDivide; }
        private set { }
    }
    [Header("次のオブジェクトが生成されるまでのクールタイム")]
    [SerializeField] private float _nextObjectGeneratedCoolTime = 1f;
    /// <summary>
    /// 次のオブジェクトが生成されるまでのクールタイム
    /// </summary>
    public float NextObjectGeneratedCoolTime
    {
        get { return _nextObjectGeneratedCoolTime; }
        private set { }
    }
    [Header("スポナーの移動速度")]
    [SerializeField] private float _spawnerMoveSpeed = 5f;
    /// <summary>
    /// スポナーの移動速度
    /// </summary>
    public float SpawnerMoveSpeed
    {
        get { return _spawnerMoveSpeed; }
    }
    [Header("スポナー移動の左側の限界値")]
    [SerializeField] private float _spawnerLeftLimit = -2.45f;
    /// <summary>
    /// スポナー移動の左側の限界値
    /// </summary>
    public float SpawnerLeftLimit
    {
        get { return _spawnerLeftLimit; }
    }

    [Header("スポナー移動の右側の限界値")]
    [SerializeField] private float _spawnerRightLimit = 2.45f;
    /// <summary>
    /// スポナー移動の右側の限界値
    /// </summary>
    public float SpawnerRightLimit
    {
        get { return _spawnerRightLimit; }
    }
    /// <summary>
    /// Lerp関数で指定する現在の位置
    /// </summary>
    [Header("Lerp関数で指定する現在の位置")]
    [SerializeField] private float _myCurrentPosition = 0.5f;
    /// <summary>
    /// Lerp関数で指定する現在の位置
    /// </summary>
    public float MyCurrentPosition
    {
        get { return _myCurrentPosition; }
    }
    /// <summary>
    /// ObjectPoolの初期容量
    /// </summary>
    [Header("ObjectPoolの初期容量")]
    [SerializeField]private int _initialCapacity = 15;
    /// <summary>
    /// ObjectPoolの初期容量
    /// </summary>
    public int InitialCapacity
    {
        get { return _initialCapacity; }
    }
    /// <summary>
    /// ObjectPoolの最大容量
    /// </summary>
    [Header("ObjectPoolの最大容量")]
    [SerializeField] private int _maximumCapacity = 15;
    /// <summary>
    /// ObjectPoolの最大容量
    /// </summary>
    public int MaximumCapacity
    {
        get { return _maximumCapacity; }
    }
    /// <summary>
    /// (処理を待たせたい)監視させたい時間
    /// </summary>
    [Header("(処理を待たせたい)監視させたい時間")]
    [SerializeField] private float _monitoringTime = 2f;
    /// <summary>
    /// (処理を待たせたい)監視させたい時間
    /// </summary>
    public float MonitoringTime
    {
        get { return _monitoringTime; }
    }
    /// <summary>
    /// 最大スコア限度
    /// </summary>
    [Header("最大スコア限度")]
    [SerializeField] private int _maxScoreLimit = 9999999;
    /// <summary>
    /// 最大スコア限度
    /// </summary>
    public int MaxScoreLimit
    {
        get { return _maxScoreLimit; }
    }
}
