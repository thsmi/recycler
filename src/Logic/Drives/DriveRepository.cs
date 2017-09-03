using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Recycler.Drives
{
    class DriveRepository
    {
        internal static class NativeMethods
        {

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern bool GetVolumeNameForVolumeMountPoint(
                string lpszVolumeMountPoint, 
                [Out] StringBuilder lpszVolumeName, 
                uint cchBufferLength);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetVolumePathNamesForVolumeNameW(
                [MarshalAs(UnmanagedType.LPWStr)] string lpszVolumeName,
                [MarshalAs(UnmanagedType.LPWStr)] string lpszVolumePathNames,
                uint cchBuferLength,
                ref UInt32 lpcchReturnLength);

        }

        public List<PhysicalDrive> GetPhysicalDriveByUuid(string uuid)
        {
            return GetPhysicalDriveByVolume(@"\\?\Volume" + uuid + @"\");
        }

        public List<PhysicalDrive> GetPhysicalDriveByVolume(string volume)
        {

            // https://blogs.msdn.microsoft.com/adioltean/2005/04/17/ntfs-curiosities-part-2-volumes-volume-names-and-mount-points/

            List<PhysicalDrive> result = new List<PhysicalDrive>();

            if (volume.StartsWith(@"\\?\Volume") == false)
                return result;

            if (volume.Length != 38 + 11)
                return result;

            uint lpcchReturnLength = 0;
            string buffer = "";

            NativeMethods.GetVolumePathNamesForVolumeNameW(volume, buffer, (uint)buffer.Length, ref lpcchReturnLength);
            if (lpcchReturnLength == 0)
                return result;

            buffer = new string(new char[lpcchReturnLength]);
            if (!NativeMethods.GetVolumePathNamesForVolumeNameW(volume, buffer, lpcchReturnLength, ref lpcchReturnLength))
            {
                return result;
            }

            foreach (string mount in buffer.Split('\0'))
            {
                if (mount.Length != 3)
                    continue;

                // Check if it is a folder or drive...
                result.Add(new PhysicalDrive(mount, volume));
            }

            return result;
        }

        public List<PhysicalDrive> GetPhysicalDrives()
        {

            List<PhysicalDrive> item = new List<PhysicalDrive>();

            foreach (DriveInfo driveinfo in DriveInfo.GetDrives())
            {

                const int MaxVolumeNameLength = 100;
                StringBuilder sb = new StringBuilder(MaxVolumeNameLength);

                if (!NativeMethods.GetVolumeNameForVolumeMountPoint(driveinfo.Name, sb, (uint)MaxVolumeNameLength))
                    continue;

                item.Add(new PhysicalDrive(driveinfo.Name, sb.ToString()));
            }

            return item;
        }

        public List<VirtualDrive> GetVirtualDrives()
        {
            List<VirtualDrive> items = new List<VirtualDrive>();

            string[] dosnames = VirtualDrive.HKLM_VIRTUAL_DRIVES.GetValueNames();
            foreach (string dosname in dosnames)
            {
                if (dosname.Length != 2)
                    continue;

                if (dosname[1] != ':')
                    continue;

                if (Char.ToLower(dosname[0]) < 'a' && Char.ToLower(dosname[0]) > 'z')
                    continue;

                items.Add(new VirtualDrive(dosname));
            }
            // For A to Z check for registry key.

            return items;
        }

        public List<NetworkDrive> GetNetworkDrives()
        {
            List<NetworkDrive> items = new List<NetworkDrive>();

            string[] drives = NetworkDrive.HKCU_NETWORK_DRIVES.GetSubKeyNames();
            foreach (string drive in drives)
            {
                items.Add(new NetworkDrive(drive));
            }

            return items;
        }


        public List<IDrive> GetDrives()
        {
            List<IDrive> items = new List<IDrive>();

            items.AddRange(GetPhysicalDrives());
            items.AddRange(GetVirtualDrives());
            items.AddRange(GetNetworkDrives());

            return items;
        }

    }
}
