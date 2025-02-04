using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRoadManager : MonoBehaviour
{
    public void OnClickRoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickRoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void RoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
