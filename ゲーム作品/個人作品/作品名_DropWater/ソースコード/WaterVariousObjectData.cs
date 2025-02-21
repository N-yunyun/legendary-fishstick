using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "CreateData/WaterData")]
public class WaterVariousObjectData : ScriptableObject
{
    /// <summary>
    ///水のタイプのカテゴリ
    /// </summary>
    public enum _waterType
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
    /// 自分の水のタイプ
    /// </summary>
    [Header("自分のタイプを設定する")]
    [SerializeField]private _waterType _myWaterType;
    /// <summary>
    /// 自分の水の種類(Get)
    /// </summary>
    public _waterType MyWaterType
    {
        get { return _myWaterType; }
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
    private GameObject _myObject = null;
    /// <summary>
    /// 自分のオブジェクト(GetSet)
    /// </summary>
    public GameObject GetSetMyObject
    {
        get => _myObject;
        set => _myObject = value;
    }
    /// <summary>
    /// 自分が次に進化するオブジェクト
    /// </summary>
    [Header("自分が次に進化するオブジェクトを設定する")]
    [SerializeField]
    private GameObject _nextEvolvingObject = null;
    /// <summary>
    /// 自分が次に進化するオブジェクト
    /// </summary>
    public GameObject GetSetNextEvolvingObject
    {
        get => _nextEvolvingObject;
        set => _nextEvolvingObject = value;
    }

    
}
