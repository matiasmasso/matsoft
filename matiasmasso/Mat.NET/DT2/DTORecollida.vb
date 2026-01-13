Public Class DTORecollida
    Property Id As Integer
    Property Fch As Date = Date.MinValue
    Property Delivery As DTODelivery
    Property OrigenNom As String
    Property OrigenAdr As String
    Property OrigenZip As DTOZip
    Property OrigenTel As String
    Property OrigenContact As String
    Property DestiNom As String
    Property DestiAdr As String
    Property DestiZip As DTOZip
    Property DestiTel As String
    Property DestiContact As String
    Property Bultos As Integer
    Property EstatDeLaMercancia As EstatsDeLaMercancia = EstatsDeLaMercancia.Desconegut
    Property Motiu As String
    Property Carrec As Carrecs
    Property Accio As Accions
    Property Items As List(Of DTOQtySku)

    Public Enum EstatsDeLaMercancia
        Desconegut
        Bo
        Malmes
    End Enum

    Public Enum Carrecs
        Indeterminat
        Nostre
        Transportista
        Magatzem
    End Enum

    Public Enum Accions
        Per_determinar
        Entrar_en_stock
        Tramitar_assegurança
    End Enum


    Shared Function Factory(oDelivery As DTODelivery) As DTORecollida
        Dim oRecollida As New DTORecollida
        With oRecollida
            .Delivery = oDelivery
            .Fch = DateTime.Today
            .OrigenNom = oDelivery.Customer.NomComercialOrDefault()
            .OrigenAdr = oDelivery.Customer.Address.Text
            .OrigenZip = oDelivery.Customer.Address.Zip
            .OrigenTel = oDelivery.Tel
            .DestiNom = oDelivery.Mgz.NomComercialOrDefault()
            Dim oAddress As DTOAddress = oDelivery.Mgz.Address
            .DestiAdr = oAddress.Text
            .DestiZip = oAddress.Zip
            .DestiTel = oDelivery.Mgz.Telefon
            .Items = New List(Of DTOQtySku)
            For Each item In oDelivery.Items
                .Items.Add(New DTOQtySku(item.Qty, item.Sku))
            Next
        End With
        Return oRecollida
    End Function

End Class
