Public Class Frm_Banks
    Private _Mode As Modes
    Private _AllowEvents As Boolean = False
    Private _DefaultCountry As DTOCountry

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

    Private Sub Frm_Banks_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadCountries()
        _AllowEvents = True
    End Sub

    Private Sub LoadCountries()
        If _DefaultCountry Is Nothing Then _DefaultCountry = BLL.BLLCountries.DefaultCountry
        Dim oCountries As List(Of DTOCountry) = BLL.BLLBanks.Countries(BLL.BLLApp.Lang)
        Xl_Countries1.Load(oCountries, _DefaultCountry)
        LoadBanks()
    End Sub

    Private Sub LoadBanks()
        Dim oCountry As DTOCountry = Xl_Countries1.Value
        Dim oBanks As List(Of DTOBank) = BLL.BLLBanks.FromCountry(oCountry)
        Xl_Banks1.Load(oBanks, IIf(_Mode = Modes.SelectBank, BLL.Defaults.SelectionModes.Selection, BLL.Defaults.SelectionModes.Browse))
        LoadBranches()
    End Sub

    Private Sub LoadBranches()
        Dim oBank As DTOBank = Xl_Banks1.Value
        Dim oBranches As List(Of DTOBankBranch) = BLL.BLLBankBranches.FromBank(oBank)
        Xl_BankBranches1.Load(oBranches, IIf(_Mode = Modes.SelectBranch, BLL.Defaults.SelectionModes.Selection, BLL.Defaults.SelectionModes.Browse))
    End Sub

    Private Sub Xl_Countries1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Countries1.ValueChanged
        If _AllowEvents Then
            LoadBanks()
        End If
    End Sub

    Private Sub Xl_BankBranches1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_BankBranches1.OnItemSelected
        If _AllowEvents Then
            RaiseEvent OnItemSelected(Me, e)
            Me.Close()
        End If
    End Sub

    Private Sub Xl_Banks1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Banks1.RequestToAddNew
        Dim oBank As DTOBank = BLL.BLLBank.NewBank(Xl_Countries1.Value)
        Dim oFrm As New Frm_Bank(oBank)
        AddHandler oFrm.AfterUpdate, AddressOf LoadBanks
        oFrm.Show()
    End Sub

    Private Sub Xl_Banks1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_Banks1.ValueChanged
        If _AllowEvents Then
            LoadBranches()
        End If
    End Sub

    Private Sub Xl_Banks1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Banks1.OnItemSelected
        RaiseEvent OnItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_BankBranches1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BankBranches1.RequestToAddNew
        Dim oBranch As DTOBankBranch = BLL.BLLBankBranch.NewBranch(Xl_Banks1.Value)
        Dim oFrm As New Frm_BankBranch(oBranch)
        AddHandler oFrm.AfterUpdate, AddressOf LoadBranches
        oFrm.Show()
    End Sub


    Private Sub Xl_Banks1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Banks1.RequestToRefresh
        LoadBanks()
    End Sub
End Class