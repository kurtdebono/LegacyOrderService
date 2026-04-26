using System.Text;

namespace LegacyOrderService.Models
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();

        public List<string> Errors { get; }
        public int? ParsedQuantity { get; }

        public ValidationResult(List<string> errors, int? quantity)
        {
            Errors = errors;
            ParsedQuantity = quantity;
        }

        public string GetFullErrorMessage()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($@"Order could not be processed due to the following issues:");
            
            foreach(string error in Errors)
            {
                stringBuilder.AppendLine($@"> {error}");
            }

            return stringBuilder.ToString();
        }
    }
}