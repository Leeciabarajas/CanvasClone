﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SeeSharpLMS.Data;
using SeeSharpLMS.Models;

namespace SeeSharpLMS
{
    public class DetailsModel : PageModel
    {
        private readonly SeeSharpLMS.Data.ApplicationDbContext _context;

        public DetailsModel(SeeSharpLMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public string FilePath { get; set; }
        public Assignment Assignment { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assignment = await _context.Assignment
                .Include(a => a.AssignmentType)
                .Include(a => a.Section).FirstOrDefaultAsync(m => m.Id == id);

            if (Assignment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
