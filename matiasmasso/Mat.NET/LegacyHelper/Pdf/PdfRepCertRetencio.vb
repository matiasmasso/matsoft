Public Class PdfRepCertRetencio
    Inherits PdfTemplateMembrete

    Public Sub New(Model As DTORepCertRetencio)
        MyBase.New(Model.Rep.Lang)

        MyBase.AddColumn(PdfColumn.Types.Number, "Liquidación")
        MyBase.AddColumn(PdfColumn.Types.Fch, "Fecha")
        MyBase.AddColumn(PdfColumn.Types.Import, "Base Imponible")
        MyBase.AddColumn(PdfColumn.Types.Import, "IVA")
        MyBase.AddColumn(PdfColumn.Types.Import, "Retención")
        MyBase.AddColumn(PdfColumn.Types.Import, "Líquido")

        MyBase.DrawDestination(Model.Rep)

        MyBase.DrawTitle(DTORepCertRetencio.Title(Model))

        MyBase.DrawColumnHeaders()

        For Each oRepLiq As DTORepLiq In Model.RepLiqs
            MyBase.DrawRow(oRepLiq.Id,
                           oRepLiq.Fch,
                           oRepLiq.BaseImponible,
                           DTORepLiq.GetIVAAmt(oRepLiq),
                           DTORepLiq.GetIRPFAmt(oRepLiq),
                           DTORepLiq.GetLiquid(oRepLiq))
        Next

        MyBase.DrawRow(Nothing, "total",
                       DTORepCertRetencio.BaseImponible(Model),
                       DTORepCertRetencio.IVA(Model),
                       DTORepCertRetencio.IRPF(Model),
                       DTORepCertRetencio.Liquid(Model))

    End Sub


End Class

