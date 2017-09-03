using Microsoft.Win32;
using Recycler.Core;

namespace Recycler.BitBucket
{
    /// <summary>
    ///   An abstract base class which implements the read and write logic for 
    ///   all fields which all bitbuckets have in common
    /// </summary>
    public abstract class AbstractBitBucket : IBitBucket
    {
        private string uuid;

        public AbstractBitBucket(string uuid)
        {
            this.uuid = uuid;
        }

        /// <inheritdoc/>
        public string Uuid
        {
            get
            {
                return this.uuid;
            }
        }

        public abstract RegKey RegistryKey { get; }

        /// <inheritdoc/>
        public int MaxCapacity
        {
            get
            {
                return this.RegistryKey.GetInt(this.uuid, "MaxCapacity");
            }
            set
            {
                this.RegistryKey.SetInt(this.uuid, "MaxCapacity", value);
            }
        }

        /// <inheritdoc/>
        public int NukeOnDelete
        {
            get
            {
                return this.RegistryKey.GetInt(this.Uuid, "NukeOnDelete");
            }
            set
            {
                this.RegistryKey.SetInt(this.Uuid, "NukeOnDelete", value);
            }
        }
    }
}
