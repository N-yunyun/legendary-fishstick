using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 次に出てくる予定のオブジェクトの画像を描画する(画面右上にあるimageにアタッチする)
/// </summary>
public class NextImage : MonoBehaviour
{
    [SerializeField] private Image NextWaterImage;
    [SerializeField] private RadomWaterSelect randomWaterSelect;
    //インスペクターで画像を更新したいImageとRandomWaterSelector(オブジェクト)をセット

    /// <summary>
    /// 次に出てくる水のスプライトをimageに入れる
    /// </summary>
    public void NextImageInsert()
    {
        NextWaterImage.sprite = randomWaterSelect.GetReservedWaters.GetComponent<SpriteRenderer>().sprite;
    }


}
