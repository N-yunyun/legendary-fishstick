using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Food : MonoBehaviour
{
    [Tooltip("食べ物を食べるときに出す音を入れる")]
    public AudioClip eatSound;
    [Tooltip("ダメージを受ける時に出す音を入れる")]
    public AudioClip damegeSound;

    /// <summary>
    /// 「食べると体力が回復する食べ物」を格納
    /// </summary>
    GameObject[] foods1;

    /// <summary>
    /// 「食べるとダメージを受ける食べ物」を格納
    /// </summary>
    GameObject[] dameges;

    /// <summary>
    /// 「食べると体力がすごく回復する食べ物」を格納
    /// </summary>
    GameObject[] foods2;

    /// <summary>
    /// 体力を管理するスクリプト
    /// </summary>
    [SerializeField] private HealthManager healthMng;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()

    {
        //オーディオソース取得
        audioSource = GetComponent<AudioSource>();

        //タグがついたオブジェクトを取得してそれぞれに対応する入れ物に格納
        foods1 = GameObject.FindGameObjectsWithTag("Foods");
        dameges = GameObject.FindGameObjectsWithTag("damege");  
        foods2 = GameObject.FindGameObjectsWithTag("Foods2");

    }

    // Update is called once per frame

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);デバッグ用

        //ぶつかったタグが回復する食べ物なら
        if (collision.gameObject.tag == "Foods")
        {
            //音出す
            audioSource.PlayOneShot(eatSound);

            //ぶつかった食べ物を非表示にする
            collision.gameObject.SetActive(false);

            //回復
            healthMng.Damage(1);
        }

        //ぶつかったアイテムがダメージを受ける食べ物なら
        else if (collision.gameObject.tag == "damege")
        {
            //省略
            audioSource.PlayOneShot(damegeSound);
            collision.gameObject.SetActive(false);

            //ダメージ受ける
            healthMng.Damage(-2);

            //ダメージを受けた時の色を赤に変更して
            this.GetComponent<SpriteRenderer>().color = Color.red;

            //0.2秒後に白の色に戻すメソッドを呼び出す      
            Invoke("ColorBack", 0.2f);

        }

        //ぶつかったアイテムが多く回復する食べ物ならなら
        else if (collision.gameObject.tag == "Foods2")
        {

            //省略
            audioSource.PlayOneShot(eatSound);
            collision.gameObject.SetActive(false);

            //おおめに回復
            healthMng.Damage(3);

        }
    }
    /// <summary>
    /// Playerの色を白に戻す(ダメージを受けたときに赤くする処理とセットで使う)
    /// </summary>
    private void ColorBack()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }



}
