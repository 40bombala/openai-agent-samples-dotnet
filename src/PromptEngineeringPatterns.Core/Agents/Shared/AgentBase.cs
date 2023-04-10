namespace PromptEngineeringPatterns.Core.Agents.Shared;

using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;

public abstract class AgentBase
{
    private readonly string _context;
    private readonly IOpenAIService _openAiService;

    protected AgentBase(IOpenAIService openAiService, string context)
    {
        _openAiService = openAiService;
        _context = context;
    }

    protected float? Temperature { get; init; }

    protected string Model { get; init; } = Models.ChatGpt3_5Turbo;

    protected async Task<string> GetResponse(string message)
    {
        ChatCompletionCreateResponse completionResult = await _openAiService.ChatCompletion.CreateCompletion(
            new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(_context),
                    ChatMessage.FromUser(message)
                },
                Model = Model,
                Temperature = Temperature
            });

        return completionResult switch
        {
            { Successful: true } => completionResult.Choices.First().Message.Content,
            var _ => throw new Exception("Failed to get response from OpenAI")
        };
    }

    protected async Task<ChatMessage> GetResponse(IEnumerable<ChatMessage> messages)
    {
        var chatMessages = new List<ChatMessage>
        {
            ChatMessage.FromSystem(_context)
        };

        ChatCompletionCreateResponse completionResult = await _openAiService.ChatCompletion.CreateCompletion(
            new ChatCompletionCreateRequest
            {
                Messages = chatMessages.Concat(messages).ToList(),
                Model = Model,
                Temperature = Temperature
            });

        return completionResult switch
        {
            { Successful: true } => completionResult.Choices.First().Message,
            var _ => throw new Exception("Failed to get response from OpenAI")
        };
    }
}
