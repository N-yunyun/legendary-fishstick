using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// �V�[���̓ǂݍ��݂������ǂ�
/// </summary>
public class SceneLoadManager : MonoBehaviour
{
    /// <summary>
    /// �Q�[���V�[����ǂݍ���
    /// </summary>
    public void OnClickLoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// �^�C�g���V�[����ǂݍ���
    /// </summary>
    public void OnClickLoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    /// <summary>
    /// ���U���g�V�[����ǂݍ���
    /// </summary>
    public void LoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
