using UnityEngine;

/// <summary>
/// �N���X�̃C���X�^���X���V�[�����Ɉ�������݂��邱�Ƃ�ۏ؂���
/// </summary>
/// <typeparam name="T">�V���O���g���Ƃ��ĊǗ�����N���X�̌^�iMonoBehaviour���p���j</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// �V���O���g���C���X�^���X
    /// </summary>
    public static T Instance { get; private set; }

    /// <summary>
    /// �V���O���g����null�`�F�b�N
    /// </summary>
    protected virtual void Awake()
    {
        // �V���O���g����null�`�F�b�N
        // ���łɃC���X�^���X�����݂���ꍇ�A���g��j������
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"[Singleton] Another instance of {GetType()} already exists. Destroying this one.");
            Destroy(this);
            return;
        }

        // ���̃C���X�^���X��B��̃C���X�^���X�Ƃ��ĕۑ�
        Instance = this as T;
    }
}