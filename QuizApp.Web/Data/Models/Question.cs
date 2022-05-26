using System;
namespace QuizApp.Web.Data.Models;

public class Question
{
    public Guid Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public ICollection<Response> Responses { get; set; } = new List<Response> ();
}
