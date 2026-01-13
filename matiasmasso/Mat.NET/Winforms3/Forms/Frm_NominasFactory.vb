Public Class Frm_NominasFactory
    Private _Filename As String
    Private _Nominas As List(Of DTONomina)
    Private _Fch As Date

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(Optional filename As String = "")
        MyBase.New
        InitializeComponent()
        _Filename = filename
    End Sub

    Private Async Sub Frm_NominasFactory_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not String.IsNullOrEmpty(_Filename) Then
            Await refresca()
        End If
    End Sub

    Private Async Function refresca() As Task
        UIHelper.ToggleProggressBar(Panel1, True)
        Application.DoEvents()

        Dim exs As New List(Of Exception)
        Dim fch As Date = Nothing
        Dim nif As String = ""
        If NominaEscuraHelper.CheckFile(exs, _Filename, Current.Session.User, fch) Then
            LabelInfo.Text = String.Format("nomines {0} del {1:dd/MM/yy}", GlobalVariables.Emp.Org.nom, fch)
            _Nominas = Await NominaEscuraHelper.nominas(exs, _Filename, Current.Session.User, AddressOf ShowProgress)

            Xl_ProgressBar1.Visible = False
            If exs.Count = 0 Then
                Xl_NominasFactory1.Load(_Nominas, _Nominas)
                LoadTotal()
                ButtonOk.Enabled = True
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
                Me.Close()
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Function


    Private Sub ShowProgress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        Static done As Boolean
        If Not done Then
            UIHelper.ToggleProggressBar(Panel1, False)
            Xl_ProgressBar1.Visible = True
            Xl_ProgressBar1.ButtonCancel.Visible = False
            Application.DoEvents()
            done = True
        End If
        Xl_ProgressBar1.ShowProgress(min, max, value, label, CancelRequest)
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oNominas As List(Of DTONomina) = Xl_NominasFactory1.CheckedValues

        Xl_ProgressBar1.Visible = True
        Dim cancelRequest As Boolean
        For Each oNomina In oNominas
            Dim sStaff As String = DTOStaff.AliasOrNom(oNomina.Staff)
            Xl_ProgressBar1.ShowProgress(0, oNominas.Count, oNominas.IndexOf(oNomina), "desant nómina de " & sStaff, cancelRequest)
            If cancelRequest Then Exit For

            Dim exs2 As New List(Of Exception)
            If Not Await FEB.Nomina.Update(oNomina, exs2) Then
                Dim ex As New Exception(sStaff & "- error al desar la nómina:")
                exs.Add(ex)
                exs.AddRange(exs2)
            End If
        Next


        If exs.Count = 0 Then
            RaiseEvent afterupdate(Me, MatEventArgs.empty)
            Dim oFrm As New Frm_BancTransferFactory(Frm_BancTransferFactory.Modes.Staff)
            oFrm.Show()
            Me.Close()
        Else
            Xl_ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If


    End Sub


    Private Function CurrentFch() As Date
        Dim retval As Date = DateTimePicker1.Value
        Return retval
    End Function


    Private Sub Xl_NominasNew1_AfterItemCheckedChange(sender As Object, e As MatEventArgs)
        LoadTotal()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub LoadFch()
        If _Fch = Nothing Then
            _Fch = New Date(DTO.GlobalVariables.Today().Year, DTO.GlobalVariables.Today().Month, 1).AddMonths(1).AddDays(-1)
        End If
        DateTimePicker1.Value = _Fch
    End Sub

    Private Sub LoadTotal()
        Dim oNominas As List(Of DTONomina) = Xl_NominasFactory1.CheckedValues
        LabelUserStatus.Text = "total: " & DTOAmt.CurFormatted(DTOAmt.Factory(oNominas.Sum(Function(x) x.Liquid.Eur)))
        LabelUserStatus.Visible = True
    End Sub

    Private Async Sub ImportarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "fitxers pdf|*.pdf"
            If .ShowDialog Then
                _Filename = .FileName
                Await refresca()
            End If
        End With
    End Sub


End Class