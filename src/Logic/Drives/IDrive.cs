using System;
using Recycler.BitBucket;

namespace Recycler.Drives
{
    public enum RecycleBinStatus { NotConfigured, Deactivated, Activated }

    public interface IDrive
    {      
        /// <summary>
        ///   The drive e.g. d:\
        /// </summary>
        string Drive { get; }

        /// <summary>
        /// The path for the drive as it is used to the operating system
        /// </summary>
        string Path { get; }

        /// <summary>
        ///   A human readible drive description
        /// </summary>
        string Type { get; }

        /// <summary>
        ///  Defines the bit buckets maximal capacity
        /// </summary>
        string MaxSize { get; set; }

        /// <summary>
        /// The dives recycle bin status.
        /// </summary>
        RecycleBinStatus RecycleBinStatus { get; set; }

        /// <summary>
        ///   Checks the drive has a recycle bin
        /// </summary>
        /// <returns>true in case the drive has a bit bucket othewise false</returns>
        Boolean HasBitBucket();

        /// <summary>
        /// Gets the recycle bin for the given drive. 
        /// In case the Recycle bin does not exists it will create it. 
        /// </summary>
        /// <returns>the bitbucket for the drive.</returns>
        IBitBucket GetBitBucket();

        /// <summary>
        /// Removes the recycle bin configuration for the given drive
        /// This may not alway be possible and most likely requires a restart.
        /// </summary>
        void RemoveBitBucket();
    }
}
