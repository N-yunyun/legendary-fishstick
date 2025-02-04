using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// ゲームオーバーのイベントの内容をつかさどる
/// </summary>
public class GameOverEvent : MonoBehaviour
{
    /// <summary>
    /// 画面を暗くするために必要な画像(黒の半透明)
    /// </summary>
    [Header("GameOverPanelを入れる")]
    [SerializeField] private Image _gameOverPanel;
    /// <summary>
    /// ゲームオーバーの文字
    /// </summary>
    [Header("GameOverTextを入れる")]
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private SceneRoadManager _sceneRoadManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameOverPanel.enabled = false;

        #region Action　メモ

        //引数の型を指定していないAction型のものに追加するときは
        //(Actionメソッド)名前　+= () => {　追加したい関数名(引数など);　};
        //必ず「;」を括弧内と括弧外につける(計2個)
        #endregion

    }
    /// <summary>
    /// ゲームオーバーになったときに画面の操作をできなくして文字を表示する
    /// </summary>
    public void GameOverCall()
    {
        Debug.Log("ゲームオーバー！");
        _gameOverPanel.enabled = true;
        _gameOverText.enabled = true;
        _sceneRoadManager.RoadResultScene();
            }
}
