using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create State Data")]
public class StateManager : ScriptableObject
{

    [Header("�v���C���[�̎��")]
    [SerializeField]
    public GameObject PlayerPrefab;

    /// <summary>
    /// �����Őݒ肵�����C���[�ɂ������C��������
    /// </summary>
    [Header("�����Őݒ肵�����C���[�ɂ������C��������")]
    [SerializeField]
    public LayerMask groundLayer;

    /// <summary>
    /// �W�����v��
    /// </summary>
    [Header("�W�����v��")]
    [SerializeField]
    public float jumpForce = 5000f;

    /// <summary>
    /// �����X�s�[�h
    /// </summary>
    [Header("�����X�s�[�h")]
    [SerializeField]
    public float moveForce = 6000f;

    /// <summary>
    ///�@���~����X�s�[�h
    /// </summary>
    [Header("���~����X�s�[�h")]
    [SerializeField]
    public float downForce = 4000f;

    /// <summary>
    ///�@�W�����v�̍��������l
    /// </summary>
    [Header("�W�����v�̍��������l")]
    [SerializeField]
    public int jumpEndNum = 5;

    /// <summary>
    /// HP�ŏ��l
    /// </summary>
    [Header("HP�ŏ��l")]
    [SerializeField]
    public int MinHp = 0;

    /// <summary>
    /// HP�ő�l
    /// </summary>
    [Header("HP�ő�l")]
    [SerializeField]
    public int MaxHp = 10;

    /// <summary>
    /// Ray�̑�3����:����
    /// </summary>
    [Header("Ray�̑�3����:����")]
    [SerializeField]
    public float rayLong = 1f;

}






