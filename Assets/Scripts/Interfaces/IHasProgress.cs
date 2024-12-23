using System;

public interface IHasProgress 
{
    public event EventHandler<OnProgressBarChangedEventArgs> OnProgressBarUpdate;    // declaring event

    public class OnProgressBarChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }    // making class for sending extra data with the event
}
