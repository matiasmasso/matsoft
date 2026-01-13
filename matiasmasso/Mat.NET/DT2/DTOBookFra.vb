Public Class DTOBookFra
    Property Cca As DTOCca
    Property Cta As DTOPgcCta
    Property Contact As DTOContact
    Property FraNum As String

    Property Csv As String = ""
    Property SiiLog As DTOSiiLog
    'Property SiiErrCod As Integer
    Property SiiEstadoCuadre As Integer
    Property SiiTimestampEstadoCuadre As DateTime
    Property SiiTimestampUltimaModificacion As DateTime

    Property IvaBaseQuotas As List(Of DTOBaseQuota)
    Property IrpfBaseQuota As DTOBaseQuota
    Property ClaveExenta As String = ""
    Property TipoFra As String
    Property ClaveRegimenEspecialOTrascendencia As String
    Property Dsc As String
    Property Import As DTOInvoice.ExportCods

    Public Property IsNew As Boolean
    Public Property IsLoaded As Boolean

    Public Enum Modes
        All
        OnlyIva
        OnlyIRPF
    End Enum

    Public Sub New(oCca As DTOCca)
        MyBase.New
        _Cca = oCca
        _IvaBaseQuotas = New List(Of DTOBaseQuota)
    End Sub

    Shared Function Factory(oCca As DTOCca, oProveidor As DTOProveidor) As DTOBookFra
        Dim retval As New DTOBookFra(oCca)
        With retval
            .IsNew = True
            .Contact = oProveidor
            .Import = DTOBookFra.ImportCod(retval)
        End With
        Return retval
    End Function



    Public Function BaseDevengada() As DTOAmt
        Dim retval = DTOAmt.factory
        For Each item As DTOBaseQuota In _IvaBaseQuotas
            retval.Add(item.Base)
        Next
        Return retval
    End Function

    Public Function Total() As DTOAmt
        Dim retval = DTOAmt.factory
        For Each item As DTOBaseQuota In _IvaBaseQuotas
            retval.Add(item.Base)
            retval.Add(item.Quota)
        Next
        If _IrpfBaseQuota IsNot Nothing Then
            retval.Substract(_IrpfBaseQuota.Quota)
        End If
        Return retval
    End Function

    Public Function BaseQuotaIvaSoportat() As DTOBaseQuota
        Dim retval As DTOBaseQuota = _IvaBaseQuotas.FirstOrDefault(Function(x) x.Tipus <> 0)
        Return retval
    End Function

    Public Function BaseQuotaIvaExenta() As DTOBaseQuota
        Dim retval As DTOBaseQuota = _IvaBaseQuotas.FirstOrDefault(Function(x) x.Tipus = 0)
        Return retval
    End Function

    Public Function QuotaDeducible(oTaxes As List(Of DTOTax)) As DTOAmt
        Dim retval = DTOAmt.factory
        If _ClaveRegimenEspecialOTrascendencia = "09" Then 'intracomunitari

            Dim oTaxIva = oTaxes.FirstOrDefault(Function(x) x.Codi = DTOTax.Codis.Iva_Standard)
            If oTaxIva IsNot Nothing Then
                Dim dcTaxIvaTipus As Decimal = oTaxIva.Tipus
                retval = BaseDevengada.Percent(dcTaxIvaTipus)
            End If
        Else
            For Each item As DTOBaseQuota In _IvaBaseQuotas
                retval.Add(item.Quota)
            Next
        End If
        Return retval
    End Function

    Public Function YearMonth() As DTOYearMonth
        Dim retval As DTOYearMonth = Nothing
        If _Cca IsNot Nothing Then
            retval = New DTOYearMonth(_Cca.Fch.Year, _Cca.Fch.Month)
        End If
        Return retval
    End Function

    Shared Function ImportCod(oBookFra As DTOBookFra)
        Dim retval As DTOInvoice.ExportCods = DTOContact.ExportCod(oBookFra.Contact)
        Return retval
    End Function

    Shared Function CausasExencion() As List(Of KeyValuePair(Of String, String))
        Dim retval As New List(Of KeyValuePair(Of String, String))
        retval.Add(New KeyValuePair(Of String, String)("", "(sel·leccionar causa exempció)"))
        retval.Add(New KeyValuePair(Of String, String)("E1", "E1 (Art.20)"))
        retval.Add(New KeyValuePair(Of String, String)("E2", "E2 (Art.21) Extracomunitari"))
        retval.Add(New KeyValuePair(Of String, String)("E3", "E3 (Art.22)"))
        retval.Add(New KeyValuePair(Of String, String)("E4", "E4 (Art.24)"))
        retval.Add(New KeyValuePair(Of String, String)("E5", "E5 (Art.25) Intracomunitari"))
        retval.Add(New KeyValuePair(Of String, String)("E6", "E6-Altres"))
        Return retval
    End Function


    Shared Function TiposFra() As List(Of KeyValuePair(Of String, String))
        Dim retval As New List(Of KeyValuePair(Of String, String))
        retval.Add(New KeyValuePair(Of String, String)("", "(sel·leccionar tipus factura)"))
        retval.Add(New KeyValuePair(Of String, String)("F1", "F1 Factura"))
        retval.Add(New KeyValuePair(Of String, String)("F2", "F2 Factura Simplificada (ticket)"))
        retval.Add(New KeyValuePair(Of String, String)("F3", "F3 Factura emitida en sustitución de facturas simplificadas facturadas y declaradas"))
        retval.Add(New KeyValuePair(Of String, String)("F4", "F4 Asiento resumen de facturas"))
        retval.Add(New KeyValuePair(Of String, String)("F5", "F5 Importaciones (DUA)"))
        retval.Add(New KeyValuePair(Of String, String)("F6", "F6 Justificantes contables"))
        retval.Add(New KeyValuePair(Of String, String)("R1", "R1 Factura Rectificativa (art. 80 tres LIVA)"))
        retval.Add(New KeyValuePair(Of String, String)("R2", "R2 Factura Rectificativa (art. 80 cuatro LIVA - concurso)"))
        retval.Add(New KeyValuePair(Of String, String)("R3", "R3 Factura Rectificativa (Resto art. 80 uno y dos) – deuda incobrable"))
        retval.Add(New KeyValuePair(Of String, String)("R4", "R4 Factura Rectificativa (Resto)"))
        retval.Add(New KeyValuePair(Of String, String)("R5", "R5 Factura Rectificativa en facturas simplificadas"))
        Return retval
    End Function

    Shared Function regEspOTrascs() As List(Of KeyValuePair(Of String, String))
        Dim retval As New List(Of KeyValuePair(Of String, String))
        retval.Add(New KeyValuePair(Of String, String)("", "(sel·leccionar tipus Reg.Esp.O Trascendencia)"))
        retval.Add(New KeyValuePair(Of String, String)("01", "01 Operación de Régimen General"))
        retval.Add(New KeyValuePair(Of String, String)("02", "02 Exportación"))
        retval.Add(New KeyValuePair(Of String, String)("03", "03 Bienes usados, arte, antigüedades, colección"))
        retval.Add(New KeyValuePair(Of String, String)("04", "04 Oro de inversión"))
        retval.Add(New KeyValuePair(Of String, String)("05", "05 Régimen especial de Agencias de Viajes"))
        retval.Add(New KeyValuePair(Of String, String)("08", "08 Operaciones sujetas al IPSI / IGIC"))
        retval.Add(New KeyValuePair(Of String, String)("09", "09 Adquisiciones intracomunitarias de bienes y prestaciones de servicios"))
        retval.Add(New KeyValuePair(Of String, String)("12", "12 Operaciones de arrendamiento de local de negocio"))
        retval.Add(New KeyValuePair(Of String, String)("13", "13 Factura correspondiente a una importación (informada sin asociar a un DUA)"))
        retval.Add(New KeyValuePair(Of String, String)("16", "16 Primer semestre de 2017"))
        Return retval
    End Function

    Shared Function Caption(oBookFra As DTOBookFra) As String
        Dim retval As String = "Fra." & oBookFra.FraNum & " del " & Format(oBookFra.Cca.Fch, "dd/MM/yy")
        Return retval
    End Function


    Shared Function Filename(oExercici As DTOExercici, Optional iMes As Integer = 0, Optional ToFch As Date = Nothing) As String
        Dim retval As String = ""
        If ToFch = Nothing And iMes = 0 Then
            retval = String.Format("{0}.{1} Llibre de Factures rebudes.xlsx", oExercici.Emp.Org.Nif, oExercici.Year)
        ElseIf iMes > 0 Then
            retval = String.Format("{0}.{1}.{2} Llibre de Factures rebudes.xlsx", oExercici.Emp.Org.Nif, oExercici.Year, iMes)
        ElseIf ToFch.Day = 31 And ToFch.Month = 3 Then
            retval = String.Format("{0}.{1}.Q1 Llibre de Factures rebudes.xlsx", oExercici.Emp.Org.Nif, oExercici.Year)
        Else
            retval = String.Format("{0}.{1:yyyyMMdd} Llibre de Factures rebudes.xlsx", oExercici.Emp.Org.Nif, ToFch)
        End If

        Return retval
    End Function
End Class
