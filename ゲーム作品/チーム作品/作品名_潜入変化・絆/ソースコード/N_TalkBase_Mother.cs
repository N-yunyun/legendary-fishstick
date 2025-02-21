using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using TMPro;

/// <summary>
/// 基底クラス(親クラス)
/// 会話ウィンドウを表示し、プレイヤーの会話送りのボタン入力を待つ並列処理を発生させ、会話が終わったらウィンドウを閉じ、並列処理を終了
/// </summary>
public abstract class N_TalkBase_Mother : MonoBehaviour
{
    /*https://qiita.com/_tybt/items/c5e90c8825361d788829 
      [UnityでRPGを作るpart3 接触してるときにボタンを押したらメッセージを表示する]を参考に作成*/

    /*https://xr-hub.com/archives/19842
     * abstractについては{[第14回] 抽象クラス（abstract）の使い方を学ぶ｜Unityで学ぶC#入門}に詳細が載ってます*/

    #region このスクリプト {抽象クラス(abstract)}について
    /*
     ・抽象メソッドを1つ以上持つクラスのこと
     ・[スクリプト界のテンプレート]のようなもの

      「こういうメソッドを実装して」というメソッドのテンプレートを作り、
      「処理内容はこの抽象クラス(テンプレート)を使用する別のスクリプトで書いてね」というもの(要約すると、メソッドの実装指示書)

    この抽象クラスがあることにより、キャラクター全員に共通してなければならないHPやMPなどの実装のし忘れが無くなる
    メソッド名が統一できるので、「攻撃のメソッド名はキャラクターの職業全てで同じだが、メソッドの中身(攻撃方法)はそれぞれは違う」などといったことも容易に可能


    テンプレートのメソッドには以下の情報だけ書かれている

        ・戻り値の型
        ・引数の型
        ・引数名
        ・メソッド名

     ・クラス名の前にabstractをつけると抽象クラスになる{ public abstract class クラス名 }

     ・メソッド名の前にabstractをつけると抽象メソッドにできる

      ※この抽象クラスは誰かに使って(継承)もらわないと存在することができない(抽象クラス単体だけでは使いものにならない)
    　※抽象クラスのメソッドをサブクラス(親である抽象クラスを継承する子供のようなもの)で必ずオーバーライドして(メソッドの実際の中身(処理内容)を書いて)もらう
    　※オーバーライドするメソッドには「override」をつけなければならない{ public override メソッド名}
    　※複数の親クラスを持つことができない
    
     
     */

    #endregion




    #region 変数一覧

    //会話イベントのステータス
    private enum TalkState
    {
        /// <summary>
        /// 通常会話イベント
        /// </summary>
        DefaultTalk,
        /// <summary>
        /// 特別な会話イベント
        /// </summary>
        SpecialTalk,
        /// <summary>
        /// 協力アクションイベント
        /// </summary>
        TeamWorkEvent,
        /// <summary>
        /// 会話なし
        /// </summary>
        NoneTalk,

    }
    /// <summary>
    /// TalkStateの入れ物
    /// デフォルトは会話なし
    /// </summary>

    [SerializeField]
    TalkState talkState = TalkState.NoneTalk;

    /// <summary>
    /// キャンバスそのものではなく、キャンバスの子オブジェクトであるメッセージウィンドウ(Image)のこと
    /// </summary>
    [SerializeField] private Image window;
    [SerializeField] private Image window2;

    [SerializeField] private TextMeshProUGUI talks;
    [SerializeField] private TextMeshProUGUI talks2;

    //private Rigidbody2D rb2d;
    /// <summary>
    /// 特定のイベントが発生するゲームオブジェクトと接触しているかの接触判定用
    /// </summary>
    //private bool isCollision = false;

    /// <summary>
    /// アクションボタン(Bボタン)が押されているかのフラグ(InputSystem用)
    /// </summary>
    protected bool isAction = false;

