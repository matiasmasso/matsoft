
Imports Microsoft.Office.Interop

Public Class Frm_NominasNew
    Private _Filename As String
    Private _NominasEscuraFile As NominasEscuraFile
    Private _Nominas As Nominas
    Private _BackGroundWorker As New System.ComponentModel.BackgroundWorker
    Private _Cancel As Boolean
    Private _IsLoaded As Boolean
    Private _AllowEvents As Boolean

    Public Sub New(sFilename As String, DtFch As Date)
        MyBase.New()
        Me.InitializeComponent()
        _Filename = sFilename
        DateTimePicker1.Value = DtFch
        LabelUserStatus.Text = ""
    End Sub

    Private Sub Frm_NominasNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Show()
        Application.DoEvents()

        LoadFch()
        LoadConcepte()
        LoadBancs()
        LoadNominas()
        LoadTotal()

        _IsLoaded = True
        _AllowEvents = True
    End Sub

    Private Sub Frm_NominasNew_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If _NominasEscuraFile IsNot Nothing Then
            _NominasEscuraFile.Close()
        End If
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of exception)
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0

        Dim oNominas As Nominas = Xl_NominasNew1.SelectedValues()
        _BackGroundWorker = New System.ComponentModel.BackgroundWorker
        _BackGroundWorker.WorkerReportsProgress = True
        AddHandler _BackGroundWorker.DoWork, AddressOf BackGroundWorker_DoWork
        AddHandler oNominas.ProgressChanged, AddressOf ReportProgress
        AddHandler _BackGroundWorker.RunWorkerCompleted, AddressOf BackGroundWorker_RunWorkerCompleted
        _BackGroundWorker.RunWorkerAsync(oNominas)


    End Sub


    Private Sub BackGroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        Dim oNominas As Nominas = e.Argument
        Dim idx As Integer
        For Each oItm As Nomina In oNominas
            idx += 1
            Dim iPercent As Integer = 100 * idx / oNominas.Count
            Dim sUserState As String = "registrant nómina de " & oItm.Staff.AliasOrRaoSocial & "..."
            _BackGroundWorker.ReportProgress(iPercent, sUserState)
            Dim exs As New List(Of exception)
            NominaLoader.Update(oItm, exs)
        Next
    End Sub

    Private Sub BackGroundWorker_RunWorkerCompleted()
        ProgressBar1.Visible = False
        LabelUserStatus.Text = ""
        ExportaFitxerBancari()
        Me.Close()
    End Sub

    Private Sub ReportProgress(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs)
        Me.ProgressBar1.Value = e.ProgressPercentage
        Me.LabelUserStatus.Text = e.UserState
    End Sub


    Private Sub ExportaFitxerBancari()
        Dim exs As New List(Of exception)
        Dim oNominas As Nominas = Xl_NominasNew1.SelectedValues
        Dim oBancTransfer As BancTransfer = GetBancTransfer(oNominas, CurrentBanc)
        If oBancTransfer.Update(exs) Then

            Dim oAeb341 As AEB341 = Nothing
            If BLL_BankTransfer.AEB341(oBancTransfer, oAeb341, exs) Then
                Dim oDlg As New Windows.Forms.SaveFileDialog
                With oDlg
                    .FileName = "AEB341_nominas_" & oBancTransfer.Cca.yea & "_" & oBancTransfer.Cca.Id & ".txt"
                    .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                    .FilterIndex = 1
                    If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                        oAeb341.Save(.FileName)
                    End If
                End With
            Else
                UIHelper.WarnError(exs, "els següents comptes no han passat la validació:")
            End If

            Me.Close()
        Else
            MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If
    End Sub


    Private Function GetBancTransfer(ByVal oNominas As Nominas, ByVal oBanc As Banc) As BancTransfer
        Dim sBancNom As String = oBanc.Abr
        Dim DtFch As Date = DateTimePicker1.Value
        Dim sConcepte As String = sBancNom & "-transferencia nómines"
        Dim oCtaDeutor As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.Ctas.PagasTreballadors)

        Dim oBancTransfer As New BancTransfer(oBanc, DtFch, sConcepte)
        With oBancTransfer
            For Each oNomina As Nomina In oNominas
                Dim oStaff As Staff = oNomina.Staff
                If oNomina.Liquid IsNot Nothing Then
                    Dim DcLiquido As Decimal = oNomina.Liquid.Eur
                    If DcLiquido > 0 Then
                        Dim oCcb As New Ccb(oCtaDeutor, oStaff, New MaxiSrvr.Amt(DcLiquido), DTOCcb.DhEnum.Debe)
                        Dim oItm As New BankTransferItm(oCcb, oStaff.Iban.Digits)
                        .Itms.Add(oItm)
                    End If
                End If
            Next

        End With
        Return oBancTransfer
    End Function

    Private Function CurrentBanc() As Banc
        Dim retval As Banc = Nothing
        If ComboBoxBanc.SelectedItem IsNot Nothing Then
            Dim oBanc As DTOBanc = ComboBoxBanc.SelectedItem
            retval = New Banc(oBanc.Guid)
        End If
        Return retval
    End Function

    Private Function CurrentFch() As Date
        Dim retval As Date = DateTimePicker1.Value
        Return retval
    End Function


    Private Sub ComboBoxBanc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxBanc.SelectedIndexChanged
        If _AllowEvents Then
            LoadConcepte()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            LoadConcepte()
            Xl_NominasNew1.Fch = DateTimePicker1.Value
        End If
    End Sub

    Private Sub Xl_NominasNew1_AfterItemCheckedChange(sender As Object, e As MatEventArgs) Handles Xl_NominasNew1.AfterItemCheckedChange
        LoadTotal()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        If _IsLoaded Then
            Me.Close()
        Else
            _Cancel = True
        End If
    End Sub

    Private Sub LoadBancs()
        Dim oBancs As List(Of DTOBanc) = BLL.BLLBancs.All()

        With ComboBoxBanc
            .DataSource = oBancs
            .DisplayMember = "Abr"
        End With

        Dim sGuid As String = BLL.BLLDefault.EmpValue(DTODefault.Codis.BancNominaTransfers)
        If GuidHelper.IsGuid(sGuid) Then
            Dim oGuid As New Guid(sGuid)
            Dim oBanc As DTOBanc = oBancs.Find(Function(x) x.Guid.Equals(oGuid))
            If oBanc IsNot Nothing Then
                ComboBoxBanc.SelectedItem = oBanc
            End If
        End If
    End Sub

    Private Sub LoadFch()
        Dim DtFch As Date = New Date(Today.Year, Today.Month, 1).AddMonths(1).AddDays(-1)
        DateTimePicker1.Value = DtFch
    End Sub

    Private Sub LoadConcepte()
        Dim iMes As Integer = CurrentFch.Month
        Dim oBanc As Banc = CurrentBanc()
        Dim sMes As String = Lang.CAT.Mes(iMes)
        Dim sb As New System.Text.StringBuilder
        If oBanc IsNot Nothing Then
            sb.Append(oBanc.AliasOrRaoSocial & " - ")
        End If
        sb.Append("nómines " & sMes)
        TextBoxConcepte.Text = sb.ToString
    End Sub

    Private Sub LoadNominas()
        Dim exs As New List(Of Exception)
        _Nominas = New Nominas
        _NominasEscuraFile = New NominasEscuraFile(DateTimePicker1.Value)
        If _NominasEscuraFile.Open(_Filename, exs) Then

            ProgressBar1.Visible = True
            Cursor = Cursors.WaitCursor
            LabelUserStatus.Text = "carregant nomines..."
            Application.DoEvents()

            Dim idx As Integer
            For Each oPage As NominaEscura In _NominasEscuraFile.Pages
                If _Cancel Then Exit For
                Application.DoEvents()

                idx += 1
                ProgressBar1.Value = 100 * idx / _NominasEscuraFile.Pages.Count
                LabelUserStatus.Text = oPage.Name
                Application.DoEvents()

                Dim oNomina As Nomina = oPage.LoadNomina(exs)
                _Nominas.Add(oNomina)
            Next

            Cursor = Cursors.Default
            ProgressBar1.Visible = False
            LabelUserStatus.Text = ""

            Xl_NominasNew1.Load(_Nominas)
            ButtonOk.Enabled = True
        Else
            UIHelper.WarnError(exs, "error al obrir el fitxer de nómines")
        End If

    End Sub

    Private Sub LoadTotal()
        Dim oNominas As Nominas = Xl_NominasNew1.SelectedValues()
        LabelUserStatus.Text = "total " & oNominas.TotalLiquid.Formatted
        LabelUserStatus.Visible = True
    End Sub


End Class