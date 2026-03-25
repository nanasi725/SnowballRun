using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;    // 雪玉（Circle）
    public Vector3 offset;      // 雪玉とカメラの距離（ずらし量）

    void Start()
    {
        // ゲーム開始時の「カメラと雪玉の距離」を自動で計算して記録
        // これにより、今の位置関係をキープして追いかけます
        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    // Updateの後に呼ばれるLateUpdateを使うのが、カメラ移動の鉄則です
    void LateUpdate()
    {
        if (player != null)
        {
            // X（横）も Y（縦）も、プレイヤーの位置にオフセットを足した場所に移動
            // これでプレイヤーが常に中心（または設定したズレ位置）に来ます
            transform.position = player.position + offset;
        }
    }
}