Public Class Frm_SkuPmcs
    Private _Sku As DTOProductSku

    Public Sub New(oSku As DTOProductSku)
        MyBase.New
        InitializeComponent()
        _Sku = oSku
    End Sub

    Private Async Sub Frm_SkuPmcs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.DeliveryItems.All(exs, _Sku)
        If exs.Count = 0 Then
            Xl_SkuPmcs1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_SkuPmcs1_RequestToUpdatePmcs(sender As Object, e As MatEventArgs) Handles Xl_SkuPmcs1.RequestToUpdatePmcs
        Dim exs As New List(Of Exception)
        If Await FEB2.Mgz.SetPrecioMedioCoste(_Sku, GlobalVariables.Emp.Mgz, exs) Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class