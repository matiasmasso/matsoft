Public Class DTODeliveryHeader
    Property Guid As Guid
    Property Id As Integer
    Property Fch As Date
    Property Customer As DTOCustomer
    Property ImportAdicional As DTOAmt
    Property Import As DTOAmt
    Property Transmisio As DTOTransmisio
    Property Invoice As DTOInvoice
    Property Cod As DTOPurchaseOrder.Codis
    Property CashCod As DTO.DTOCustomer.CashCodes
    Property PortsCod As DTO.DTOCustomer.PortsCodes
    Property Facturable As Boolean
    Property Transportista As DTOTransportista
    Property Tracking As String
    Property EtiquetesTransport As DTODocFile
    Property UsrLog As DTOUsrLog2


    Public Class DTOCustomer
        Property Guid As Guid
        Property FullNom As String
    End Class

    Public Class DTOAmt
        Property eur As Decimal
    End Class

    Public Class DTOTransmisio
        Property Guid As Guid
        Property id As Integer
    End Class

    Public Class DTOInvoice
        Property Guid As Guid
        Property num As Integer

    End Class

    Public Class DTOUsrLog
        Property usrCreated As DTOUser

    End Class
    Public Class DTOUser
        Property Guid As Guid
        Property emailAddress As String
        Property nickname As String
    End Class

    Public Class DTOTransportista
        Property Guid As Guid
        Property abr As String
    End Class

    Public Class DTODocFile
        Property hash As String
    End Class
End Class
