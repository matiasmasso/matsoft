Public Class DTOApp
    Shared Property Current() As DTOApp
    Property Type As AppTypes
    Property Langs As List(Of DTOLang)
    Property Lang As DTOLang = DTOLang.ESP
    Property Curs As List(Of DTOCur)
    Property Cur As DTOCur
    Property Taxes As List(Of DTOTax)
    Property PgcPlan As DTOPgcPlan
    Shared Property WebRootUrl As String
    Property WebLocalPort As String


    Public Enum AppTypes
        NotSet
        MatNet
        Spv
        Web
        WebServices
        MatSched
        InglesinaSelfiesContest
        OldAspMatWeb
        Wcf
        WebApi
    End Enum



End Class
