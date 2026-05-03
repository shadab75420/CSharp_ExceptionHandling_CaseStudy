# CSharp_ExceptionHandling_CaseStudy
This project implements a Banking Transaction System in C# using exception handling. 
The system allows users to perform basic banking operations such as deposit, withdrawal, 
and balance checking while enforcing business rules.
Business Rules:
Minimum balance must be ₹1000
Withdrawal amount cannot exceed available balance
Deposit amount must be greater than 0
User must enter correct PIN (maximum 3 attempts) should be there 

The program ensures that invalid operations are handled gracefully using built-in and custom exceptions.

Exception Types Used
Built-in Exceptions:
FormatException → Handles invalid input (non-numeric values)
Exception → General exception handler
Custom Exceptions:
InvalidAmountException → Thrown when deposit amount is less than or equal to 0
InsufficientBalanceException → Thrown when withdrawal violates balance rules
InvalidPinException → Thrown when incorrect PIN is entered
Sample Output

Enter Account Holder Name: Shadab

Set your PIN: ****

Enter PIN: ****

Deposit
Withdraw
Check Balance
Exit
Choose option: 1

Enter amount: 2000
Deposited: ₹2000

Operation Completed.

Enter PIN: ****
Choose option: 2

Enter amount: 7000
Balance Error: Insufficient balance.

Operation Completed.

Enter PIN: ****
Choose option: 3

Current Balance: ₹7000

Operation Completed.

How to Run the Code:
Open the project in Visual Studio or any C# IDE
Make sure .NET SDK is installed
Build the project
Run the program (Ctrl + F5 or dotnet run)
Follow on-screen instructions
