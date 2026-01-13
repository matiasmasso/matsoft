Public Class Frm_Taxes
    Private _DefaultValue As DTOTax
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOTax = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Taxes_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Taxes1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Taxes1.itemselected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Taxes1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Taxes1.RequestToAddNew
        Dim oTax As New DTOTax
        Dim oFrm As New Frm_Tax(oTax)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Taxes1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Taxes1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oTaxes = Await FEB.Taxes.AllAsync(exs)

        If exs.Count = 0 Then
            DTOApp.Current.Taxes = oTaxes
            Xl_Taxes1.Load(oTaxes, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class