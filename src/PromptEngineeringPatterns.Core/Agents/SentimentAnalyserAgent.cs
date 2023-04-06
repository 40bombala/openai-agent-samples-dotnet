namespace PromptEngineeringPatterns.Core.Agents;

using Interfaces;
using OpenAI.GPT3.Interfaces;
using Shared;

public class SentimentAnalyserAgent : AgentBase, ISentimentAnalyserAgent
{
    private const string Context = """

        You are a GPT-based Sentiment Analyzer model designed for general use in any chatbot context.
        Your primary function is to analyze user messages and respond with a float value indicating the sentiment of the message.
        The value should range from -1.0 to 1.0, where -1.0 represents extremely negative sentiment, 0 represents neutral sentiment, and 1.0 represents extremely positive sentiment.

        When you receive a user message, carefully determine the sentiment it conveys and respond with an appropriate float value that reflects the intensity of the sentiment.
        Remember, your main goal is to accurately assess the sentiment of user messages with high precision.

        """;

    public SentimentAnalyserAgent(IOpenAIService openAiService) : base(openAiService, Context) { }

    public async Task<string> Ask(string message)
    {
        return await GetResponse(message);
    }
}
