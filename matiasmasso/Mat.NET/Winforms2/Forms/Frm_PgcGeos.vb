Public Class Frm_PgcGeos
    Private _Exercici As DTOExercici
    Private _values As List(Of DTOPgcGeo)
    Private _AllowEvents As Boolean

    Public Sub New(Optional oExercici As DTOExercici = Nothing)
        InitializeComponent()
        _Exercici = oExercici
    End Sub

    Private Async Sub Frm_PgcGeos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim exs As New List(Of Exception)
        Dim years = Await FEB.Exercicis.Years(exs, GlobalVariables.Emp)
        If exs.Count = 0 Then
            ComboBox1.DataSource = years
            If _Exercici IsNot Nothing Then
                ComboBox1.SelectedItem = _Exercici.Year
            End If
            Await refresca()
            _AllowEvents = True
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim oExercici As DTOExercici = DTOExercici.FromYear(Current.Session.Emp, CurrentYear())
        _values = Await FEB.PgcGeos.All(oExercici, exs)
        If exs.Count = 0 Then
            Xl_PgcGeos1.Load(_values, oExercici.Year)
            ProgressBar1.Visible = False
            Cursor = Cursors.Default
        Else
            ProgressBar1.Visible = False
            Cursor = Cursors.Default
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentYear() As Integer
        Return ComboBox1.SelectedItem
    End Function


    Private Async Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Await refresca()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        ProgressBar1.Visible = True
        Dim sheetName = String.Format("{0}", CurrentYear)
        Dim filename = String.Format("M+O Distribució Geografica de Comptes {0}", CurrentYear)
        Dim oSheet As New MatHelper.Excel.Sheet(sheetName, filename)
        With oSheet
            .AddColumn("Compte")
            .AddColumn("Descripció")
            .AddColumn("Total", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("CCAA", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Espanya", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("CEE", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Resta", MatHelper.Excel.Cell.NumberFormats.Euro)
            .DisplayTotals = True
        End With
        For Each item In _values
            Dim oRow = oSheet.AddRow()
            oRow.AddCell(item.CtaId)
            oRow.AddCell(item.CtaNom)
            oRow.AddCell(item.Tot)
            oRow.AddCell(item.CCAA)
            oRow.AddCell(item.Esp)
            oRow.AddCell(item.CEE)
            oRow.AddCell(item.RestOfTheWorld())
        Next

        Dim exs As New List(Of Exception)
        If UIHelper.ShowExcel(oSheet, exs) Then
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class