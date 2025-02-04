using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// オブジェクトプールを管理する
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    #region 変数
    public ObjectPool<GameObject> _dropObjectPool;
    public ObjectPool<GameObject> _waterObjectPool;
    public ObjectPool<GameObject> _puddleObjectPool;
    public ObjectPool<GameObject> _pondObjectPool;
    public ObjectPool<GameObject> _lakeObjectPool;
    public ObjectPool<GameObject> _riverObjectPool;
    public ObjectPool<GameObject> _seaObjectPool;
    /// <summary>
    /// 生成したゲームオブジェクトを入れる
    /// </summary>
    private GameObject _returnGameObject;
    private GameObject _needWaterObject;
    #endregion
    void Awake()
    {
        _dropObjectPool = new ObjectPool<GameObject>(
            CreateDropPoolObject,
            GetActiveDropPoolObject,
            ReleaseDropPoolObject,
            DestroyDropPoolObject,
            false
            );
        _waterObjectPool = new ObjectPool<GameObject>(
            CreateWaterPoolObject,
            GetActiveWaterPoolObject,
            ReleaseWaterPoolObject,
            DestroyWaterPoolObject,
            false
            );
        _puddleObjectPool = new ObjectPool<GameObject>(
            CreatePuddlePoolObject,
            GetActivePuddlePoolObject,
            ReleasePuddlePoolObject,
            DestroyPuddlePoolObject,
            false
            );
        _pondObjectPool = new ObjectPool<GameObject>(
            CreatePondPoolObject,
            GetActivePondPoolObject,
            ReleasePondPoolObject,
            DestroyPondPoolObject,
            false
            );
        _lakeObjectPool = new ObjectPool<GameObject>(
            CreateLakePoolObject,
            GetActiveLakePoolObject,
            ReleaseLakePoolObject,
            DestroyLakePoolObject,
            false
            );
        _riverObjectPool = new ObjectPool<GameObject>(
            CreateRiverPoolObject,
            GetActiveRiverPoolObject,
            ReleaseRiverPoolObject,
            DestroyRiverPoolObject,
            false
            );
        _seaObjectPool = new ObjectPool<GameObject>(
            CreateSeaPoolObject,
            GetActiveSeaPoolObject,
            ReleaseSeaPoolObject,
            DestroySeaPoolObject,
            false
            );
    }
    /// <summary>
    /// しずくを生成する
    /// </summary>
    /// <returns>進化か落とすかで生成したオブジェクトを返す</returns>
    private GameObject CreateDropPoolObject()
    {
        Debug.Log("オブジェクトを取得(生成)が呼ばれた!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);

        return _returnGameObject;
        
    }/// <summary>
    /// 水を生成する
    /// </summary>
    /// <returns>進化か落とすかで生成したオブジェクトを返す</returns>
    private GameObject CreateWaterPoolObject()
    {
        //Debug.Log("オブジェクトを取得(生成)が呼ばれた!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// 水たまりを生成する
     /// </summary>
     /// <returns>進化か落とすかで生成したオブジェクトを返す</returns>
    private GameObject CreatePuddlePoolObject()
    {
        //Debug.Log("オブジェクトを取得(生成)が呼ばれた!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// 池を生成する
     /// </summary>
     /// <returns>進化か落とすかで生成したオブジェクトを返す</returns>
    private GameObject CreatePondPoolObject()
    {
        //Debug.Log("オブジェクトを取得(生成)が呼ばれた!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// 湖を生成する
     /// </summary>
     /// <returns>進化か落とすかで生成したオブジェクトを返す</returns>
    private GameObject CreateLakePoolObject()
    {
        //Debug.Log("オブジェクトを取得(生成)が呼ばれた!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// 川を生成する
     /// </summary>
     /// <returns>進化か落とすかで生成したオブジェクトを返す</returns>
    private GameObject CreateRiverPoolObject()
    {
        //Debug.Log("オブジェクトを取得(生成)が呼ばれた!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// 海を生成する
     /// </summary>
     /// <returns>進化か落とすかで生成したオブジェクトを返す</returns>
    private GameObject CreateSeaPoolObject()
    {
        //Debug.Log("オブジェクトを取得(生成)が呼ばれた!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }

    /// <summary>
    /// しずくを取得する
    /// </summary>
    /// <param name="getObj">取得したいオブジェクト</param>
    private void GetActiveDropPoolObject(GameObject getObj)
    {
        Debug.Log("オブジェクトを取得(有効化)が呼ばれた!");
        getObj.SetActive(true);
        
    }/// <summary>
     /// 水を取得する
     /// </summary>
     /// <param name="getObj">取得したいオブジェクト</param>
    private void GetActiveWaterPoolObject(GameObject getObj)
    {
        Debug.Log("オブジェクトを取得(有効化)が呼ばれた!");
        //Debug.Log("OnGetActivePoolObjectの" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// 水たまりを取得する
     /// </summary>
     /// <param name="getObj">取得したいオブジェクト</param>
    private void GetActivePuddlePoolObject(GameObject getObj)
    {
        Debug.Log("オブジェクトを取得(有効化)が呼ばれた!");
        //Debug.Log("OnGetActivePoolObjectの" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// 池を取得する
     /// </summary>
     /// <param name="getObj">取得したいオブジェクト</param>
    private void GetActivePondPoolObject(GameObject getObj)
    {
        Debug.Log("オブジェクトを取得(有効化)が呼ばれた!");
        //Debug.Log("OnGetActivePoolObjectの" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// 湖を取得する
     /// </summary>
     /// <param name="getObj">取得したいオブジェクト</param>
    private void GetActiveLakePoolObject(GameObject getObj)
    {
        Debug.Log("オブジェクトを取得(有効化)が呼ばれた!");
        //Debug.Log("OnGetActivePoolObjectの" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// 川を取得する
     /// </summary>
     /// <param name="getObj">取得したいオブジェクト</param>
    private void GetActiveRiverPoolObject(GameObject getObj)
    {
        Debug.Log("オブジェクトを取得(有効化)が呼ばれた!");
        //Debug.Log("OnGetActivePoolObjectの" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// 海を取得する
     /// </summary>
     /// <param name="getObj">取得したいオブジェクト</param>
    private void GetActiveSeaPoolObject(GameObject getObj)
    {
        Debug.Log("オブジェクトを取得(有効化)が呼ばれた!");
        //Debug.Log("OnGetActivePoolObjectの" + getObj);
        getObj.SetActive(true);
        
    }
    /// <summary>
    /// しずくを開放する
    /// </summary>
    /// <param name="ReleaseObj">解放したいオブジェクト</param>
    private void ReleaseDropPoolObject(GameObject ReleaseObj)
    {
        Debug.Log("オブジェクトの解放が呼ばれた");
        //オブジェクトを非表示にする
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// 水を開放する
     /// </summary>
     /// <param name="ReleaseObj">解放したいオブジェクト</param>
    private void ReleaseWaterPoolObject(GameObject ReleaseObj)
    {
        Debug.Log("オブジェクトの解放が呼ばれた");
        //オブジェクトを非表示にする
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// 水たまりを開放する
     /// </summary>
     /// <param name="ReleaseObj">解放したいオブジェクト</param>
    private void ReleasePuddlePoolObject(GameObject ReleaseObj)
    {
        Debug.Log("オブジェクトの解放が呼ばれた");
        //オブジェクトを非表示にする
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// 池を開放する
     /// </summary>
     /// <param name="ReleaseObj">解放したいオブジェクト</param>
    private void ReleasePondPoolObject(GameObject ReleaseObj)
    {
        //オブジェクトを非表示にする
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// 湖を開放する
     /// </summary>
     /// <param name="ReleaseObj">解放したいオブジェクト</param>
    private void ReleaseLakePoolObject(GameObject ReleaseObj)
    {
        //オブジェクトを非表示にする
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// 川を開放する
     /// </summary>
     /// <param name="ReleaseObj">解放したいオブジェクト</param>
    private void ReleaseRiverPoolObject(GameObject ReleaseObj)
    {
        //オブジェクトを非表示にする
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// 海を開放する
     /// </summary>
     /// <param name="ReleaseObj">解放したいオブジェクト</param>
    private void ReleaseSeaPoolObject(GameObject ReleaseObj)
    {
        //オブジェクトを非表示にする
        ReleaseObj.SetActive(false);

    }
    /// <summary>
    /// しずくを破棄する
    /// </summary>
    /// <param name="DestroyObj">消したいオブジェクト</param>
    private void DestroyDropPoolObject(GameObject DestroyObj)
    {
        //そのままDestory
        Destroy(DestroyObj);
    }/// <summary>
     /// 水を破棄する
     /// </summary>
     /// <param name="DestroyObj">消したいオブジェクト</param>
    private void DestroyWaterPoolObject(GameObject DestroyObj)
    {
        //そのままDestory
        Destroy(DestroyObj);
    }/// <summary>
     /// 水たまりを破棄する
     /// </summary>
     /// <param name="DestroyObj">消したいオブジェクト</param>
    private void DestroyPuddlePoolObject(GameObject DestroyObj)
    {
        //そのままDestory
        Destroy(DestroyObj);
    }/// <summary>
     /// 池を破棄する
     /// </summary>
     /// <param name="DestroyObj">消したいオブジェクト</param>
    private void DestroyPondPoolObject(GameObject DestroyObj)
    {
        //そのままDestory
        Destroy(DestroyObj);
    }/// <summary>
     /// 湖を破棄する
     /// </summary>
     /// <param name="DestroyObj">消したいオブジェクト</param>
    private void DestroyLakePoolObject(GameObject DestroyObj)
    {
        //そのままDestory
        Destroy(DestroyObj);
    }/// <summary>
     /// 川を破棄する
     /// </summary>
     /// <param name="DestroyObj">消したいオブジェクト</param>
    private void DestroyRiverPoolObject(GameObject DestroyObj)
    {
        //そのままDestory
        Destroy(DestroyObj);
    }
    /// <summary>
    /// 海を破棄する
    /// </summary>
    /// <param name="DestroyObj">消したいオブジェクト</param>
    private void DestroySeaPoolObject(GameObject DestroyObj)
    {
        //そのままDestory
        Destroy(DestroyObj);
    }
   /// <summary>
   /// 変数初期化メソッドを呼んだあと、オブジェクトの生成を仲介する
   /// </summary>
   /// <param name="needObject">生成してほしいゲームオブジェくト</param>
    public void MediatingObjectCreation(GameObject needObject)
    {
        InitializeVariablesObject();
        _needWaterObject = needObject;
        Debug.Log(_needWaterObject);
        //Debug.Log(_returnGameObject);
    }
    /// <summary>
    /// GameObject型の変数を初期化する
    /// </summary>
    public void InitializeVariablesObject()
    {
        _needWaterObject = null;
        _returnGameObject = null;
    }
        



    }

