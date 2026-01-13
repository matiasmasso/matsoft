Public Class Frm_PgcGeos
    Private _AllowEvents As Boolean
    Private _Exercici As DTOExercici

    Public Sub New(Optional oExercici As DTOExercici = Nothing)
        InitializeComponent()

        _Exercici = oExercici
    End Sub

    Private Async Sub Frm_PgcGeos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim exs As New List(Of Exception)
        Dim years = Await FEB2.Exercicis.Years(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            ComboBox1.DataSource = years
            If _Exercici IsNot Nothing Then
                ComboBox1.SelectedItem = _Exercici.Year
            End If
            refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim iYear As Integer = ComboBox1.SelectedItem
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim oExercici As DTOExercici = DTOExercici.FromYear(Current.Session.Emp, iYear)
        Dim items = Await FEB2.PgcGeos.All(oExercici, exs)
        Cursor = Cursors.Default
        If exs.Count = 0 Then
            Xl_PgcGeos1.Load(items, oExercici.Year)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        refresca()
    End Sub
End Class