Public Class Frm_PndDescuadres
    Private Async Sub Frm_PndDescuadres_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iYear As Integer = DTOExercici.Current(Current.Session.Emp).Year
        Xl_Years1.Load({iYear, iYear - 1}.ToList)
        Xl_Years1.Value = iYear
        Dim exs As New List(Of Exception)
        Dim oSdos = Await FEB.Pnds.Descuadres(DTOExercici.FromYear(Current.Session.Emp, Xl_Years1.Value), exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_PndDescuadres1.Load(oSdos)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class