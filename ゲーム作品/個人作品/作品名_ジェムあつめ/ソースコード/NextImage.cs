using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 次に出てくる予定のオブジェクトの画像を描画する(画面右上にあるimageにアタッチする)
/// </summary>
public class NextImage : MonoBehaviour
{
    [Header("登場するオブジェクトを表示させたいimageをセット")]
    [SerializeField] private Image _nextImage;
    [Header("RandomWaterSelectorをセット")]
    [SerializeField] private RandomObjectsSelect _randomObjectsSelecter;

    /// <summary>
    /// 次に出てくるオブジェクトのスプライトをimageに入れる
    /// </summary>
    public void NextImageInsert()
    {
        _nextImage.sprite = _randomObjectsSelecter.ReservedObject.GetComponent<SpriteRenderer>().sprite;
    }


}
