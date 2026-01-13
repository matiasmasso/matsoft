Public Class DTOMatsoftSupportCase
    Inherits DTOBaseGuid

    Property Product As Products
    Property User As DTOUser
    Property FchOpen As Date
    Property FchClose As Date
    Property Dsc As String
    Property Answer As String
    <JsonIgnore> Property Screenshot As Image

    Public Enum Products
        NotSet
        MatNet
        Web
        Taller
        Altres
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
