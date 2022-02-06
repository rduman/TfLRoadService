using FluentValidation;
using TfL.RoadManagement.TFL.Exceptions;

namespace TfL.RoadManagement.RoadStatus.Infrastructure;

public static class ConsoleExceptionHandler
{
    private static readonly IDictionary<Type, Action<UnhandledExceptionEventArgs>> exceptionHandlers =
        new Dictionary<Type, Action<UnhandledExceptionEventArgs>>
        {
                {typeof(ValidationException), HandleValidationException},
                {typeof(NotFoundException), HandleNotFoundException},
        };

    public static void HandleException(object sender, UnhandledExceptionEventArgs context)
    {
        var type = context.ExceptionObject.GetType();
        if (exceptionHandlers.ContainsKey(type))
        {
            exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }


    private static void HandleValidationException(UnhandledExceptionEventArgs context)
    {
        var exception = context.ExceptionObject as ValidationException;

        foreach (var validationError in exception.Errors.Select(e => e.ErrorMessage))
            Console.WriteLine(validationError);

        Environment.Exit(0);
    }

    private static void HandleNotFoundException(UnhandledExceptionEventArgs context)
    {
        var exception = context.ExceptionObject as NotFoundException;
        Console.WriteLine(exception?.Message);

        Environment.Exit(1);
    }

    private static void HandleUnknownException(UnhandledExceptionEventArgs context)
    {
        Console.WriteLine($"An unknown error occurred. {context.ExceptionObject}");
        Environment.Exit(0);
    }
}

