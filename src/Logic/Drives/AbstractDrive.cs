using System.Windows;
using Recycler.BitBucket;
using Recycler.Drives;

namespace Recycler.Logic.Drives
{
    public abstract class AbstractDrive : IDrive
    {
        /// <inheritdoc/>
        public abstract string Drive { get; }
        /// <inheritdoc/>
        public abstract string Path { get; }
        /// <inheritdoc/>
        public abstract string Type { get; }

        /// <inheritdoc/>
        public string MaxSize
        {
            get
            {
                if (this.HasBitBucket() == false)
                    return "";

                return "" + this.GetBitBucket().MaxCapacity;
            }
            set
            {
                this.GetBitBucket().MaxCapacity = int.Parse(value);
            }
        }

        /// <inheritdoc/>
        public RecycleBinStatus RecycleBinStatus
        {
            get
            {
                if (this.HasBitBucket() == false)
                    return RecycleBinStatus.NotConfigured;

                if (this.GetBitBucket().NukeOnDelete == 0)
                    return RecycleBinStatus.Activated;

                return RecycleBinStatus.Deactivated;
            }
            set
            {
                if (value.Equals(RecycleBinStatus.NotConfigured))
                {
                    this.RemoveBitBucket();
                    return;
                }

                if (value.Equals(RecycleBinStatus.Deactivated))
                {
                    this.GetBitBucket().NukeOnDelete = 1;
                    return;
                }

                this.GetBitBucket().NukeOnDelete = 0;
                return;
            }
        }

        /// <inheritdoc/>
        public abstract IBitBucket GetBitBucket();

        /// <inheritdoc/>
        public abstract bool HasBitBucket();

        /// <inheritdoc/>
        public abstract void RemoveBitBucket();
    }
}
