using Recycler.BitBucket;
using Recycler.Logic.Drives;

namespace Recycler.Drives
{
    public class PhysicalDrive : AbstractDrive
    {
        private string drive;
        private string volume;

        public PhysicalDrive(string drive, string volume)
        {
            this.drive = drive;
            this.volume = volume;
        }

        /// <inheritdoc/>
        public override string Drive {
            get { return this.drive;  }
        }

        /// <inheritdoc/>
        public override string Path {
            get { return this.volume;  }
        }

        /// <inheritdoc/>
        public override string Type {
            get
            {
                return "Physical Drive";
            }
        }

        private string Uuid
        {
            get
            {
                if (volume.StartsWith(@"\\?\Volume") == false)
                    return null;

                string uuid = volume.Substring(10, 38);
                if (uuid.Length != 38)
                    return null;

                return uuid;
            }
        }

        /// <inheritdoc/>
        public override bool HasBitBucket()
        {
            if (this.Uuid == null)
                return false;

            if (!VolumeBitBucket.HKCU_VOLUME_BITBUCKET.HasKey(this.Uuid))
                return false;

            return true;
        }

        /// <inheritdoc/>
        public override IBitBucket GetBitBucket()
        {
            return new VolumeBitBucket(this.Uuid);
        }

        /// <inheritdoc/>
        public override void RemoveBitBucket()
        {
            VolumeBitBucket.HKCU_VOLUME_BITBUCKET.RemoveKey(this.Uuid);
        }
    }
}
