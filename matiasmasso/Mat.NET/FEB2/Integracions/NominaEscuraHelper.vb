Public Class NominaEscuraHelper
    Property filename As String
    Property lines As List(Of String)

    Public Enum segmentNums
        nifEmpresa = 4
        segSocial = 12
        totals = 36
        fch = 40
        liquid = 43
    End Enum

    Public Enum Totals
        RemTotal
        pagasExtras
        BaseSegSocial
        BaseAccYDesempleo
        BaseIrpf
        TotalDevengado
        TotalADeducir
    End Enum

    Shared Function isNominaEscura(segments As List(Of String)) As Boolean
        Dim retval = segments(11) = "Nº AFILIACION. S.S. TARIFA COD.CT SECCION PERIODO NRO. TOT. DIAS"
        Return retval
    End Function

    Shared Function CheckFile(exs As List(Of Exception), filename As String, oUser As DTOUser, ByRef fch As Date) As Boolean
        Dim oSegments = segments(exs, filename)
        fch = readFch(oSegments)
        Dim sNif = readEmpNif(oSegments)
        If sNif <> oUser.emp.Org.nif Then
            Dim nifOwner = FEB2.Contact.SearchSync(exs, oUser, sNif, DTOContact.SearchBy.nif)
            If nifOwner Is Nothing Then
                exs.Add(New Exception("Aquestes nómines no son de " & oUser.emp.Org.nom))
            Else
                exs.Add(New Exception("Aquestes nómines son de " & oUser.emp.Org.nom & vbCrLf & "cal canviar d'empresa per importar-les"))
            End If
        End If
        Return exs.Count = 0
    End Function

    Shared Function CheckFile(exs As List(Of Exception), byteArray As Byte(), oUser As DTOUser, ByRef fch As Date) As Boolean
        Dim oSegments = segments(exs, byteArray)
        fch = readFch(oSegments)
        Dim sNif = readEmpNif(oSegments)
        If sNif <> oUser.emp.Org.nif Then
            Dim nifOwner = FEB2.Contact.SearchSync(exs, oUser, sNif, DTOContact.SearchBy.nif)
            If nifOwner Is Nothing Then
                exs.Add(New Exception("Aquestes nómines no son de " & oUser.emp.Org.nom))
            Else
                exs.Add(New Exception("Aquestes nómines son de " & oUser.emp.Org.nom & vbCrLf & "cal canviar d'empresa per importar-les"))
            End If
        End If
        Return exs.Count = 0
    End Function

    Shared Async Function nominas(exs As List(Of Exception), filename As String, oUser As DTOUser, Optional ShowProgress As ProgressBarHandler = Nothing) As Task(Of List(Of DTONomina))
        Dim retval As New List(Of DTONomina)
        Dim cancelRequest As Boolean
        Dim oCert = Await FEB2.Cert.Find(oUser.emp.Org, exs)
        Dim oExercici As DTOExercici = Nothing
        Dim oStaffs As New List(Of DTOStaff)
        Dim oCtas As New List(Of DTOPgcCta)
        Dim filenames = iTextPdfHelper.splitFileIntoPages(exs, filename)
        Dim firstFile As Boolean = True
        For Each filename In filenames
            If firstFile Then
                Dim oFirstFileSegments = segments(exs, filename)
                Dim empNif = readEmpNif(oFirstFileSegments)
                If empNif = oUser.emp.Org.nif Then
                    Dim fch = readFch(oFirstFileSegments)
                    oExercici = DTOExercici.FromYear(oUser.emp, fch.Year)
                    oStaffs = Await FEB2.Staffs.All(exs, oExercici)
                    oCtas = Await FEB2.PgcCtas.All(exs, oExercici.year)
                    firstFile = False
                Else
                    exs.Add(New Exception("Aquestes nomines no son de " & oUser.emp.Org.nom & ""))
                    Exit For
                End If
            End If

            Dim exs2 As New List(Of Exception)
            Dim oNomina = readNomina(exs2, filename, oUser, oStaffs, oCtas, oCert)
            If exs2.Count = 0 Then
                retval.Add(oNomina)
            Else
                exs.AddRange(exs2)
            End If

            Dim sProgressText As String = DTOStaff.AliasOrNom(oNomina.staff)
            ShowProgress(1, filenames.Count, filenames.IndexOf(filename) + 1, sProgressText, cancelRequest)
            If cancelRequest Then Exit For
        Next
        Return retval
    End Function



    Shared Function readNomina(exs As List(Of Exception), filename As String, oUser As DTOUser, oStaffs As List(Of DTOStaff), oCtas As List(Of DTOPgcCta), oCert As DTOCert) As DTONomina
        Dim retval As DTONomina = Nothing
        Dim oSegments = segments(exs, filename)
        If exs.Count = 0 Then
            Dim oStaff = readStaff(exs, oSegments, oStaffs)
            If exs.Count = 0 Then
                retval = New DTONomina(oStaff)
                With retval
                    .dietes = DTOAmt.Empty
                    .embargos = DTOAmt.Empty
                    .deutes = DTOAmt.Empty
                    .segSocial = DTOAmt.Empty
                    .irpf = DTOAmt.Empty

                    .items = readItems(oSegments)
                    For Each oItem In .items
                        Select Case oItem.Concepte.Id
                            Case 602, 603, 604, 678 'dietas
                                .dietes.Add(oItem.Devengo)
                            Case 703
                                .embargos.Add(oItem.Deduccio)
                            Case 740
                                .deutes.Add(oItem.Deduccio)
                            Case 995, 996, 997
                                .segSocial.Add(oItem.Deduccio)
                            Case 999
                                .irpf = oItem.Deduccio
                        End Select
                    Next
                    .irpfBase = GetIrpfBase(oSegments)
                    .devengat = GetTotalDevengat(oSegments)
                    .liquid = GetLiquid(oSegments)

                    Dim dtFch = readFch(oSegments)
                    .cca = FEB2.Nomina.Cca(retval, dtFch, oCtas, oUser, exs)

                    Dim sIbanDigits = DTOIban.Formated(oStaff.iban)
                    Dim oIbanText = iTextPdfHelper.MatPdfText.Factory(sIbanDigits, New Rectangle(75, 620, 1000, 1000))
                    Dim signedFilename = filename.Replace(".pdf", ".signed.pdf")
                    'Dim destFilename = filename.Replace(".pdf", ".edited.pdf")

                    'provisional mentres no funciona la signatura:
                    Dim oIbanSigFake = iTextPdfHelper.MatPdfImage.Factory(oCert.ImageUri, New Rectangle(140, 600, 200, 40))

                    iTextPdfHelper.write(exs, filename, signedFilename, {oIbanText, oIbanSigFake})
                    'iTextPdfHelper.write(exs, filename, destFilename, {oIbanText, oIbanSigFake})

                    'Dim signedFilename = destFilename.Replace(".edited.pdf", ".signed.pdf")

                    Dim signRect As Rectangle = New Rectangle(140, 600, 200, 40)

                    'sustitueix la signatura

                    'preventivament anulat
                    'PdfSignatureHelper.Sign(exs, destFilename, signedFilename, oCert.memoryStream, oCert.Pwd, signRect, oCert.ImageUri)

                    .cca.docFile = FEB2.DocFile.Factory(signedFilename, exs)
                End With
            End If
        End If
        Return retval
    End Function

    Private Shared Function readFch(oSegments As List(Of String)) As Date
        Dim sFch = oSegments(segmentNums.fch).Trim
        Dim iPos = sFch.IndexOf(" ")
        Dim iDay As Integer = sFch.Substring(0, iPos)
        Dim sMonths = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}.ToList
        Dim sMonth = sMonths.FirstOrDefault(Function(x) sFch.Contains(x))
        Dim iMonth = sMonths.IndexOf(sMonth) + 1
        Dim sYear = sFch.Substring(sFch.Length - 4, 4)
        Dim iYear = CInt(sYear)
        Dim retval = New Date(iYear, iMonth, iDay)
        Return retval
    End Function

    Private Shared Function readEmpNif(oSegments As List(Of String)) As String
        Dim segment = oSegments(segmentNums.nifEmpresa).Replace("NIF.", "").Trim
        Dim retval = segment.Substring(0, segment.IndexOf(" "))
        Return retval
    End Function

    Private Shared Function readItems(oSegments As List(Of String)) As List(Of DTONomina.Item)
        Dim retval As New List(Of DTONomina.Item)
        For idx As Integer = 14 To 33
            Dim segment = oSegments(idx)
            Dim sCod As String = segment.Substring(23, 6)
            Dim sConcepte As String = segment.Substring(32, 36).Trim
            If IsNumeric(sCod) Then
                Dim oCod As New DTONomina.Concepte(sCod, sConcepte)
                Dim sQty = segment.Substring(0, 12)
                Dim sPrice As String = segment.Substring(12, 11)
                Dim sDevengo As String = segment.Substring(70, 11)
                Dim sDeduccio As String = segment.Substring(84, 11)
                Dim oItem As New DTONomina.Item(oCod)
                With oItem
                    If IsNumeric(sQty) Then .Qty = CInt(sQty.Trim)
                    If IsNumeric(sPrice) Then .Price = DTOAmt.Factory(CDec(sPrice.Trim))
                    If IsNumeric(sDevengo) Then .Devengo = DTOAmt.Factory(CDec(sDevengo.Trim))
                    If IsNumeric(sDeduccio) Then .Deduccio = DTOAmt.Factory(CDec(sDeduccio.Trim))
                End With
                retval.Add(oItem)
            Else
                Exit For
            End If
        Next
        Return retval
    End Function

    Shared Function GetTotalDevengat(oSegments As List(Of String)) As DTOAmt
        Dim segment = oSegments(segmentNums.totals)
        Dim fields = TextHelper.splitByLength(segment, 14)
        Dim field = fields(Totals.TotalDevengado).Trim
        Dim retval = ParseAmt(field)
        Return retval
    End Function

    Shared Function GetIrpfBase(oSegments As List(Of String)) As DTOAmt
        Dim segment = oSegments(segmentNums.totals)
        Dim fields = TextHelper.splitByLength(segment, 14)
        Dim field = fields(Totals.BaseIrpf).Trim
        Dim retval = ParseAmt(field)
        Return retval
    End Function

    Shared Function GetLiquid(oSegments As List(Of String)) As DTOAmt
        Dim segment = oSegments(segmentNums.liquid).Trim
        Dim retval = ParseAmt(segment)
        Return retval
    End Function

    Shared Function ParseAmt(src As String) As DTOAmt
        Dim value As Decimal = 0
        If Not String.IsNullOrEmpty(src) Then
            value = Decimal.Parse(src, System.Globalization.NumberStyles.Number, New System.Globalization.CultureInfo("es-ES"))
        End If
        Dim retval = DTOAmt.Factory(value)
        Return retval
    End Function

    Shared Function segments(exs As List(Of Exception), filename As String) As List(Of String)
        Dim textStream = iTextPdfHelper.readText(filename, exs)
        Dim retval = textStream.Split(vbLf).ToList
        Return retval
    End Function

    Shared Function segments(exs As List(Of Exception), byteArray As Byte()) As List(Of String)
        Dim textStream = iTextPdfHelper.readText(byteArray, exs)
        Dim retval = textStream.Split(vbLf).ToList
        Return retval
    End Function

    Shared Function readStaff(exs As List(Of Exception), oSegments As List(Of String), oStaffs As List(Of DTOStaff)) As DTOStaff
        Dim retval As DTOStaff = Nothing
        Dim segment = oSegments(segmentNums.segSocial).Trim
        Dim fields = segment.Split(" ")
        If fields.Count > 0 Then
            Dim sSegSocNum = fields.First
            retval = oStaffs.FirstOrDefault(Function(x) TextHelper.Match(x.numSs, sSegSocNum))
        End If
        Return retval
    End Function

End Class
