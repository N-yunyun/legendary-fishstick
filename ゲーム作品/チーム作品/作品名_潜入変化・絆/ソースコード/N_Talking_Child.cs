using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class N_Talking_Child : N_TalkBase_Mother
{
    // セリフはインスペクターで書き込む

    /// <summary>
    /// 通常会話用のメッセージリスト
    /// </summary>
    [Header("通常会話用のメッセージ文(Sizeでセリフ文を増やす)")]
    [SerializeField]
    private List<string> talkMessageList;

    /// <summary>
    /// 捕獲会話用のメッセージリスト
    /// </summary>
    [Header("捕獲会話用のメッセージ文(Sizeでセリフ文を増やす)")]
    [SerializeField]
    private List<string> catchMessageList;
    /// <summary>
    /// 特別会話用のメッセージリスト
    /// </summary>
    [Header("特別会話用のメッセージ文(省略)")]
    [SerializeField]
    private List<string> specialTalkMessageList;

    //親クラスから呼ばれるコールバックメソッド (接触 & ボタン押したときに実行)
    protected override IEnumerator OnActionNormal()
    {
        //Debug.Log("コルーチン呼び出された");
        for (int i = 0; i < talkMessageList.Count; ++i)
        {
            // 1フレーム分 処理を待機
            yield return null;

            // セリフ文をwindowに表示

            if (GetIsCatch())
            {
                showMessage(catchMessageList[i]);
            }
            else
            {
                showMessage(talkMessageList[i]);
            }
            


            // ボタン入力を待機
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0));

        }

        yield break;
    }
    protected override IEnumerator OnActionSpecial()
    {
        for (int i = 0; i < specialTalkMessageList.Count; ++i)
        {
            // 1フレーム分 処理を待機
            yield return null;

            // セリフ文をwindowに表示
            //特別会話用のテキストリスト
            showMessage(specialTalkMessageList[i]);
            

            // ボタン入力を待機
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0));

        }

        yield break;
    }

}
