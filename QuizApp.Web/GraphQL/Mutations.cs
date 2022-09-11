using System;
using Microsoft.EntityFrameworkCore;
using QuizApp.Web.Data;
using QuizApp.Web.Data.Models;

namespace QuizApp.Web.GraphQL;

public class Mutations
{
    public async Task<Guid> AddQuestion ([Service] ApplicationDbContext context,
        QuestionInput questionInput)
    {
        questionInput.Validate ();

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

    public async Task<string> UpdateResponses ([Service] ApplicationDbContext context,
        UpdatedResponsesInput[] updatedResponses)
    {       
        foreach (var updatedResponse in updatedResponses)
        {
            updatedResponse.Validate ();

            var response = await context.Responses!.SingleOrDefaultAsync (x => x.Id == updatedResponse.ResponseId);

            if (response == null)
                throw new GraphQLException ($"No Reponse with id {updatedResponse.ResponseId} was found");

            response.Text = updatedResponse.Text;
            response.CorrectAnswer = updatedResponse.CorrectAnswer;

            context.Responses!.Update(response);
        }       
       
        await context.SaveChangesAsync ();

        return "Updated responses susccesfully";
    }
}

