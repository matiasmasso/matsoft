Imports DTO.Integracions

Public Class PdfLogisticLabelsCarrefour
    Inherits _PdfBase
    Public Sub New(items As List(Of DTOCarrefourItem))
        MyBase.New(Printing.PaperKind.A6)

        For Each item As DTOCarrefourItem In items
            If Not item.Equals(items.First) Then MyBase.NewPage()

            Dim oMainLabel As New PdfLogisticLabelMainCarrefour(item)
            oMainLabel.DrawLabel(Me)

            MyBase.NewPage()

            Dim oSideLabel As New PdfLogisticLabelSideCarrefour(item)
            oSideLabel.DrawLabel(Me)
        Next
    End Sub

End Class
