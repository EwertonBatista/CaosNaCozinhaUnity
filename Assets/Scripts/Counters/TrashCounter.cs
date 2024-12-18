using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{

    public event EventHandler OnTrashThrow;
    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnTrashThrow?.Invoke(this, EventArgs.Empty);
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();

        }
    }
}
 