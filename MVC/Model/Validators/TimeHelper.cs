using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shared;
using Shared.Extensions;

namespace Model.Validations
{
    public static class TimeHelper
    {
        public static IEnumerable<ValidationResult> ValidationResult(string timeUnit, string errorMsg, Time obj = null)
        {
            var listValidations = new List<ValidationResult>
            {
                new ValidationResult(errorMsg, new[]
                {
                    timeUnit
                })
            };

            if(obj != null)
            {
                listValidations.Add(new ValidationResult(" ", new[]
                {
                    string.Format("{0}.{1}", timeUnit, Helpers.GetExpressionText<Time>(m => m.Value)),
                    string.Format("{0}.{1}", timeUnit, Helpers.GetExpressionText<Time>(m => m.TimeUnit))
                }));
            }

            return new List<ValidationResult>
            {
                new ValidationResult(errorMsg, new[]
                {
                    timeUnit
                }),
                new ValidationResult(" ", new[]
                {
                    string.Format("{0}.{1}", timeUnit, Helpers.GetExpressionText<Time>(m => m.Value)),
                    string.Format("{0}.{1}", timeUnit, Helpers.GetExpressionText<Time>(m => m.TimeUnit))
                })
            };
        }

        public static IEnumerable<ValidationResult> ValidationResult(List<string> timeUnits, string errorMsg)
        {
            var vResults = new List<ValidationResult>();

            timeUnits.ForEach(t => vResults.AddRange(new List<ValidationResult>
            {
                new ValidationResult(errorMsg, timeUnits),
                new ValidationResult(" ", new[]
                {
                    string.Format("{0}.{1}", t, Helpers.GetExpressionText<Time>(m => m.Value)),
                    string.Format("{0}.{1}", t, Helpers.GetExpressionText<Time>(m => m.TimeUnit))
                })
            }));

            return vResults;
        }
    }
}
