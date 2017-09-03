using Microsoft.Win32;
using Recycler.Core;

namespace Recycler.BitBucket
{
    
    public class KnownFolderBitBucket : AbstractBitBucket
    {
        public static readonly RegKey HKCU_KNOWNFOLDER_BITBUCKET 
            = new RegKey(RegistryHive.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Explorer\BitBucket\KnownFolder\");

        public static readonly RegKey HKLM_KNOWNFOLDER_DESCRIPTION
            = new RegKey(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FolderDescriptions\");

        public const string KNOWNFOLDER_RELATIVE_PATH = "RelativePath";
        public const string KNOWNFOLDER_NAME = "Name";
        public const string KNOWNFOLDER_CATEGORY = "Category";
        public const string KNOWNFOLDER_X_BITBUCKET_UUID = "X-BitBucketUuid";

        public KnownFolderBitBucket(string uuid) : base(uuid)
        {
        }

        public override RegKey RegistryKey
        {
            get { return HKCU_KNOWNFOLDER_BITBUCKET; }
        }

        public string RelativePath
        {
            get
            {
                return HKLM_KNOWNFOLDER_DESCRIPTION.GetString(this.Uuid, KNOWNFOLDER_RELATIVE_PATH, null);
            }
            set
            {
                HKLM_KNOWNFOLDER_DESCRIPTION.SetString(this.Uuid, KNOWNFOLDER_RELATIVE_PATH, value);
            }
        }

        public string Name
        {
            get
            {
                return HKLM_KNOWNFOLDER_DESCRIPTION.GetString(this.Uuid, KNOWNFOLDER_NAME, null);
            }
            set
            {
                HKLM_KNOWNFOLDER_DESCRIPTION.SetString(this.Uuid, KNOWNFOLDER_NAME, value);
            }
        }

        public int Category
        {
            get
            {
                return HKLM_KNOWNFOLDER_DESCRIPTION.GetInt(this.Uuid, KNOWNFOLDER_CATEGORY, -1);
            }
            set
            {
                HKLM_KNOWNFOLDER_DESCRIPTION.SetInt(this.Uuid, KNOWNFOLDER_CATEGORY, value);
            }
        }

        public string XUuid
        {
            get
            {
                return HKLM_KNOWNFOLDER_DESCRIPTION.GetString(this.Uuid, KNOWNFOLDER_X_BITBUCKET_UUID, null);
            }
            set
            {
                HKLM_KNOWNFOLDER_DESCRIPTION.SetString(this.Uuid, KNOWNFOLDER_X_BITBUCKET_UUID, value);
            }
        }
    }

}
