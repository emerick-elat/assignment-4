using System.Text;

namespace Bank.UseCases.Account
{
    internal static class Helpers
    {
        public static string GenerateAccountNumber(Entities.Customer customer)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            sb.Append(customer.FirstName.ToUpper().Substring(0, 2));//2
            sb.Append(DateTime.Now.Month.ToString("00"));//2
            sb.Append(random.Next(0, 9999).ToString("0000"));//4
            sb.Append(DateTime.Now.Year.ToString("0000"));//4
            sb.Append(DateTime.Now.Day.ToString("00"));//2
            sb.Append(customer?.LastName?.ToUpper().Substring(0, 2));//2
            return sb.ToString();
        }
    }
}
