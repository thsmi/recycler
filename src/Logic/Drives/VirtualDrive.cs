using Microsoft.Win32;
using Recycler.Core;
using Recycler.Logic.Drives;

namespace Recycler.Drives
{
    public class VirtualDrive : AbstractKnownFolderDrive
    {
        private string drive;

        public static readonly RegKey HKLM_VIRTUAL_DRIVES 
            = new RegKey(RegistryHive.LocalMachine, @"SYSTEM\CurrentControlSet\Control\Session Manager\DOS Devices\");

        public VirtualDrive(string drive)
        {
            this.drive = drive;
        }

        public override string Drive
        {
            get
            {
                return drive + @"\";
            }
        }

        public override string Path
        {
            get
            {
                return VirtualDrive.HKLM_VIRTUAL_DRIVES.GetString("", drive);
            }
        }

        public override string Type
        {
            get
            {
                return "Virtual Drive (DOS Device)";
            }
        }


    }
}
