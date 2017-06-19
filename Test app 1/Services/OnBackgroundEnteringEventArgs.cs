using System;

namespace Test_app_1.Services
{
    public class OnBackgroundEnteringEventArgs : EventArgs
    {
        public SuspensionState SuspensionState { get; set; }
        public Type Target { get; private set; }

        public OnBackgroundEnteringEventArgs(SuspensionState suspensionState, Type target) : base()
        {
            SuspensionState = suspensionState;
            Target = target;
        }
    }
}
