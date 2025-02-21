using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 次に出てくる水をランダムで選び、そのプレハブを取り出してスポナーに引き渡す
/// </summary>
public class RadomWaterSelect : MonoBehaviour
{
    /// <summary>
    /// 登場させたい水のリスト(WaterCollision型)
    /// </summary>
    /// <param name=""></param>
    [Header("登場させたい水")] 
    [SerializeField] private GameObject[] _WaterPrefabs;
/// <summary>
/// 登場させるオブジェクト
/// </summary>
    private GameObject reservedWaters;
    /// <summary>
    /// 登場させるオブジェクト(Get)
    /// </summary>
    public GameObject GetReservedWaters
    {
        get { return reservedWaters; }
    }
   
    void Start()
    {
        Pop();//Popメソッドで次に出すべきプレファブを返す
        /*
         * 処理の仕組み
         * 
         * 登場させたい種類の数だけRandomWaterSelectorのインスペクターで配列を増やし、PrefabVariantを入れる
         */
    }
    /// <summary>
    /// ランダムで次に出すprefabを選ぶ
    /// </summary>
    /// <returns>次に出すprefab</returns>
    public GameObject Pop()
    {
        //返す時に使う変数をわかりやすくした
        int index = Random.Range(0, _WaterPrefabs.Length);//ランダムレンジ関数でランダムな値をインデックス指数に代入
        reservedWaters = _WaterPrefabs[index];//ランダムなプレハブを選出
        //Debug.Log(reservedWaters);
        //Debug.Log(reservedWaters.gameObject.name);

        return reservedWaters;
    }

    
}
