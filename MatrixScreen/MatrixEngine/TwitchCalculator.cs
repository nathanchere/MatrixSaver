using FerretLib.SFML;

namespace MatrixScreen
{
    public class TwitchCalculator
    {
        private float _changeCounter;
        private double _secondsCounter;

        private float _chanceOfChange;
        private float _minTwitchFrequency;
        private float _maxTwitchFrequency;

        private bool _changeFrequencyOnTrigger = GetRandom.Bool(0.1f);

        public TwitchCalculator()
        {
            _chanceOfChange = GetRandom.Float(0.4f);
        }

        public bool IsTriggered(ChronoEventArgs chrono)
        {

        }

    }
}