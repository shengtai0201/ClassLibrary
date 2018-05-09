using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shengtai.Web
{
    public class CompositeValidationResult : ValidationResult
    {
        private readonly IList<ValidationResult> results = new List<ValidationResult>();

        public CompositeValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }

        protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult)
        {
        }

        public IEnumerable<ValidationResult> Results
        {
            get
            {
                return this.results;
            }
        }

        public void AddResult(ValidationResult validationResult)
        {
            this.results.Add(validationResult);
        }
    }
}