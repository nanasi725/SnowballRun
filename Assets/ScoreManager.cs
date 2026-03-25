using UnityEngine;
using TMPro; // ★TextMeshProを使うための魔法

public class ScoreManager : MonoBehaviour
{
    public Transform player;       // 雪玉
    public TextMeshProUGUI scoreText; // 画面の文字
    
    private float startPosX;

    void Start()
    {
        // ゲーム開始時の雪玉の位置を覚える
        startPosX = player.position.x;
    }

    void Update()
    {
        if (player != null)
        {
            // 現在のX位置から開始地点を引いて、距離を計算
            float distance = player.position.x - startPosX;
            
            // 小数点を切り捨てて、画面の文字を更新
            // "F0" は小数点なし、" m" は単位です
            scoreText.text = distance.ToString("F0") + " m";
        }
    }
}