using DTO;

namespace Web.Services
{
    public interface IPdfService
    {
        Task OpenLineageBranchesPdfAsync(List<AncestorModel> line1, List<AncestorModel> line2);
    }
}
