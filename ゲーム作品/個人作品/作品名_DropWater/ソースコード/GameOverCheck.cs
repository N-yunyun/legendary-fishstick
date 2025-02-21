using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ゲームオーバーの条件を満たしているか監視し、満たしていたらゲームオーバーのイベントを呼ぶ
/// </summary>
public class GameOverCheck : MonoBehaviour
{
    /// <summary>
    /// 接触フラグ
    /// </summary>
    //private bool isColision = false;
    /// <summary>
    /// 一定時間待ち、待ち終わったらゲームオーバーにするコルーチン
    /// </summary>
    private IEnumerator timerCoroutine;
    /// <summary>
    /// (処理を待たせたい)監視させたい時間
    /// </summary>
    private float waitTime = 1.0f;
    /// <summary>
    /// メモリーにかさばらないようにキャッシュしておいたWaitForSeconds
    /// </summary>
    private WaitForSeconds waitForSeconds;
    [SerializeField] private GameOverEvent gameOverDisplay;

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(waitTime);
        timerCoroutine = TimeMeasurement();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //接触したらとりあえずオンにする(水を落とすときに絶対接触するから)
        //一定時間経った状態でまだオンならゲームオーバーにする

        //コルーチンがちゃんと入ってたら
        if (timerCoroutine != null)
        {
            //コルーチンを呼び出す
            StartCoroutine(timerCoroutine);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //接触しなくなったらコルーチンをやめてほしいので、止める
        //Debug.Log("時間の計測中止");
        StopCoroutine(timerCoroutine);

        //中断されたコルーチンを再開したときに処理が途中からになってしまうの防ぐため、初期化する
        
        //コルーチンをリセット
        timerCoroutine = null;
        //中に入れなおす
        timerCoroutine = TimeMeasurement();
    }
    /// <summary>
    /// timeの時間だけ処理を待機し、待機し終わったらゲームオーバーを呼ぶ
    /// </summary>
    /// <param name="time">監視させたい時間</param>
    /// <returns></returns>
    private IEnumerator TimeMeasurement()
    {
        //Debug.Log("時間の計測開始");

        //水が枠内に入っているか監視するため、指定時間待つ
        yield return waitForSeconds;
        //Debug.Log(time +"秒待ったからゲームオーバー呼ぶ");
        gameOverDisplay.GameOverCall();
        yield break;
    }
}
