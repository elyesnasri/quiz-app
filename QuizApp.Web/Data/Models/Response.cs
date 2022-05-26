using System;
namespace QuizApp.Web.Data.Models
{
    public class Response
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool CorrectAnswer { get; set; }

        public Question? Question { get; set; }
    }
}

