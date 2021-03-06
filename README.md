# Recycler

Creates and configures windows recycle bins on virtual and network drives.

Is is based on the discussion in the [Microsoft Forum](https://social.technet.microsoft.com/Forums/windows/en-US/a349801f-398f-4139-8e8b-b0a92f599e2b/enable-recycle-bin-on-mapped-network-drives)

![screenshot](https://user-images.githubusercontent.com/2531380/30004527-613db51a-90d1-11e7-9fea-c6d02a7f31da.png)

It is a standalone and portable applications and can be used without installing it. The minimal requirements are .NET 4.5 and Windows 7 or better.
  * https://github.com/thsmi/recycler/releases or https://github.com/thsmi/recycler/releases/download/1.0/Recycler.exe

If you like this project consider donating:

  * via Paypal [![PayPayl donate button](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=8DWJHGLLWTP5N "Donate to this project using Paypal")

## Limitations

So this tool is based upon a well known hack. It is unlikely, but it may break with any windows update.

The following limitations are inherrent to windows and can not be changed:

  * Each drive and user has his own separate recycle bin.
  * Deleted files are not moved across drives, data always stays on the same drive.
  * You need adminstrator right to activate and deactivate a recycle bin.
  * The configuration is persistent and system wide. In order to activate or deactivate a recycle bin you have to reboot. The changes are only applied at startup.


## Recycle Bins

Windows defines two types of recycle bins, one for volumes (partitions on harddrives) and one for knownfolder (any valid path).

Both have a slightly differnt behaviour. 

### Volumes
Recycle bins for volumes are identified by their per volume id. Typically they are partitons on a harddrive. 
The volume id is unique to a computer and created at the time when the device was discovered for the first time. 
This id never changes and may be bound to a path or driveletter. 

Virtual and network drives are no volumes, they can not have a volume id.

Windows activates by default a recycle bin for new volumes.

### Known folders
Known folders are somehow magical and confusing. Any valid path can be defined to be a known folder. 
Whenever windows stumbles upon such a folder it applies some magic. They can be used to activate a recycle bin on a 
network drive or virtural drive.

But be warned the known folder configuration is per computer applies to all users and it is persistent.

So consider the following example:
You mounted a directory to the virtual s:\ drive . Then activate the recycle bin for the virtual s:\ drive . 
After some time you unmount the virtual s:\ drive and map a network share to the virtual s:\ drive.

You will endup having a recycle bin on the s:\ drive. Because the configuration is persistent to the s:\ path.

## Bugs

Please report bugs via the [issue tracker](https://github.com/thsmi/recycler/issues) 
or send an email to schmid-thomas at gmx.net . 

## License

The extension is free and open source software, it is made available to you 
under the terms of the [MIT License](https://github.com/thsmi/recycler/blob/master/LICENSE).
