/*
* Study the code of this application to calculate the sum of integers from 0 to N, and then
* change the application code so that the following requirements are met:
* 1. The calculation must be performed asynchronously.
* 2. N is set by the user from the console. The user has the right to make a new boundary in the calculation process,
* which should lead to the restart of the calculation.
* 3. When restarting the calculation, the application should continue working without any failures.
*/

using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens;

internal class Program
{
    public static (Task, CancellationTokenSource) CurrentTask;

    /// <summary>
    /// The Main method should not be changed at all.
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        Console.WriteLine("Mentoring program L2. Async/await.V1. Task 1");
        Console.WriteLine("Calculating the sum of integers from 0 to N.");
        Console.WriteLine("Use 'q' key to exit...");
        Console.WriteLine();

        Console.WriteLine("Enter N: ");

        var input = Console.ReadLine();
        while (input.Trim().ToUpper() != "Q")
        {
            if (int.TryParse(input, out var n))
            {
                CalculateSum(n);
            }
            else
            {
                Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
                Console.WriteLine("Enter N: ");
            }

            Console.WriteLine("Enter N: ");
            input = Console.ReadLine();
        }

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }

    private static void CalculateSum(int n)
    {

        if (CurrentTask.Item1 != null)
        {
            CurrentTask.Item2.Cancel(false);
        }

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var sum = 0;

        // todo: make calculation asynchronous

        var calcTask = Task.Run(() => Calculator.Calculate(n, cancellationToken))
            .ContinueWith(x => PrintSum(n, x.Result), TaskContinuationOptions.NotOnCanceled);

        CurrentTask = (calcTask, cancellationTokenSource);

        //var calculateSumTask = Task.Run(() => Calculator.Calculate(n, cancellationToken));
        //var printSumTask = calculateSumTask.ContinueWith(x => PrintSum(n, calculateSumTask.Result), TaskContinuationOptions.NotOnCanceled);

        ////var input = EnterN();
        //if(input == "Q")
        //{
        //    return;
        //}
        //if (!calculateSumTask.IsCompleted)
        //{
        //    cancellationTokenSource.Cancel();
        //    Console.WriteLine($"Sum for {n} cancelled...");
        //    Console.WriteLine($"The task for {input} started... Enter N to cancel the request:");
        //}
        //// todo: add code to process cancellation and uncomment this line    

        //CalculateSum(input);
    }

    static string EnterN()
    {
        var inputCorrect = false;
        int newN = default;
        Console.WriteLine("Enter N: ");

        var input = Console.ReadLine();

        while (!inputCorrect && input.ToUpper() != "Q")
        {

            inputCorrect = int.TryParse(input, out newN);
            if (!inputCorrect)
            {
                Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
                Console.WriteLine("Enter N: ");

                input = Console.ReadLine();
            }

        }

        return input.ToUpper();
    }

    static void PrintSum(int n, long sum)
    {
        Console.WriteLine($"Sum for {n} = {sum}.");
        Console.WriteLine();
        Console.WriteLine("Enter N: ");
    }

}
