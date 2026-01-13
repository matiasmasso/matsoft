
Imports System.ComponentModel
Imports Microsoft.Office.Interop

Public Class Frm_NominasNew
    Private _Word As Word.Application
    Private _Filename As String
    Private _Nominas As List(Of DTONomina)
    Private _BackGroundWorker As New System.ComponentModel.BackgroundWorker
    Private _Cancel As Boolean
    Private _CancelRequest As Boolean
    Private _IsLoaded As Boolean
    Private _AllowEvents As Boolean

    Public Sub New(sFilename As String, DtFch As Date)
        MyBase.New()
        Me.InitializeComponent()
        _Filename = sFilename
        DateTimePicker1.Value = DtFch
    End Sub

    Private Sub Frm_NominasNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Show()
        Application.DoEvents()
        LoadFch()

        Xl_ProgressBar1.Visible = True

        _BackGroundWorker = New BackgroundWorker
        _BackGroundWorker.WorkerReportsProgress = True
        _BackGroundWorker.WorkerSupportsCancellation = True
        AddHandler _BackGroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
        AddHandler _BackGroundWorker.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
        AddHandler _BackGroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted

        'Dim oDoWorkEventArgs As New DoWorkEventArgs(_DataSource)
        _BackGroundWorker.RunWorkerAsync()


    End Sub

    Private Sub BackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs)
        Dim exs As New List(Of Exception)
        _Nominas = BLLNominaEscura.Factory(_Filename, DateTimePicker1.Value, BLLSession.Current.User, AddressOf Do_Progress, exs)
        e.Result = exs
    End Sub

    Private Sub BackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        Xl_NominasNew1.Load(_Nominas)
        LoadTotal()
        Xl_ProgressBar1.ShowProgress(0, 100, e.ProgressPercentage, e.UserState, _CancelRequest)
    End Sub

    Private Sub Do_Progress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        _BackGroundWorker.ReportProgress(100 * value / max, label)
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        Dim exs As List(Of Exception) = e.Result
        If exs.Count = 0 Then
            Xl_NominasNew1.Load(_Nominas)
            LoadTotal()
            ButtonOk.Enabled = True

            _IsLoaded = True
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs, "error al redactar les nómines")
        End If
        Xl_ProgressBar1.Visible = False
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oNominas As List(Of DTONomina) = Xl_NominasNew1.SelectedValues()

        Xl_ProgressBar1.Visible = True
        Dim cancelRequest As Boolean
        For Each oNomina In oNominas
            Dim sStaff As String = BLLStaff.AliasOrNom(oNomina.Staff)
            Xl_ProgressBar1.ShowProgress(0, oNominas.Count, oNominas.IndexOf(oNomina), "desant nómina de " & sStaff, cancelRequest)
            If cancelRequest Then Exit For

            Dim exs2 As New List(Of Exception)
            If Not BLLNomina.Update(oNomina, exs2) Then
                Dim ex As New Exception(sStaff & "- error al desar la nómina:")
                exs.Add(ex)
                exs.AddRange(exs2)
            End If
        Next


        If exs.Count = 0 Then
            Dim oFrm As New Frm_BancTransferFactory(Frm_BancTransferFactory.Modes.Staff)
            oFrm.Show()
            Me.Close()
        Else
            Xl_ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If


    End Sub


    Private Function Save(oNominas As Nominas, Progress As ProgressBarHandler, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        Dim idx As Integer
        For Each oItm As Nomina In oNominas
            idx += 1
            Dim iPercent As Integer = 100 * idx / oNominas.Count
            Dim sUserState As String = "registrant nómina de " & BLLStaff.AliasOrNom(oItm.Staff) & "..."
            Progress(0, oNominas.Count, idx, sUserState, False)
            Dim ex2s As New List(Of Exception)
            If Not NominaLoader.Update(oItm, ex2s) Then
                exs.Add(New Exception("error al registrar la nomina de " & BLLStaff.AliasOrNom(oItm.Staff)))
                exs.AddRange(ex2s)
                retval = False
            End If
        Next
        Return retval
    End Function


    Private Function CurrentFch() As Date
        Dim retval As Date = DateTimePicker1.Value
        Return retval
    End Function


    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs)
        If _AllowEvents Then
            Xl_NominasNew1.Fch = DateTimePicker1.Value
        End If
    End Sub

    Private Sub Xl_NominasNew1_AfterItemCheckedChange(sender As Object, e As MatEventArgs)
        LoadTotal()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub LoadFch()
        Dim DtFch As Date = New Date(Today.Year, Today.Month, 1).AddMonths(1).AddDays(-1)
        DateTimePicker1.Value = DtFch
    End Sub


    Private Sub LoadTotal()
        Dim oNominas As List(Of DTONomina) = Xl_NominasNew1.SelectedValues()
        LabelUserStatus.Text = "total " & BLLApp.GetAmt(oNominas.Sum(Function(x) x.Liquid.Eur)).CurFormatted
        LabelUserStatus.Visible = True
    End Sub


End Class