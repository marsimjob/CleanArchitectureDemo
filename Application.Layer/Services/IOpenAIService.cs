using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.Services
{
   public interface IOpenAIService
    {
        Task<string> GenerateSummaryAsync(string name, string series, string brand, CancellationToken cancellationToken);
    }
}
