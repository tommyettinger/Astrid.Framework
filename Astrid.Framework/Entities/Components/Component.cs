﻿using Newtonsoft.Json;

namespace Astrid.Framework.Entities.Components
{
    public abstract class Component
    {
        protected Component()
        {
            Name = string.Empty;
        }

        [JsonIgnore]
        public Entity Entity { get; internal set; }
        public string Name { get; set; }
    }
}
