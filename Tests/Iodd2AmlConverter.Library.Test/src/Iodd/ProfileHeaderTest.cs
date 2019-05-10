using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test.Iodd
{

    [TestClass]
    public class ProfileHeaderTest
    {

        private const string XmlText =
            @"<ProfileHeader>
                <ProfileIdentification>IO Device Profile</ProfileIdentification>
                <ProfileRevision>1.1</ProfileRevision>
                <ProfileName>Device Profile for IO Devices</ProfileName>
                <ProfileSource>IO-Link Consortium</ProfileSource>
                <ProfileClassID>Device</ProfileClassID>
                <ISO15745Reference>
                  <ISO15745Part>1</ISO15745Part>
                  <ISO15745Edition>1</ISO15745Edition>
                  <ProfileTechnology>IODD</ProfileTechnology>
                </ISO15745Reference>
            </ProfileHeader>";

        private ProfileHeader Header { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Header = new ProfileHeader();
            Header.Deserialize(XElement.Parse(XmlText));
        }

        [TestMethod]
        public void HasCorrectProfileIdentification()
        {
            Assert.AreEqual("IO Device Profile", Header.ProfileIdentification);
        }

        [TestMethod]
        public void HasCorrectProfileRevision()
        {
            Assert.AreEqual("1.1", Header.ProfileRevision);
        }

        [TestMethod]
        public void HasCorrectProfileName()
        {
            Assert.AreEqual("Device Profile for IO Devices", Header.ProfileName);
        }

        [TestMethod]
        public void HasCorrectProfileSource()
        {
            Assert.AreEqual("IO-Link Consortium", Header.ProfileSource);
        }

        [TestMethod]
        public void HasCorrectProfileClassId()
        {
            Assert.AreEqual("Device", Header.ProfileClassId);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Header.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}