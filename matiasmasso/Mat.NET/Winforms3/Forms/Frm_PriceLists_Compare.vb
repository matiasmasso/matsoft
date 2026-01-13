Public Class Frm_PriceLists_Compare
    Private _Candidate As DTOPriceListSupplier

    Public Sub New(oCandidate As DTOPriceListSupplier)
        MyBase.New
        InitializeComponent()

        _Candidate = oCandidate

    End Sub

    Private Async Sub Frm_PriceLists_Compare_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim oVigent = Await FEB.PriceListItemsSupplier.Vigent(exs, _Candidate.Proveidor)
        If exs.Count = 0 Then
            Xl_SupplierPriceListCompare1.Load(oVigent, _Candidate)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class