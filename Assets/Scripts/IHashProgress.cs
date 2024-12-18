using System;
using UnityEngine;

public interface IHashProgress
{
    public event EventHandler<OnProgressChangedEventArts> OnProgressChanged;
    public class OnProgressChangedEventArts : EventArgs
    {
        public float progressNormalized;
    }
}
