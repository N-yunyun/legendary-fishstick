using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {
    private Touch _playersTouch;
    private int _touchCountLimit = 1;
    // Start is called before the first frame update
    private void Start() {
#if UNITY_STANDALONE

        Debug.Log("UNITY_STANDALONE");
#elif UNITY_EDITOR
        Debug.Log("UNITY_EDITOR");
#elif UNITY_ANDROID
        Debug.Log("UNITY_ANDROID");
#endif
    }
#if UNITY_STANDALONE

    private void Update() {

        if (!Input.anyKeyDown) {
            return;
        }

        SceneManager.LoadScene("MainScene1-night");
    }
#endif
#if UNITY_ANDROID

    private void Update()
    {

    //指が画面に一つも触れられていなかったら
        if (Input.touchCount == 0) { return; }

        //タッチされている指の数が1本しか
        if (Input.touchCount == _touchCountLimit)
        {
            // タッチ情報の取得
            _playersTouch = Input.GetTouch(0);

            //タッチされていた指が離されたら
            if (_playersTouch.phase == TouchPhase.Ended)
            {
                Debug.Log("指が離された");
                SceneManager.LoadScene("MainScene1-night");

            }

        }
    }
#endif
}


