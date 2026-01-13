Imports System.Windows

Public Class DTODoc
    Property Dest As List(Of String)
    Property Fch As Date
    Property Num As String
    Property Obs As List(Of String)
    Property Itms As List(Of DTODocItm)
    Property Cur As DTOCur
    Property Lang As DTOLang
    Property Dto As Decimal
    Property PuntsQty As Integer
    Property PuntsTipus As Decimal
    Property PuntsBase As DTOAmt
    Property Dpp As Decimal

    Property Valorat As Boolean
    Property IvaBaseQuotas As List(Of DTOTaxBaseQuota)
    Property RecarrecEquivalencia As Boolean
    Property Coletillas As List(Of String)

    Property DisplayTotalLogistic As Boolean
    Property DisplayPunts As Boolean
    Property WriteTemplate As Boolean
    Property Incoterm As DTOProveidor.Incoterms
    Property BOC As Boolean 'Ensobradora: Begining of Collection
    Property EOC As Boolean 'Ensobradora: End of Collection
    Property SelFeed1 As Boolean 'Ensobradora: End of Collection
    Property Estilo As Estilos

    Property CustomLines As List(Of String)

    Property SideLabel As SideLabels

    Public Enum Estilos
        Comanda
        Albara
        Factura
        Proforma
    End Enum

    Public Enum SumConcepts
        SumaDeImportes
        BaseImponible
        SumaAnterior
        SumaParcial
        SumaySigue
        Total
    End Enum

    Public Enum SideLabels
        None
        Export
        FacturaRectificativa
    End Enum

    Public Enum FontStyles
        Regular
        Bold
        Italic
        Underline
    End Enum


    Public Sub New(oEstilo As DTODoc.Estilos, oLang As DTOLang, oCur As DTOCur, Optional ByVal BlWriteTemplate As Boolean = True)
        MyBase.New()
        _Estilo = oEstilo
        _Lang = oLang
        _Cur = oCur
        _WriteTemplate = BlWriteTemplate
        _Dest = New List(Of String)
        _Obs = New List(Of String)
        _Itms = New List(Of DTODocItm)
        _IvaBaseQuotas = New List(Of DTOTaxBaseQuota)
    End Sub

    Public Function BackColor() As SixLabors.ImageSharp.Color
        Dim retval As SixLabors.ImageSharp.Color
        Select Case Estilo
            Case Estilos.Comanda
                retval = Color.Yellow
            Case Estilos.Albara
                retval = Color.LightSalmon
            Case Estilos.Factura
                retval = Color.SkyBlue
            Case Estilos.Proforma
                retval = Color.LightGreen
        End Select
        Return retval
    End Function

    Public Function DtoColumnDisplay() As Boolean
        Dim retVal As Boolean = _Itms.Exists(Function(x) x.Dto <> 0)
        Return retVal
    End Function

    Public Function Descomptes() As Boolean
        Return (_Dto <> 0) 'Or _Dpp <> 0 Or _PuntsTipus <> 0)
    End Function

    Public Function Impostos() As Boolean
        Return _IvaBaseQuotas.Count > 0
    End Function
End Class

Public Class DTODocItm
    Property Ref As String
    Property Text As String
    Property Qty As Integer
    Property Preu As DTOAmt
    Property Dto As Decimal
    Property Punts As Decimal
    Property Boxes As Integer
    Property m3 As Decimal
    Property Kg As Integer
    Property Hyperlink As String

    Property FontStyle As DTODoc.FontStyles
    Property MinLinesBeforeEndPage As Integer
    Property LeftPadChars As Integer

    Public Sub New(Optional ByVal sText As String = "", Optional ByVal oFontStyle As DTODoc.FontStyles = Nothing, Optional ByVal IntQty As Integer = 0, Optional ByVal oPreu As DTOAmt = Nothing, Optional ByVal SngDto As Decimal = 0, Optional ByVal SngPunts As Decimal = 0, Optional ByVal IntLeftPadChars As Integer = 0, Optional ByVal IntMinLinesBeforeEndPage As Integer = 0, Optional ByVal sRef As String = "", Optional Hyperlink As String = "")
        MyBase.New()
        _Ref = sRef
        _Text = sText
        If oFontStyle = Nothing Then
            _FontStyle = DTODoc.FontStyles.Regular ' oFontStyle
        Else
            _FontStyle = oFontStyle
        End If
        _Qty = IntQty
        _Preu = oPreu
        _Dto = SngDto
        _Punts = SngPunts
        _LeftPadChars = IntLeftPadChars
        _MinLinesBeforeEndPage = IntMinLinesBeforeEndPage
    End Sub

    Public Function Import() As DTOAmt
        Dim retval As DTOAmt = DTOAmt.FromQtyPriceDto(_Qty, _Preu, _Dto)
        Return retval
    End Function


End Class
