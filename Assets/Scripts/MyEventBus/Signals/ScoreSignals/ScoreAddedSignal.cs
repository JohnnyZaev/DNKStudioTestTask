namespace MyEventBus.Signals.ScoreSignals
{
    public class ScoreAddedSignal
    {
        public readonly int Value;

        public ScoreAddedSignal(int value)
        {
            Value = value;
        }
    }
}
