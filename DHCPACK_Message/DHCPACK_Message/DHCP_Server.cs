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
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DHCPACK_Message
{
    public class DHCP_Server
    {
        #region "Variables to Call"
        public DHCPData dData; //This is a structure which contains the relevant information that 
                               //the DHCP server needs to provide to the DCHP client after the request.
        
        //This table contains few Ip address that the DHCP server can provide to the DHCP client and that are not in use.
        public string[] AvailableIP = new string[] { "192.168.1.30", "192.168.1.128", "128.192.1.28", "192.168.1.255", "128.130.1.32", "192.168.1.58", "10.61.33.110","10.1.1.13","128.1.110.55" };

        string NextLine = "\n";
        #endregion

        #region "Structures"

        public struct DHCPData
        {
            public string IPAddr;         //Ip address that the DHCP Server provides to DHCP client after a request
            public string SubMask;        //Subnet Mask, here the default used is 0
            public uint LeaseTime;        //The default value is: 86400 sec (24h), the time that the DHCP client
                                          //can use the IP address allocate by the DHCP server without renewing it.
            public string ServerName;     //This is the server name
            public string MyIP;           //this is the Ip address of the DHCP client entered into the main 
                                          //program and the DHCP Server will use to identify the client which the request of an IP
            public string RouterIP;       //This is the network router ip address
            public string DomainName;     //This is the domain name that is provided by the Dhcp client to the DHCP 
                                          //server that he will use to allocate the IP address
            public string LogServerIP;    //This is the ServerIP address that we are logged in.
        }
        #endregion

        //This method will pick one random Ip address from the Ip address table
        //and give it to the client as the allocated temporary Ip address.
        //This method could have been more realistic by using the System.Net function such as Ping
        //or IPEndpoint or IPAddress however due to a lack of an environment (network) available to me, I couldn't build it
        //however I know how to do it.
        public string GetIPAdd()
        {
            Random rand = new Random();
            int i = rand.Next(0, AvailableIP.Length - 1);
            return AvailableIP[i];
        }
        


        //Through this method the DHCP Server responds to the DHCP client by offering an Ip Address
        public bool DhcpServer_Offer(bool DiscoveryStatus)
        {
            string str = string.Empty;
            bool Ack_Message; //This variable confirms that the DHCP Server has allocated a valid IP address with the needed information

            if (DiscoveryStatus == true)
            {
                dData.IPAddr = GetIPAdd();
                dData.SubMask = "255.255.255.0";
                dData.LeaseTime = 86400;
                dData.ServerName = "DHCP Server Simulator";
                dData.RouterIP = "0.0.0.0";
                dData.LogServerIP = "1.0.0.0";
                dData.DomainName = "cetitec.com";
                Ack_Message = true;
                str = "IP Address request from the DHCP client to the DHCP Server: SUCCEEDED";


            }
            else
            {
                str = "IP Address request from the DHCP client to the DHCP Server: FAILED";
                Ack_Message = false;
            }
            Console.WriteLine(str);
            return Ack_Message;
        }


        //This message displays the result of request made by the DHCP Client to the DHCP Server, whether it failed or passed
        public bool DHCP_Ack_Message(bool Ack_Message)
        {
            if (Ack_Message==true)
            {
                Console.WriteLine(NextLine + "Client IP address:" + dData.IPAddr + NextLine + "Lease Time in Sec:" + dData.LeaseTime + NextLine + "Option-Domain Name Server:" + dData.ServerName
                    + NextLine + "Router IP:" + dData.RouterIP + NextLine + "Server IP Address:" + dData.LogServerIP + NextLine + "Your IP address:"+ dData.MyIP 
                    + NextLine + "Option-Subnet Mask:" + dData.SubMask + NextLine + "Option-Domain Name Server:" + dData.DomainName + NextLine);
            }
            return Ack_Message;
        }
        
    }
}
