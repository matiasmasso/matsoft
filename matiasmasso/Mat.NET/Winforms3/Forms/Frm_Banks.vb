Public Class Frm_Banks
    Private _Mode As Modes
    Private _AllowEvents As Boolean = False
    Private _DefaultCountry As DTOCountry
    Private _DefaultBank As DTOBank
    Private _DefaultBranch As DTOBankBranch
    Private _isShowingAllCountries As Boolean

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Public Enum Modes
        Browse
        SelectBank
        SelectBranch
    End Enum

    Public Sub New(Optional oDefaultCountry As DTOCountry = Nothing, Optional oMode As Modes = Modes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Mode = oMode
        _DefaultCountry = oDefaultCountry

    End Sub

    Public Sub New(oDefaultBank As DTOBank, Optional oMode As Modes = Modes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Mode = oMode
        _DefaultCountry = oDefaultBank.Country
        _DefaultBank = oDefaultBank

    End Sub

    Public Sub New(oDefaultBranch As DTOBankBranch, Optional oMode As Modes = Modes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Mode = oMode
        If oDefaultBranch IsNot Nothing Then
            If oDefaultBranch.Bank IsNot Nothing Then
                _DefaultCountry = oDefaultBranch.Bank.Country
            End If
            _DefaultBank = oDefaultBranch.Bank
            _DefaultBranch = oDefaultBranch
        End If
    End Sub

    Private Async Sub Frm_Banks_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Await LoadCountries(exs)
        If exs.Count = 0 Then
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadCountries(exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        If _DefaultCountry Is Nothing Then _DefaultCountry = GlobalVariables.Emp.defaultCountry
        Dim oCountries As List(Of DTOCountry)
        If _isShowingAllCountries Then
            oCountries = Await FEB.Countries.All(DTOApp.Current.Lang, exs)
        Else
            oCountries = Await FEB.Banks.Countries(DTOApp.Current.Lang, exs)
        End If
        If exs.Count = 0 Then
            Xl_Countries1.Load(oCountries, _DefaultCountry)
            Await LoadBanks()
        End If
        Return retval
    End Function

    Private Async Sub LoadBanks(sender As Object, e As MatEventArgs)
        Await LoadBanks()
    End Sub

    Private Async Function LoadBanks() As Task
        Dim exs As New List(Of Exception)
        Dim oCountry As DTOCountry = Xl_Countries1.Value
        Dim oBanks = Await FEB.Banks.All(oCountry, exs)
        If exs.Count = 0 Then
            Xl_Banks1.Load(oBanks, _DefaultBank, IIf(_Mode = Modes.SelectBank, DTO.Defaults.SelectionModes.Selection, DTO.Defaults.SelectionModes.Browse))
            Await LoadBranches(exs)
            If exs.Count <> 0 Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function LoadBranches(exs As List(Of Exception), Optional oDefaultBranch As DTOBankBranch = Nothing) As Task
        If oDefaultBranch Is Nothing Then oDefaultBranch = _DefaultBranch
        Dim oBank As DTOBank = Xl_Banks1.Value
        Dim oBranches = Await FEB.BankBranches.All(oBank, exs)
        If exs.Count = 0 Then
            Xl_BankBranches1.Load(oBranches, oDefaultBranch, IIf(_Mode = Modes.SelectBranch, DTO.Defaults.SelectionModes.Selection, DTO.Defaults.SelectionModes.Browse))
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Countries1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Countries1.ValueChanged
        If _AllowEvents Then
            Dim exs As New List(Of Exception)
            Await LoadBanks()
        End If
    End Sub

    Private Sub Xl_BankBranches1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_BankBranches1.OnItemSelected
        If _AllowEvents Then
            RaiseEvent OnItemSelected(Me, e)
            Me.Close()
        End If
    End Sub

    Private Sub Xl_Banks1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Banks1.RequestToAddNew
        Dim oBank As DTOBank = DTOBank.Factory(Xl_Countries1.Value)
        Dim oFrm As New Frm_Bank(oBank)
        AddHandler oFrm.AfterUpdate, AddressOf LoadBanks
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Banks1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Banks1.ValueChanged
        If _AllowEvents Then
            Dim exs As New List(Of Exception)
            Await LoadBranches(exs)
            If exs.Count <> 0 Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Xl_Banks1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Banks1.OnItemSelected
        RaiseEvent OnItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_BankBranches1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BankBranches1.RequestToAddNew
        Dim oBranch = DTOBankBranch.Factory(Xl_Banks1.Value)
        Dim oFrm As New Frm_BankBranch(oBranch)
        AddHandler oFrm.AfterUpdate, AddressOf onNewBranch
        oFrm.Show()
    End Sub

    Private Async Sub onNewBranch(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Await LoadBranches(exs, e.Argument)
        If exs.Count <> 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Banks1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Banks1.RequestToRefresh
        Await LoadBanks()
    End Sub

    Private Async Sub Xl_BankBranches1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BankBranches1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Await LoadBranches(exs)
        If exs.Count <> 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_Countries1_ShowAllCountries(sender As Object, e As MatEventArgs) Handles Xl_Countries1.ShowAllCountries
        _isShowingAllCountries = Not _isShowingAllCountries
        Dim exs As New List(Of Exception)
        Await LoadCountries(exs)
        If exs.Count = 0 Then
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class