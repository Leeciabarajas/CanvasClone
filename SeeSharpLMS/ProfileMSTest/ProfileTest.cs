using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeeSharpLMS.Models;
using SeeSharpLMS.DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SeeSharpLMS.Utility;
using SeeSharpLMS.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ProfileMSTest
{
    [TestClass]
    public class ProfileTest
    {
        //databse connection 
        private readonly DataAccessClass _dataAccess;

        //a useer we will look at 
        public ApplicationUser user; 

        private readonly UserManager<IdentityUser> _userManager;

        

        [TestInitialize]
        public void Setup() { 

        }

        [TestCleanup]
        public void TearDown() { 
        
        }


        public ProfileTest() {
            //TODO: move the code below to the common place and try to create everthing only once
            // instead of creating everthing on every test call. 
            // or change in the main project SeeSharpLMS.
            _dataAccess = new DataAccessClass();
        }

        [TestMethod]
        
        public void TestUpdateUser() {
            
            //the id i want to find (our student account)
            string id = "992b71f1-8438-40c1-acde-05d77bd3fa9b"; // primary key id 
            // int id = 7;//display ID 

            //************How do I grab a specific student using the ID assigend above*************
            user = _dataAccess._context.ApplicationUser.FirstOrDefault(u => u.Id == id); 


            //instance of dataAccess Layer 
            SeeSharpLMS.DataAccessLayer.DataAccessClass data = new SeeSharpLMS.DataAccessLayer.DataAccessClass();

            //update user information with this stuff
            data.UpdateUser(user, "Jane1", "Deer", "Student@gmail.com", "Hi, I am student!");

            //compare what is supposed to be with user from database 
            Assert.AreEqual("Jane1", user.FirstName);
            Assert.AreEqual("Deer", user.LastName);
            Assert.AreEqual("Student@gmail.com", user.Email);
            Assert.AreEqual("Hi, I am student!", user.Description);



        }

        // Data driven unit test
        [DataTestMethod]
        [DataRow("992b71f1-8438-40c1-acde-05d77bd3fa9b")]
        [DataRow("Bad Data")] //TODO: Try to create a more generic data driven unit test, then replace Bad Data
        public void TestUpdateUserWithData(string id)
        {
            //TODO: notice the parameterized test.
            //the id i want to find (our student account)
             // primary key id 
            // int id = 7;//display ID 

            //************How do I grab a specific student using the ID assigend above*************
            user = _dataAccess._context.ApplicationUser.FirstOrDefault(u => u.Id == id);


            //instance of dataAccess Layer 
            SeeSharpLMS.DataAccessLayer.DataAccessClass data = new SeeSharpLMS.DataAccessLayer.DataAccessClass();

            //update user information with this stuff
            data.UpdateUser(user, "Jane1", "Deer", "Student@gmail.com", "Hi, I am student!");

            //compare what is supposed to be with user from database 
            Assert.AreEqual("Jane1", user.FirstName);
            Assert.AreEqual("Deer", user.LastName);
            Assert.AreEqual("Student@gmail.com", user.Email);
            Assert.AreEqual("Hi, I am student!", user.Description);



        }

        [TestMethod]
        
        public void CreateUserInstructor() { 
            
        
        }

        [TestMethod]
        public void CreateStudentTest() { 
        }

        //THIS NEEDS TO BE IN ANOTHER CLASS BUT I DON'T KNOW HOW TO RUN THAT RIGHT NOW
        [DataTestMethod]
        [DataRow("e5b095bc-b8fb-4728-96dd-18d5b9c695c1", 23)]//Good test
        [DataRow("e5b095bc-b8fb-4728-96dd-18d5b9c695c1", 300)]//bad test
        [DataRow("test", 100)]//garbage data
        public void TestEnrollmentAdd(string userId, int sectionId)
        {
            //get the user from the db
            var user = _dataAccess._context.ApplicationUser.Find(userId);
            //add the enrollment for the student

            try
            {
                _dataAccess.AddEnrollment(user, sectionId);
            }
            catch(Exception e)
            {

            }
            

            Enrollment enrollment = _dataAccess._context.Enrollment.FirstOrDefault(e => e.SectionId == sectionId && e.StudentId == userId);

            //verify that the enrollment was added properly
            Assert.IsNotNull(enrollment);
        }

        //THIS NEEDS TO BE IN ANOTHER CLASS BUT I DON'T KNOW HOW TO RUN THAT RIGHT NOW
        [DataTestMethod]
        [DataRow("e5b095bc-b8fb-4728-96dd-18d5b9c695c1", 26)]//Good test
        public void TestEnrollmentRemove(string userId, int sectionId)
        {
            //get the user from the db
            var user = _dataAccess._context.ApplicationUser.Find(userId);
            //add the enrollment for the student

            try
            {
                _dataAccess.RemoveEnrollment(user, sectionId);
            }
            catch (Exception e)
            {

            }

            Enrollment enrollment = _dataAccess._context.Enrollment.FirstOrDefault(e => e.SectionId == sectionId && e.StudentId == userId);

            //verify that the enrollment was removed properly
            Assert.IsNull(enrollment);
        }


    }
}
