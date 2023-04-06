// Configure DI and services

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromptEngineeringPatterns.Core;
using PromptEngineeringPatterns.Core.Agents.Interfaces;

using IHost host = Host.CreateDefaultBuilder(args)
                       .ConfigureServices(
                            services =>
                            {
                                services.RegisterAgents()
                                        .BuildServiceProvider();
                            })
                       .Build();

var extractorAgent = host.Services.GetRequiredService<ITextExtractorAgent>();
var classifierAgent = host.Services.GetRequiredService<IIntentClassifierAgent>();

string extractorAgentResponse = await extractorAgent!.Ask(
    """
    <!DOCTYPE html>
    <html lang="en">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Text Extractor Test</title>
        <style>
            h1 { color: blue; }
            p { font-family: Arial, sans-serif; }
        </style>
    </head>
    <body>
        <h1>Welcome to the Text Extractor Test</h1>
        <p>This is a simple HTML example to test the text extractor model. The model should be able to extract the text content without including any HTML tags, scripts, or styles.</p>
        <p>Remember to visit our <a href="https://www.example.com">website</a> for more information.</p>
        <script>
            console.log("This is a JavaScript script that should not be extracted by the text extractor model.");
        </script>
    </body>
    </html>
    """);

Console.WriteLine(extractorAgentResponse);

string classifierAgentResponse = await classifierAgent!.Ask(
    """
    Hi, I'd like to know how to apply for a new credit card. Can you please guide me through the process?
    """);

Console.WriteLine(classifierAgentResponse);

await host.RunAsync();
