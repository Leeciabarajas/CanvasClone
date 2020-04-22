using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeeSharpLMS.DataAccessLayer;
using SeeSharpLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTest
{
    [TestClass]
    public class SubmissionUploadTest
    {
        private readonly DataAccessClass _dataAccess;


        public SubmissionUploadTest()
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
        public void CreateSubmission()
        { 
        }


        [TestMethod]
        public void UpdateUploadTest()
        {
            int id = 38;
            int stid = 2;
            Submission submission = new Submission();
            submission = _dataAccess._context.Submission.FirstOrDefault(s => s.Id == id & s.StudentAssignmentId == stid);

            SeeSharpLMS.DataAccessLayer.DataAccessClass data = new SeeSharpLMS.DataAccessLayer.DataAccessClass();

            data.UpdateSubmission(submission);
            Assert.AreEqual(2, submission.StudentAssignmentId);
            Assert.AreEqual(null, submission.Link);
            Assert.AreEqual(DateTime.Now, submission.Date);
            Assert.AreEqual("You are genius!", submission.TextEntry);
            Assert.AreEqual("file.txt", submission.FilePath);

        }

        [TestMethod]
        public void CreateUploadTest()
        {
            //database connection
            //Submission submission = new Submission();
            //submission.StudentAssignmentId = 13;
            //submission.Link = "www.root.com";
            //submission.TextEntry = "Testing";
            //submission.Date = DateTime.Now;
            //submission.FilePath = "";

            //SeeSharpLMS.DataAccessLayer.DataAccessClass data = new DataAccessClass();
            //try
            //{
            //    data.AddSubmission(submission, 13);
            //}
            //catch (Exception e)
            //{
            //    System.Console.WriteLine(e);
            //}
            //Assert.AreEqual(13, submission.StudentAssignmentId);
            //Assert.AreEqual("www.root.com", submission.Link);
            //Assert.AreEqual("Testing", submission.TextEntry);
            //Assert.AreEqual(DateTime.Now, submission.Date);
            //Assert.AreEqual("", submission.FilePath);
        }


        [DataTestMethod]
        public void TestSubmissionRemove(int staId)
        {
            //var staId = _dataAccess._context.Submission.Find(staId);
            staId = 2;
            try
            {
                _dataAccess.RemoveSubmission(staId);
            }
            catch (Exception e)
            { 
                
            }
            Submission submission = _dataAccess._context.Submission.FirstOrDefault(s => s.StudentAssignmentId == staId); ;
            Assert.IsNull(submission);
        }
    }
}
