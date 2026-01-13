Public Class Frm_Rep
    Private _Rep As DTORep
    Private _Tab As Tabs
    Private _IsLoaded(10) As Boolean
    Public Enum Tabs
        General
        RepcomFollowUp
        RepLiqs
        Retencions
    End Enum

    Public Sub New(oRep As DTORep, Optional oTab As Tabs = Tabs.General)
        MyBase.New()
        Me.InitializeComponent()
        _Rep = oRep
        _Tab = oTab
    End Sub

    Private Async Sub Frm_Rep_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = String.Format("Rep: {0}", _Rep.NicknameOrNom)
        TabControl1.SelectedIndex = _Tab
        Await LoadTab()
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Await LoadTab()
    End Sub

    Private Async Function LoadTab() As Task
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.RepcomFollowUp
                If Not _IsLoaded(Tabs.RepcomFollowUp) Then
                    Dim oOrders = Await FEB.Rep.Archive(_Rep, exs)
                    If exs.Count = 0 Then
                        Xl_RepComsFollowUp1.Load(oOrders)
                        _IsLoaded(Tabs.RepcomFollowUp) = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.RepLiqs
            Case Tabs.Retencions
                If Not _IsLoaded(Tabs.Retencions) Then
                    Await RefrescaRetencions()
                    _IsLoaded(Tabs.Retencions) = True
                End If
        End Select

    End Function

    Private Async Function RefrescaRetencions() As Task
        Dim exs As New List(Of Exception)
        Dim oRetencions = Await FEB.RepCertsRetencio.All(exs, _Rep)
        If exs.Count = 0 Then
            Xl_RepCertRetencions1.Load(oRetencions)
        Else
            UIHelper.WarnError(exs)
        End If

    End Function
    Private Async Sub Xl_RepCertRetencions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_RepCertRetencions1.RequestToRefresh
        Await RefrescaRetencions()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Select Case TabControl1.SelectedIndex
            Case Tabs.RepcomFollowUp
                Dim exs As New List(Of Exception)
                Dim oSheet = FEB.RepComsFollowUp.Excel(_Rep, exs)
                'If Not UIHelper.ShowExcel(oSheet, exs) Then
                ' UIHelper.WarnError(exs)
                'End If
        End Select
    End Sub


End Class