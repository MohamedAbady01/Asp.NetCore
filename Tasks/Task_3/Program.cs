namespace Task_3
{
    internal class Program
    {
        public static double medicalCoverage = 5000;


        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter Brain Surgery Cost:");
                double surgeryCost;
                if (!double.TryParse(Console.ReadLine(), out surgeryCost))
                {
                    Console.WriteLine("Invalid input. Please enter a valid amount.");
                    return;
                }

                if (surgeryCost <= medicalCoverage)
                {
                    medicalCoverage -= surgeryCost;
                    Console.WriteLine($"Your medical insurance covers this. {surgeryCost} EGP will be deducted from your coverage, and you now have {medicalCoverage} EGP left.");
                }
                else
                {
                    Console.WriteLine("Your medical insurance doesn't cover this");

                    Console.WriteLine("Do you want to check the rest as debit? (Yes/No)");

                    string choice = Console.ReadLine().Trim().ToLower();

                    if (choice == "yes")
                    {
                        double remainingCost = surgeryCost - medicalCoverage;
                        medicalCoverage = 0;
                        Console.WriteLine($"Okay, {medicalCoverage} EGP will be deducted from your coverage, and {remainingCost} EGP will be checked as debit.");
                    }
                    else if (choice == "no")
                    {
                        Console.WriteLine("Sorry, you can't make this surgery.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 'Yes' or 'No'.");
                    };
                }
            }
        }
    }
}
   
