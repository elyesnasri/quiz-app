using System;
using QuizApp.Web.Data;
using QuizApp.Web.Data.Models;

namespace QuizApp.Web.GraphQL;

public class Mutations
{
    public async Task<Guid> AddQuestion ([Service] ApplicationDbContext context, QuestionInput questionInput)
    {
        var responses = questionInput.Responses
            .Select (x => new Response
            {
                Text = x.Text,
                CorrectAnswer = x.CorrectAnswer
            })
            .ToList ();

        var question = new Question
        {
            Text = questionInput.Text,
            Responses = responses
        };

        var questionEntity = await context.Questions!.AddAsync (question);
        await context.SaveChangesAsync ();

        return questionEntity.Entity.Id;
    }
}

