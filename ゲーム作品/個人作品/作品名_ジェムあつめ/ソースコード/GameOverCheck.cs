using System.Collections;
using UnityEngine;
/// <summary>
/// ゲームオーバーの条件を満たしているか監視し、満たしていたらゲームオーバーのイベントを呼ぶ
/// </summary>
public class GameOverCheck : MonoBehaviour

{
    #region 変数
    /// <summary>
    /// 一定時間待ち、待ち終わったらゲームオーバーにするコルーチン
    /// </summary>
    private Coroutine _runningCoroutine;
    /// <summary>
    /// 定数データファイル
    /// </summary>
    [Header("定数のデータファイル(ScriptableObject)をセット")]
    [SerializeField] private ConstData _constData;
    /// <summary>
    /// キャッシュしておいたWaitForSeconds
    /// </summary>
    private WaitForSeconds _monitoringWaitForSeconds;
    [Header("GameOverEventがアタッチされたオブジェクトをセット")]
    [SerializeField] private GameOverEvent _gameOverDisplay;
    #endregion
    private void Start()
    {
        _monitoringWaitForSeconds = new WaitForSeconds(_constData.MonitoringTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //接触したらとりあえずオンにする
        //一定時間経った状態でまだオンならゲームオーバーにする

        //コルーチンがちゃんと入ってたら
        if (_runningCoroutine == null)
        {
            _runningCoroutine = StartCoroutine(WaitAndGameOver());
            //コルーチンを呼び出す
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //接触しなくなったらコルーチンをやめてほしいので、止める
        if (_runningCoroutine != null)
        {
            StopCoroutine(_runningCoroutine);
            _runningCoroutine = null;
        }

    }
    /// <summary>
    /// timeの時間だけ処理を待機し、待機し終わったらゲームオーバーを呼ぶ
    /// </summary>
    /// <param name="time">監視させたい時間</param>
    /// <returns></returns>
    private IEnumerator WaitAndGameOver()
    {
        //水が枠内に入っているか監視するため、指定時間待つ
        yield return _monitoringWaitForSeconds;
        //待ち終わったらゲームオーバーイベントを呼ぶ
        _gameOverDisplay.CallGameOver();

        _runningCoroutine = null;
    }
}
