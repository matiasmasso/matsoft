Imports System.Net

Public Class Frm_Contracts
    Private _User As DTOUser
    Private _Contact As DTOContact
    Private _DefaultValue As DTOContract
    Private _AllContracts As List(Of DTOContract)
    Private _SelectionMode As DTO.Defaults.SelectionModes
    Private _AllowEvents As Boolean

    Public Event ItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oContact As DTOContact = Nothing)
        MyBase.New
        InitializeComponent()

        _User = Current.Session.User
        _Contact = oContact
    End Sub

    Public Sub New(oDefaultValue As DTOContract, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        InitializeComponent()

        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub


    Private Async Sub Frm_Contracts_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Await RefrescaCodis() Then
            ImprimirActiusToolStripMenuItem.Text = String.Format("Imprimir actius a {0:dd/MM/yy}", DateTimePicker1.Value)
            Await LoadContracts()
            _AllowEvents = True
        End If
    End Sub

    Private Sub Xl_ContractCodis1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ContractCodis1.ValueChanged
        If _AllowEvents Then
            RefrescaContracts()
        End If
    End Sub

    Private Sub Xl_ContractCodis1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ContractCodis1.RequestToAddNew
        Dim oContractCodi As New DTOContractCodi
        oContractCodi.Nom = "(nou tipus de contracte)"
        Dim oFrm As New Frm_ContractCodi(oContractCodi)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaCodis
        oFrm.Show()
    End Sub

    Private Sub Xl_Contracts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contracts1.RequestToAddNew
        Dim oContract As New DTOContract
        oContract.Nom = "(nou contracte)"
        oContract.Codi = Xl_ContractCodis1.Value

        Dim oFrm As New Frm_Contract(oContract)
        AddHandler oFrm.AfterUpdate, AddressOf LoadContracts
        oFrm.Show()
    End Sub

    Private Sub CheckBoxFch_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFch.CheckedChanged
        DateTimePicker1.Enabled = CheckBoxFch.Checked
        RefrescaContracts()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        ImprimirActiusToolStripMenuItem.Text = String.Format("Imprimir actius a {0:dd/MM/yy}", DateTimePicker1.Value)
        RefrescaContracts()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oContracts = CurrentContracts()
        Dim oSheet As MatHelper.Excel.Sheet = FEB.Contracts.Excel(oContracts)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Contracts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Contracts1.onItemSelected
        RaiseEvent ItemSelected(Me, e)
    End Sub

    Private Function CurrentContracts() As List(Of DTOContract)
        Dim retval = _AllContracts

        If Not IncludePrivatsToolStripMenuItem.Checked Then
            retval = retval.
                Where(Function(x) x.Privat = False).
                ToList
        End If

        If CheckBoxFch.Checked Then
            Dim DtFch As Date = DateTimePicker1.Value
            retval = retval.
                Where(Function(x) x.FchFrom <= DtFch And (x.FchTo >= DtFch Or x.FchTo <> Nothing)).
                ToList
        End If

        Return retval
    End Function

    Private Async Sub LoadContracts(sender As Object, e As MatEventArgs)
        Await LoadContracts()
    End Sub

    Private Async Function LoadContracts() As Task(Of Boolean)
        Dim retval As Boolean
        Dim exs As New List(Of Exception)
        _AllContracts = Await FEB.Contracts.All(exs, Current.Session.User, oContact:=_Contact)
        If exs.Count = 0 Then
            RefrescaContracts()
            retval = True
        Else
            UIHelper.WarnError(exs)
        End If
        Return retval
    End Function

    Private Async Sub RefrescaCodis(sender As Object, e As MatEventArgs)
        Await RefrescaCodis()
    End Sub

    Private Async Function RefrescaCodis() As Task(Of Boolean)
        Dim retval As Boolean
        Dim exs As New List(Of Exception)
        Dim oCodis = Await FEB.ContractCodis.All(exs)
        If exs.Count = 0 Then
            Xl_ContractCodis1.Load(oCodis)
            retval = True
        Else
            UIHelper.WarnError(exs)
        End If
        Return retval
    End Function

    Private Sub RefrescaContracts()
        Dim oCodi = Xl_ContractCodis1.Value
        Dim oContracts = CurrentContracts().
            Where(Function(x) x.Codi.Equals(oCodi)).
            ToList

        Xl_Contracts1.Load(oContracts, _DefaultValue, _SelectionMode)
    End Sub

    Private Async Sub Xl_Contracts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contracts1.RequestToRefresh
        Await LoadContracts()
    End Sub

    Private Sub ImprimirActiusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImprimirActiusToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New FolderBrowserDialog
        With oDlg
            .ShowNewFolderButton = True
            If .ShowDialog = DialogResult.OK Then
                ProgressBar1.Visible = True
                Application.DoEvents()
                Dim oCodi = Xl_ContractCodis1.Value
                Dim path As String = String.Format("{0}\contractes {1}", .SelectedPath, oCodi.Nom)
                System.IO.Directory.CreateDirectory(path)

                Dim oContracts = CurrentContracts().
                    Where(Function(x) x.Codi.Equals(oCodi) And x.DocFile IsNot Nothing And x.Privat = False And x.FchFrom <= DateTimePicker1.Value And (x.FchTo = Nothing Or x.FchTo >= DateTimePicker1.Value)).
                    ToList

                For Each oContract In oContracts
                    Dim url = FEB.DocFile.DownloadUrl(oContract.DocFile, True)
                    Dim fileName As String = String.Format("{0}\{1}.{2}.pdf", path, Sanitize(oContract.ContactNom()), Sanitize(oContract.Num))
                    Using client As New WebClient()
                        client.DownloadFile(url, fileName)
                    End Using
                Next
                ProgressBar1.Visible = False
            End If
        End With


    End Sub

    Private Function Sanitize(src As String) As String
        Dim invalids = System.IO.Path.GetInvalidFileNameChars()
        Dim retval = String.Join("_", src.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd(".")
        Return retval
    End Function
End Class