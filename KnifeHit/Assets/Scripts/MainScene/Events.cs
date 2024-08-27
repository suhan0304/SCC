using System;

public static class Events 
{
    public static Action OnTouchScreen;
    public static Action OnHitTarget;
    public static Action OnCollisionBetweenKnives;
    public static Action OnAllKnivesOnHit;
    public static Action OnFinishStage;
    public static Action OnGameOver;
    public static Action OnNewBestScore;
    public static Action<int> OnStartStage;
}
