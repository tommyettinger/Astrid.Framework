﻿using System;

namespace Astrid.Framework.Assets
{
    public interface IAssetLoader
    {
    }

    public abstract class AssetLoader<T> : IAssetLoader
        where T : IAsset
    {
        public abstract T Load(AssetManager assetManager, string assetPath);
    }
}
