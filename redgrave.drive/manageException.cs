using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedShare.Network
{
    public static class ExceptionManagment
    {
        public static void findException(int err)
        {
            switch (err)
            {
                case 5: throw new RedShare.Network.AccessDeniedException("Access is denied.");
                case 85: throw new RedShare.Network.DriveAlreadyExistsException("The local device name is already in use.");
                case 66: throw new RedShare.Network.BadDeviceTypeException("The network resource type is not correct.");
                case 67: throw new RedShare.Network.BadNetworkName("The network name cannot be found.");
                case 86: throw new RedShare.Network.InvalidPasswordException("The specified network password is not correct.");
                case 87: throw new RedShare.Network.BadNetworkName("The parameter is incorrect.");
                case 170: throw new RedShare.Network.BusyRessourceException("The requested resource is in use.");
                case 487: throw new RedShare.Network.InvalidAddressException("Attempt to access invalid address.");
                case 1200: throw new RedShare.Network.BadDeviceException("The specified device name is invalid.");
                case 1202: throw new RedShare.Network.DeviceAlreadyRemembered("The local device name has a remembered connection to another network resource.");
                case 1203: throw new RedShare.Network.NoNetworkExceptionOrBadPath("The network path was either typed incorrectly, does not exist, or the network provider is not currently available. Please try retyping the path or contact your network administrator.");
                case 1204 :throw new RedShare.Network.BadProviderException("The specified network provider name is invalid.");
                case 1205: throw new RedShare.Network.BadUsernameException("Unable to open the network connection profile.");
                case 1206: throw new RedShare.Network.BadProfileException("The network connection profile is corrupted.");
                case 1208: throw new RedShare.Network.BadProfileException("An extended error has occurred.");
                case 1219: throw new RedShare.Network.CredentialConflictException("Multiple connections to a server or shared resource by the same user, using more than one user name, are not allowed. Disconnect all previous connections to the server or shared resource and try again.");
                case 1222: throw new RedShare.Network.NoNetworkException("The network is not present or not started");
                case 1223: throw new RedShare.Network.ConnectionCancelledException("The operation was canceled by the user.");
                case 1326: throw new RedShare.Network.LogonFailureException("The user name or password is incorrect.");
                case 2202: throw new RedShare.Network.BadUsernameException("The specified username is invalid.");

                default:throw new UnknownException("Something went wrong - System Error : " + err);
            }
        }
    }
}
