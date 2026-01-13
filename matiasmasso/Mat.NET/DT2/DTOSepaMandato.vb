Public Class DTOSepaMandato
    Inherits DTOBaseGuid
    Property Iban As DTOIban
    Property Lliurador As DTOContact
    Property Ref As String
    Property FchFrom As Date
    Property FchTo As Date
    Property DocFile As DTODocFile
    Property UsrLog As DTOUsrLog


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
