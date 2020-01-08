using Refit;
using System.Threading.Tasks;

namespace aiala.Backend.ApiModels.Refit
{
    public interface IAzureVisionApi
    {
        [Post("/")]
        Task<VisionAnalysisModel> PostVisionAnalysis(
            [Body] VisionUrlBody body,
            [AliasAs("visualFeatures")] string features,
            [Header("Ocp-Apim-Subscription-Key")] string subscriptionKey);
    }
}
