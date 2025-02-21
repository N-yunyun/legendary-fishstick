using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                   //キャンバス系のイメージなどを使うときは絶対つけないとエラー
using TMPro;                            //TMProってついてるやつもこれを絶対つける
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

/// <summary>
/// 体力を管理するスクリプト
/// </summary>
public class HealthManager : MonoBehaviour
{
    #region 変数一覧

    /// <summary>
    ///時間計測用のカウント
    /// </summary>
    private float seconds;

    /// <summary>
    /// 現在のHP
    /// </summary>
    public int NowHp;

    /// <summary>
    /// HPの数値化表示用
    /// </summary>
    public TMP_InputField inputTextField;

    /// <summary>
    /// HPゲージ表示用
    /// </summary>
    public Slider hpGauge;

    /// <summary>
    /// キャラデータ
    /// </summary>
    [SerializeField]
    [Header("ここにScriptableObjectファイルを入れる")]
    private StateManager stateManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        //ゲーム開始時は最大値から始まるので現在のHPには最大値を設定している
        NowHp = stateManager.MaxHp;

        hpGauge.minValue = stateManager.MinHp;
        hpGauge.maxValue = stateManager.MaxHp;
        hpGauge.value = NowHp;


        //キャンバスに[HPの現状値/最大値]を表示
        inputTextField.text = NowHp.ToString() + "/" + stateManager.MaxHp;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //1秒ごとにカウントを1増やしていく
        seconds += Time.deltaTime;

        //3秒ごとに
        if (seconds >= 3)
        {
            //カウントをリセット
            seconds = 0;

            //HPを1減らす(少しずつおなかが減る感覚)
            Damage(-1);

        }

        //HPが0になったら
        if (NowHp <= 0)
        {
            //ゲームオーバー
            SceneManager.LoadScene("GameOver");
        }

    }
   
    /// <summary>
    /// ダメージと回復計算
    /// </summary>
    /// <param name="healthValue">ダメージ&&回復量</param>
    public void Damage(int healthValue)
    {
        //現在のHPが0より上、かつ最大値以下なら
        if (NowHp <= stateManager.MaxHp && NowHp > stateManager.MinHp)
        {

            NowHp = NowHp + healthValue;
            
            
            //HPが最大値を下回らないように制御
            if (NowHp >= stateManager.MaxHp)
            {
                NowHp = stateManager.MaxHp;
            }
            
            //HPが最小値を下回らないように制御
            else if (NowHp <= stateManager.MinHp)
            {
                
                NowHp = stateManager.MinHp;

            }

            //HPゲージに反映
            hpGauge.value = NowHp;

        }
   
        //ダメージUIを都度更新
    inputTextField.text = NowHp.ToString() + "/" + stateManager.MaxHp;
        
    }

}
