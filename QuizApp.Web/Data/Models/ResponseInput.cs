namespace QuizApp.Web.Data.Models;

public record ResponseInput (
        string Text,
        bool CorrectAnswer)
{
    public void Validate ()
    {
        if (string.IsNullOrWhiteSpace (Text))
        {
            throw new GraphQLException ("The text must not be null or empty");
        }
    }
}

public record UpdatedResponsesInput (
    Guid ResponseId,
    string Text,
    bool CorrectAnswer)
{
    public void Validate ()
    {
        if (string.IsNullOrWhiteSpace (Text))
        {
            throw new GraphQLException ($"The text of the reponse with Id {ResponseId} must not be null or empty");
        }
    }
}