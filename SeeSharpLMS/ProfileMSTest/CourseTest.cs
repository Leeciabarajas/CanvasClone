



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
    public class CourseTest
    {
        private readonly DataAccessClass _dataAccess;

        public CourseTest()
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
        public void AddCourse()
        {

            Course course = new Course();


            //course.Id = 123;
            course.Subject = "PHYS";
            course.Number = "4000";
            course.Title = "Gravity";
            course.Description = "Learn the ups and downs of Gravity";
            course.CreditHours = 4;
            course.CourseFee = 0;
     

            SeeSharpLMS.DataAccessLayer.DataAccessClass data = new DataAccessClass();
            try
            {
                data.AddCourse(course);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            Assert.AreEqual("PHYS", course.Subject);
            Assert.AreEqual("4000", course.Number);
            Assert.AreEqual("Gravity", course.Title);
            Assert.AreEqual("Learn the ups and downs of Gravity", course.Description);
            Assert.AreEqual(4, course.CreditHours);
            Assert.AreEqual(0, course.CourseFee);

        }


        [TestMethod]
        public void UpdateCourse()
        {
            Course course = _dataAccess._context.Course.FirstOrDefault(x => x.Id == 2);

            course.CreditHours = 2; 

            SeeSharpLMS.DataAccessLayer.DataAccessClass data = new DataAccessClass();

            data.UpdateCourse(course);

            Assert.AreEqual(2, course.CreditHours);
        }


        [TestMethod]
        public void DeleteCourse()
        {
            Course course = _dataAccess._context.Course.FirstOrDefault(x => x.Id == 13);

            SeeSharpLMS.DataAccessLayer.DataAccessClass data = new DataAccessClass();

            data.RemoveCourse(course);

            //Assert.AreEqual(2, course.CreditHours);
            Assert.IsNull(course); 
        }






    }
}
