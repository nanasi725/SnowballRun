using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private bool isInitialized = false; // 準備ができたかどうかの旗

    void Start()
    {
        TryInit();
    }

    void LateUpdate()
    {
        // 1. プレイヤーがいない、もしくは「行方不明」なら、探しに行く
        if (player == null)
        {
            isInitialized = false;
            TryInit();
            return; // 見つかるまで、このあとの処理（座標計算）は絶対にやらない！
        }

        // 2. 準備ができていれば、追いかける
        if (isInitialized)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.125f);
        }
    }

    // プレイヤーを探して、初期設定をする関数
    void TryInit()
    {
        // ヒエラルキーの中から「Player」タグがついたオブジェクトを探す
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");

        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
            offset = transform.position - player.position;
            isInitialized = true; // 準備完了！
        }
    }
}