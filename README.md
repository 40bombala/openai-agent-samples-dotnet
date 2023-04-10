# # Prompt Engineering Patterns - OpenAI Agents for .NET

This repository contains a collection of examples demonstrating how to utilize OpenAI APIs in .NET 7.0 using the
Betalgo.OpenAI.GPT3 package. The solution demonstrates a simple implementation of multiple AI agents that can perform
various tasks by using OpenAI's GPT-3 language model.

## Configuration

1. Add your OpenAI API key and (optionally) organization ID in `appsettings.json` or use .NET user secrets:

```json
{
  "OpenAIServiceOptions": {
    "ApiKey": "Your API key goes here",
    "Organization": "Your Organization ID goes here (optional)"
  }
}
``` 

For more information on how to use .NET user secrets, refer to
the [official documentation](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets).

## Register AI Agents

Register the AI agents using the `RegisterAgents` extension method:

```csharp
services.RegisterAgents();
```

## Inject Services

Inject the required services into your classes:

```csharp
public class MyClass
{
    private readonly IIntentClassifierAgent _intentClassifierAgent;
    private readonly ITextExtractorAgent _textExtractorAgent;

    public MyClass(IIntentClassifierAgent intentClassifierAgent, ITextExtractorAgent textExtractorAgent)
    {
        _intentClassifierAgent = intentClassifierAgent;
        _textExtractorAgent = textExtractorAgent;
    }

    // Your methods here
} 
```

## Use AI Agents

Use the AI agents to perform tasks:

```csharp
var intentResult = await _intentClassifierAgent.Ask("What is the weather like today?");
var extractedText = await _textExtractorAgent.Ask("<html><body><p>Hello, World!</p></body></html>");
``` 

## Running the Web App

To run the Web app, navigate to the `src/PromptEngineeringPatterns.Web` folder and given the `appsettings.json` file is
configured execute the following
command:

```bash
dotnet run 
```

## References

For more detailed documentation on how to use their Betalgo.OpenAI.GPT3 package, please visit
the [official repository](https://github.com/betalgo/openai).