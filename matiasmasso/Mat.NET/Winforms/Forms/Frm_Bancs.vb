Public Class Frm_Bancs
    Private _DefaultValue As DTOBanc
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOBanc = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Bancs_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Templates1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Bancs1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Bancs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Bancs1.RequestToAddNew
        Dim oBanc As New DTOBanc
        Dim oFrm As New Frm_Contact(oBanc)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Bancs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Bancs1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oBancs = Await FEB2.Bancs.All(Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Xl_Bancs1.Load(oBancs, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class