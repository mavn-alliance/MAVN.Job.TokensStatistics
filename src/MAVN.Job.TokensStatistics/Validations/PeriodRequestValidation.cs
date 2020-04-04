using System;
using FluentValidation;
using JetBrains.Annotations;
using MAVN.Job.TokensStatistics.Client.Models.Requests;

namespace MAVN.Job.TokensStatistics.Validations
{
    [UsedImplicitly]
    public class PeriodRequestValidation : AbstractValidator<PeriodRequest>
    {
        public PeriodRequestValidation()
        {
            RuleFor(o => o.FromDate.Date)
                .NotEmpty()
                .WithMessage("From Date is required")
                .LessThanOrEqualTo(x => DateTime.UtcNow.Date)
                .WithMessage("From Date must be equal or earlier than today.");

            RuleFor(o => o.ToDate.Date)
                .NotEmpty()
                .WithMessage("To Date is required")
                .GreaterThanOrEqualTo(x => x.FromDate.Date)
                .WithMessage("To Date must be equal or later than From Date.")
                .LessThanOrEqualTo(x => DateTime.UtcNow.Date)
                .WithMessage("To Date must be equal or earlier than today.");
        }
    }
}
