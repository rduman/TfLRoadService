using FluentValidation;
using TfL.RoadManagement.TFL.Models;

namespace TfL.RoadManagement.TFL.Validator;

public class RoadStatusRequestValidator : AbstractValidator<RoadStatusRequest>
{
    public RoadStatusRequestValidator()
    {
        RuleFor(x => x.RoadIds).NotEmpty().WithMessage(x => $"{nameof(x.RoadIds)} are required");
    }
}