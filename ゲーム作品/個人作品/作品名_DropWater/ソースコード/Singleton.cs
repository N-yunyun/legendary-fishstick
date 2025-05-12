using UnityEngine;

/// <summary>
/// クラスのインスタンスがシーン中に一つだけ存在することを保証する
/// </summary>
/// <typeparam name="T">シングルトンとして管理するクラスの型（MonoBehaviourを継承）</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    public static T Instance { get; private set; }

    /// <summary>
    /// シングルトンのnullチェック
    /// </summary>
    protected virtual void Awake()
    {
        // シングルトンのnullチェック
        // すでにインスタンスが存在する場合、自身を破棄する
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"[Singleton] Another instance of {GetType()} already exists. Destroying this one.");
            Destroy(this);
            return;
        }

        // このインスタンスを唯一のインスタンスとして保存
        Instance = this as T;
    }
}