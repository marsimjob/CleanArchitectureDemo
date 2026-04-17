using Application.Layer.Services;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

namespace Infrastructure.Layer
{
    public class OpenAIService : IOpenAIService
    {
        private readonly ChatClient _chat;
        public OpenAIService(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidCastException("OpenAI key was not found in configuration");

            _chat = new ChatClient(model: "gpt-4o-mini", apiKey: apiKey);
        }

        public async Task<string> GenerateSummaryAsync(string name, string series, string brand, CancellationToken cancellationToken)
        {
            var prompt = $"Summarize this toy in 2-3 sentences. They toy: Name {name}, Series: {series}, Brand: {brand}"
                          + "Keep it safe for work, keep it friendly, and keep it concise.";

            ChatCompletion complete = await _chat.CompleteChatAsync(prompt);

            return complete.Content[0].Text;
        }
    }
}
