using Microsoft.Win32;
using System;
using System.Collections.Generic;
using Recycler.Core;

namespace Recycler.BitBucket
{
    class BitBucketRepository
    {
        public List<IBitBucket> GetBitBucketFolders()
        {
            List<IBitBucket> list = new List<IBitBucket>();

            list.AddRange(this.GetKnownFolderBitBuckets());
            list.AddRange(this.GetVolumeBitBuckets());

            return list;
        }


        public List<VolumeBitBucket> GetVolumeBitBuckets()
        {
            List<VolumeBitBucket> list = new List<VolumeBitBucket>();

            string[] subkeys = VolumeBitBucket.HKCU_VOLUME_BITBUCKET.GetSubKeyNames();
            foreach (string subkey in subkeys)
            {
                list.Add(new VolumeBitBucket(subkey));
            }

            return list;
        }



        public KnownFolderBitBucket GetKnownFolderBitBucketByUuid(string uuid)
        {
            if (!KnownFolderBitBucket.HKCU_KNOWNFOLDER_BITBUCKET.HasKey(uuid))
                return null;

            return new KnownFolderBitBucket(uuid);
        }


        public List<KnownFolderBitBucket> GetKnownFolderBitBuckets()
        {
            List<KnownFolderBitBucket> list = new List<KnownFolderBitBucket>();

            string[] subkeys = KnownFolderBitBucket.HKCU_KNOWNFOLDER_BITBUCKET.GetSubKeyNames();
            foreach (string subkey in subkeys)
            {
                list.Add(new KnownFolderBitBucket(subkey));
            }

            return list;
        }



    }
}
