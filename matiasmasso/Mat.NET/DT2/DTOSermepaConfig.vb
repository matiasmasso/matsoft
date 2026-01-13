Public Class DTOSermepaConfig
    Inherits DTOBaseGuid

    Property Nom As String
    Property MerchantCode As String
    Property SignatureKey As String
    Property SermepaUrl As String
    Property MerchantURL As String
    Property UrlOK As String
    Property UrlKO As String
    Property UserAdmin As String
    Property Pwd As String
    Property FchFrom As Date
    Property FchTo As Date

    Public Enum Environments
        NotSet
        Production
        Testing
    End Enum
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

End Class
