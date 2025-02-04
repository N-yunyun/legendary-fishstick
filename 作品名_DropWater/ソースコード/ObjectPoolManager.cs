using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// �I�u�W�F�N�g�v�[�����Ǘ�����
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    #region �ϐ�
    public ObjectPool<GameObject> _dropObjectPool;
    public ObjectPool<GameObject> _waterObjectPool;
    public ObjectPool<GameObject> _puddleObjectPool;
    public ObjectPool<GameObject> _pondObjectPool;
    public ObjectPool<GameObject> _lakeObjectPool;
    public ObjectPool<GameObject> _riverObjectPool;
    public ObjectPool<GameObject> _seaObjectPool;
    /// <summary>
    /// ���������Q�[���I�u�W�F�N�g������
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
    /// �������𐶐�����
    /// </summary>
    /// <returns>�i�������Ƃ����Ő��������I�u�W�F�N�g��Ԃ�</returns>
    private GameObject CreateDropPoolObject()
    {
        Debug.Log("�I�u�W�F�N�g���擾(����)���Ă΂ꂽ!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);

        return _returnGameObject;
        
    }/// <summary>
    /// ���𐶐�����
    /// </summary>
    /// <returns>�i�������Ƃ����Ő��������I�u�W�F�N�g��Ԃ�</returns>
    private GameObject CreateWaterPoolObject()
    {
        //Debug.Log("�I�u�W�F�N�g���擾(����)���Ă΂ꂽ!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// �����܂�𐶐�����
     /// </summary>
     /// <returns>�i�������Ƃ����Ő��������I�u�W�F�N�g��Ԃ�</returns>
    private GameObject CreatePuddlePoolObject()
    {
        //Debug.Log("�I�u�W�F�N�g���擾(����)���Ă΂ꂽ!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// �r�𐶐�����
     /// </summary>
     /// <returns>�i�������Ƃ����Ő��������I�u�W�F�N�g��Ԃ�</returns>
    private GameObject CreatePondPoolObject()
    {
        //Debug.Log("�I�u�W�F�N�g���擾(����)���Ă΂ꂽ!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// �΂𐶐�����
     /// </summary>
     /// <returns>�i�������Ƃ����Ő��������I�u�W�F�N�g��Ԃ�</returns>
    private GameObject CreateLakePoolObject()
    {
        //Debug.Log("�I�u�W�F�N�g���擾(����)���Ă΂ꂽ!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// ��𐶐�����
     /// </summary>
     /// <returns>�i�������Ƃ����Ő��������I�u�W�F�N�g��Ԃ�</returns>
    private GameObject CreateRiverPoolObject()
    {
        //Debug.Log("�I�u�W�F�N�g���擾(����)���Ă΂ꂽ!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }/// <summary>
     /// �C�𐶐�����
     /// </summary>
     /// <returns>�i�������Ƃ����Ő��������I�u�W�F�N�g��Ԃ�</returns>
    private GameObject CreateSeaPoolObject()
    {
        //Debug.Log("�I�u�W�F�N�g���擾(����)���Ă΂ꂽ!");
        _returnGameObject = Instantiate(_needWaterObject);
        Debug.Log(_needWaterObject);
        Debug.Log(_returnGameObject);
        return _returnGameObject;

    }

    /// <summary>
    /// ���������擾����
    /// </summary>
    /// <param name="getObj">�擾�������I�u�W�F�N�g</param>
    private void GetActiveDropPoolObject(GameObject getObj)
    {
        Debug.Log("�I�u�W�F�N�g���擾(�L����)���Ă΂ꂽ!");
        getObj.SetActive(true);
        
    }/// <summary>
     /// �����擾����
     /// </summary>
     /// <param name="getObj">�擾�������I�u�W�F�N�g</param>
    private void GetActiveWaterPoolObject(GameObject getObj)
    {
        Debug.Log("�I�u�W�F�N�g���擾(�L����)���Ă΂ꂽ!");
        //Debug.Log("OnGetActivePoolObject��" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// �����܂���擾����
     /// </summary>
     /// <param name="getObj">�擾�������I�u�W�F�N�g</param>
    private void GetActivePuddlePoolObject(GameObject getObj)
    {
        Debug.Log("�I�u�W�F�N�g���擾(�L����)���Ă΂ꂽ!");
        //Debug.Log("OnGetActivePoolObject��" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// �r���擾����
     /// </summary>
     /// <param name="getObj">�擾�������I�u�W�F�N�g</param>
    private void GetActivePondPoolObject(GameObject getObj)
    {
        Debug.Log("�I�u�W�F�N�g���擾(�L����)���Ă΂ꂽ!");
        //Debug.Log("OnGetActivePoolObject��" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// �΂��擾����
     /// </summary>
     /// <param name="getObj">�擾�������I�u�W�F�N�g</param>
    private void GetActiveLakePoolObject(GameObject getObj)
    {
        Debug.Log("�I�u�W�F�N�g���擾(�L����)���Ă΂ꂽ!");
        //Debug.Log("OnGetActivePoolObject��" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// ����擾����
     /// </summary>
     /// <param name="getObj">�擾�������I�u�W�F�N�g</param>
    private void GetActiveRiverPoolObject(GameObject getObj)
    {
        Debug.Log("�I�u�W�F�N�g���擾(�L����)���Ă΂ꂽ!");
        //Debug.Log("OnGetActivePoolObject��" + getObj);
        getObj.SetActive(true);
        
    }/// <summary>
     /// �C���擾����
     /// </summary>
     /// <param name="getObj">�擾�������I�u�W�F�N�g</param>
    private void GetActiveSeaPoolObject(GameObject getObj)
    {
        Debug.Log("�I�u�W�F�N�g���擾(�L����)���Ă΂ꂽ!");
        //Debug.Log("OnGetActivePoolObject��" + getObj);
        getObj.SetActive(true);
        
    }
    /// <summary>
    /// ���������J������
    /// </summary>
    /// <param name="ReleaseObj">����������I�u�W�F�N�g</param>
    private void ReleaseDropPoolObject(GameObject ReleaseObj)
    {
        Debug.Log("�I�u�W�F�N�g�̉�����Ă΂ꂽ");
        //�I�u�W�F�N�g���\���ɂ���
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// �����J������
     /// </summary>
     /// <param name="ReleaseObj">����������I�u�W�F�N�g</param>
    private void ReleaseWaterPoolObject(GameObject ReleaseObj)
    {
        Debug.Log("�I�u�W�F�N�g�̉�����Ă΂ꂽ");
        //�I�u�W�F�N�g���\���ɂ���
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// �����܂���J������
     /// </summary>
     /// <param name="ReleaseObj">����������I�u�W�F�N�g</param>
    private void ReleasePuddlePoolObject(GameObject ReleaseObj)
    {
        Debug.Log("�I�u�W�F�N�g�̉�����Ă΂ꂽ");
        //�I�u�W�F�N�g���\���ɂ���
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// �r���J������
     /// </summary>
     /// <param name="ReleaseObj">����������I�u�W�F�N�g</param>
    private void ReleasePondPoolObject(GameObject ReleaseObj)
    {
        //�I�u�W�F�N�g���\���ɂ���
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// �΂��J������
     /// </summary>
     /// <param name="ReleaseObj">����������I�u�W�F�N�g</param>
    private void ReleaseLakePoolObject(GameObject ReleaseObj)
    {
        //�I�u�W�F�N�g���\���ɂ���
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// ����J������
     /// </summary>
     /// <param name="ReleaseObj">����������I�u�W�F�N�g</param>
    private void ReleaseRiverPoolObject(GameObject ReleaseObj)
    {
        //�I�u�W�F�N�g���\���ɂ���
        ReleaseObj.SetActive(false);

    }/// <summary>
     /// �C���J������
     /// </summary>
     /// <param name="ReleaseObj">����������I�u�W�F�N�g</param>
    private void ReleaseSeaPoolObject(GameObject ReleaseObj)
    {
        //�I�u�W�F�N�g���\���ɂ���
        ReleaseObj.SetActive(false);

    }
    /// <summary>
    /// ��������j������
    /// </summary>
    /// <param name="DestroyObj">���������I�u�W�F�N�g</param>
    private void DestroyDropPoolObject(GameObject DestroyObj)
    {
        //���̂܂�Destory
        Destroy(DestroyObj);
    }/// <summary>
     /// ����j������
     /// </summary>
     /// <param name="DestroyObj">���������I�u�W�F�N�g</param>
    private void DestroyWaterPoolObject(GameObject DestroyObj)
    {
        //���̂܂�Destory
        Destroy(DestroyObj);
    }/// <summary>
     /// �����܂��j������
     /// </summary>
     /// <param name="DestroyObj">���������I�u�W�F�N�g</param>
    private void DestroyPuddlePoolObject(GameObject DestroyObj)
    {
        //���̂܂�Destory
        Destroy(DestroyObj);
    }/// <summary>
     /// �r��j������
     /// </summary>
     /// <param name="DestroyObj">���������I�u�W�F�N�g</param>
    private void DestroyPondPoolObject(GameObject DestroyObj)
    {
        //���̂܂�Destory
        Destroy(DestroyObj);
    }/// <summary>
     /// �΂�j������
     /// </summary>
     /// <param name="DestroyObj">���������I�u�W�F�N�g</param>
    private void DestroyLakePoolObject(GameObject DestroyObj)
    {
        //���̂܂�Destory
        Destroy(DestroyObj);
    }/// <summary>
     /// ���j������
     /// </summary>
     /// <param name="DestroyObj">���������I�u�W�F�N�g</param>
    private void DestroyRiverPoolObject(GameObject DestroyObj)
    {
        //���̂܂�Destory
        Destroy(DestroyObj);
    }
    /// <summary>
    /// �C��j������
    /// </summary>
    /// <param name="DestroyObj">���������I�u�W�F�N�g</param>
    private void DestroySeaPoolObject(GameObject DestroyObj)
    {
        //���̂܂�Destory
        Destroy(DestroyObj);
    }
   /// <summary>
   /// �ϐ����������\�b�h���Ă񂾂��ƁA�I�u�W�F�N�g�̐����𒇉��
   /// </summary>
   /// <param name="needObject">�������Ăق����Q�[���I�u�W�F���g</param>
    public void MediatingObjectCreation(GameObject needObject)
    {
        InitializeVariablesObject();
        _needWaterObject = needObject;
        Debug.Log(_needWaterObject);
        //Debug.Log(_returnGameObject);
    }
    /// <summary>
    /// GameObject�^�̕ϐ�������������
    /// </summary>
    public void InitializeVariablesObject()
    {
        _needWaterObject = null;
        _returnGameObject = null;
    }
        



    }

