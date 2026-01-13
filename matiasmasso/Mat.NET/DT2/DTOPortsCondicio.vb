Public Class DTOPortsCondicio
    Property Id As Ids
    Property Nom As String
    Property UnitsQty As Integer
    Property UnitsMinPreu As DTOAmt
    Property PdcMinVal As DTOAmt
    Property Cod As Cods
    Property Fee As DTOAmt

    Property IsLoaded As Boolean
    Property IsNew As Boolean

    Public Enum Ids
        SegunZona
        PeninsulaBalears
        Andorra
        Canaries
        ResteDelMon
        eCom
        Portugal
    End Enum

    Public Enum Cods
        PortsPagats
        CarrecEnFactura
        PortsDeguts
        Reculliran
    End Enum

    Public Sub New(Id As Ids)
        MyBase.New
        _Id = Id
    End Sub
End Class
