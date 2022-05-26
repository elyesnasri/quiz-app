using System;
using QuizApp.Web.Data;
using QuizApp.Web.Data.Models;

namespace QuizApp.Web.GraphQL;

public class Queries
{
    [UseProjection]
    public IQueryable<Question> GetQuestions ([Service] ApplicationDbContext context)
        => context.Questions!;
}

