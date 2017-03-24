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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DHCPACK_Message;


namespace DHCPACK_Message.Tests
{
    [TestClass]
    public class DHCPACK_Message_Test
    {
        bool V_DRequest, IpAddFormat;
        bool V_Offer; bool expectedresult;
        DHCP_Server sDhcp = new DHCP_Server();
        DHCP_Client cDhcp = new DHCP_Client();


        [TestMethod]
        //This method return a failure if Ip Address or/and Domain Name are/is wrong 
        public void Test_Valid_IPaddress_DomainName_MAC_ReturnSuccess_OnScreen()
        {

            //Arrange 
            ////Operator chooses this values for his local machine.
            string IPAdd = "10.168.1.155";  //This Ip Address has the right format and 4 values without letters
            string Mask = "0";              //The subnet mac value is defautl 0.
            string DomainName = "cetitec.com";//This Domain Name is the defaut value: cetitec.com

            //Act
            IpAddFormat = cDhcp.CheckIPValidFormat(IPAdd);
            cDhcp = new DHCP_Client(IpAddFormat, IPAdd);
            V_DRequest = cDhcp.DhcpClient_Discover_Request(sDhcp, Mask, IpAddFormat, DomainName);
            V_Offer = sDhcp.DhcpServer_Offer(V_DRequest);
            expectedresult = sDhcp.DHCP_Ack_Message(V_Offer);

            //Assert...
            Assert.IsTrue(expectedresult);
        }

        [TestMethod]
        //This method test to make sure the Ip Address doesn't have letters. 
        public void Test_WrongIPaddressNonInt_ReturnFailureOnScreen()
        {

            //Arrange 
            ////Operator chooses this values for his local machine.
            string IPAdd = "10.168.X.1";  //The Ip Address has a wrong format, there is a letter.

            string Mask = "0";
            string DomainName = "cetitec.com";

            //Act
            IpAddFormat = cDhcp.CheckIPValidFormat(IPAdd);
            cDhcp = new DHCP_Client(IpAddFormat, IPAdd);
            V_DRequest = cDhcp.DhcpClient_Discover_Request(sDhcp, Mask, IpAddFormat, DomainName);
            V_Offer = sDhcp.DhcpServer_Offer(V_DRequest);
            expectedresult = sDhcp.DHCP_Ack_Message(V_Offer);
            
            //Assert...
            Assert.IsFalse(expectedresult);
        }

        [TestMethod]
        //This method test to make sure the Ip Address has 4 values. 
        public void Test_WrongIPaddressNot4Values_ReturnFailureOnScreen()
        {

            //Arrange 
            ////Operator chooses this values for his local machine.
            string IPAdd = "10.168.1";  //The Ip Address has a wrong format, there there are only 3 values.

            string Mask = "0";
            string DomainName = "cetitec.com";
                                  
            //Act
            IpAddFormat = cDhcp.CheckIPValidFormat(IPAdd);
            cDhcp = new DHCP_Client(IpAddFormat, IPAdd);
            V_DRequest = cDhcp.DhcpClient_Discover_Request(sDhcp, Mask, IpAddFormat, DomainName);
            V_Offer = sDhcp.DhcpServer_Offer(V_DRequest);
            expectedresult = sDhcp.DHCP_Ack_Message(V_Offer);

            //Assert...
            Assert.IsFalse(expectedresult);
       }

        [TestMethod]
        //This method tests to make sure the Subnet MAC address has the default 0: 
        public void Test_WrongMaskNotDefaultZero_ReturnFailureOnScreen()
        {

            //Arrange 
            ////Operator chooses this values for his local machine.
            string IPAdd = "10.168.1.155";  

            string Mask = "1"; //The subnet MAC address is diffrent than the default.
            string DomainName = "cetitec.com";

            //Act
            IpAddFormat = cDhcp.CheckIPValidFormat(IPAdd);
            cDhcp = new DHCP_Client(IpAddFormat, IPAdd);
            V_DRequest = cDhcp.DhcpClient_Discover_Request(sDhcp, Mask, IpAddFormat, DomainName);
            V_Offer = sDhcp.DhcpServer_Offer(V_DRequest);
            expectedresult = sDhcp.DHCP_Ack_Message(V_Offer);

            //Assert...
            Assert.IsFalse(expectedresult);
        }

        [TestMethod]
        //This method tests to make sure the DomainName is: cetitec.com 
        public void Test_WrongDomainName_ReturnFailureOnScreen()
        {

            //Arrange 
            ////Operator chooses this values for his local machine.
            string IPAdd = "10.168.1.155";
            string Mask = "0"; 

            string DomainName = "cetitec.org";//This Domain Name is not the same as the defaut: cetitec.com

            //Act
            IpAddFormat = cDhcp.CheckIPValidFormat(IPAdd);
            cDhcp = new DHCP_Client(IpAddFormat, IPAdd);
            V_DRequest = cDhcp.DhcpClient_Discover_Request(sDhcp, Mask, IpAddFormat, DomainName);
            V_Offer = sDhcp.DhcpServer_Offer(V_DRequest);
            expectedresult = sDhcp.DHCP_Ack_Message(V_Offer);

            //Assert...
            Assert.IsFalse(expectedresult);
        }

        [TestMethod]
        //This method returns a failure if the Ip Address or/and Domain Name are/is wrong 
        public void Test_WrongDomainNameandIpAddress_ReturnFailureOnScreen()
        {

            //Arrange 
            ////Operator chooses this values for his local machine.
            string IPAdd = "10.168.1";  //This Ip Address only has 3 values instead of 4

            string Mask = "0";
            string DomainName = "cetitec.org";//This Domain Name is not the same as the defaut: cetitec.com

            //Act
            IpAddFormat = cDhcp.CheckIPValidFormat(IPAdd);
            cDhcp = new DHCP_Client(IpAddFormat, IPAdd);
            V_DRequest = cDhcp.DhcpClient_Discover_Request(sDhcp, Mask, IpAddFormat, DomainName);
            V_Offer = sDhcp.DhcpServer_Offer(V_DRequest);
            expectedresult = sDhcp.DHCP_Ack_Message(V_Offer);

            //Assert...
            Assert.IsFalse(expectedresult);
        }

        [TestMethod]
        //This method return a failure if Ip Address has a value more than 255 
        public void Test_WrongFormatIPaddress_ReturnFailure_OnScreen()
        {

            //Arrange 
            ////Operator chooses this values for his local machine.
            string IPAdd = "10.168.1.300";  //This Ip Address only has 4 values but one of them is more 255
            string Mask = "0";
            string DomainName = "cetitec.com";

            //Act
            IpAddFormat = cDhcp.CheckIPValidFormat(IPAdd);
            cDhcp = new DHCP_Client(IpAddFormat, IPAdd);
            V_DRequest = cDhcp.DhcpClient_Discover_Request(sDhcp, Mask, IpAddFormat, DomainName);
            V_Offer = sDhcp.DhcpServer_Offer(V_DRequest);
            expectedresult = sDhcp.DHCP_Ack_Message(V_Offer);

            //Assert...
            Assert.IsFalse(expectedresult);
        }
    }
}
