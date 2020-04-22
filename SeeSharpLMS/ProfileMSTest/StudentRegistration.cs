using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpLMS.DataAccessLayer;
using SeeSharpLMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTest
{
    //I'm not quite sure how to run these test right now
    [TestClass]
    public class StudentRegistration
    {
        private readonly DataAccessClass _dataAccess;

        public StudentRegistration()
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

        [DataTestMethod]
        [DataRow("e5b095bc-b8fb-4728-96dd-18d5b9c695c1", 23)]//Good test
        [DataRow("e5b095bc-b8fb-4728-96dd-18d5b9c695c1", 300)]//bad test
        [DataRow("test", 100)]//garbage data
        public void TestRegisterAdd(string userId, int sectionId)
        {
            //get the user from the db
            var user = _dataAccess._context.ApplicationUser.Find(userId);
            //add the enrollment for the student

            Enrollment enrollment = new Enrollment()
            {
                SectionId = sectionId,
                StudentId = userId,
            };

            _dataAccess.AddEnrollment(enrollment);

            enrollment = _dataAccess._context.Enrollment.Find(enrollment);

            //verify that the enrollment was added properly
            Assert.IsNotNull(enrollment);
        }
    }
}
