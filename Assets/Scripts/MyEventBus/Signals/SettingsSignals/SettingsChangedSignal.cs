namespace MyEventBus.Signals.SettingsSignals
{
    public class SettingsChangedSignal
    {
        public readonly int Difficulty;

        public SettingsChangedSignal(int difficulty)
        {
            Difficulty = difficulty;
        }
    }
}
