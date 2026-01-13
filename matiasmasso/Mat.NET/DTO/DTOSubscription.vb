Public Class DTOSubscription
    Property Emp As DTOEmp
    Property Id As Ids
    Property Nom As DTOLangText
    Property Dsc As DTOLangText
    Property Reverse As Boolean = False 'Reverse: señala els que han retirat la subscripcio
    Property Rols As List(Of DTORol)
    Property IsLoaded As Boolean

    Public Enum Ids
        NotSet
        Stocks
        Facturacio
        ConfirmacioEnviament
        ConfirmacioComanda_Deprecated
        AvisArribadaCamion
        VencimentsPropers
        Noticias
        NovaIncidencia
        IT
        Blog
        Comptabilitat
        CopiaAvisVenciment
        CopiaFactura
        StoreLocatorExcelMailing
        TransportOrdenDeCarga
        Transmisio
        RocheAltaGln = 90
        EmailReport
        AltaClient
        CopiaSorteigConfirmacioParticipacio
        CopiaSorteigWinnerCongrats
        AvisRecollidesServeiTecnic
        IncidenciasPedido
    End Enum

    Public Sub New(oId As Ids)
        MyBase.New()
        _Id = oId
        _Rols = New List(Of DTORol)
    End Sub

    Shared Function GetNom(oSubscription As DTOSubscription, Optional ByVal oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.Current.Lang
        'Dim retval As String = oLang.Tradueix(oSubscription.Nom_ESP, oSubscription.Nom_CAT, oSubscription.Nom_ENG)
        Dim retval As String = oSubscription.Nom.Tradueix(oLang) ' oLang.Tradueix(oSubscription.Nom_ESP, oSubscription.Nom_CAT, oSubscription.Nom_ENG)
        Return retval
    End Function

    Shared Function NomOrId(oSubscription As DTOSubscription, Optional ByVal oLang As DTOLang = Nothing) As String
        Dim sNom As String = oSubscription.Nom.Tradueix(oLang)
        Dim retval As String = IIf(sNom = "", oSubscription.Id.ToString, sNom)
        Return retval
    End Function

    Shared Function GetDsc(oSubscription As DTOSubscription, Optional ByVal oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.Current.Lang
        Dim retval As String = oSubscription.Dsc.Tradueix(oLang)
        Return retval
    End Function
End Class
