using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 主に生成された時と相手のオブジェクトとぶつかったときに動く()
/// </summary>
public class WaterCollision : MonoBehaviour //ObjectPoolManager
{
    #region 変数一覧
    //[SerializeField]
    //private ObjectPoolManager _objectPoolManager;
    /// <summary>
    /// 定数データファイル(ScriptableObject)
    /// </summary>
    [SerializeField]private WaterObjectConstData _waterObjectConstData;
    /// <summary>
    /// オブジェクト各種類のデータファイル(ScriptableObject)
    /// </summary>
    [SerializeField] public WaterVariousObjectData _waterVariousObjectData;
    /// <summary>
    /// Awake時点でコンポーネントの取得を一回のみ行うためのフラグ
    /// </summary>
    private bool _isAwakeSeconce = false;
    /// <summary>
    /// 出現する水の番号(タイプ関係なし)
    /// </summary>
    private static int _waterSerialNumber = 0;
    /// <summary>
    /// 出現する水の番号(タイプ関係なし)
    /// </summary>
    [SerializeField]
    private int _mySerialNumber = 0;
    /// <summary>
    /// ぶつかった相手のゲームオブジェクト
    /// </summary>
    private GameObject _collisionGameObject;
    /// <summary>
    /// ぶつかった相手のゲームオブジェクト
    /// </summary>
    private GameObject _nextGameObject;

