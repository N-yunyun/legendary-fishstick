using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create State Data")]
public class StateManager : ScriptableObject
{

    [Header("プレイヤーの種類")]
    [SerializeField]
    public GameObject PlayerPrefab;

    /// <summary>
    /// ここで設定したレイヤーにだけレイが当たる
    /// </summary>
    [Header("ここで設定したレイヤーにだけレイが当たる")]
    [SerializeField]
    public LayerMask groundLayer;

    /// <summary>
    /// ジャンプ力
    /// </summary>
    [Header("ジャンプ力")]
    [SerializeField]
    public float jumpForce = 5000f;

    /// <summary>
    /// 歩くスピード
    /// </summary>
    [Header("歩くスピード")]
    [SerializeField]
    public float moveForce = 6000f;

    /// <summary>
    ///　下降するスピード
    /// </summary>
    [Header("下降するスピード")]
    [SerializeField]
    public float downForce = 4000f;

    /// <summary>
    ///　ジャンプの高さ制限値
    /// </summary>
    [Header("ジャンプの高さ制限値")]
    [SerializeField]
    public int jumpEndNum = 5;

    /// <summary>
    /// HP最小値
    /// </summary>
    [Header("HP最小値")]
    [SerializeField]
    public int MinHp = 0;

    /// <summary>
    /// HP最大値
    /// </summary>
    [Header("HP最大値")]
    [SerializeField]
    public int MaxHp = 10;

    /// <summary>
    /// Rayの第3引数:長さ
    /// </summary>
    [Header("Rayの第3引数:長さ")]
    [SerializeField]
    public float rayLong = 1f;

}






