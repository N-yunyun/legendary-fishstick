using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーン上に存在するボタンのイベントを管理する
/// </summary>
public class ButtonManager : SceneChangeManager {
    /*    /// <summary>
        /// 入力を管理するコンポーネント
        /// </summary>
        [SerializeField]private InputManager _inputManager;
    */
    [SerializeField] private bool _isMoveForward = false;
    [SerializeField] private bool _isMoveBack = false;
    [SerializeField] private GameObject _pauseImages;
    private ColorBlock _autoButtonColorBlock;
    /// <summary>
    /// オート時の色（赤）
    /// </summary>
    private Color _activeColor = Color.red;
    /// <summary>
    /// 通常時の色（白）
    /// </summary>
    private Color _defaultColor = Color.white;

    /// <summary>
    /// オートアクセルになっているか
    /// </summary>
    private bool _isAutoAccelerate = false;
    /// <summary>
    /// 前進ボタンが押されているか
    /// </summary>
    public bool IsMoveForward {
        get {

            return _isMoveForward;
        }
    }
    /// <summary>
    /// 後進ボタンが押されているか
    /// </summary>
    public bool IsMoveBack {
        get {
            return _isMoveBack;
        }
    }
    private void Start() {

        //ポーズ画面を無効化する
        _pauseImages.SetActive(false);
    }
    /// <summary>
    /// オートアクセルの状態を切り替える
    /// </summary>
    public void ToggleSwitchingAutoAccelerate() {
        //オートアクセルフラグと_forwardMoveの切り替え
        if (_isAutoAccelerate) {
            _isAutoAccelerate = false;
            ForwardButtonUp();
        } else {
            ForwardButtonDown();
            _isAutoAccelerate = true;
        }

        Debug.Log("オートアクセルモード: " + (_isAutoAccelerate ? "有効" : "無効"));
    }

    /// <summary>
    /// アクセルボタンが押された時に後進フラグを無効化して前進フラグを有効化
    /// </summary>

    public void ForwardButtonDown() {

        BackButtonUp();
        //前進ボタンを押したらオートアクセル状態は解除する
        _isMoveForward = true;
        Debug.Log("アクセルボタンが押された");

    }
    /// <summary>
    /// アクセルボタンが離された時に前進フラグを無効化
    /// </summary>
    public void ForwardButtonUp() {
        Debug.Log("アクセルボタンが離された");
        _isMoveForward = false;

    }
    /// <summary>
    /// バックボタンが押された時に前進フラグを無効化して後進フラグを有効化
    /// </summary>
    public void BackButtonDown() {
        ForwardButtonUp();
        _isAutoAccelerate = false;
        _isMoveBack = true;
        Debug.Log("バックボタンが押された");
    }
    /// <summary>
    /// バックボタンが離された時に後進フラグを無効化
    /// </summary>
    public void BackButtonUp() {
        _isMoveBack = false;
        Debug.Log("バックボタンが離された");

    }
    /// <summary>
    /// メニューボタンを押したらメニューを開く
    /// </summary>
    public void PauseGame() {
        _pauseImages.SetActive(false);
        Time.timeScale = 0;
    }
    /// <summary>
    /// ゲームに戻るボタンを押したらゲームに戻る
    /// </summary>
    public void ResumeGame() {
        _pauseImages.SetActive(false);
        Time.timeScale = 1;
    }
}
