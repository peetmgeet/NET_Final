﻿//Peter Van Horn
//.NET Final Project
//05/03/2024
//each question of a quiz is a Submission/edit page, so the quiz taker hops
//through a series of submission/edit pages to answer each question

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using VanHorn_NET_Final.Models;

namespace VanHorn_NET_Final.Pages.Submissions
{
    public class EditModel : PageModel
    {
        private readonly VanHorn_NET_Final.Models.DomainContext _context;

        public EditModel(VanHorn_NET_Final.Models.DomainContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Submission Submission { get; set; } = default!;
        [BindProperty]
        public int SelectedOptionId { get; set; }
        [BindProperty]
        public IList<Question> Questions { get; set; }
        public IList<Option> Options { get; set; }
        [BindProperty]
        public int QuestionCount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? quizId, int questionCount)
        {
            var submission = await _context.Submissions
                .FirstOrDefaultAsync(m => m.SubId == id);
            Questions = await _context.Questions
                .Where(q => q.QuizId == quizId).ToListAsync();
            Options = await _context.Options.ToListAsync();
            QuestionCount = questionCount;
            if (id == null)
            {
                return NotFound();
            }

            if (submission == null)
            {
                return NotFound();
            }
            
            Submission = submission;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedOptionId == 0)
            {
                ModelState.AddModelError(string.Empty, "Please select an option.");
                return Page();
            }
            QuestionCount++;
            if (Submission.Answers == null)
            {
                Submission.Answers = new List<Answer>();
            }
            var selectedOption = await _context.Options.FindAsync(SelectedOptionId);
            Answer answer = new Answer
            {
                AnswerText = selectedOption.OptionText,
                Correct = selectedOption.Correct
            };
            Submission.Answers.Add(answer);
            _context.Attach(Submission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmissionExists(Submission.SubId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("/Submissions/Edit", new { id = Submission.SubId, quizId = Submission.QuizId, QuestionCount });
        }

        private bool SubmissionExists(int id)
        {
            return _context.Submissions.Any(e => e.SubId == id);
        }
    }
}