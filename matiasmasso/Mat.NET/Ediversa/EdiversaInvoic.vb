Public Class EdiversaInvoic
    Inherits EdiversaBaseFile

    Property id As String
    Property fch As Date
    Property tipoDocumento As TiposDocumento
    Property refDocs As List(Of RefDoc)
    Property items As List(Of Item)
    Property totals As List(Of Amt)
    Property cur As String


    Public Enum TiposDocumento
        NotSet
        FacturaComercial = 380
        Abono = 381
        NotaDeCargo = 383
        FacturaCorregida = 384
        FacturaRecapitulativa = 385
    End Enum


    Public Sub New()
        MyBase.New
        _refDocs = New List(Of RefDoc)
        _items = New List(Of Item)
    End Sub

    Shared Function Factory(src As String, exs As List(Of Exception)) As EdiversaInvoic
        Dim retval As New EdiversaInvoic
        Dim item As Item = Nothing
        For Each segment In segments(src)
            Select Case segment.tag
                Case "INV"
                    retval.id = segment.stringValue(1)
                    retval.tipoDocumento = segment.integerValue(2)
                Case "DTM"
                    retval.fch = segment.dateValue(1, exs)
                Case "RFF"
                    Dim oRefDoc = RefDoc.factory(segment, exs)
                    retval.refDocs.Add(oRefDoc)
                Case "NADSCO"
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.proveedor)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .refMercantil = segment.stringValue(3)
                        .domicilio = segment.stringValue(4)
                        .poblacion = segment.stringValue(5)
                        .zip = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                        .pais = segment.stringValue(8)
                    End With
                Case "NADBCO"
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.cliente)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .nif = segment.stringValue(6)
                        .pais = segment.stringValue(7)
                    End With
                Case "NADSU"
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.proveedor)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .refMercantil = segment.stringValue(3)
                        .domicilio = segment.stringValue(4)
                        .poblacion = segment.stringValue(5)
                        .zip = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                        .codProveedor = segment.stringValue(8)
                        .codCliente = segment.stringValue(9)
                        .pais = segment.stringValue(14)
                    End With
                Case "NADBY"
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.cliente)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .nif = segment.stringValue(6)
                        .seccion = segment.stringValue(7)
                        .pais = segment.stringValue(8)
                    End With

                Case "NADII" 'emisor de la factura
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.emisor)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .nif = segment.stringValue(6)
                        .pais = segment.stringValue(7)
                    End With
                Case "NADIV" 'receptor de la factura
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.receptor)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .nif = segment.stringValue(6)
                        .codCliente = segment.stringValue(7)
                        .aprobacion = segment.stringValue(8)
                        .pais = segment.stringValue(9)
                    End With
                Case "NADMS" 'emisor del mensaje
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.emisorMsg)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .pais = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                    End With
                Case "NADMR" 'receptor del mensaje
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.receptorMsg)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .pais = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                    End With
                Case "NADDP" 'receptor mercancia
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.receptorMercancia)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .pais = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                    End With
                Case "NADPR" 'emisor del pago
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.emisorPago)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .pais = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                    End With
                Case "NADPE" 'receptor del pago
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.receptorPago)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .pais = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                    End With
                Case "NADPW" 'punto de recogida de la mercancia
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.recogidaMercancia)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .pais = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                    End With
                Case "NADUD" 'receptor final de la mercancia
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.receptorFinalMercancia)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .pais = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                    End With
                Case "NADFW" 'agente expedidor de la carga
                    Dim oInterlocutor = retval.AddInterlocutor(Interlocutor.cods.forwarder)
                    With oInterlocutor
                        .ean = segment.eanValue(1, exs)
                        .nom = segment.stringValue(2)
                        .domicilio = segment.stringValue(3)
                        .poblacion = segment.stringValue(4)
                        .zip = segment.stringValue(5)
                        .pais = segment.stringValue(6)
                        .nif = segment.stringValue(7)
                    End With

                    '....

                Case "CUX"
                    If segment.integerValue(2) = 4 Then 'divisa de la factura
                        retval.cur = segment.stringValue(1)
                    End If

                Case "PAT" 'condiciones de pago
                Case "TDT" 'transporte
                Case "TOD" 'lugar de salida
                Case "ALC" 'descuentos y cargos globales

                Case "LIN"
                    item = retval.addItem(segment, exs)
                Case "PIALIN"
                    item.skuProveedor = segment.stringValue(1)
                    item.skuComprador = segment.stringValue(2)
                Case "IMDLIN"
                    item.skuDescription = segment.stringValue(1)
                Case "QTYLIN"
                    If segment.stringValue(1) = "47" Then 'cantidad facturada
                        item.qty = segment.stringValue(1)
                    End If
                Case "MOALIN"
                    If segment.stringValue(3) <> "" Then
                        item.addAmt(Amt.Qualifiers.grossAmt, segment, 3)
                    End If
                    If segment.stringValue(1) <> "" Then
                        item.addAmt(Amt.Qualifiers.netAmt, segment, 1)
                    End If
                Case "PRILIN"
                    Select Case segment.stringValue(1)
                        Case "AAA"
                            item.addAmt(Amt.Qualifiers.netPrice, segment, 2)
                        Case "AAB"
                            item.addAmt(Amt.Qualifiers.priceList, segment, 2)
                    End Select
                Case "RFFLIN"
                    Dim oRefDoc = RefDoc.factory(segment, exs)
                    item.refDocs.Add(oRefDoc)
                Case "TAXLIN"
                Case "ALCLIN"
                    Dim oDto = Dto.factory(segment, exs)
                    item.dtos.Add(oDto)
                Case "CNTRES"
                Case "MOARES"
                    retval.addTotal(Amt.Qualifiers.netAmt, segment, 1)
                    retval.addTotal(Amt.Qualifiers.grossAmt, segment, 2)
                    retval.addTotal(Amt.Qualifiers.base, segment, 3)
                    retval.addTotal(Amt.Qualifiers.payable, segment, 4)
                    retval.addTotal(Amt.Qualifiers.taxes, segment, 5)
                    retval.addTotal(Amt.Qualifiers.discounts, segment, 6)
                    retval.addTotal(Amt.Qualifiers.attachedDoc, segment, 7)
                    retval.addTotal(Amt.Qualifiers.charges, segment, 8)
                    retval.addTotal(Amt.Qualifiers.payable, segment, 9)
                    retval.addTotal(Amt.Qualifiers.total, segment, 10)
                    retval.addTotal(Amt.Qualifiers.tax2, segment, 11)
            End Select
        Next

        Return retval
    End Function

    Private Function addItem(oSegment As Segment, exs As List(Of Exception)) As Item
        Dim retval = Item.factory(oSegment, exs)
        _items.Add(retval)
        Return retval
    End Function

    Public Sub addTotal(qualifier As Amt.Qualifiers, segment As Segment, idx As Integer)
        Dim value = segment.decimalValue(idx)
        If value <> 0 Then
            Dim oAmt = Amt.factory(qualifier, value)
            _totals.Add(oAmt)
        End If
    End Sub

    Public Function proveidor() As Interlocutor
        Dim retval = MyBase.interlocutors.FirstOrDefault(Function(x) x.cod = Interlocutor.cods.proveedor)
        Return retval
    End Function


    Public Class Item
        Property ean As Ean
        Property skuProveedor As String
        Property skuComprador As String
        Property skuDescription As String
        Property lin As Integer
        Property qty As Integer

        Property price As Decimal
        Property netPrice As Decimal
        Property netAmount As Decimal
        Property brutAmount As Decimal
        Property refDocs As List(Of RefDoc)
        Property dtos As List(Of Dto)
        Property amts As List(Of Amt)

        Public Sub New()
            MyBase.New
            _refDocs = New List(Of RefDoc)
            _dtos = New List(Of Dto)
            _amts = New List(Of Amt)
        End Sub

        Shared Function factory(segment As Segment, exs As List(Of Exception)) As Item
            Dim retval As New Item
            If segment.stringValue(2) = "EN" Then
                retval.ean = segment.eanValue(1, exs)
            End If
            retval.lin = segment.integerValue(3)
            Return retval
        End Function

        Public Sub addAmt(qualifier As Amt.Qualifiers, segment As Segment, idx As Integer)
            Dim value = segment.decimalValue(idx)
            If value <> 0 Then
                Dim oAmt = Amt.factory(qualifier, value)
                _amts.Add(oAmt)
            End If
        End Sub
    End Class

    Public Class RefDoc
        Property id As String
        Property fch As Date
        Property calificador As Calificadors

        Public Enum Calificadors
            albaran
            pedido
            documento
            contrato
            autDevolucion
        End Enum

        Shared Function factory(segment As Segment, exs As List(Of Exception)) As RefDoc
            Dim retval As New RefDoc
            With retval
                .calificador = ParseCalificador(segment.stringValue(1))
                .id = segment.stringValue(2)
                .fch = segment.dateValue(3, exs)
            End With
            Return retval
        End Function

        Shared Function ParseCalificador(src As String) As Calificadors
            Dim retval As Calificadors
            Select Case src
                Case "DQ"
                    retval = Calificadors.albaran
                Case "ON"
                    retval = Calificadors.pedido
                Case "CD"
                    retval = Calificadors.documento
                Case "CT"
                    retval = Calificadors.contrato
            End Select
            Return retval
        End Function
    End Class

    Public Class Dto
        Property qualifier As Qualifiers
        Property sequence As Integer
        Property mode As Modes
        Property percent As Decimal
        Property amount As Decimal
        Public Enum Qualifiers
            Discount
            Charge
            Included
        End Enum

        Public Enum Modes
            volumen
            comercial
            prontopago
            otros
        End Enum

        Shared Function factory(segment As Segment, exs As List(Of Exception)) As Dto
            Dim retval As New Dto
            Select Case segment.stringValue(1)
                Case "A"
                    retval.qualifier = Qualifiers.Discount
                Case "C"
                    retval.qualifier = Qualifiers.Charge
                Case "N"
                    retval.qualifier = Qualifiers.Included
            End Select
            retval.sequence = segment.integerValue(2)
            Select Case segment.stringValue(3)
                Case "DI"
                    retval.mode = Modes.comercial
            End Select
            retval.percent = segment.decimalValue(5)
            Return retval
        End Function

    End Class

    Public Class Amt
        Property qualifier As Qualifiers
        Property value As Decimal

        Public Enum Qualifiers
            priceList
            netPrice
            netAmt
            grossAmt
            base
            payable
            taxes
            discounts
            attachedDoc
            charges
            total
            tax2
        End Enum

        Shared Function factory(qualifier As Qualifiers, value As Decimal) As Amt
            Dim retval As New Amt
            retval.qualifier = qualifier
            retval.value = value
            Return retval
        End Function
    End Class

End Class
