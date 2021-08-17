using UnityEngine;

public class DifficultyAdjuster : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.SetGameMode(1);
    }

    public void SetDifficulty(int difficulty)
    {
        GameManager.Instance.SetGameMode(difficulty);
    }
}
