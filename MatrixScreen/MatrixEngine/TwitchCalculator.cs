using FerretLib.SFML;

namespace MatrixScreen
{
    public class TwitchCalculator
    {
        private const float MINIMUM_TWITCH_FREQUENCY = 0.06f;
        private const float MAXIMUM_TWITCH_FREQUENCY = 20f;

        private double _secondsCounter;

        private float _twitchFrequency;
        private float _minTwitchFrequency;
        private float _maxTwitchFrequency;
        private float _chanceOfTrigger;

        private bool _changeFrequencyOnTrigger = GetRandom.Bool(0.1f);

        public TwitchCalculator()
        {
            UpdateTriggerFrequency(true);

            if (_minTwitchFrequency > _maxTwitchFrequency)
            {
                var swap = _minTwitchFrequency;
                _minTwitchFrequency = _maxTwitchFrequency;
                _maxTwitchFrequency = swap;
            }
        }

        private void UpdateTriggerFrequency(bool force = false)
        {
            if (force || _changeFrequencyOnTrigger)
            {
                _minTwitchFrequency = GetRandom.Float(MINIMUM_TWITCH_FREQUENCY, MAXIMUM_TWITCH_FREQUENCY);
                _maxTwitchFrequency = GetRandom.Float(MINIMUM_TWITCH_FREQUENCY, MAXIMUM_TWITCH_FREQUENCY);
                _chanceOfTrigger = GetRandom.Float(0.05f, 0.95f);
            }
            _twitchFrequency = GetRandom.Float(_minTwitchFrequency, _maxTwitchFrequency);
        }

        public bool IsTriggered(ChronoEventArgs chrono)
        {
            _secondsCounter += chrono.Delta;

            if (_secondsCounter > _twitchFrequency)
            {
                if (GetRandom.Bool(_chanceOfTrigger))
                {
                    _secondsCounter -= _twitchFrequency;
                    UpdateTriggerFrequency();
                    return true;
                }
            }

            return false;
        }

    }
}