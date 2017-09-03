using Microsoft.Win32;
using Recycler.Core;
using Recycler.Logic.Drives;

namespace Recycler.Drives
{
    public class NetworkDrive : AbstractKnownFolderDrive
    {
        private string drive;

        public static readonly RegKey HKCU_NETWORK_DRIVES 
            = new RegKey(RegistryHive.CurrentUser, @"Network\");

        public NetworkDrive(string drive)
        {
            this.drive = drive;
        }

        /// <inheritdoc/>
        public override string Drive
        {
            get
            {
                return drive + @":\";
            }
        }

        /// <inheritdoc/>
        public override string Path
        {
            get
            {
                return HKCU_NETWORK_DRIVES.GetString(this.drive, "RemotePath");
            }
        }

        /// <inheritdoc/>
        public override string Type
        {
            get
            {
                return "Networkshare";
            }
        }
    }
}
