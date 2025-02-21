using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Playerの動作
/// </summary>
public class Move : MonoBehaviour
{
    [Header("rigidbodyとanimatorがアタッチされたオブジェクトを入れておく")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator anime;

    /// <summary>
    /// キャラデータ
    /// </summary>
    [SerializeField]
    [Header("ここにScriptableObjectファイルを入れる")]
    private StateManager stateManager;

    /// <summary>
    /// 体力を管理するスクリプト
    /// </summary>
    [SerializeField]
    [Header("ここにHealthManagerを入れる")]
    private HealthManager healthMng;

    /// <summary>
    /// ステータス(swich文とセットで使う)
    /// </summary>
    private enum JumpState
    {
        /// <summary>
        /// ジャンプ中
        /// </summary>
        Wait,

        /// <summary>
        /// ジャンプ待ち
        /// </summary>
        Jump,

        /// <summary>
        /// ジャンプ終わり
        /// </summary>
        JumpEnd,

        /// <summary>
        /// ジャンプボタン押されてない状態(通常モード)
        /// </summary>
        DontJump,

        //これ作る
        /// <summary>
        /// ダウン(下降)
        /// </summary>
        Down

    }

    /// <summary>
    /// JumpStateの状態を
    /// 入れるための変数(状態を見るときはこれを使う)
    /// </summary>
    [SerializeField] JumpState jumpState = JumpState.DontJump;

    /// <summary>
    ///飛んだ距離を計測する
    /// </summary>
    [SerializeField] private int jumpNum = 0;



    /// <summary>
    /// 移動キーが押されているか
    /// </summary>
    private bool isMove = false;

    /// <summary>
    /// ←キーが押されているか
    /// </summary>
    [SerializeField]
    private bool isLeft = false;

    /// <summary>
    /// ↓キーが押されているか
    /// </summary>
    [SerializeField]
    private bool isDown = false;

    /// <summary>
    /// ↑キーが押されているか
    /// </summary>
    [SerializeField]
    private bool isJump = false;

    /// <summary>
    /// 接水判定
    /// </summary>
    private bool isWater = false;

    void Start()
    {
        //通常状態のアニメを再生
        anime.Play("stand");

    }

    // Update is called once per frame
    void Update()
    {
        #region 個人的メモ

        //ちなみにレイキャストヒット無印は3Dだから間違えないように気を付ける
        //レイヤーマスクを指定すると、レイはそこで指定したレイヤーのものにしか当たらなくなる
        //そのオブジェクトの中心から、downは下に、upは上に、rightは右に、leftは左に向かってレイを飛ばす。

        #endregion

        #region Raycastの処理

        //ヒット変数に代入(引数1:レイの始点、引数2:レイの向き、引数3:レイの距離、引数4:レイヤーマスク)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, stateManager.rayLong, stateManager.groundLayer);

        //レイを可視化(引数1:レイの始点、引数2:レイの向きと長さ(掛け算で書く)、引数3:レイの色)
        Debug.DrawRay(transform.position, Vector2.up * stateManager.rayLong, Color.red);

        #endregion

        isJump = Input.GetKey(KeyCode.UpArrow);


        if (hit)//レイキャストが何かに当たった場合する処理
        {
            isWater = true;

            //当たった相手のコライダーの名前を表示
            //Debug.Log(hit.collider.name);

        }
        else
        {
            isWater = false;
            jumpState = JumpState.JumpEnd;
        }

        switch (jumpState)
        {

            //主に水中にいるとき
            case JumpState.DontJump:

                MoveOrJumpReady();

                break;

            case JumpState.Wait:

                //初期化
                jumpNum = 0;

                //ジャンプに移行
                jumpState = JumpState.Jump;

                break;

            case JumpState.Jump:

                if (!isJump)
                {
                    //通常に移行
                    jumpState = JumpState.DontJump;

                }

                //現在の数値が終わりの数値を超えたら
                else if (jumpNum >= stateManager.jumpEndNum)
                {
                    //ジャンプ終了に移行
                    jumpState = JumpState.JumpEnd;
                }

                break;

            case JumpState.JumpEnd:

                anime.SetBool("isJump", false);
                //地面にいたら
                if (isWater)
                {
                    jumpState = JumpState.DontJump;//通常状態に移行
                }

                break;

        }

        #region 接水確認のデバッグ用処理

        //isWaterの値を取得してリアルタイムで文字を値の中身に変える
        //GameObject.Find("GroundCheck").GetComponent<Text>().text = isWater.ToString();
        //テキスト変数は最初にusing UnityEngine.UIを書かないと使えない
        //.text = 変数名.ToString()で変数の内容をストリング型に変換してUIに表示させる
        #endregion

    }

    private void FixedUpdate()
    {
        switch (jumpState)
        {

            //主に水中にいるとき
            case JumpState.DontJump:



                if (isMove)
                {
                    //左押されてたら
                    if (isLeft)
                    {
                        //プレイヤーを左向きにする
                        transform.localScale = new Vector3(-1, 1, 1);

                        //左に力を加える
                        rb2d.AddForce(new Vector2(-stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));

                        IsDown();

                    }

                    //右押されてたら
                    else
                    {
                        //プレイヤーを右向きにする
                        transform.localScale = new Vector3(1, 1, 1);

                        //右に力を加える
                        rb2d.AddForce(new Vector2(stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));

                        IsDown();
                    }

                }

                IsDown();

                break;

            case JumpState.Wait:


                break;

            case JumpState.Jump:

                //左向いてるか右向いてるかで力を加える方向を変える
                if (isLeft)
                {
                    //ジャンプ処理
                    rb2d.AddForce(new Vector2(rb2d.velocity.x, stateManager.jumpForce * Time.deltaTime));

                    if (isMove)
                    {
                        //左に力を加える
                        rb2d.AddForce(new Vector2(-stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));

                    }
                }
                else
                {

                    rb2d.AddForce(new Vector2(rb2d.velocity.x, stateManager.jumpForce * Time.deltaTime));

                    if (isMove)
                    {
                        //右に力を加える
                        rb2d.AddForce(new Vector2(stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));
                    }
                }


                ////左押されてたら
                //if (isLeft)
                //{
                //    //プレイヤーを左向きにする
                //    transform.localScale = new Vector3(-1, 1, 1);



                //    IsDown();



                //    //右押されてたら
                //    else
                //    {
                //        //プレイヤーを右向きにする
                //        transform.localScale = new Vector3(1, 1, 1);

                //        //右に力を加える
                //        rb2d.AddForce(new Vector2(stateManager.moveForce * Time.deltaTime, rb2d.velocity.y));

                //        IsDown();
                //    }

                //}

                if (!isWater)
                {
                    //ジャンプの高さ制限用カウントON
                    jumpNum++;
                }



                break;

            case JumpState.JumpEnd:

                //地面にいたら
                if (isWater)
                {
                    jumpState = JumpState.DontJump;//通常状態に移行
                }

                break;

        }

    }

    /// <summary>
    /// 下ボタンが押されていれば下降する
    /// </summary>
    private void IsDown()
    {
        //下ボタンが押されてなければ処理負担軽減用にreturn
        if (!isDown)
        {
            return;
        }
        else
        {
            rb2d.AddForce(new Vector2(rb2d.velocity.x, -stateManager.downForce * Time.deltaTime));
        }
    }

    /// <summary>
    /// 横に移動できるかジャンプできるか判定
    /// </summary>
    private void MoveOrJumpReady()
    {

        //→が押されている間は
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isLeft = true;
            isMove = true;
        }

        //←が押されている間は
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            isLeft = false;
            isMove = true;
        }


        //↑キーが押されている間は
        else if (isJump)
        {
            isDown = false;
            isJump = true;

            jumpState = JumpState.Wait;
        }

        //↓キーが押されている間は
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            isJump = false;
            isDown = true;
            


        }
        //移動キーが押されてなければ
        else
        {
            isMove = false;
            isDown = false;

        }

    }

}



