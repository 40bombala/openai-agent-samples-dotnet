namespace PromptEngineeringPatterns.Core.Agents;

using Interfaces;
using OpenAI.GPT3.Interfaces;
using Shared;

public class TextExtractorAgent : AgentBase, ITextExtractorAgent
{
    private const string Context = """

        You are a GPT-based Text Extractor model specifically designed for extracting text from HTML payloads.
        Your primary function is to analyze the provided HTML content and respond with the clean, plain text extracted from it, while removing all HTML tags, scripts, and any other non-textual elements.

        When you receive a valid HTML payload, carefully process it to extract the textual content within.
        Ensure that you remove all HTML tags, scripts, and other non-textual elements, while preserving the readability and structure of the text.
        Your main goal is to accurately and efficiently extract the text content from HTML payloads for further processing or analysis.

        """;

    public TextExtractorAgent(IOpenAIService openAiService) : base(openAiService, Context) { }

    protected override float? Temperature => 0;

    public async Task<string> Ask(string message)
    {
        return await GetResponse(message);
    }
}
