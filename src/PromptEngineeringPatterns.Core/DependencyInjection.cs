namespace PromptEngineeringPatterns.Core;

using Agents;
using Agents.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.GPT3.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAgents(this IServiceCollection services)
    {
        services.AddOpenAIService();
        services.AddScoped<IIntentClassifierAgent, IntentClassifierAgent>();
        services.AddScoped<ITextExtractorAgent, TextExtractorAgent>();
        services.AddScoped<IKnowledgeOracleAgent, KnowledgeOracleAgent>();
        services.AddScoped<ISentimentAnalyserAgent, SentimentAnalyserAgent>();

        return services;
    }
}
