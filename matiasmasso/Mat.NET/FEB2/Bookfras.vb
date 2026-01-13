Public Class BookFra
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oCca As DTOCca) As Task(Of DTOBookFra)
        Return Await Api.Fetch(Of DTOBookFra)(exs, "BookFra", oCca.Guid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oBookFra As DTOBookFra) As Boolean
        If Not oBookFra.IsLoaded And Not oBookFra.IsNew Then
            Dim pBookFra = Api.FetchSync(Of DTOBookFra)(exs, "BookFra", oBookFra.Cca.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBookFra)(pBookFra, oBookFra, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Pdf(exs As List(Of Exception), oBookFra As DTOBookFra) As Task(Of Byte())
        Dim retval As Byte() = Nothing
        If oBookFra IsNot Nothing AndAlso oBookFra.Cca IsNot Nothing Then
            retval = Await Api.FetchBinary(exs, "Cca/pdf", oBookFra.Cca.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oBookFra As DTOBookFra) As Task(Of Boolean)
        Return Await Api.Update(Of DTOBookFra)(oBookFra, exs, "BookFra")
        oBookFra.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oBookFra As DTOBookFra) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBookFra)(oBookFra, exs, "BookFra")
    End Function

    Shared Async Function LogSii(exs As List(Of Exception), oBookFra As DTOBookFra) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOBookFra, Boolean)(oBookFra, exs, "BookFra/LogSii")
        oBookFra.IsNew = False
    End Function

    Shared Async Function FindOrNew(exs As List(Of Exception), oCca As DTOCca) As Task(Of DTOBookFra)
        Dim retval = Await FEB2.BookFra.Find(exs, oCca)
        If FEB2.Cca.Load(oCca, exs) Then
            If retval Is Nothing Then
                retval = New DTOBookFra(oCca)
                retval.IsNew = True
                Dim oSujeta As New DTOBaseQuota(DTOAmt.Empty)
                Dim oExenta As New DTOBaseQuota(DTOAmt.Empty)
                Dim oIrpf As New DTOBaseQuota(DTOAmt.Empty)
                For Each item As DTOCcb In oCca.Items
                    If item.Cta.IsBaseImponibleIva Then
                        retval.Cta = item.cta
                        oSujeta.baseImponible.add(item.amt)
                        oIrpf.baseImponible.add(item.amt)
                        retval.Contact = item.Contact
                    ElseIf item.Cta.IsQuotaIva Then
                        If oSujeta.Quota Is Nothing Then oSujeta.Quota = DTOAmt.Empty
                        oSujeta.Quota.Add(item.Amt)
                    ElseIf item.Cta.isQuotaIrpf Then
                        If oIrpf.Quota Is Nothing Then oIrpf.Quota = DTOAmt.Empty
                        oIrpf.Quota.Add(item.Amt)
                    End If
                Next
                If oSujeta.Quota IsNot Nothing Then
                    If oSujeta.Quota.IsNotZero Then
                        oSujeta.CalcTipus()
                        retval.IvaBaseQuotas.Add(oSujeta)
                    End If
                End If
                If oExenta.baseImponible.isNotZero Then
                    retval.ivaBaseQuotas.Add(oExenta)
                End If
                If oIrpf.Quota IsNot Nothing Then
                    If oIrpf.Quota.IsNotZero Then
                        retval.IrpfBaseQuota = oIrpf
                    End If
                End If
                retval.FraNum = TextHelper.GuessFraNum(oCca.Concept)
            End If
        End If
        Return retval
    End Function



End Class


