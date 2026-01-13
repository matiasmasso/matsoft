

Public Class Frm_QualityDistribution
    Private _Proveidor As DTOProveidor
    Private _AllowEvents As Boolean

    Public Sub New(oProveidor As DTOProveidor)
        MyBase.New
        InitializeComponent()
        _Proveidor = oProveidor
    End Sub

    Private Async Sub Frm_QualityDistribution_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.Value = DTO.GlobalVariables.Today().AddYears(-1)
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim Items = Await FEB.QualityDistribution.All(exs, _Proveidor, DateTimePicker1.Value)
        Items = Items.OrderByDescending(Function(x) x.Skus.Count).ToList
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_QualityDistribution1.Load(Items)
        End If
    End Function

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Do_Excel()
    End Sub

    Private Sub Xl_QualityDistribution1_requestForExcel(sender As Object, e As MatEventArgs) Handles Xl_QualityDistribution1.requestForExcel
        Do_Excel()
    End Sub

    Private Sub Do_Excel()
        Dim exs As New List(Of Exception)
        Dim items = Xl_QualityDistribution1.SelectedItems
        If items.Count <= 1 Then items = Xl_QualityDistribution1.Values
        items = items.OrderByDescending(Function(x) x.Skus.Count).ToList
        Dim oSheet = DTOQualityDistribution.ExcelSheet(items)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            Await refresca()
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_QualityDistribution1.Filter = e.Argument
    End Sub


End Class