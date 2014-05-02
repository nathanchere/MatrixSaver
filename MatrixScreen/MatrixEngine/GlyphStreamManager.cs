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
        private const int MAX_STREAMS = 5;
        private const float CHANCE_OF_NEW_STREAM = 0.2f; // TODO - implement chance of occurring per second, max MAX_STREAMS

        private readonly Rectangle _workingArea;
        private List<GlyphStream> streams;

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
            if (streams.Count < MAX_STREAMS && GetRandom.Float(0,1) < CHANCE_OF_NEW_STREAM)
                AddNewGlyphStream();
        }
    }
}