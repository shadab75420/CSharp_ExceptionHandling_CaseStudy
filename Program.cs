using System;
using System.IO;

// Custom Exceptions
class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message) { }
}

class InvalidAmountException : Exception
{
    public InvalidAmountException(string message) : base(message) { }
}

class InvalidPinException : Exception
{
    public InvalidPinException(string message) : base(message) { }
}

// Logger Class
class Logger
{
    private static string path = "transactions.txt";

    public static void Log(string message)
    {
        File.AppendAllText(path, DateTime.Now + " - " + message + Environment.NewLine);
    }
}

// BankAccount Class
class BankAccount
{
    public string AccountHolderName { get; set; }
    public double Balance { get; set; }
    private int Pin;

    public BankAccount(string name, int pin)
    {
        AccountHolderName = name;
        Pin = pin;
        Balance = 5000;
    }

    public void ValidatePin(int inputPin)
    {
        if (inputPin != Pin)
        {
            throw new InvalidPinException("Incorrect PIN.");
        }
    }

    public void Deposit(double amount)
    {
        if (amount <= 0)
            throw new InvalidAmountException("Deposit must be greater than 0.");

        Balance += amount;
        Console.WriteLine("Deposited: ₹" + amount);
        Logger.Log("Deposited ₹" + amount);
    }

    public void Withdraw(double amount)
    {
        if (amount > Balance)
            throw new InsufficientBalanceException("Insufficient balance.");

        if ((Balance - amount) < 1000)
            throw new InsufficientBalanceException("Minimum balance 1000 required.");

        Balance -= amount;
        Console.WriteLine("Withdrawn:" + amount);
        Logger.Log("Withdrawn " + amount);
    }

    public void CheckBalance()
    {
        Console.WriteLine("Current Balance:" + Balance);
        Logger.Log("Checked Balance:" + Balance);
    }
}

// Main Program
class Program
{
    // Method to hide PIN input
    static int ReadHiddenPin()
    {
        string pin = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Enter)
            {
                pin += key.KeyChar;
                Console.Write("*");
            }

        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return Convert.ToInt32(pin);
    }

    static void Main()
    {
        Console.Write("Enter Account Holder Name: ");
        string name = Console.ReadLine();

        Console.Write("Set your PIN: ");
        int pin = ReadHiddenPin();

        BankAccount account = new BankAccount(name, pin);

        while (true)
        {
            int attempts = 0;
            bool authenticated = false;

            while (attempts < 3)
            {
                try
                {
                    Console.Write("\nEnter PIN: ");
                    int enteredPin = ReadHiddenPin();

                    account.ValidatePin(enteredPin);
                    authenticated = true;
                    break;
                }
                catch (InvalidPinException ex)
                {
                    attempts++;
                    Console.WriteLine(ex.Message + " Attempts left: " + (3 - attempts));
                }
            }

            if (!authenticated)
            {
                Console.WriteLine("Account blocked due to 3 incorrect PIN attempts.");
                break;
            }

            try
            {
                Console.WriteLine("\n1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Check Balance");
                Console.WriteLine("4. Exit");
                Console.Write("Choose option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter amount: ");
                        double dep = Convert.ToDouble(Console.ReadLine());
                        account.Deposit(dep);
                        break;

                    case 2:
                        Console.Write("Enter amount: ");
                        double wit = Convert.ToDouble(Console.ReadLine());
                        account.Withdraw(wit);
                        break;

                    case 3:
                        account.CheckBalance();
                        break;

                    case 4:
                        Console.WriteLine("Thank you.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            catch (InvalidAmountException ex)
            {
                Console.WriteLine("Amount Error: " + ex.Message);
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine("Balance Error: " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Operation Completed.");
            }
        }
    }
}