Public Class Bookfras
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oMode As DTOBookFra.Modes, oExercici As DTOExercici, Optional iMes As Integer = 0, Optional oContact As DTOContact = Nothing) As Task(Of List(Of DTOBookFra))
        Return Await Api.Fetch(Of List(Of DTOBookFra))(exs, "BookFras", oMode, oExercici.Emp.Id, oExercici.Year, iMes, OpcionalGuid(oContact))
    End Function

    Shared Function Excel(oBookFras As List(Of DTOBookFra), sSheetName As String, sFilename As String) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet(sSheetName, sFilename)
        FEB2.Bookfras.LoadExcel(retval, oBookFras)
        Return retval
    End Function

    Shared Async Function Excel(exs As List(Of Exception), oExercici As DTOExercici, Optional FchTo As Date = Nothing, Optional ShowProgress As ProgressBarHandler = Nothing) As Task(Of ExcelHelper.Sheet)
        Dim retval As ExcelHelper.Sheet = Nothing
        Dim CancelRequest As Boolean = False

        Dim items = Await FEB2.Bookfras.All(exs, DTOBookFra.Modes.All, oExercici)
        If exs.Count = 0 Then
            Dim sFilename As String = DTOBookFra.Filename(oExercici, 0, FchTo)
            retval = New ExcelHelper.Sheet(oExercici.Year, sFilename)
            If FchTo <> Nothing Then
                items = items.Where(Function(x) x.Cca.Fch <= FchTo).ToList
            End If
            FEB2.Bookfras.LoadExcel(retval, items, ShowProgress)
        End If
        Return retval
    End Function

    Shared Sub LoadExcel(ByRef oExcelSheet As ExcelHelper.Sheet, items As List(Of DTOBookFra), Optional ShowProgress As ProgressBarHandler = Nothing)
        Dim oLang As DTOLang = DTOLang.CAT

        With oExcelSheet
            .AddColumn("registre", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("proveidor", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("NIF", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("factura", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("compte", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Base sujeta", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Tipo Iva", ExcelHelper.Sheet.NumberFormats.Decimal2Digits)
            .AddColumn("Quota Iva", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Base exenta", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Clau", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Iva Comunitari", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Iva Extracomunitari", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Total devengado", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Base Irpf", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Tipo Irpf", ExcelHelper.Sheet.NumberFormats.Decimal2Digits)
            .AddColumn("Quota Irpf", ExcelHelper.Sheet.NumberFormats.Euro)
        End With

        Dim oRow As ExcelHelper.Row = oExcelSheet.AddRow
        Dim iRows As Integer = items.Count
        Dim sFormula As String = "SUM(R[+1]C[0]:R[" & iRows & "]C[0])"


        If items.Count = 0 Then
            With oRow
                .AddCell()
                .AddCell()
                .AddCell("(sense factures registrades en aquest periode)")
            End With
        Else
            With oRow
                .AddCell()
                .AddCell()
                .AddCell("Total")
                .AddCell()
                .AddCell()
                .AddCell()
                .AddFormula(sFormula) 'base sujeta
                .AddCell() 'tipus Iva
                .AddFormula(sFormula) 'quota Iva
                .AddFormula(sFormula) 'base exenta
                .AddCell() 'clau exempcio
                .AddFormula(sFormula) 'Iva comunitari
                .AddFormula(sFormula) 'Iva ExtraComunitari
                .AddFormula(sFormula) 'Total Devengat
                .AddFormula(sFormula) 'Base Irpf
                .AddCell() 'tipus Irpf
                .AddFormula(sFormula) 'Quota Irpf
            End With

            Dim oTaxIva As DTOTax = DTOTax.Closest(DTOTax.Codis.Iva_Standard, items.First.Cca.Fch)

            Dim CancelRequest As Boolean = False
            For Each item As DTOBookFra In items
                oRow = oExcelSheet.AddRow
                With item
                    If .Cca.DocFile IsNot Nothing Then
                        oRow.AddCell(.Cca.Id, FEB2.DocFile.DownloadUrl(.Cca.DocFile, True))
                    Else
                        oRow.AddCell(.Cca.Id)
                    End If
                    oRow.AddCell(.Cca.Fch)
                    oRow.AddCell(.Contact.Nom)
                    oRow.AddCell(.contact.PrimaryNifValue())
                    oRow.AddCell(.FraNum)
                    oRow.AddCell(DTOPgcCta.FullNom(.Cta, oLang))
                    Dim oDevengat = DTOAmt.Empty
                    Dim oSujeta As DTOBaseQuota = item.IvaBaseQuotas.FirstOrDefault(Function(x) x.Tipus <> 0)
                    If oSujeta Is Nothing Then
                        oRow.AddCell()
                        oRow.AddCell()
                        oRow.AddCell()
                    Else
                        oDevengat.add(oSujeta.baseImponible)
                        oRow.AddCellAmt(oSujeta.baseImponible)
                        oRow.AddCell(oSujeta.Tipus)
                        oRow.AddCellAmt(oSujeta.Quota)
                    End If
                    Dim oExenta As DTOBaseQuota = item.IvaBaseQuotas.FirstOrDefault(Function(x) x.Tipus = 0)
                    If oExenta Is Nothing Then
                        oRow.AddCell()
                        oRow.AddCell()
                        oRow.AddCell()
                        oRow.AddCell()
                    Else
                        Dim oQuota As DTOAmt = oExenta.baseImponible.percent(oTaxIva.tipus)
                        oDevengat.add(oExenta.baseImponible)
                        oRow.AddCellAmt(oExenta.baseImponible)
                        oRow.AddCell(item.ClaveExenta)
                        Select Case item.ClaveExenta
                            Case "E5"
                                oRow.AddCellAmt(oQuota)
                                oRow.AddCell()
                            Case "E2"
                                oRow.AddCell()
                                oRow.AddCellAmt(oQuota)
                            Case Else
                                oRow.AddCell()
                                oRow.AddCell()
                        End Select
                    End If
                    oRow.AddCellAmt(oDevengat)

                    Dim oIrpf As DTOBaseQuota = item.IrpfBaseQuota
                    If oIrpf Is Nothing Then
                        oRow.AddCell()
                        oRow.AddCell()
                        oRow.AddCell()
                    Else
                        If oIrpf.Quota Is Nothing Then
                            oRow.AddCell()
                            oRow.AddCell()
                            oRow.AddCell()
                        Else
                            oRow.AddCellAmt(oIrpf.baseImponible)
                            oRow.AddCell(oIrpf.Tipus)
                            oRow.AddCellAmt(oIrpf.Quota)
                        End If
                    End If
                End With

                If ShowProgress IsNot Nothing Then
                    ShowProgress(0, items.Count, oExcelSheet.Rows.Count, "redactant full d'Excel...", CancelRequest)
                End If
                If CancelRequest Then Exit For
            Next
        End If

    End Sub


    Shared Async Function SendToSii(oEmp As DTOEmp, entorno As DTO.Defaults.Entornos, oBookFras As List(Of DTOBookFra), ShowProgress As ProgressBarHandler, exs As List(Of Exception)) As Task(Of DTOTaskResult)
        Dim retval As New DTOTaskResult
        Dim oX509Cert = Await FEB2.Cert.X509Certificate2(oEmp.Org, exs)
        If exs.Count = 0 Then
            Dim BlCancelRequest As Boolean
            Dim sLabel As String = ""
            For Each oBookFra As DTOBookFra In oBookFras
                If FEB2.BookFra.Load(exs, oBookFra) Then
                    sLabel = String.Format("Pas 1 de 3 passos: carregant fra.{0} del {1:dd/MM/yyy} a {2}", oBookFra.fraNum, oBookFra.cca.fch, oBookFra.contact.nom)
                    If ShowProgress IsNot Nothing Then
                        ShowProgress(0, oBookFras.Count - 1, oBookFras.IndexOf(oBookFra), sLabel, BlCancelRequest)
                    End If
                    If BlCancelRequest Then
                        Dim idx As Integer = oBookFras.IndexOf(oBookFra)
                        oBookFras = oBookFras.Take(idx + 1)
                        Exit For
                    End If
                End If
            Next

            sLabel = String.Format("Pas 2 de 3 passos: enviant {0} factures rebudes a Hisenda", oBookFras.Count)
            If ShowProgress IsNot Nothing Then
                ShowProgress(0, oBookFras.Count - 1, 0, sLabel, BlCancelRequest)
            End If


            retval = New DTOTaskResult
            If AeatSii.FacturasRecibidas.Send(entorno, oBookFras, DTOTax.closest, oX509Cert, exs) Then
                retval.Succeed()

                For Each oBookFra As DTOBookFra In oBookFras
                    Await FEB2.BookFra.LogSii(exs, oBookFra)


                    If ShowProgress IsNot Nothing Then
                        sLabel = String.Format("Pas 3 de 3 passos: desant fra.{0} del {1:dd/MM/yyy} a {2}", oBookFra.fraNum, oBookFra.cca.fch, oBookFra.contact.nom)
                        ShowProgress(0, oBookFras.Count - 1, oBookFras.IndexOf(oBookFra), sLabel, BlCancelRequest)
                    End If
                Next
            Else
                retval.Fail(exs, "")
            End If

            If exs.Count = 0 Then
                retval.Succeed()
            Else
                retval.DoneWithErrors()
                retval.AddExceptions(exs)
            End If
        Else
            retval.AddExceptions(exs)
        End If

        Return retval
    End Function

    Shared Function Consulta(oEmp As DTOEmp, entorno As DTO.Defaults.Entornos, oCert As Security.Cryptography.X509Certificates.X509Certificate2, oExercici As DTOExercici, periodo As Integer, exs As List(Of Exception)) As List(Of DTOSiiConsulta)
        Dim retval As New List(Of DTOSiiConsulta)
        Dim oFacturasRecibidas() As AeatSii.SoapFacturasRecibidas.RegistroRespuestaConsultaRecibidasType = AeatSii.FacturasRecibidas.ConsultaRecibidas(DTO.Defaults.Entornos.produccion, oCert, oExercici, periodo, exs)
        For Each oFacturaRecibida As AeatSii.SoapFacturasRecibidas.RegistroRespuestaConsultaRecibidasType In oFacturasRecibidas
            Dim item As New DTOSiiConsulta
            With item
                If TypeOf oFacturaRecibida.IDFactura.IDEmisorFactura.Item Is AeatSii.SoapFacturasRecibidas.IDOtroType Then
                    Dim idOtroType As AeatSii.SoapFacturasRecibidas.IDOtroType = oFacturaRecibida.IDFactura.IDEmisorFactura.Item
                    .Nif = idOtroType.ID
                Else
                    .Nif = oFacturaRecibida.IDFactura.IDEmisorFactura.Item
                End If
                .Invoice = oFacturaRecibida.IDFactura.NumSerieFacturaEmisor
                .Fch = oFacturaRecibida.IDFactura.FechaExpedicionFacturaEmisor
                .Csv = oFacturaRecibida.DatosPresentacion.CSV
                .CsvFch = oFacturaRecibida.DatosPresentacion.TimestampPresentacion
                .EstadoRegistro = oFacturaRecibida.EstadoFactura.EstadoRegistro + 1
                .EstadoCuadre = oFacturaRecibida.EstadoFactura.EstadoCuadre
                .TimestampEstadoCuadre = oFacturaRecibida.EstadoFactura.TimestampEstadoCuadre
                .TimestampUltimaModificacion = oFacturaRecibida.EstadoFactura.TimestampUltimaModificacion
                .CodigoErrorRegistro = oFacturaRecibida.EstadoFactura.CodigoErrorRegistro
                .DescripcionErrorRegistro = oFacturaRecibida.EstadoFactura.DescripcionErrorRegistro
            End With
            retval.Add(item)
        Next
        Return retval
    End Function
End Class
