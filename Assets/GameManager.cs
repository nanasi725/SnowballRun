using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject resultPanel;   // リザルトパネル
    public TextMeshProUGUI resultText; // リザルト用の文字
    public ScoreManager scoreManager; // 距離を測っているスクリプト

    // ゲームオーバー時に呼ばれる魔法
    public void GameOver()
    {
        // 1. リザルトパネルを表示する
        resultPanel.SetActive(true);

        // 2. スコア（距離）を取得して表示する
        // ScoreManagerから現在のテキストをそのまま持ってくるのが一番簡単です
        resultText.text = "今回の記録\n" + scoreManager.scoreText.text;

        // 3. ゲームの動きを止める（一時停止）
        Time.timeScale = 0f;
    }

    // ボタンを押した時に呼ばれる魔法
    public void Retry()
    {
        // 時間の流れを元に戻して、シーンを再読み込み
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}