    /// <summary>
    /// 二つの水がぶつかった接触部分の中心の位置
    /// </summary>
    private Vector3 _myselfAndOtherPersonsCenter = default;
    /// <summary>
    /// 二つの水の間を滑らかに移動させるための補間
    /// </summary>
    private Quaternion _myselfAndOtherPersonsRotation = default;
    /// <summary>
    /// 進化先(生成するオブジェクト)の速度の入れ物
    /// </summary>
    private Vector3 _nextGameObjectVelocity = default;
    /// <summary>
    /// ぶつかった相手のWaterCollision
    /// </summary>
    private WaterCollision _opponentsWaterCollision;
    /// <summary>
    /// 進化先のゲームオブジェクトのRigidbody2D
    /// </summary>
    private Rigidbody2D _nextRb2d;
    /// <summary>
    /// 自身のRigidbody2D
    /// </summary>
    private Rigidbody2D _thisRb2d;
    /// <summary>
    /// ぶつかった相手のRigidbody2D
    /// </summary>
    private Rigidbody2D _collidedRb2d;
    /// <summary>
    /// angularVelocityの値を変更するときに使う
    /// </summary>
    private float _myselfAndOtherPersonsAngularVelocity = default;
    /// <summary>
    /// スコア加算処理を行うためのイベント(シーンをまたいでスコアを保持する)
    /// </summary>
    //自分用のメモ
    //関数を参照型として扱い、沢山保持して一斉に実行できる参照型
    public static Action<int> _onScoreAdded;
    #endregion 
    private void Awake()
    {
        //水の通し番号をコピー
        _mySerialNumber = _waterSerialNumber;

        //コピー後に番号を増やして、次に来る水と番号が被らないようにする
        _waterSerialNumber++;
        if (_isAwakeSeconce)
        {
            return;
        }
        else
        {
            _thisRb2d = gameObject.GetComponent<Rigidbody2D>();
            _isAwakeSeconce = true;
        }

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("自分の進化系は(OnCollision2d[0])" + _nextWaterPrefab);
        //ぶつかった相手にWaterCollisionがついてない&相手の水タイプが自分と同じでなければ
        if (!collision.gameObject.TryGetComponent(out _opponentsWaterCollision))
        {
            return;
        }
        if (_opponentsWaterCollision._waterVariousObjectData.MyWaterType != this._waterVariousObjectData.MyWaterType)
        {
            _opponentsWaterCollision = null;
            return;
        }
        //進化と進化のための処理が走る原理
        //今のところぶつかった相手との番号を比較して、
        //番号が小さい(古い)方のオブジェクトだけが以下の処理を走らせることが出来る
        if (_waterVariousObjectData.GetSetNextEvolvingObject != null && _mySerialNumber < _opponentsWaterCollision._mySerialNumber)
        {
            _collisionGameObject = collision.gameObject;
            GetOthersRigidbody2D();
            GetColisionCenterPositionAndLinearInterpolation();
            //スコアを加算
            _onScoreAdded?.Invoke(_waterVariousObjectData.GetSetScore);
            //自分と相手を消して進化先のオブジェクトを生成する
            Destroy(this.gameObject);
            Destroy(_collisionGameObject);
            _nextGameObject = Instantiate(_waterVariousObjectData.GetSetNextEvolvingObject);

            #region 没オブジェクトプール
            ////オブジェクトごとにオブジェクトプールが異なるので、処理を分ける
            //switch (_waterVariousObjectData.MyWaterType)
            //{
            //    case WaterVariousObjectData._waterType.Drop:
            //        //_dropObjectPool.Release(gameObject); //自身と
            //        //_dropObjectPool.Release(_collisionGameObject); //相手を消す
            //        //Debug.Log(_dropObjectPool);
            //        //MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        //_nextGameObject = _dropObjectPool.Get();//進化先のオブジェクトを生成
            //        break;
            //    case WaterVariousObjectData._waterType.Water:
            //        _waterObjectPool.Release(gameObject); //自身と
            //        _waterObjectPool.Release(_collisionGameObject); //相手を消す                                                                       
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _waterObjectPool.Get();//進化先のオブジェクトを生成
            //        break;

            //    case WaterVariousObjectData._waterType.Puddle:
            //        _puddleObjectPool.Release(gameObject); //自身と
            //        _puddleObjectPool.Release(_collisionGameObject); //相手を消す                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _puddleObjectPool.Get();//進化先のオブジェクトを生成
            //        break;
            //    case WaterVariousObjectData._waterType.Pond:
            //        _pondObjectPool.Release(gameObject); //自身と
            //        _pondObjectPool.Release(_collisionGameObject); //相手を消す                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _pondObjectPool.Get();//進化先のオブジェクトを生成
            //        break;
            //    case WaterVariousObjectData._waterType.Lake:
            //        _lakeObjectPool.Release(gameObject); //自身と
            //        _lakeObjectPool.Release(_collisionGameObject); //相手を消す                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _lakeObjectPool.Get();//進化先のオブジェクトを生成

            //        break;
            //    case WaterVariousObjectData._waterType.River:
            //        _riverObjectPool.Release(gameObject); //自身と
            //        _riverObjectPool.Release(_collisionGameObject); //相手を消す                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _riverObjectPool.Get();//進化先のオブジェクトを生成

            //        break;
            //    case WaterVariousObjectData._waterType.Sea:
            //        _seaObjectPool.Release(gameObject); //自身と
            //        _seaObjectPool.Release(_collisionGameObject); //相手を消す                                                                          
            //        MediatingObjectCreation(_waterVariousObjectData.GetSetNextEvolvingObject);
            //        _nextGameObject = _seaObjectPool.Get();//進化先のオブジェクトを生成
            //        break;
            //}
            #endregion

            //オブジェクト生成時にスコア加算
            //_onScoreAdded.Invoke(WaterVariousObjectData._score);
            ChangeNextObjectPositionAndRotation();
            _nextRb2d = _nextGameObject.GetComponent<Rigidbody2D>();
            TakeVelocityAndAngularvelocity();
            GiveVelocityAndAngularVelocity();
            InitializeVariables();
            //Debug.Log("次のオブジェクトは" + _nextGameObject.gameObject.name);
        }
    }
    /// <summary>
     /// ぶつかった相手のRigidbody2Dを取得する
     /// </summary>
    private void GetOthersRigidbody2D()
    {
        _collidedRb2d = _opponentsWaterCollision._thisRb2d;
    }
    /// <summary>
    /// 生成したオブジェクトの位置と回転を変更
    /// </summary>
    private void ChangeNextObjectPositionAndRotation()
    {
        //生成したオブジェクトの位置と回転を同時に変更する
        _nextGameObject.transform.SetLocalPositionAndRotation(this._myselfAndOtherPersonsCenter, this._myselfAndOtherPersonsRotation);
    }
    /// <summary>
    /// ぶつかった相手と自分の速度と角速度を取得して足して半分に割る
    /// </summary>
    private void TakeVelocityAndAngularvelocity()
    {
        //ぶつかった相手と自分の速度と角速度を足して割る
        _nextGameObjectVelocity = (_thisRb2d.velocity + _collidedRb2d.velocity) / _waterObjectConstData.GetDivideIntoTwo;
        _myselfAndOtherPersonsAngularVelocity = (_thisRb2d.angularVelocity + _collidedRb2d.angularVelocity) / _waterObjectConstData.GetDivideIntoTwo;
    }
    /// <summary>
    /// 新しく生成したオブジェクトに速度と角速度を与える
    /// </summary>
    private void GiveVelocityAndAngularVelocity()
    {
        //ぶつかった相手と自分の速度と角速度の平均を新しく生成したオブジェクトに与える
        _nextRb2d.velocity = _nextGameObjectVelocity;
        _nextRb2d.angularVelocity = _myselfAndOtherPersonsAngularVelocity;
    }
    /// <summary>
    /// 二つのオブジェクトの接触部分の中心の位置と線形補間を取得する
    /// </summary>
    private void GetColisionCenterPositionAndLinearInterpolation()
    {
        //二つの水がぶつかった接触部分の中心の位置
        _myselfAndOtherPersonsCenter = (transform.position + _opponentsWaterCollision.transform.position) / _waterObjectConstData.GetDivideIntoTwo;
        #region Quaternion.Lerpとは

        /*線形補間をしてくれる。引数は三つ
         * 
         * 「線形補間」
         * 
         * 離れた場所に二点があった場合、その間を直線であることを想定して近似的に補う方法
         * 
         * Lerp(始まりの位置(Vector3), 終わりの位置(Vector3), 現在の位置(float))
         */

        #endregion
        //二つの水の間を滑らかに移動させるための補間
        _myselfAndOtherPersonsRotation = Quaternion.Lerp(transform.rotation, _opponentsWaterCollision.transform.rotation, _waterObjectConstData.GetMyCurrentPosition);
    }
    /// <summary>
    /// 変数を初期化する
    /// </summary>
    private void InitializeVariables()
    {
        _collisionGameObject = null;
        _opponentsWaterCollision = null;
        _collidedRb2d = null;
        _nextGameObjectVelocity = default;
        _myselfAndOtherPersonsAngularVelocity = default;
        _myselfAndOtherPersonsCenter = default;
        _myselfAndOtherPersonsRotation = default;
        _mySerialNumber = 0;
    }

}




