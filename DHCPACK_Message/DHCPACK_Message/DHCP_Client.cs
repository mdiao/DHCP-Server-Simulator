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
    public class DHCP_Client
    {
        #region "Variable to Call"
        private string NetCard, MacMask="0";
        public string[] IpFormat;
        char[] delimiter = { '.' };
        public int number;
        bool IpFormatValid;
        #endregion

        //This method verifies that the Ip address has the correct format
        public bool CheckIPValidFormat(string Ipvalue)
        {
            
            try
            {
                IpFormat = Ipvalue.Split(delimiter);
                if (IpFormat.Length == 4)
                {

                    for (int i = 0; i < IpFormat.Length; i++)
                    {
                        if (Int32.TryParse(IpFormat[i], out number) && number <=255)
                        {
                            IpFormatValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Make sure that the IP address has Four values separated by dots '.'\n");
                            IpFormatValid = false;
                            return IpFormatValid;
                        }
                    }
                }
                else
                {
                    IpFormatValid = false;
                    Console.WriteLine("Make sure that the IP address has Four values separated by dots '.'\n");
                    
                }
                    
            }
            catch (Exception)
            {
                Console.WriteLine("Make sure that the IP address has Four values separated by dots '.'\n");
                IpFormatValid = false;
            }

            return IpFormatValid;
        }
        //This method is the constructor for the DHCP Client Class
        public DHCP_Client()
        {

        }
        //This method override the constructor if the Ip address is valid
        public DHCP_Client(bool valid, string NetCard1)
        {
            if (valid == true)
            {
                NetCard = NetCard1;
            }
            else
                Console.WriteLine("The Ip address:" + NetCard1 + " Format is invalid:");
            
        }
        //string property to contain the class name
        private string ClassName
        {
            get { return "DHCP_Client"; }
        }

        //This Method makes the DHCP client broadcast a message of request of an IP address to the DHCP Server
        public bool DhcpClient_Discover_Request(DHCP_Server DataServer, string MacId, bool IpValidation, string DomainName)
        {
            string str = string.Empty;
            bool DHCP_Discover; // DHCP_Discover=true ---> DHCP client is looking for DHCP server to lease an IP address
                                // DHCP_Discover= false---> DHCP clien information is not valide to lease an IP address from a Server
            
            //This condition verifies that the MAC used is the default value 0, the Ip address format is verified and the domain name: cetitec.com
            if (MacId.StartsWith(MacMask) == true && IpValidation == true && (DomainName.Equals("cetitec.com") == true)) 
            {
                DHCP_Discover = true;
                DataServer.dData.MyIP = NetCard;// This informs the DHCP Server, the IP address of the DHCP client which 
                                                //broadcasted the message of request                 
                str = "IP requested for Mac: " + MacId;
            }
            //This condition informs the operator that the MAC is not the correct one
            else if (MacId.StartsWith(MacMask) == false && IpValidation == true)
            {
                DHCP_Discover = false;
                str = "Mac: " + MacId + " is not part of the mask, you should chooose a subnet mask = 0!";

            }
            //This condition informs the operator that the Domain name is not the correct one which should be cetitec.com.
            else if((DomainName.Equals("cetitec.com") == false))
            {
                DHCP_Discover = false;
                Console.WriteLine("Make sure that the DomainName is: cetitec.com \n");
            }
            //This condition informs the operator that something is wrong with the IP address he entered earlier
            else 
            {
                DHCP_Discover = false;
                Console.WriteLine("Make sure that the IP address has Four values separated by dots '.'\n");
            }
          
            Console.WriteLine(str);     
            return DHCP_Discover;
        }

    }
}
