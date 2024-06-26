﻿//Peter Van Horn
//.NET Final Project
//05/03/2024
//quiz is the parent object, each quiz has a list of questions and each question has a list of options.

namespace VanHorn_NET_Final.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int? TeacherId { get; set; }
    }
}
