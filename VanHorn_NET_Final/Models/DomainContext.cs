﻿using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using VanHorn_NET_Final.Models;

namespace VanHorn_NET_Final.Models
{
    public class DomainContext : DbContext
    {
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }

        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quiz>()
                .HasMany(e => e.Questions)
                .WithOne(q => q.Quiz)
                .HasForeignKey(e => e.QuestionId);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.OptionId);

            modelBuilder.Entity<Teacher>()
                .HasMany(q => q.Quizzes)
                .WithOne(t => t.Teacher)
                .HasForeignKey(q => q.QuizId);

            modelBuilder.Entity<Student>()
                .HasMany(q => q.Quizzes)
                .WithOne(s => s.Student)
                .HasForeignKey(q => q.QuizId);
        }
    }
}

