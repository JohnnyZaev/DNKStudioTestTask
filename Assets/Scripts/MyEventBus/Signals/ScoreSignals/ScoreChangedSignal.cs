namespace MyEventBus.Signals.ScoreSignals
{
    public class ScoreChangedSignal
    {
        public readonly int Score;

        public ScoreChangedSignal(int score)
        {
            Score = score;
        }
    }
}
