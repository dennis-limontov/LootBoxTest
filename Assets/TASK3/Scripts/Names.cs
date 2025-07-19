namespace LootBox
{
    public static class Names
    {
        public static class Events
        {
            public const string ON_BLUR_PICTURE = "On" + ModelFields.BLUR_PUCTURE + "Changed";
            public const string ON_BUTTON_CLICKED = "OnBtn";
            public const string ON_REEL_STOPPED = "OnReelStopped";
            public const string ON_SPIN_STARTED = "OnSpinStarted";
            public const string ON_SPIN_STOPPED = "OnSpinStopped";
            public const string ON_SPIN_STOPPING = "OnSpinStopping";
            public const string TEST = "Test";
        }

        public static class FsmStates
        {
            public const string IDLE = "Idle";
            public const string SPIN = "Spin";
            public const string STOP = "Stop";
        }

        public static class ModelFields
        {
            public const string BLUR_PUCTURE = "BlurPicture";
            public const string IS_READY_TO_START = "IsReadyToStart";
            public const string IS_READY_TO_STOP = "IsReadyToStop";
            public const string WINNERS = "Winners";
        }
    }
}