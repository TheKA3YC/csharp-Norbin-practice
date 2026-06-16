using System.Text;

static string CalculateCompoundInterestForYears(double initialDeposit, int years, double interestRate)
{
    var сompoundInterestForYears = new StringBuilder();
    double growth = 1 + interestRate / 100;
    for (var i = 1; i <= years; i++)
    {
        initialDeposit = initialDeposit * growth;
        сompoundInterestForYears.AppendLine($"Год {i}: {Math.Round(initialDeposit, 2)} руб.");
    }

    return сompoundInterestForYears.ToString();
}

Console.WriteLine(CalculateCompoundInterestForYears(1000, 3, 10));