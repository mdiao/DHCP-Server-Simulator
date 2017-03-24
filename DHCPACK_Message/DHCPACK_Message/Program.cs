#region Copyright Information
/*
 * (C)  20016-2017, Mamadou Diao Bah
 *
 * For more information, please contact me at:
 * badiawo@gmail.com 
 * 
 * 
 * 
 */
#endregion




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHCPACK_Message
{
    class Program
    {

        static void Main(string[] args)
        {   
            
            //Arrange 
            string IPAdd = "10.168.1.1", Mask = "0", DomainName = "cetitec.com";//Operator chooses this too value for his local machine.

            bool V_DRequest, IpAddFormat;
            bool V_Offer;

            DHCP_Client cDhcp;
            DHCP_Server sDhcp;
            
            sDhcp = new DHCP_Server();
            cDhcp = new DHCP_Client();

            //Act
            IpAddFormat = cDhcp.CheckIPValidFormat(IPAdd);
            cDhcp = new DHCP_Client(IpAddFormat, IPAdd);
            V_DRequest = cDhcp.DhcpClient_Discover_Request(sDhcp, Mask, IpAddFormat, DomainName);
            V_Offer = sDhcp.DhcpServer_Offer(V_DRequest);

            //Assert...
            sDhcp.DHCP_Ack_Message(V_Offer);

            
            
        }
    }
}
