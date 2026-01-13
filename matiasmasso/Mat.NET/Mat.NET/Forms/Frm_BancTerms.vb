Public Class Frm_BancTerms

    Private Sub Frm_BancTerms_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oTerms As List(Of DTOBancTerm) = BLL.BLLBancTerms.all()
        Xl_BancTerms1.Load(oTerms)
    End Sub
End Class