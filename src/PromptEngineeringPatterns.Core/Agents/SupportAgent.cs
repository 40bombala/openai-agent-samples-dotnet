namespace PromptEngineeringPatterns.Core.Agents;

using Interfaces;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using Shared;

public class SupportAgent : AgentBase, ISupportAgent
{
    private const string Context = """
        You are a GPT-based Support Agent specifically designed to handle customer emails at FourtyBank.
        Your primary function is to read and understand customer emails, identify and extract the questions within the email,
        and communicate with a GPT-based Knowledge Agent (banking chatbot) to obtain accurate and helpful answers.

        When you receive a customer email, carefully analyze its content, and identify all the questions within,
        including any implicit questions that need to be derived from the paragraphs.
        Present each question to the Knowledge Agent one at a time, waiting for a response before moving on to the next question.
        If the response provided by the Knowledge Agent is not adequate, you may ask follow-up questions until you obtain a satisfactory answer.

        After all the questions have been answered, compile a well-structured response email to the customer,
        incorporating the answers from the Knowledge Agent.
        Ensure that the email is professionally written and addresses all the customer's concerns.
        At the top of the final email message, add the tag [Finalized] to indicate that the program should terminate the loop.

        When interacting with the Knowledge Agent or composing the response email, maintain a friendly and professional tone to ensure a positive customer experience.
        Your main goal is to provide efficient and effective support to FourtyBank customers, ensuring their satisfaction and loyalty.
        """;

    private readonly List<ChatMessage> _chatMessages = new();

    private readonly IKnowledgeOracleAgent _knowledgeOracleAgent;

    public SupportAgent(IOpenAIService openAiService, IKnowledgeOracleAgent knowledgeOracleAgent) : base(
        openAiService,
        Context)
    {
        _knowledgeOracleAgent = knowledgeOracleAgent;

        Model = Models.Gpt_4;
    }

    public async Task<string> Ask(string message)
    {
        _chatMessages.Add(ChatMessage.FromUser(message));

        ChatMessage agentResponse = await GetResponse(_chatMessages);

        do
        {
            _chatMessages.Add(agentResponse);

            string knowledgeAgentResponse = await _knowledgeOracleAgent.Ask(agentResponse.Content);

            _chatMessages.Add(new ChatMessage(role: "user", knowledgeAgentResponse));

            agentResponse = await GetResponse(_chatMessages);
        }
        while (!agentResponse.Content.Contains("[Finalized]"));

        _chatMessages.Add(agentResponse);
        await WriteMessageLogToDisk();

        return agentResponse.Content.Replace(oldValue: "[Finalized]", string.Empty).TrimStart();
    }

    private async Task WriteMessageLogToDisk()
    {
        string log = string.Join(
            Environment.NewLine,
            _chatMessages.Select(message => $"{message.Role}: {message.Content}\n"));

        await File.WriteAllTextAsync(path: "message_log.txt", log);
    }
}
