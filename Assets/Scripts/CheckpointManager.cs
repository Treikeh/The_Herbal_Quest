using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckpointManager
{
    public static Action CheckpointSaved;
    public static Action CheckpointReloaded;


    public static void SaveCheckpoint()
    {
        CheckpointSaved?.Invoke();
    }

    public static void ReloadCheckpoint()
    {
        CheckpointReloaded?.Invoke();
        GameManager.CurrenctGameState = GameManager.GameStates.Running;
    }
}
