﻿using Serilog;
using System;
using System.Threading.Tasks;

namespace LambentLight
{
    /// <summary>
    /// Base for all of the content managers.
    /// </summary>
    public abstract class BaseManager
    {
        #region Properties

        /// <summary>
        /// If this manager is ready to work.
        /// </summary>
        public bool ReadyToWork { get; private set; } = false;

        #endregion

        #region Public Functions

        /// <summary>
        /// Initializes the current manager
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            Log.Information($"Initializing {GetType().Name}");
            // If the cache could not be loaded
            if (!LoadCache())
            {
                // Update the items
                await Update();
            }
            // And say that this manager is ready to work
            ReadyToWork = true;
        }
        /// <summary>
        /// Updates the information of this manager.
        /// </summary>
        public abstract Task Update();
        /// <summary>
        /// Loads the existing items from the cache (if present).
        /// </summary>
        public virtual bool LoadCache() => throw new NotImplementedException();
        /// <summary>
        /// Saves the existing items onto the cache directory.
        /// </summary>
        public virtual void SaveCache() => throw new NotImplementedException();

        #endregion
    }
}
