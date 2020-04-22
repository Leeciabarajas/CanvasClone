# seesharp

<h3>Test Accounts</h3>
 Instructor:

instructor@gmai.com; Admin123$

Student:

student@gmail.com; Admin123$

<h3>Identity Info:</h3>

It appears that Identities also uses sessions, but in a different way than just setting Session variables

Also, it looks like Identities is using a GUID string id for users right now, so all references to InstructorIds and StudentIds use that string Id as a ForeignKey. The ApplicationUser class has a DisplayId which is a simple integer that increments with each user for our Profile "W Number". ApplicationUser also has a description field for the Profile page.

<h3>How to Use Identities:</h3>

On the cshtml pages, you need to put this line:
"@using SeeSharpLMS.Utility" after the "@page" directive to pull in the roles 'InstructorRole' and 'StudentRole'

To block off sections of a cshtml page based on the current user's role, you can do something like the following:
"@if(User.IsInRole(SD.InstructorRole))
{
}"

To get the current user's information for a page, you need this:

//declared at the class level

private readonly UserManager<IdentityUser> _userManager; 
public ApplicationUser user;

//The page constructor uses 'Dependency Injection' by having a paramater of the UserManager<IdentityUser>
  
public LandingPageModel(Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
      {
          _context = context;
          _userManager = userManager;
      }
  
 //set in an OnGet or OnPost Method
 //This goes to the User Table, and will get the user that matches the current user's id so that you can access FirstName, Description, Etc.
 
 user = _context.ApplicationUser.FirstOrDefault(u => u.Id == _userManager.GetUserId(User));
 
 FirstOrDefault can return a null object if nothing matches the comparison criteria, so we should always check for that and display an error to the user if true.
