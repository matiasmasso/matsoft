
Public Class Frm_RepLiqs
    Private _Rep As DTORep

    Public Sub New(oRep As DTORep)
        MyBase.New()
        Me.InitializeComponent()
        _Rep = oRep
    End Sub

    Private Async Sub Frm_RepLiqs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Rep.Load(exs, _Rep) Then
            Me.Text = String.Format("Liquidacions a {0}", _Rep.NickName)
            Await refresca()
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oRepLiqs = Await FEB.Repliqs.Headers(exs, _Rep)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_RepRepLiqs1.Load(oRepLiqs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_RepRepLiqs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepRepLiqs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Do_Excel(Xl_RepRepLiqs1.Values)
    End Sub

    Private Sub Xl_RepRepLiqs1_RequestToExcel(sender As Object, e As MatEventArgs) Handles Xl_RepRepLiqs1.RequestToExcel
        Do_Excel(e.Argument)
    End Sub

    Private Sub Do_Excel(items As List(Of DTORepLiq))
        Dim exs As New List(Of Exception)
        Dim oSheet = FEB.Repliqs.Excel(_Rep, items)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class