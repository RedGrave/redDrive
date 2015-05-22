using System;
using System.Text;
using System.Runtime.InteropServices;

namespace RedShare.Network
{
    public static class share
    {
        [DllImport("mpr.dll")]
        public static extern int WNetAddConnection2(NETRESOURCE lptresource,
           string password, string username, uint flags);
        
        [DllImport("mpr.dll")]
        public static extern int WNetCancelConnection2(string drivename,uint flags,bool fForce);
        
        #region windows api parameters

        public enum dwScope : uint  //    https://msdn.microsoft.com/en-us/library/windows/desktop/aa385353(v=vs.85).aspx
        {
            RESOURCE_CONNECTED = 0x00000001,
            RESOURCE_GLOBALNET = 0x00000002,
            RESOURCE_REMEMBERED = 0x00000003,
        }
        public enum dwType : uint    //    https://msdn.microsoft.com/en-us/library/windows/desktop/aa385353(v=vs.85).aspx
        {
            RESOURCETYPE_ANY = 0x00000000,
            RESOURCETYPE_DISK = 0x00000001,
            RESOURCETYPE_PRINT = 0x00000002,
        }
        public enum dwDisplayType : uint //    https://msdn.microsoft.com/en-us/library/windows/desktop/aa385353(v=vs.85).aspx
        {
            RESOURCEDISPLAYTYPE_GENERIC = 0x00000000,
            RESOURCEDISPLAYTYPE_DOMAIN = 0x00000001,
            RESOURCEDISPLAYTYPE_SERVER = 0x00000002,
            RESOURCEDISPLAYTYPE_SHARE = 0x00000003,
            RESOURCEDISPLAYTYPE_FILE = 0x00000004,
            RESOURCEDISPLAYTYPE_GROUP = 0x00000005,
            RESOURCEDISPLAYTYPE_NETWORK = 0x00000006,
            RESOURCEDISPLAYTYPE_ROOT = 0x00000007,
            RESOURCEDISPLAYTYPE_SHAREADMIN = 0x00000008,
            RESOURCEDISPLAYTYPE_DIRECTORY = 0x00000009,
            RESOURCEDISPLAYTYPE_TREE = 0x0000000a,
            RESOURCEDISPLAYTYPE_NDSCONTAINER = 0x0000000b,
        }
        public enum dwUsage : uint     //    https://msdn.microsoft.com/en-us/library/windows/desktop/aa385353(v=vs.85).aspx
        {
            RESOURCEUSAGE_CONNECTABLE = 0x00000001,
            RESOURCEUSAGE_CONTAINER = 0x00000002,
            RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
            RESOURCEUSAGE_SIBLING = 0x00000008,
            RESOURCEUSAGE_ATTACHED = 0x000000010,
        }
        public enum dwFlags : uint     //    https://msdn.microsoft.com/en-us/library/windows/desktop/aa385353(v=vs.85).aspx
        {
            CONNECT_UPDATE_PROFILE = 0x00000001,
            CONNECT_UPDATE_RECENT = 0x00000002,
            CONNECT_TEMPORARY = 0x00000004,
            CONNECT_INTERACTIVE = 0x00000008,
            CONNECT_PROMPT = 0x000000010,
            CONNECT_REDIRECT = 0x000000080,
            CONNECT_CURRENT_MEDIA = 0x00000200,
            CONNECT_COMMANDLINE = 0x00000800,
            CONNECT_CMD_SAVECRED = 0x00001000,
            CONNECT_CRED_RESET = 0x00002000
        }
        public enum umountDwFlags : uint     //    https://msdn.microsoft.com/en-us/library/windows/desktop/aa385353(v=vs.85).aspx
        {
            NO_OPTION = 0x00000000,
            CONNECT_UPDATE_PROFILE = 0x00000001
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NETRESOURCE
        {
            public dwScope scope = 0;
            public dwType type = 0;
            public dwDisplayType displayType = 0;
            public dwUsage usage = 0;
            public string lpLocalName = null;
            public string lpRemoteName = null;
            public string lpComment = null;
            public string lpProvider = null;
        };

        #endregion

        /// <summary>
        /// Map Drive
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="netShare"></param>
        /// <param name="localDrive"></param>
        public static void mapDrive(string username, string password, string netShare, string localDrive)
        {
            NETRESOURCE nr = new NETRESOURCE();
            nr.usage = dwUsage.RESOURCEUSAGE_CONNECTABLE;
            nr.type = dwType.RESOURCETYPE_DISK;
            nr.scope = dwScope.RESOURCE_GLOBALNET;
            nr.displayType = dwDisplayType.RESOURCEDISPLAYTYPE_SHARE;
            nr.lpLocalName = localDrive;
            nr.lpRemoteName = netShare;
            nr.lpProvider = null;
            nr.lpComment = null;

            int err = WNetAddConnection2(nr, password, username, (uint)dwFlags.CONNECT_UPDATE_PROFILE);

            if(err != 0)
            {
                RedShare.Network.ExceptionManagment.findException(err);
            }
        }
        /// <summary>
        /// Unmap Drive
        /// </summary>
        /// <param name="localdrive"></param>
        public static void unmapDrive(string localdrive)
        {
            int err = WNetCancelConnection2(localdrive, (uint)umountDwFlags.CONNECT_UPDATE_PROFILE, false);
            if(err != 0)
            {
                ExceptionManagment.findException(err);
            }
        }

        /// <summary>
        /// Unmap Drive
        /// </summary>
        /// <param name="localdrive"></param>
        /// <param name="force"></param>
        public static void unmapDrive(string localdrive, bool force)
        {
            int err = WNetCancelConnection2(localdrive, (uint)umountDwFlags.CONNECT_UPDATE_PROFILE, force);
            if (err != 0)
            {
                ExceptionManagment.findException(err);
            }
        }
    }
}
