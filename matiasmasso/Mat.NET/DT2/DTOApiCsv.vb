Public Class DTOApiCsv
    Inherits DTOBaseGuid

    Property Cod As Cods
    Property FchCreated As DateTime

    Public Enum Cods
        notset
        outVivace
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