    /// <summary>
    /// キツネ用の当たり判定
    /// </summary>
    private bool isColAnimal = false;
    /// <summary>
    ///人間用の当たり判定
    /// </summary>
    private bool isColHuman = false;
    /// <summary>
    /// 人間とキツネが同時に当たっているときにのみオンになるチーム当たり判定
    /// </summary>
    private bool isColTeam = false;

    /// <summary>
    /// 通常会話と特別会話の切り替えフラグ(オンなら特別会話、オフなら通常会話)
    /// </summary>
    private bool isSpecial = false;

    /// <summary>
    /// プレイヤーを捕まえるかどうかのフラグ
    /// </summary>
    private bool isCatch = false;
    public bool GetIsCatch()
    {
        return isCatch;
    }
    /// <summary>
    /// プレイヤーの入力待ちをするコルーチン
    /// </summary>
    private IEnumerator coroutine;

    /// <summary>
    /// 人間用の捕獲スクリプト格納
    /// </summary>
    private AlertAndLife_O subAlertAndLife_O;
    /// <summary>
    /// 動物用の捕獲スクリプト格納
    /// </summary>
    private AlertAndLife_O mainAlertAndLife_O;
    private N_SubMove n_SubMove;
    private N_MainMove n_MainMove;


    #region コルーチンメモ

    //プレイヤーの会話送りを待っている間も他の処理を止めないようするため、並列処理として使う(パイプラインのようなもの)

    #endregion

    #endregion


    // 当たり判定があるオブジェクトの範囲内に入ったとき
    protected void OnTriggerEnter2D(Collider2D collider)
    {

        #region 自分用のメモ
        /*
          Equalsは文字列を比較して一致しているかを真か偽で返すメソッド
        ※なので入れ物はstring型ではなくbool型にしています
        */
        #endregion

        // ぶっちゃけスイッチ文の中で一個一個同じようなフラグオン処理しなくても
        //スイッチ文の外でフラグオン処理したほうが書くの一回で済むし楽だと思う

        //ていうか、両方とも変身したらタグ変わるからオブジェクト名で判断したほうが良い


        if (!collider)
        {
            return;
        }

        //キツネ(プレイヤー1)に当たったら
        if (collider.gameObject.name.Equals("MainPlayer"))
        {
            //rb2d = collider.GetComponent<Rigidbody2D>();
            //キツネの衝突フラグをオン
            isColAnimal = true;

            n_MainMove = collider.GetComponent<N_MainMove>();
            mainAlertAndLife_O = collider.GetComponent<AlertAndLife_O>();
            //Debug.Log(n_MainMove);
            Debug.Log(("キツネは") + isColAnimal);
            Debug.Log(mainAlertAndLife_O);


        }

        //人間(プレイヤー2)に当たったら
        if (collider.gameObject.name.Equals("SubPlayer"))
        {
            //rb2d = collider.GetComponent<Rigidbody2D>();
            //Debug.Log("人間の衝突フラグをオン");
            //人間の衝突フラグをオン
            isColHuman = true;
            n_SubMove = collider.GetComponent<N_SubMove>();
            subAlertAndLife_O = collider.GetComponent<AlertAndLife_O>();

            //Debug.Log(("人間は") + isColHuman);
        }

        //N_Talking_Childがアタッチされているゲームオブジェクトのタグによって処理を変えるよ
        switch (this.gameObject.tag)
        {

            //自動的に会話が始まる当たり範囲の場合
            case "TalkStart":

                //プレイヤーのどちらかが当たっていたら
                if (isColAnimal || isColHuman)
                {
                    //通常会話イベントに移行
                    talkState = TalkState.DefaultTalk;
                }

                break;

            //鍵がかかったドアの場合
            case "LockDoor":

                //キツネと人間が同時に当たっているとき(両方のフラグがオンの時)
                if (isColHuman && isColAnimal)
                {
                    //チームタグをオン
                    isColTeam = true;
                    //Debug.Log("チームタグは" + isColTeam);

                    //協力アクションイベントのステータスに移行
                    talkState = TalkState.TeamWorkEvent;

                    #region デバッグ用
                    //Debug.Log(("キツネは") + isColAnimal);
                    //Debug.Log(("人間は") + isColHuman);
                    //Debug.Log(("二人は") + isColTeam);
                    #endregion 
                }

                //二人が同時に揃ってなければ
                else if (!isColTeam)
                {
                    //通常会話イベントに移行
                    talkState = TalkState.DefaultTalk;

                }

                break;


            case "TalkEnemy":

                //捕まる処理はここで呼び出したり書き込まなくても自動的に別のスクリプトで行われるから大丈夫

                //キツネが接しているときは会話は発生しない
                if (isColAnimal || collider.CompareTag("Human"))
                {
                    Debug.Log("捕まえる選択肢に移動");
                    isCatch = true;
                    talkState = TalkState.DefaultTalk;
                }

                //変装後のタグによって会話内容を変える
                else
                {
                    if (collider.gameObject.CompareTag("Soldier"))
                    {
                        isCatch = false;
                        talkState = TalkState.SpecialTalk;
                    }
                    else if (collider.gameObject.CompareTag("Boss"))
                    {
                        isCatch = false;
                        talkState = TalkState.DefaultTalk;
                    }

                    //通常の会話するイベント(人間(変身時)のみ会話可能)
                }

                break;
        }

    }

