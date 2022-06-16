﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens;

internal static class Calculator
{
    // todo: change this method to support cancellation token
    public static Task<long> Calculate(int n, CancellationToken token)
    {
        long sum = 0;

        for (var i = 0; i < n; i++)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"Sum for {n} cancelled. TaskId: {Task.CurrentId}");
                return Cancelled(token);
            }

            // i + 1 is to allow 2147483647 (Max(Int32)) 
            sum = sum + (i + 1);
            Thread.Sleep(50);

        }

        return Task.FromResult(sum);
    }
    static async Task<long> Cancelled(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return 0;
    }
}
