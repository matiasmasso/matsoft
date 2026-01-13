Public Class Frm_Csas

    Private _banc As DTOBanc
    Private _csas As List(Of DTOCsa)
    Private _lastLogYear As Integer
    Private _selectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        mailingLogs
    End Enum

    Public Sub New(Optional oBanc As DTOBanc = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Banc = oBanc
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()

    End Sub

    Private Async Sub Frm_Csas_LoadAsync(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim iYears As New List(Of Integer)
        ProgressBar1.Visible = True
        If _Banc Is Nothing Then
            iYears = Await FEB.Csas.YearsAsync(Current.Session.Emp, exs)
        Else
            Me.Text = Me.Text & " de " & _Banc.AbrOrNom()
            iYears = Await FEB.Csas.YearsAsync(_Banc, exs)
        End If

        If exs.Count = 0 Then

            Xl_Years1.Load(iYears)
            If _Banc IsNot Nothing Then
                RemesaDeExportacioToolStripMenuItem.Enabled = _Banc.Equals(DTOBanc.Wellknown(DTOBanc.Wellknowns.CaixaBank))
                NovaRemesaToolStripMenuItem.Enabled = True
            End If
            If Await refrescaAsync(exs) Then
                ProgressBar1.Visible = False
            Else
                ProgressBar1.Visible = False
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Sub Xl_Csas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Csas1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Csas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Csas1.RequestToAddNew
        Dim oCsa = DTOCsa.Factory(Current.Session.Emp)
        Dim oFrm As New Frm_Csa(oCsa)
        'AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Csas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Csas1.RequestToRefresh
        Dim exs As New List(Of Exception)
        If Not Await refrescaAsync(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refrescaAsync(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        ProgressBar1.Visible = True
        If _Banc Is Nothing Then
            Dim o As New List(Of DTOCsa)
            _Csas = Await FEB.Csas.AllAsync(Xl_Years1.Value, Current.Session.Emp, exs)
        Else
            _Csas = Await FEB.Csas.AllAsync(Xl_Years1.Value, _Banc, exs)
        End If

        If exs.Count = 0 Then
            ProgressBar1.Visible = False
            Xl_Csas1.Load(_Csas, Nothing, _SelectionMode)
            retval = True
        End If
        Return retval
    End Function

    Private Async Sub RefrescaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        If Not Await refrescaAsync(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub SepaCoreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SepaCoreToolStripMenuItem.Click
        Dim oFrm As New Frm_CsaSepaCoreFactory(_Banc, DTOCsa.FileFormats.SepaCore)
        oFrm.Show()
    End Sub

    Private Sub RemesaDeExportacioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemesaDeExportacioToolStripMenuItem.Click
        Dim oFrm As New Frm_CsaSepaCoreFactory(_Banc, DTOCsa.FileFormats.RemesesExportacioLaCaixa)
        oFrm.Show()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oSheet As MatHelper.Excel.Sheet = FEB.Csas.Excel(_csas)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Dim exs As New List(Of Exception)
        If Not Await refrescaAsync(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.mailingLogs
                If _lastLogYear <> Xl_Years1.Value Then
                    Dim exs As New List(Of Exception)
                    ProgressBar1.Visible = True
                    Dim oCsbs = Await FEB.Csbs.mailingLogs(exs, GlobalVariables.Emp, Xl_Years1.Value)
                    ProgressBar1.Visible = False
                    If exs.Count = 0 Then
                        Xl_CsbMailingLogs1.Load(oCsbs)
                        _lastLogYear = Xl_Years1.Value
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub
End Class