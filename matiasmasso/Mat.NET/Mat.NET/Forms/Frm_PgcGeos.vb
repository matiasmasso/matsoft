Public Class Frm_PgcGeos
    Private _AllowEvents As Boolean

    Private Sub Frm_PgcGeos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadYears()
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Dim iYear As Integer = ComboBox1.SelectedItem
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim oExercici As DTOExercici = BLL.BLLExercici.FromYear(iYear)
        Dim items As List(Of DTOPgcGeo) = BLL.BLLPgcGeos.FromExercici(oExercici)
        Xl_PgcGeos1.Load(items)
        Cursor = Cursors.Default
    End Sub

    Private Sub LoadYears()
        With ComboBox1
            .DataSource = BLL.BLLExercicis.Years(BLL.BLLApp.Emp)
        End With
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        refresca()
    End Sub
End Class