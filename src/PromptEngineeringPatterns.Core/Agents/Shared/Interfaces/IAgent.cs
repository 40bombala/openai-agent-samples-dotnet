namespace PromptEngineeringPatterns.Core.Agents.Shared.Interfaces;

public interface IAgent
{
    public Task<string> Ask(string message);
}
