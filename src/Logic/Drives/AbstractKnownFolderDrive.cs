using System;
using System.IO;
using Recycler.BitBucket;

namespace Recycler.Logic.Drives
{
    public abstract class AbstractKnownFolderDrive : AbstractDrive
    {
        private string Uuid
        {
            get
            {
                string drive = this.Drive;

                string[] subkeys = KnownFolderBitBucket.HKCU_KNOWNFOLDER_BITBUCKET.GetSubKeyNames();
                foreach (string subkey in subkeys)
                {
                    if (KnownFolderBitBucket.HKLM_KNOWNFOLDER_DESCRIPTION.HasKey(subkey) == false)
                        continue;

                    string path = KnownFolderBitBucket.HKLM_KNOWNFOLDER_DESCRIPTION.GetString(
                        subkey, KnownFolderBitBucket.KNOWNFOLDER_RELATIVE_PATH, null);

                    if (string.IsNullOrWhiteSpace(path))
                        continue;

                    if (path.Equals(drive) == false)
                        continue;

                    // We have a match...
                    return subkey;
                }

                return null;
            }
        }

        /// <inheritdoc/>
        public override bool HasBitBucket()
        {
            return (this.Uuid != null) ? true : false;
        }

        /// <inheritdoc/>
        public override IBitBucket GetBitBucket()
        {
            string uuid = this.Uuid;

            if (uuid != null)
                return new KnownFolderBitBucket(uuid);

            // we need to create a new known folder...
            uuid = Guid.NewGuid().ToString("B");

            KnownFolderBitBucket bitBucket = new KnownFolderBitBucket(uuid);

            // Initialize the known folder...
            bitBucket.Name = "" + this.Drive + " Drive";
            bitBucket.RelativePath = this.Drive;
            bitBucket.Category = 0x4;
            bitBucket.MaxCapacity = 24330;

            // This is some magic. We add a backref to the known folder we created...
            bitBucket.XUuid = uuid;

            return bitBucket;
        }

        /// <inheritdoc/>
        public override void RemoveBitBucket()
        {
            string uuid = this.Uuid;

            if (uuid == null)
                return;
           
            KnownFolderBitBucket.HKCU_KNOWNFOLDER_BITBUCKET.RemoveKey(uuid);

            // We only delete the known folder if we are sure we created it.
            string xuuid = KnownFolderBitBucket.HKLM_KNOWNFOLDER_DESCRIPTION
                .GetString(uuid, KnownFolderBitBucket.KNOWNFOLDER_X_BITBUCKET_UUID, null);

            if (uuid.Equals(xuuid) == false)
            {
                // TODO show a warning message that the folder is bound to a system defined well known folder
                // and can not be deleted...
                return;
            }

            KnownFolderBitBucket.HKLM_KNOWNFOLDER_DESCRIPTION.RemoveKey(xuuid);
        }

        public void CreateRecycleBin(string name = "Recycle Bin")
        {
            Directory.CreateDirectory(this.Drive + name + ".{645FF040-5081-101B-9F08-00AA002F954E}");
        }
    }
}
