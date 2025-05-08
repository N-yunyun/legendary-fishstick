using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// シーンの読み込みをつかさどる
/// </summary>
public class SceneLoadManager : MonoBehaviour
{
    /// <summary>
    /// ゲームシーンを読み込む
    /// </summary>
    public void OnClickLoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// タイトルシーンを読み込む
    /// </summary>
    public void OnClickLoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    /// <summary>
    /// リザルトシーンを読み込む
    /// </summary>
    public void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
