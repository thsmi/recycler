using Microsoft.Win32;
using System.Collections.Generic;
using Recycler.Core;
using Recycler.Drives;

namespace Recycler.BitBucket
{
    public class VolumeBitBucket : AbstractBitBucket
    {
        public static readonly RegKey HKCU_VOLUME_BITBUCKET = new RegKey(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Explorer\BitBucket\Volume\");

        public VolumeBitBucket(string uuid) : base(uuid)
        {
        }

        public override RegKey RegistryKey
        {
            get { return HKCU_VOLUME_BITBUCKET; }
        }

        public List<PhysicalDrive> getDrives()
        {
            // It is perfectly fine for a drive to have more than one driveletter.
            return new DriveRepository().GetPhysicalDriveByUuid(Uuid);
        }

    }
}
