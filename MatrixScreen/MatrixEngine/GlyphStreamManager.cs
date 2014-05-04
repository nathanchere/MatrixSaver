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
        private const int MAX_STREAMS = Config.MaximumGlyphStreams;
        private const float CHANCE_OF_NEW_STREAM = Config.ChanceOfNewGlyphStream; // TODO - implement chance of occurring per second, max MAX_STREAMS

        private readonly Rectangle _workingArea;
        private List<GlyphStream> streams;
        private double _runningDelta;

        public GlyphStreamManager(Vector2u workingArea)
        {
            _workingArea = workingArea.ToRectangle();
            streams = new List<GlyphStream>();            
        }

        private void AddNewGlyphStream()
        {    
            streams.Add(new GlyphStream(_workingArea));
        }

        public void Render(RenderTarget canvas)
        {
            streams.ForEach(x => x.Render(canvas));
        }

        public void Update(ChronoEventArgs chronoArgs)
        {
            streams.ForEach(x => x.Update(chronoArgs));

            streams = streams.Where(x => !x.IsExpired).ToList();

            // Add new streams
            _runningDelta += chronoArgs.Delta;

            if (_runningDelta >= 1)
            {
                var outcome = GetRandom.Double(0, _runningDelta);
                var chance = CHANCE_OF_NEW_STREAM;// GetRandom.Double(0, CHANCE_OF_NEW_STREAM);
                while (outcome < chance)
                {
                    chance -= 1;
                    AddNewGlyphStream();
                }

                _runningDelta -= 1;
            }

                                     
        }
    }
}