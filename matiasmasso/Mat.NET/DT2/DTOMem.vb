Public Class DTOMem
    Inherits DTOBaseGuid

    Property Contact As DTOContact
    Property Fch As DateTime
    Property Text As String
    Property Cod As Cods
    Property UsrLog As DTOUsrLog2
    Property Usr As DTOUser
    Property FchCreated As DateTime
    Property Docfiles As List(Of DTODocFile)

    Public Enum Cods
        Despaitx
        Rep
        Impagos
        NotSet
    End Enum

    Public Sub New()
        MyBase.New()
        _docfiles = New List(Of DTODocFile)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _docfiles = New List(Of DTODocFile)
    End Sub

    Public Shadows Function UrlSegment() As String
        Dim retval = MyBase.UrlSegment("raport")
        Return retval
    End Function

End Class
