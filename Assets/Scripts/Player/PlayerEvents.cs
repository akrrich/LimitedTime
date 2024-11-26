using System;

public class PlayerEvents
{
    private static event Action onReloadingText;
    public static Action OnReloadingText { get => onReloadingText; set => onReloadingText = value; }

    private static event Action onReloadingFinished;
    public static Action OnReloadingFinished { get => onReloadingFinished; set => onReloadingFinished = value; }

    private static event Action onRespawningPlayer;
    public static Action OnRespawningPlayer { get => onRespawningPlayer; set => onRespawningPlayer = value; }
}
