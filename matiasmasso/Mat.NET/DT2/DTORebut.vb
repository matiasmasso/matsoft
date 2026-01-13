Public Class DTORebut
    Property Lang As DTOLang
    Property Id As String
    Property Amt As DTOAmt
    Property Fch As Date
    Property Vto As Date
    Property Concepte As String
    Property IbanDigits As String
    Property Nom As String
    Property Adr As String
    Property Cit As String

    Public Sub New(
        Optional ByVal oLang As DTOLang = Nothing,
        Optional ByVal sId As String = "",
        Optional ByVal oAmt As DTOAmt = Nothing,
        Optional ByVal DtFch As Date = Nothing,
        Optional ByVal DtVto As Date = Nothing,
        Optional ByVal sNom As String = "",
        Optional ByVal sAdr As String = "",
        Optional ByVal sCit As String = "",
        Optional ByVal sConcepte As String = "",
        Optional ByVal sIbanDigits As String = "")

        MyBase.New()
        _Lang = oLang
        _Id = sId
        _Amt = oAmt
        _Fch = DtFch
        _Vto = DtVto
        _Concepte = sConcepte
        _IbanDigits = sIbanDigits
        _Nom = sNom
        _Adr = sAdr
        _Cit = sCit
    End Sub
End Class
