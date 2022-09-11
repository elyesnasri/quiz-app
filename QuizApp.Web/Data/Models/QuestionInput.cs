using System;

namespace QuizApp.Web.Data.Models;

public record QuestionInput (
    string Text,
    IReadOnlyList<ResponseInput> Responses)
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Text))
        {
            throw new GraphQLException ("The text must not be null or empty");
        }

        foreach (var response in Responses)
        {
            response.Validate ();
        }
    }
}

