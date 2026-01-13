Public Class DTOIncentiu
    Inherits DTOBaseGuid

    Property FchFrom As Date
    Property FchTo As Date
    Property Product As DTOProduct 'marca per discriminar a qui se li comunica
    Property Channels As List(Of DTODistributionChannel)
    Property Products As List(Of DTOProduct) 'productes als que se li aplicará automaticament la promo
    Property QtyDtos As List(Of DTOQtyDto)

    Property Title As DTOLangText
    Property Excerpt As DTOLangText
    Property Bases As DTOLangText
    Property ManufacturerContribution As String

    Property CliVisible As Boolean
    Property RepVisible As Boolean

    Property OnlyInStk As Boolean
    Property Cod As Cods
    Property Evento As DTOEvento
    Property PdcsCount As Integer
    Property PointsOfSale As List(Of DTOContact)

    <JsonIgnore> Property Thumbnail As Image
    Property Unitats As Integer
    Property Dto As Integer

    Public Enum wellknowns
        NotSet
        FeriasLocales
    End Enum

    Public Enum Cods
        NotSet
        Dto
        FreeUnits
    End Enum

    Public Sub New()
        MyBase.New()
        _Title = New DTOLangText()
        _Excerpt = New DTOLangText
        _Bases = New DTOLangText
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Title = New DTOLangText()
        _Excerpt = New DTOLangText
        _Bases = New DTOLangText
    End Sub

    Shared Function wellknown(id As DTOIncentiu.wellknowns) As DTOIncentiu
        Dim retval As DTOIncentiu = Nothing
        Select Case id
            Case DTOIncentiu.wellknowns.FeriasLocales
                retval = New DTOIncentiu(New Guid("DA758FDB-983F-4A43-B0A9-70DF027CC878"))
        End Select
        Return retval
    End Function


    Shared Function GetDto(oIncentiu As DTOIncentiu, ByVal iQty As Integer) As Decimal
        Dim retval As Decimal
        For Each oItm As DTOQtyDto In oIncentiu.QtyDtos
            If iQty < oItm.Qty Then Exit For
            retval = oItm.Dto
        Next
        Return retval
    End Function


End Class

Public Class DTOIncentiuResult
    Property Incentiu As DTOIncentiu
    Property QtyDto As DTOQtyDto 'acumula les unitats dels albarans i fixa el descompte que li toca
End Class
