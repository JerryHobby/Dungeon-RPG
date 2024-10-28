using System;

public class GameEvents
{
    public static event Action OnStartGame;
    public static event Action OnEndGame;
    public static event Action<int> OnEnemyCount;
    public static event Action OnVictory;

    public static void RaiseStartGame() => OnStartGame?.Invoke();
    public static void RaiseEndGame() => OnEndGame?.Invoke();
    public static void RaiseEnemyCount(int count) => OnEnemyCount?.Invoke(count);
    public static void RaiseVictory() => OnVictory?.Invoke();
}