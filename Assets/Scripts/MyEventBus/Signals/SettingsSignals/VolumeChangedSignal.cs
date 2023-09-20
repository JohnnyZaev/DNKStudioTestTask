namespace MyEventBus.Signals.SettingsSignals
{
    public class VolumeChangedSignal
    {
        public readonly float Volume;

        public VolumeChangedSignal(float volume)
        {
            Volume = volume;
        }
    }
}
