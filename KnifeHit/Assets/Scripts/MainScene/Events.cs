using System;

public static class Events 
{
    public static Action OnTouchScreen;
    public static Action OnHitTarget;
    public static Action OnCollisionBetweenKnives;
    public static Action OnAllKnivesOnHit;
    public static Action OnFinishStage;
    public static Action OnBossSpawn;
    public static Action OnBossDestroy;
    public static Action OnGameOver;
    public static Action OnNewBestScore;
    public static Action<int> OnStartStage;

#region ButtonEvents
    public static Action OnPlayButton;
    public static Action OnRestartButton;
#endregion
}
