﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VanHorn_NET_Final.Models;

namespace VanHorn_NET_Final.Pages.Quizzes
{
    [Authorize(Policy = "TeacherOnly")]
    public class CreateModel : PageModel
    {
        private readonly VanHorn_NET_Final.Models.DomainContext _context;

        public CreateModel(VanHorn_NET_Final.Models.DomainContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Quiz Quiz { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Quizzes.Add(Quiz);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}