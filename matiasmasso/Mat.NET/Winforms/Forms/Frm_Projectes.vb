Public Class Frm_Projectes

    Private _DefaultValue As DTOProjecte
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOProjecte = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Projectes_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Projectes1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Projectes1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Projectes1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Projectes1.RequestToAddNew
        Dim oProjecte As New DTOProjecte
        oProjecte.FchFrom = Today
        Dim oFrm As New Frm_Projecte(oProjecte)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Projectes1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Projectes1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oProjectes = Await FEB2.Projectes.All(exs)
        If exs.Count = 0 Then
            Xl_Projectes1.Load(oProjectes, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class