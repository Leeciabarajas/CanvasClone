using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpLMS.DataAccessLayer;
using SeeSharpLMS.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MSTest
{
    [TestClass]
    public class SectionTests
    {
        private readonly DataAccessClass _dataAccess;
        
        public SectionTests()
        {
            _dataAccess = new DataAccessClass();
        }

        [TestInitialize]
        public void Setup()
        {

        }
        
        [TestCleanup]
        public void TearDown()
        {

        }
        
        
        [TestMethod]
        public void CreateSectionTest()
        {
            //databse connection 
            
            
            Section section = new Section();

            section.InstructorId = "c5659548-0794-44b9-9441-e462ab2f8b17";
            section.Location = "online";
            section.StartTime = new TimeSpan(5,30,0);
            section.EndTime = new TimeSpan(7, 30, 0);
            section.TermId = 2;
            section.Days = "MWF";
            section.EnrollmentCap = 15;
            section.CourseId = 5;

            SeeSharpLMS.DataAccessLayer.DataAccessClass data = new DataAccessClass();
            try
            {


                data.AddSection(section);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e);
            }
            Assert.AreEqual("c5659548-0794-44b9-9441-e462ab2f8b17", section.InstructorId);
            Assert.AreEqual("online", section.Location);
            Assert.AreEqual(new TimeSpan(5, 30, 0), section.StartTime);
            Assert.AreEqual(new TimeSpan(7, 30, 0), section.EndTime);
            Assert.AreEqual(2, section.TermId);
            Assert.AreEqual("MWF", section.Days);
            Assert.AreEqual(15, section.EnrollmentCap);
            Assert.AreEqual(5, section.CourseId);
        }

        [TestMethod]
        public void UpdateSectionTest()
        {
            Section section = _dataAccess._context.Section.FirstOrDefault(x => x.Id == 29);

            section.InstructorId = "485fda5a-00e6-4d2a-85c0-4cf8fbbc98ce";
            section.Location = "TEST ROOM";
            section.StartTime = new TimeSpan(8, 45, 0);
            section.EndTime = new TimeSpan(9, 50, 0);
            section.TermId = 2;
            section.Days = "TR";
            section.EnrollmentCap = 55;
            section.CourseId = 9;

            SeeSharpLMS.DataAccessLayer.DataAccessClass data = new DataAccessClass();

            data.UpdateSection(section);

            Assert.AreEqual("485fda5a-00e6-4d2a-85c0-4cf8fbbc98ce", section.InstructorId);
            Assert.AreEqual("TEST ROOM", section.Location);
            Assert.AreEqual(new TimeSpan(8, 45, 0), section.StartTime);
            Assert.AreEqual(new TimeSpan(9, 50, 0), section.EndTime);
            Assert.AreEqual(2, section.TermId);
            Assert.AreEqual("TR", section.Days);
            Assert.AreEqual(55, section.EnrollmentCap);
            Assert.AreEqual(9, section.CourseId);
        }
    }
}
