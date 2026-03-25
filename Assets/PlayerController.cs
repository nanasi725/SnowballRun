using UnityEngine;
using UnityEngine.SceneManagement; // シーン（画面）を切り替えるために必要

public class PlayerController : MonoBehaviour
{
    [Header("移動・ジャンプ設定")]
    public float moveSpeed = 5f;      // 前に進む速さ
    public float jumpForce = 12f;     // ジャンプする力
    public int maxJumps = 3;          // 最大何段ジャンプできるか

    [Header("成長設定")]
    public float growthRate = 0.05f;  // 進んだ距離に応じて大きくなる倍率
    public float maxSize = 3.0f;      // 大きさの上限

    [Header("死の判定")]
    public float deadY = -10f;        // この高さより下に落ちたらリトライ

    private Rigidbody2D rb;           // 物理演算のコンポーネント
    private int jumpCount = 0;        // 今何回ジャンプしたか
    private float startPosX;          // ゲーム開始時のX座標を記録

    // ゲーム開始時に一度だけ呼ばれる
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosX = transform.position.x;
    }

    // 1フレームごとに何度も呼ばれる（入力や移動の処理）
    void Update()
    {
        // 1. 常に右に走り続ける（Unity 6用の書き方：linearVelocity）
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        // 2. スペースキーでジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount < maxJumps)
            {
                Jump();
            }
        }

        // 3. 奈落に落ちたらリトライ
        if (transform.position.y < deadY)
        {
            RestartGame();
        }

        // 4. 雪玉を大きくする処理を呼び出す
        UpdateSize();
    }

    // ジャンプの具体的な処理
    void Jump()
    {
        // 上方向に力を加える
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpCount++;
    }

    // 距離に応じてサイズを変える処理
    void UpdateSize()
    {
        float distance = transform.position.x - startPosX;
        float newScale = 1f + (distance * growthRate);

        if (newScale < maxSize)
        {
            // XとYを同じ倍率で大きくする
            transform.localScale = new Vector3(newScale, newScale, 1f);
        }
    }

    // 何かにぶつかった時に呼ばれる
    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // 足元（上向きの面）にぶつかったら、ジャンプ回数をリセット
            if (contact.normal.y > 0.7f)
            {
                jumpCount = 0;
            }
            // 壁の横面にぶつかったら、リトライ
            else if (contact.normal.x < -0.7f)
            {
                RestartGame();
            }
        }
    }

    // ゲームを最初からやり直す
    void RestartGame()
    {
        // 現在のシーンの名前を取得して、もう一度読み込む
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}