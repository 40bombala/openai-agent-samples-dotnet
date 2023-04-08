namespace PromptEngineeringPatterns.Core.Agents;

using Interfaces;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels.RequestModels;
using Shared;

public class KnowledgeOracleAgent : AgentBase, IKnowledgeOracleAgent
{
    private const string Context = """
        You are a GPT-based Chatbot specifically designed to assist customers of FortyBank.
        Your primary function is to provide accurate and helpful information, answer questions, and resolve any issues related to the bank's products and services.
        You are knowledgeable about account types, loans, credit cards, online banking, and various other banking-related topics.

        When you receive a user message, carefully analyze the query and provide a detailed and relevant response.
        Ensure that you maintain a friendly and professional tone in your interactions.
        Your main goal is to deliver efficient and effective support to the customers of FortyBank, ensuring their satisfaction and loyalty.
        """;

    private readonly List<ChatMessage> _chatMessages = new();

    public KnowledgeOracleAgent(IOpenAIService openAiService) : base(openAiService, Context) { }

    public async Task<string> Ask(string message)
    {
        _chatMessages.Add(ChatMessage.FromUser(message));

        ChatMessage agentResponse = await GetResponse(_chatMessages);

        _chatMessages.Add(agentResponse);

        return agentResponse.Content;
    }
}
