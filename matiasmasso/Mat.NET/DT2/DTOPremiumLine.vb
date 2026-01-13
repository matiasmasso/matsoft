Public Class DTOPremiumLine
    Inherits DTOBaseGuid
    Property Nom As String
    Property Fch As Date
    Property Codi As Codis

    Property Products As List(Of DTOProductCategory)

    Public Enum wellknowns
        NotSet
        RomerPremiumLine
    End Enum

    Public Enum Codis
        NotSet
        DefaultInclude 'Inclou els clients que no estiguin exclosos
        DefaultExclude 'exclou els clients que no estiguin inclosos
    End Enum

    Public Sub New()
        MyBase.New()
        _Products = New List(Of DTOProductCategory)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Products = New List(Of DTOProductCategory)
    End Sub

    Shared Function wellknown(owellknown As DTOPremiumLine.wellknowns) As DTOPremiumLine
        Dim retval As DTOPremiumLine = Nothing
        Select Case owellknown
            Case DTOPremiumLine.wellknowns.RomerPremiumLine
                retval = New DTOPremiumLine(New Guid("95B72BC7-60B3-4D57-9997-B1A38DDA2A89"))
        End Select
        Return retval
    End Function
End Class

Public Class DTOPremiumCustomer
    Inherits DTOBaseGuid
    Property PremiumLine As DTOPremiumLine
    Property Customer As DTOCustomer
    Property Codi As Codis

    Property Obs As String
    Property DocFile As DTODocFile
    Property UsrLog As DTOUsrLog

    Public Enum Codis
        NotSet
        Included
        Excluded
    End Enum

    Public Sub New()
        MyBase.New()
        _UsrLog = New DTOUsrLog()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _UsrLog = New DTOUsrLog()
    End Sub
End Class
