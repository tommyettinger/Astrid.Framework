﻿using System;
using Astrid.Framework.Assets;
using Astrid.Framework.Audio;
using Astrid.Framework.Graphics;
using Astrid.Framework.Input;

namespace Astrid.Framework
{
    public abstract class ApplicationBase : IDisposable
    {
        protected ApplicationBase()
        {
        }

        public abstract AssetManager CreateAssetManager();
        public abstract GraphicsDevice CreateGraphicsDevice();
        public abstract InputDevice CreateInputDevice();
        public abstract AudioDevice CreateAudioDevice();
        public abstract void Run(GameBase game);

        private bool _isDisposed;

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if (isDisposing)
            {
                // Free any other managed objects here. 
            }

            // Free any unmanaged objects here. 
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
