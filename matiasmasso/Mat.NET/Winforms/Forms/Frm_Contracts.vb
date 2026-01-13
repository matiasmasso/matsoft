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

    Public Sub New(oDefaultValue As DTOContract, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        InitializeComponent()

        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub


    Private Async Sub Frm_Contracts_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Await RefrescaCodis() Then
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
        RefrescaContracts()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim oContracts = CurrentContracts()
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = FEB2.Contracts.Excel(oContracts)
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
                Where(Function(x) x.fchFrom <= DtFch And (x.fchTo >= DtFch Or x.fchTo <> Nothing)).
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
        _AllContracts = Await FEB2.Contracts.All(exs, _User, oContact:=_Contact)
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
        Dim oCodis = Await FEB2.ContractCodis.All(exs)
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


End Class