using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    /// <summary>
    /// �^�C�g���ɖ߂�
    /// </summary>
    public void ReturnTitle() {
        SceneManager.LoadScene("TitleScene");

    }
    /// <summary>
    /// �N���A�V�[���Ɉڍs����
    /// </summary>
    public void ChangeClearScene() {

        SceneManager.LoadScene("ClearScene");
    }

}
