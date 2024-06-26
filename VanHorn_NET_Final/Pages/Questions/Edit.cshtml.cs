﻿//Peter Van Horn
//.NET Final Project
//05/03/2024
//question edit allows for editing and adding options as well

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VanHorn_NET_Final.Models;

namespace VanHorn_NET_Final.Pages.Questions
{
    [Authorize(Policy = "TeacherOnly")]
    public class EditModel : PageModel
    {
        private readonly VanHorn_NET_Final.Models.DomainContext _context;

        public EditModel(VanHorn_NET_Final.Models.DomainContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; } = default!;
        public IList<Option> Options { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Options = await _context.Options.ToListAsync();
            if (id == null)
            {
                return NotFound();
            }

            var question =  await _context.Questions.FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }
            Question = question;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int quizId)
        {
            Question.QuizId = quizId;

            _context.Attach(Question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Question.QuestionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("/Options/Create", new { questionId = Question.QuestionId });
        }
        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.QuestionId == id);
        }
    }
}
