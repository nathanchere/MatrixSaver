using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FerretLib.SFML;
using SFML.Graphics;
using SFML.Window;

namespace MatrixScreen
{
    public class GlyphStreamManager : IEntity
    {
        private readonly int _maximumStreams;
        private readonly float _chanceOfNewStream;
        private readonly float NEW_STREAM_CHECK_FREQUENCY = 0.016f; // TODO: config
        private readonly GlyphStreamManagerConfig _settings;

        private readonly Rectangle _workingArea;
        private List<GlyphStream> streams;
        private double _runningDelta;

        public GlyphStreamManager(GlyphStreamManagerConfig settings, Vector2u workingArea)
        {
            _workingArea = workingArea.ToRectangle();
            streams = new List<GlyphStream>();
            _settings = settings;

            _maximumStreams = settings.MaximumGlyphStreams;
            _chanceOfNewStream = settings.ChanceOfNewGlyphStream;
        }

        private void AddNewGlyphStream()
        {    
            streams.Add(new GlyphStream(_settings.GlyphStreamConfig, _workingArea));
        }

        public void Render(RenderTarget canvas)
        {
            streams.ForEach(x => x.Render(canvas));
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            UpdateStreams(chronoArgs);
            PurgeOldStreams();
            AddNewStreams(chronoArgs);
        }

        private void UpdateStreams(ChronoEventArgs chronoArgs)
        {
            streams.ForEach(x => x.Update(chronoArgs));
        }

        private void PurgeOldStreams()
        {
            streams = streams.Where(x => !x.IsExpired).ToList();
        }

        private void AddNewStreams(ChronoEventArgs chronoArgs)
        {            
            _runningDelta += chronoArgs.Delta;

            if (_runningDelta >= NEW_STREAM_CHECK_FREQUENCY)
            {
                var outcome = GetRandom.Double(0, _runningDelta);
                var chance = _chanceOfNewStream;
                while (streams.Count <= _maximumStreams && outcome < chance)
                {
                    chance -= NEW_STREAM_CHECK_FREQUENCY;
                    AddNewGlyphStream();
                }

                _runningDelta -= NEW_STREAM_CHECK_FREQUENCY;
            }
        }
    }
}