using DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Api.Helpers
{





    public class LineageBranchesPdfDocument : IDocument
    {
        private readonly List<AncestorModel> LeftBranch;
        private readonly List<AncestorModel> RightBranch;

        public LineageBranchesPdfDocument(List<AncestorModel> left, List<AncestorModel> right)
        {
            LeftBranch = left;
            RightBranch = right;
        }

        public DocumentMetadata GetMetadata() => new DocumentMetadata
        {
            Title = "Arbre Genealògic",
            Author = "Matias Massó Cases"
        };

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Content().Row(row =>
                {
                    row.RelativeItem().Element(col => BuildColumn(col, LeftBranch));
                    row.RelativeItem().Element(col => BuildColumn(col, RightBranch));
                });
            });
        }

        private void BuildColumn(IContainer container, List<AncestorModel> branch)
        {
            container.Column(col =>
            {
                foreach (var e in branch)
                {
                    col.Item().Element(c => Box(c, e));
                    col.Item().Height(15);
                }
            });
        }

        private void Box(IContainer container, AncestorModel e)
        {
            container
                .Border(1)
                .BorderColor(Colors.Grey.Darken2)
                .Background(Colors.Grey.Lighten4)
                .Padding(10)
                .Column(col =>
                {
                    col.Item().Text($"{e.Person.Nom} ({e.Person.YearFrom})")
                        .FontSize(11)
                        .SemiBold()
                        .FontColor(Colors.Black);
                });
        }
    }
}