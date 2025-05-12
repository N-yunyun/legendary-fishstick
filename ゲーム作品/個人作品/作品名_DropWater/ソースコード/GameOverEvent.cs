using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// ゲームオーバーのイベントの内容をつかさどる
/// </summary>
public class GameOverEvent : MonoBehaviour
{
    #region 変数
    /// <summary>
    /// 画面を暗くするために必要な画像
    /// </summary>
    [Header("GameOverPanelをセット")]
    [SerializeField] private Image _gameOverPanel;
    /// <summary>
    /// ゲームオーバーの文字
    /// </summary>
    [Header("GameOverTextをセット")]
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [Header("SceneLoadManagerをセット")]
    [SerializeField]
    private SceneLoadManager _sceneLoadManager;
    #endregion
    private void Start()
    {
        _gameOverPanel.enabled = false;
    }
    /// <summary>
    /// ゲームオーバーになったときに画面の操作をできなくして文字を表示する
    /// </summary>
    public void CallGameOver()
    {
        //ゲームオーバー画面と文字を有効化して表示する
        _gameOverPanel.enabled = true;
        _gameOverText.enabled = true;

        //リザルトシーンに移行する
        _sceneLoadManager.LoadResultScene();
    }
}
