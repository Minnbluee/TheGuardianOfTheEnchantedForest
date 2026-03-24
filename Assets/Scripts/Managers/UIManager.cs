using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : Singleton<UIManager>
{
    [Header("HUD - Vida")]
    [SerializeField] private Image[] heartImages;
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartEmpty;

    [Header("HUD - Diamantes")]
    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private int diamondsRequired = 3;

    [Header("HUD - Puntos")]
    [SerializeField] private TMP_Text scoreText;

    private int _currentDiamonds = 0;
    private int _currentScore = 0;
    private int _currentHearts = 3;
    private const int MaxHearts = 3;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ResetHearts()
    {
        _currentHearts = MaxHearts;
        UpdateHeartsUI();
    }

    public bool LoseHeart()
    {
        if (_currentHearts <= 0) return false;

        _currentHearts--;
        UpdateHeartsUI();

        return _currentHearts > 0;
    }

    private void UpdateHeartsUI()
    {
        if (heartImages == null) return;

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i] == null) continue;
            heartImages[i].sprite = (i < _currentHearts) ? heartFull : heartEmpty;
        }
    }

  
    public void ResetDiamonds()
    {
        _currentDiamonds = 0;
        UpdateDiamondUI();
    }

    public bool AddDiamond()
    {
        _currentDiamonds++;
        UpdateDiamondUI();

        return _currentDiamonds >= diamondsRequired;
    }

    private void UpdateDiamondUI()
    {
        if (diamondText != null)
            diamondText.text = $"Diamantes: {_currentDiamonds} / {diamondsRequired}";
    }


    public void ResetScore()
    {
        _currentScore = 0;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        _currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Puntos: {_currentScore}";
    }


    public int CurrentHearts => _currentHearts;
    public int CurrentDiamonds => _currentDiamonds;
    public int CurrentScore => _currentScore;
    public bool HasEnoughDiamonds => _currentDiamonds >= diamondsRequired;
}
