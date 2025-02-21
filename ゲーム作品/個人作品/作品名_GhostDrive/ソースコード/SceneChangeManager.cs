using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    /// <summary>
    /// タイトルに戻る
    /// </summary>
    public void ReturnTitle() {
        SceneManager.LoadScene("TitleScene");

    }
    /// <summary>
    /// クリアシーンに移行する
    /// </summary>
    public void ChangeClearScene() {

        SceneManager.LoadScene("ClearScene");
    }

}