    // 当たり判定があるオブジェクトの範囲外に出たとき
    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("OnTriggerExit発動");
        //範囲から出たのがキツネ(プレイヤー1)なら
        if (collider.gameObject.name.Equals("MainPlayer"))
        {

            //チームフラグとキツネの衝突フラグをオフ
            isColTeam = false;
            isColAnimal = false;
            n_MainMove = null;
            Debug.Log("動物の衝突フラグをオフにする");
            //会話しない状態にする
            talkState = TalkState.NoneTalk;

            //Debug.Log(("キツネは") + isColAnimal);
            //Debug.Log(("二人は") + isColTeam);

        }

        //範囲から出たのが人間(プレイヤー2)なら
        if (collider.gameObject.name.Equals("SubPlayer"))
        {
            //チームフラグと人間の衝突フラグをオフ
            isColTeam = false;
            isColHuman = false;
            n_SubMove = null;
            //Debug.Log("人間の衝突フラグをオフにする");
            //会話しない状態にする
            talkState = TalkState.NoneTalk;

            //Debug.Log(("人間は") + isColHuman);
            //Debug.Log(("二人は") + isColTeam);

        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
        {

            switch (talkState)
            {
                //通常会話イベント
                case TalkState.DefaultTalk:

                    //ここでコルーチンに引き渡すタグをオンにする

                    isSpecial = false;

                    //Debug.Log("コルーチンを起動するメソッドを呼ぶ");
                    OnStartCoroutine();

                    if (isCatch)
                    {
                        if (isColAnimal)
                        {
                            Debug.Log("動物を捕まる処理を呼ぶ");
                            mainAlertAndLife_O.Arrest();
                        }
                        else
                        {
                            subAlertAndLife_O.Arrest();
                        }

                    }
                    else
                    {
                        return;
                    }

                    break;

                //特別会話イベント
                //自分が特定の敵NPCに変身した状態で、会話できる敵に話しかけた時に発動するイベント
                case TalkState.SpecialTalk:

                    isSpecial = true;

                    OnStartCoroutine();

                    break;

                //協力アクションイベント
                //チームタグがオンの時だけここに移行する(でもキツネのストックに鍵がないと協力イベント自体は発生しない)

                case TalkState.TeamWorkEvent:

                    //ストックに鍵がないか確認する処理

                    /*if(鍵があれば)
                    {
                        talkState = TalkState.SpecialTalk;
                    }

                    //(鍵がなければ)
                    else 
                    {
                        talkState = TalkState.DefaultTalk;
                    }*/

                    //鍵があれば協力イベント
                    //鍵がなければ通常会話に移行


                    break;

                //会話が発生しない通常のステータス
                case TalkState.NoneTalk:

                    break;
            }
        }
    }


