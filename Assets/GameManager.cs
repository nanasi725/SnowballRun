using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject resultPanel;    // リザルトパネル
    public TextMeshProUGUI resultText; // リザルト画面の文字
    public ScoreManager scoreManager;  // 距離を計算しているスクリプト

    // ゲームオーバー時に呼ばれる処理
    public void GameOver()
    {
        // 1. リザルトパネルを表示
        resultPanel.SetActive(true);

        // 2. 今のスコアを数値として取り出す ("100 m" から " m" を消して数字にする)
        string scoreStr = scoreManager.scoreText.text.Replace(" m", "");
        float currentScore = float.Parse(scoreStr);

        // 3. 保存されているハイスコアを読み込む（データがない時は 0 が入る）
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        // 4. ハイスコア更新チェック
        string message = "";
        if (currentScore > highScore)
        {
            highScore = currentScore;
            // 新しい記録を「HighScore」という名前で保存する
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save(); // 確実に保存を確定させる
            message = "<color=yellow>NEW RECORD!</color>\n"; // 新記録なら黄色い文字を出す
        }

        // 5. リザルト画面の文字を書き換える
        resultText.text = message + "今回の記録: " + currentScore.ToString("F0") + " m\n" + "最高記録: " + highScore.ToString("F0") + " m";

        // 6. ゲームを一時停止
        Time.timeScale = 0f;
    }

    // リトライボタンが押された時の処理
    public void Retry()
    {
        Time.timeScale = 1f; // 時間の流れを戻す
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}