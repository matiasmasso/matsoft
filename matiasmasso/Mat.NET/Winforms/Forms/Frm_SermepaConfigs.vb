Public Class Frm_SermepaConfigs
    Private _DefaultValue As DTOSermepaConfig
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOSermepaConfig = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_SermepaConfigs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_SermepaConfigs1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_SermepaConfigs1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_SermepaConfigs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_SermepaConfigs1.RequestToAddNew
        Dim oSermepaConfig As New DTOSermepaConfig
        Dim oFrm As New Frm_SermepaConfig(oSermepaConfig)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_SermepaConfigs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SermepaConfigs1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oSermepaConfigs = Await FEB2.SermepaConfigs.All(exs)
        If exs.Count = 0 Then
            Xl_SermepaConfigs1.Load(oSermepaConfigs, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class