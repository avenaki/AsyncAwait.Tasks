using System;
using System.Threading.Tasks;
using AsyncAwait.Task2.CodeReviewChallenge.Headers;
using CloudServices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AsyncAwait.Task2.CodeReviewChallenge.Middleware;

public class StatisticMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IStatisticService _statisticService;

    public StatisticMiddleware(RequestDelegate next, IStatisticService statisticService)
    {
        _next = next;
        _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;

        //if business logic allowed to have a lag in visits count,
        //we could also remove await from the line below
        //which would boost performance
        await _statisticService.RegisterVisitAsync(path);
        var visits = await _statisticService.GetVisitsCountAsync(path);

        context.Response.Headers.Add(CustomHttpHeaders.TotalPageVisits, visits.ToString());

        await _next(context);
    }
}
