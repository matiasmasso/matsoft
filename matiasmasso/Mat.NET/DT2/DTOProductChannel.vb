Public Class DTOProductChannel
    Inherits DTOBaseGuid

    Property Product As DTOProduct
    Property DistributionChannel As DTODistributionChannel
    Property Cod As Cods

    Property Inherited As Boolean

    Public Enum Cods
        Inclou
        Exclou
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
