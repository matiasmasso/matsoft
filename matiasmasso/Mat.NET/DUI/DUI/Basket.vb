Public Class Basket
    Inherits DUI.PurchaseOrder
    Property Message As String
    Property Success As Boolean

    Property ConfirmationEmailCode As ConfirmationEmailCodes

    Public Enum ConfirmationEmailCodes
        nobody
        user
        both
    End Enum

    Shared Function Sample() As DUI.Basket
        Dim retval As New DUI.Basket()
        With retval
            .Guid = New Guid("358220E6-5D5C-49F5-B4A5-0E984F8A70A9") '1ª comanda de Zabala Hoyos
            .user = New DUI.Guidnom
            .user.Guid = New Guid("9512706E-06AF-4859-B4AE-D639DEC471A7")
            .Customer = New DUI.Guidnom()
            .Customer.Guid = New Guid("34C6350A-CA3F-49B5-99B8-CD4D47B71B08") 'Zabala Hoyos
            .Fch = Today
            .ConfirmationEmailCode = ConfirmationEmailCodes.both
            .Obs = "test a eliminar"
            .items = New List(Of DUI.PurchaseOrderItem)
            Dim oItem As New DUI.PurchaseOrderItem
            With oItem
                .Qty = 1
                .Sku = New DUI.Sku
                With .Sku
                    .Guid = New Guid("86E72D6B-BA32-4E43-86F1-917E08595700")
                End With
                .Eur = 15
            End With
            .items.Add(oItem)
        End With
        Return retval
    End Function

End Class
