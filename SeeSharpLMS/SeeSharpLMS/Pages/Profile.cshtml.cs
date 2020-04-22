using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeeSharpLMS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using SeeSharpLMS.Utility;
using System;
using SeeSharpLMS.Infrastructure;
using Microsoft.AspNetCore.Hosting;

namespace SeeSharpLMS
{
    public class ProfileModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;

        private IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationUser user { get; set; }
        private readonly Data.ApplicationDbContext _context;

        //List to store ProfileLink information.
        public List<ProfileLink> Links;
        //public int newLinkCount;



        public ProfileModel(
            Data.ApplicationDbContext context, 
            UserManager<IdentityUser> userManager
            ,IUnitOfWork unitOfWork
            , IHostingEnvironment hostingEnvironment
            )
        { 
            _context = context;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;

        }

        public InputModel Input { get; set; }

        public class InputModel {

            public string ImagePath { get; set; }
        }


        public void OnGet()
        {

            user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            
            //If the user is an instructor, creates a list of ProfileLinks.
            if (User.IsInRole(SD.InstructorRole))
            {
                Links = (_context.ProfileLink.Where(x => x.InstructorId == user.Id)).ToList();
            }
        }

        public IActionResult OnPost(IFormFile file)
        {
            //var updateUser = _context.ApplicationUser.FirstOrDefault(x => x.Id == user.Id);
            var updateUser = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
            //updateUser.FirstName = Request.Form["EditFirstName"].ToString();
            //updateUser.LastName = Request.Form["EditLastName"].ToString();
            //updateUser.Email = Request.Form["EditEmail"].ToString();
            //updateUser.UserName = Request.Form["EditEmail"].ToString();
            //updateUser.NormalizedEmail = Request.Form["EditEmail"].ToString().ToUpper();
            //updateUser.NormalizedUserName = Request.Form["EditEmail"].ToString().ToUpper();
            //updateUser.Description = Request.Form["EditBio"].ToString();
            //_context.SaveChanges();
            ////var updateLink = new ProfileLink();
            DataAccessLayer.DataAccessClass dbAccess = new DataAccessLayer.DataAccessClass();

            dbAccess.UpdateUser(updateUser, Request.Form["EditFirstName"].ToString(), Request.Form["EditLastName"].ToString(), Request.Form["EditEmail"].ToString(), Request.Form["EditBio"].ToString());


            Links = (_context.ProfileLink.Where(x => x.InstructorId == updateUser.Id)).ToList();

            //If the user has any links, creates a new profile link object
            //then fills that object with a link from the list
            //and updates the links in the database

            //Upload the file
            if (file != null)
            {
                _unitOfWork.UploadImage(file);
                updateUser.ImagePath = file.FileName;
            }
            _context.SaveChanges();


            if (Links.Count > 0)
            {
                //Creates the profile link object.
                var updateLink = new ProfileLink();

                //For each link in the list, 
                foreach (var x in Links)
                {
                    //Fill the updatelink object with information from the list.
                    updateLink = _context.ProfileLink.FirstOrDefault(l => l.Id == x.Id);

                    //If the input that matches that link is empty the link is deleted.
                    if (Request.Form[x.Id.ToString()].ToString().Length == 0)
                    {
                        _context.ProfileLink.Remove(updateLink);
                    }
                    //Else the link is updated with new information.
                    else
                    {
                        updateLink.Link = Request.Form[x.Id.ToString()].ToString();
                    }
                    //Save changes to the links.
                    _context.SaveChanges();
                }
            }

            //For each new link input field, adds new links to the database.
            for (int i = 0; i < 5; i++)
            {
                //Create new ProfileLink to add to database.
                var updateLink = new ProfileLink();

                //If the input field is not empty,
                if (Request.Form["NewLinkId" + i].ToString().Length > 0)
                {
                    //assigns the user's id to the InstructorId,
                    updateLink.InstructorId = updateUser.Id;

                    //adds the input field's information to the ProfileLink,
                    updateLink.Link = Request.Form["NewLinkId" + i].ToString();

                    //and adds the new link to the datatable and saves the changes.
                    _context.ProfileLink.Add(updateLink);
                    _context.SaveChanges();
                }
            }
            return RedirectToPage("/Profile");

        }

    }
}