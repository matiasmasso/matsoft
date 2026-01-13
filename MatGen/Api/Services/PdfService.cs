using Api.Helpers;
using DTO;
using QuestPDF.Fluent;

namespace Api.Services
{
    public class PdfService
    {
        public static byte[] GenerateLineageBranchesPdf(List<AncestorModel> leftBranch, List<AncestorModel> rightBranch)
        {
            if (leftBranch is null)
                throw new ArgumentNullException(nameof(leftBranch));

            if (rightBranch is null)
                throw new ArgumentNullException(nameof(rightBranch));

            // Crear el document amb les dues branques
            var document = new LineageBranchesPdfDocument(leftBranch, rightBranch);

            // Generar el PDF com a byte[]
            return document.GeneratePdf();

        }
    }
}
