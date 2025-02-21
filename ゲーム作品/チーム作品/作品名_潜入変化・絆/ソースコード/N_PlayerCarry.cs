using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class N_PlayerCarry : MonoBehaviour
{/// <summary>
 /// 変化したイヌを掴んでいるか
 /// </summary>
    private bool _isCatch = false;
    /// <summary>
    /// つかみたいオブジェクト
    /// </summary>
    private GameObject _catchObject;
    private BoxCollider2D _objectCollider = default;
    [SerializeField] private N_SubMove n_SubMove;
    /// <summary>
    /// 変化した犬と接触しているかフラグ(外部通知用)
    /// </summary>
    private bool _isContact;
    /// <summary>
    /// 変化した犬と接触しているかのフラグ
    /// </summary>
    /// <returns></returns>
    public bool GetIsContact()
    {
        return _isContact;
    }

    public bool GetIsCatch()
    {
        return _isCatch;
    }
    private FindObj_T _findObj;
    private int _putOffset = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Carryable" && !_isCatch)
        {
            //Carryableタグのついたプレイヤーに触れると、そのプレイヤーを運ぶモードに移行する
            _catchObject = collision.gameObject;
            _isContact = true;
            Debug.Log("接触した");
        }
        //ぶつかったらぶつかった用のタグでN_SubMoveに知らせる
    }
    /// <summary>
    /// 犬をつかむ処理
    /// </summary>
    public void CatchMainPlayer()
    {
        //掴みフラグオン
        _isCatch = true;
        _objectCollider = _catchObject.GetComponent<BoxCollider2D>();
        _findObj = _catchObject.GetComponent<FindObj_T>();
        
        //変身解除を不能にする
        _findObj.enabled = false;
        //「一時的に変化したイヌ」を子オブジェクトにして運べるようにする
        _catchObject.transform.parent = this.gameObject.transform;
        
        //プレイヤーのTriggerを元に戻す
        _objectCollider.isTrigger = true;
        Debug.Log("プレイヤーを掴んだ");
    }
    /// <summary>
    /// 犬を離す処理
    /// </summary>
    public void MainPlayerThrowAway()
    {
        //親子関係を解除して運搬をやめる
        _catchObject.transform.parent = null;

        if (n_SubMove.GetIsRight())
        {
            //変化した犬を少し離れた場所におろす
            _catchObject.transform.position = new Vector3(this.gameObject.transform.position.x - _putOffset,
            this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else
        {
            //変化した犬を少し離れた場所におろす
            _catchObject.transform.position = new Vector3(this.gameObject.transform.position.x + _putOffset,
            this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }

        //変身解除を可能にする
        _findObj.enabled = true;

        //プレイヤーのTriggerを元に戻す
        _objectCollider.isTrigger = false;

        //接触していないことにする
        _isContact = false;
        //つかんでいないことにする
        _isCatch = false;
    }

}

