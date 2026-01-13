

Public Class Frm_LiniesTelefon
    Private _DefaultValue As DTOLiniaTelefon
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOLiniaTelefon = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_LiniesTelefon_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_LiniesTelefon1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_LiniesTelefon1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_LiniesTelefon1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_LiniesTelefon1.RequestToAddNew
        Dim oLiniaTelefon As New DTOLiniaTelefon
        Dim oFrm As New Frm_LiniaTelefon(oLiniaTelefon)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_LiniesTelefon1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_LiniesTelefon1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oLiniesTelefon = Await FEB.LiniaTelefons.All(exs)
        If exs.Count = 0 Then
            Xl_LiniesTelefon1.Load(oLiniesTelefon, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class