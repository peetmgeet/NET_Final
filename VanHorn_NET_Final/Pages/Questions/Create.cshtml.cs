﻿//Peter Van Horn
//.NET Final Project
//05/03/2024
//creating a quiz starts the path of creating the first question and its options.
//the path leads back to the quiz details where teacher can add more questions and options.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using VanHorn_NET_Final.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace VanHorn_NET_Final.Pages.Questions
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
        public Question Question { get; set; } = default!;

        [BindProperty]
        public int QuizId { get; set; }

        public async Task<IActionResult> OnPostAsync(int quizId)
        {
            Question question = new Question
            {
                QuizId = quizId,
                QuestionText = Question.QuestionText
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Options/Create", new { questionId = question.QuestionId, quizId});
        }
    }
}