using Recycler.Core;

namespace Recycler.BitBucket
{
    public interface IBitBucket
    {
        /// <summary>
        ///  returns the bit buckets unique id
        /// </summary>
        string Uuid { get; }

        RegKey RegistryKey { get; }

        /// <summary>
        ///  Defines the bit buckets maximal capacity
        /// </summary>
        int MaxCapacity { get; set; }

        /// <summary>
        ///  Defines the delete behaviour. 
        ///  If set to one the recycle bin is disabled and files will 
        ///  be delete immediately , otherwise the files will be moved
        ///  to the recycle bin.
        /// </summary>
        int NukeOnDelete { get; set; }
    }

}
