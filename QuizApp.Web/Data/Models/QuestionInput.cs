using System;

namespace QuizApp.Web.Data.Models;

public record QuestionInput (
    string Text,
    IReadOnlyList<ResponseInput> Responses);

