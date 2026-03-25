using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Start()
    {
        // ★ガード：もしplayerが設定されていなかったら何もしない（エラーを防ぐ）
        if (player == null) 
        {
            Debug.LogWarning("カメラの追いかける相手（Player）が設定されていません！");
            return; 
        }

        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // ★ガード：playerがいない場合は処理をスキップする
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.125f);
    }
}