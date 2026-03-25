using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [Header("設定")]
    public GameObject[] stagePrefabs;    // 作ったプレハブを入れるリスト
    public Transform player;             // 雪玉（Player）
    public int initialSpawnCount = 5;    // 最初に作っておく数
    public float spawnThreshold = 20f;   // あとどれくらい近づいたら次を作るか

    private Vector3 nextSpawnPos = Vector3.zero; // 次に作る場所
    private List<GameObject> activeStages = new List<GameObject>(); // 今画面にあるステージ

    void Start()
    {
        nextSpawnPos = transform.position; // 最初はこのオブジェクトの位置からスタート
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnStage();
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 【改造ポイント】
            // 「次に作る場所」から「今指定した距離（spawnThreshold）」を引いた地点を
            // プレイヤーが追い越したら、新しいステージを作る
            if (player.position.x > nextSpawnPos.x - spawnThreshold)
            {
                SpawnStage();
                RemoveOldStage();
            }
        }
    }

    void SpawnStage()
    {
        // 登録したプレハブからランダムに1つ選ぶ
        int randomIndex = Random.Range(0, stagePrefabs.Length);
        GameObject selectedPrefab = stagePrefabs[randomIndex];

        // プレハブを生成
        GameObject newStage = Instantiate(selectedPrefab, nextSpawnPos, Quaternion.identity);
        activeStages.Add(newStage);

        // 次の出現場所を、今作ったステージの「EndAnchor」の位置に更新する
        Transform anchor = newStage.transform.Find("EndAnchor");
        if (anchor != null)
        {
            nextSpawnPos = anchor.position;
        }
        else
        {
            // もしEndAnchorを付け忘れていたら、とりあえず右に10進める（エラー防止）
            nextSpawnPos += new Vector3(10, 0, 0);
        }
    }

    void RemoveOldStage()
    {
        // 画面外（後ろ）に消えたステージを削除する処理
        if (activeStages.Count > initialSpawnCount + 2)
        {
            GameObject oldest = activeStages[0];
            activeStages.RemoveAt(0);
            Destroy(oldest);
        }
    }
}