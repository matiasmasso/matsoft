Imports VideoLibrary

Public Class Frm_SkuWith
    Private _value As GuidQty
    Private _Cache As Models.ClientCache
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event DeleteRequest(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(Optional value As GuidQty = Nothing)
        InitializeComponent()
        _value = value
    End Sub

    Private Async Sub Frm_SkuWith_LoadAsync(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Cache = Await FEB.Cache.Fetch(exs, Current.Session.User)

        If exs.Count = 0 Then
            Dim oSku As DTOProductSku = Nothing
            If _value IsNot Nothing AndAlso _value.Guid <> Nothing Then
                oSku = _Cache.FindSku(_value.Guid)
                Xl_LookupProduct1.Load(oSku, DTOProduct.SelectionModes.SelectSku)
                PictureBox1.LoadAsync(oSku.thumbnailUrl())
                Xl_LookupProduct1.Enabled = False
                ButtonDel.Enabled = True
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        Dim oSku = e.Argument
        PictureBox1.LoadAsync(oSku.thumbnailUrl())
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        _value = New GuidQty With {
            .Guid = Xl_LookupProduct1.Product.Guid,
            .Qty = NumericUpDown1.Value
            }

        RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        RaiseEvent DeleteRequest(Me, New MatEventArgs(Xl_LookupProduct1.Value))
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class