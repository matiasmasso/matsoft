Public Class DTOContactDoc
    Inherits DTOBaseGuid
    Property Contact As DTOContact
    Property Type As Types
    Property Fch As Date
    Property Ref As String
    Property DocFile As DTODocFile
    Property Obsoleto As Boolean

    Public Enum Types
        NotSet
        NIF
        Escriptura
        Contracte
        SeguretatSocial
        Academic
        Tarjeta
        Retencions
        Model_145
    End Enum

    Public Sub New(ByVal oGuid As System.Guid)
        MyBase.New(oGuid)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

End Class
