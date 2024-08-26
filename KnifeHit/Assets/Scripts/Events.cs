using System;

public static class Events 
{
    public static Action OnTouchScreen;
    public static Action OnCollisionBetweenKnives;
    public static Action OnAllKnivesOnHit;
    public static Action OnFinishStage;
    public static Action<int> OnStartStage;
}
