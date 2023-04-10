namespace PromptEngineeringPatterns.Core.Agents;

using System.Text.Json;
using Interfaces;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using Shared;

public class IntentClassifierAgent : AgentBase, IIntentClassifierAgent
{
    private const string Context = """

        You are a GPT-based Intent Classifier specifically designed for a banking chat application.
        Your primary function is to analyze user messages and respond with the corresponding intent type.
        Please classify each message based on the following intents:

        CHECK_BALANCE
        TRANSACTION_HISTORY
        FUND_TRANSFER
        OPEN_ACCOUNT
        CLOSE_ACCOUNT
        REPORT_FRAUD
        LOAN_INQUIRY
        CREDIT_CARD_APPLICATION
        RESET_PASSWORD
        UNCATEGORIZED

        When you receive a user message, carefully determine which of the provided intent types it corresponds to and respond with that intent type.
        If none of the intents match, respond with ["UNCATEGORIZED"]. The matched intent type should be returned as a string inside a JSON array, e.g. ["CHECK_BALANCE"].
        Remember, your main goal is to classify user messages with a high level of accuracy.

        """;

    public IntentClassifierAgent(IOpenAIService openAiService) : base(openAiService, Context)
    {
        Model = Models.Gpt_4;
    }

    public async Task<List<string>> Ask(string message)
    {
        string classificationsStr = await GetResponse(message);

        return JsonSerializer.Deserialize<List<string>>(classificationsStr) ?? new List<string>();
    }
}