    /// <summary>
    /// 会話ウィンドウを出現させて、プレイヤーの会話送りのボタン入力を待機するメソッドを起動する(その後会話を終了する)
    /// </summary>
    private IEnumerator CreateCoroutine()
    {

        //Debug.Log("コルーチン");
        //メインプレイヤー(犬)が操作対象なら
        if (n_MainMove != null && n_MainMove.IsMainPlayer)
        {
            //犬側の会話ウィンドウを起動
            window.gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log("whindow");
            //人間側以下略
            window2.gameObject.SetActive(true);

        }

        //特別会話を呼ぶか通常会話を呼ぶか判断
        if (isSpecial)
        {
            //特別会話を呼ぶ
            yield return OnActionSpecial();
        }

        else
        {
            //通常会話を呼ぶ
            yield return OnActionNormal();
        }

        //メインプレイヤー(犬)が操作対象なら
        if (n_MainMove != null && n_MainMove.IsMainPlayer)
        {
            //犬側のメッセージを消して会話を終了
            this.talks2.text = "";
        }
        else
        {
            //人間側のメッセージを消して会話を終了
            this.talks.text = "";
        }

        //会話ウィンドウを閉じる
        this.window.gameObject.SetActive(false);
        this.window2.gameObject.SetActive(false);

        //コルーチン終了
        StopCoroutine(coroutine);

        coroutine = null;

        //会話が終わったので動けるようにする

        //メインプレイヤー(犬)が操作対象なら
        if (n_MainMove != null && n_MainMove.IsMainPlayer)
        {
            n_MainMove.Restart();
            n_MainMove.enabled = true;
        }
        else
        {
            n_SubMove.Restart();
            n_SubMove.enabled = true;
        }


    }

    /// <summary>
    /// コルーチンが既にスタートしてなければコルーチンをスタートさせる
    /// </summary>
    private void OnStartCoroutine()
    {
        if (coroutine == null)
        {
            //犬側が接触していれば
            if (n_MainMove != null)
            {
                //Debug.Log("MainPlayerを止める");
                //会話中に会話送り以外の操作ができないように
                //犬側の動作のスクリプトをいったん止める
                n_MainMove.Stop();
                n_MainMove.enabled = false;
            }
            //人間側が接触していれば
            else
            {
                //Debug.Log("SubPlayerを止める");
                //人間側の動作スクリプトをいったん止める
                n_SubMove.Stop();
                n_SubMove.enabled = false;
            }
           
            //コルーチン型にコルーチンメソッドを入れる
            coroutine = CreateCoroutine();
            //Debug.Log("コルーチンの中身は" + coroutine);

            // コルーチン起動
            StartCoroutine(coroutine);
            //Debug.Log("コルーチン起動");
        }

    }

    /// <summary>
    /// プレイヤーの会話送りのボタン入力を待機するメソッド(通常会話用)
    /// </summary>
    protected abstract IEnumerator OnActionNormal();

    /// <summary>
    /// プレイヤーの会話送りのボタン入力を待機するメソッド(特別会話用)
    /// </summary>
    protected abstract IEnumerator OnActionSpecial();

    //N_Talking_Childに使ってもらうメソッド
    //インスペクターからメッセージ文を設定する
    protected void showMessage(string message)
    {
        //メインプレイヤーが会話エリアにそもそも接触していない(N_MainMoveを取得できていないNULL)場合は無条件で人間側にメッセージを入れる
        if (n_MainMove != null)
        {
            if (n_MainMove.IsMainPlayer)
            {
                this.talks.text = message;
            }
        }
        else
        {
            this.talks2.text = message;
        }
    }

}



