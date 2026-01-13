Public Class DTOSpvOut
    Property Yea As Integer
    Private pId As Integer
    Property Customer As DTOCustomer
    Property SuProveedorNum As String
    Property Fch As Date
    Property Kgs As Integer
    Property M3 As Decimal
    Property Bts As Integer
    Property PortsCod As DTOCustomer.PortsCodes
    Property Cod As Integer 'reparacions=4
    Property Cfp As Integer
    Property Spvs As List(Of DTOSpv)
    Property Usr As DTOUser
    Property Etq As String
    Property Nom As String
    Property Adr As String
    Property Cit As String
    Property Tel As String
    Property ECB As String
    Property Cash As Boolean
    Property Val As Decimal
    Property Dto As Single
    Property Dpp As Single
    Property Iva As Single
    Property Req As Single
    Property Recogeran As Boolean
    Property Delivery As DTODelivery


    Shared Function Factory(oUser As DTOUser, DtFch As Date) As DTOSpvOut
        Dim retval As New DTOSpvOut
        With retval
            .Usr = oUser
            .Fch = DtFch
        End With
        Return retval
    End Function

    Public Sub RestoreObjects()
        If _Delivery IsNot Nothing Then
            _Delivery.restoreObjects()
        End If
        For Each oSpv In _Spvs
            oSpv.restoreObjects()
        Next
    End Sub
End Class
