Public Class Frm_DeliveryTraspassos
    Private _DefaultValue As DTODeliveryTraspas
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTODeliveryTraspas = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_DeliveryTraspassos_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_DeliveryTraspassos1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_DeliveryTraspassos1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_DeliveryTraspassos1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DeliveryTraspassos1.RequestToAddNew
        Dim oDeliveryTraspas As New DTODeliveryTraspas
        Dim oFrm As New Frm_DeliveryTraspas(oDeliveryTraspas)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_DeliveryTraspassos1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DeliveryTraspassos1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim values = Await FEB2.DeliveryTraspassos.All(exs, GlobalVariables.Emp)
        Dim oMgzs = Await FEB2.Mgzs.All(GlobalVariables.Emp, exs)
        If exs.Count = 0 Then
            Xl_DeliveryTraspassos1.Load(values, oMgzs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class