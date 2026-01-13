

Public Class NominaEscuraHelper
    Property filename As String

    Property lines As List(Of String)



    Const IDSEGMENT = "Nº AFILIACION. S.S. TARIFA COD.CT SECCION PERIODO NRO. TOT. DIAS"

    Public Enum Headers
        Dades
        Detall
        Totals
        Liquid
    End Enum

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
        Dim exs As New List(Of Exception)
        Dim sHeaderCaption = HeaderCaption(Headers.Dades)
        Dim oMatchingSegments = TextHelper.MatchingSegments(segments, sHeaderCaption)
        Dim retval = oMatchingSegments.Count > 0
        Return retval
    End Function

    Shared Function CheckFile(exs As List(Of Exception), filename As String, oUser As DTOUser, ByRef fch As Date) As Boolean
        Dim oSegments = segments(exs, filename)
        fch = readFch(exs, oSegments)
        Dim sNif = readEmpNif(exs, oSegments)
        If sNif <> oUser.Emp.Org.PrimaryNifValue() Then
            Dim nifOwner = FEB2.Contact.SearchSync(exs, oUser, sNif, DTOContact.SearchBy.nif)
            If nifOwner Is Nothing Then
                exs.Add(New Exception("Aquestes nómines no son de " & oUser.Emp.Org.Nom))
            Else
                exs.Add(New Exception("Aquestes nómines son de " & oUser.Emp.Org.Nom & vbCrLf & "cal canviar d'empresa per importar-les"))
            End If
        End If
        Return exs.Count = 0
    End Function

    Shared Function CheckFile(exs As List(Of Exception), byteArray As Byte(), oUser As DTOUser, ByRef fch As Date) As Boolean
        Dim oSegments = segments(exs, byteArray)
        fch = readFch(exs, oSegments)
        Dim sNif = readEmpNif(exs, oSegments)
        If sNif <> oUser.Emp.Org.PrimaryNifValue() Then
            Dim nifOwner = FEB2.Contact.SearchSync(exs, oUser, sNif, DTOContact.SearchBy.nif)
            If nifOwner Is Nothing Then
                exs.Add(New Exception("Aquestes nómines no son de " & oUser.Emp.Org.Nom))
            Else
                exs.Add(New Exception("Aquestes nómines son de " & oUser.Emp.Org.Nom & vbCrLf & "cal canviar d'empresa per importar-les"))
            End If
        End If
        Return exs.Count = 0
    End Function

    Shared Async Function nominas(exs As List(Of Exception), filename As String, oUser As DTOUser, Optional ShowProgress As ProgressBarHandler = Nothing) As Task(Of List(Of DTONomina))
        Dim retval As New List(Of DTONomina)
        Dim cancelRequest As Boolean
        Dim oCert = Await FEB2.Cert.Find(oUser.Emp.Org, exs)
        Dim oExercici As DTOExercici = Nothing
        Dim oStaffs As New List(Of DTOStaff)
        Dim oCtas As New List(Of DTOPgcCta)
        Dim filenames = LegacyHelper.iTextPdfHelper.splitFileIntoPages(exs, filename)
        Dim firstFile As Boolean = True
        For Each filename In filenames
            If firstFile Then
                Dim oFirstFileSegments = segments(exs, filename)
                Dim empNif = readEmpNif(exs, oFirstFileSegments)
                If empNif = oUser.Emp.Org.PrimaryNifValue() Then
                    Dim fch = readFch(exs, oFirstFileSegments)
                    oExercici = DTOExercici.FromYear(oUser.Emp, fch.Year)
                    oStaffs = Await FEB2.Staffs.All(exs, oExercici)
                    oCtas = Await FEB2.PgcCtas.All(exs, oExercici.Year)
                    firstFile = False
                Else
                    exs.Add(New Exception("Aquestes nomines no son de " & oUser.Emp.Org.nom & ""))
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

            Dim sProgressText As String = DTOStaff.aliasOrNom(oNomina.staff)
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
                    .dietes = DTOAmt.empty
                    .embargos = DTOAmt.empty
                    .deutes = DTOAmt.empty
                    .segSocial = DTOAmt.empty
                    .irpf = DTOAmt.empty

                    .items = readItems(exs, oSegments)
                    For Each oItem In .items
                        Select Case oItem.Concepte.Id
                            Case 602, 603, 604, 678 'dietas
                                .dietes.add(oItem.Devengo)
                            Case 703
                                .embargos.add(oItem.Deduccio)
                            Case 740
                                .deutes.add(oItem.Deduccio)
                            Case 995, 996, 997
                                .segSocial.add(oItem.Deduccio)
                            Case 999
                                .irpf = oItem.Deduccio
                        End Select
                    Next
                    .irpfBase = GetIrpfBase(exs, oSegments)
                    .devengat = GetTotalDevengat(exs, oSegments)
                    .liquid = GetLiquid(exs, oSegments)

                    Dim dtFch = readFch(exs, oSegments)
                    .cca = FEB2.Nomina.Cca(retval, dtFch, oCtas, oUser, exs)

                    Dim signedFilename = LegacyHelper.LegacyDivers.NominaStamper(exs, filename, oStaff, oCert)
                    .cca.docFile = LegacyHelper.DocfileHelper.Factory(signedFilename, exs)
                End With
            End If
        End If
        Return retval
    End Function

    Private Shared Function readFch(exs As List(Of Exception), oSegments As List(Of String)) As Date
        Dim sFch = oSegments(segmentNums.fch).Trim
        Dim sMonths = {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}.ToList
        Dim sMonth = sMonths.FirstOrDefault(Function(x) sFch.Contains(x))
        If String.IsNullOrEmpty(sMonth) Then
            Dim sMonthsPattern = String.Join("|", sMonths)
            Dim sPattern As String = "\d{1,2} (" & sMonthsPattern & ")\ *\d{4}"

            Dim monthSegments = TextHelper.MatchingSegments(oSegments, sPattern)
            If monthSegments.All(Function(x) x = monthSegments.First) Then
                sFch = monthSegments.First().Trim
                sMonth = sMonths.FirstOrDefault(Function(x) sFch.Contains(x))
            Else
                exs.Add(New Exception("no s'ha trobat la data del document"))
                Return Nothing
            End If
        End If
        Dim iMonth = sMonths.IndexOf(sMonth) + 1
        Dim sYear = sFch.Substring(sFch.Length - 4, 4)
        Dim iYear = CInt(sYear)
        Dim iPos = sFch.IndexOf(" ")
        Dim iDay As Integer = sFch.Substring(0, iPos)
        Dim retval = New Date(iYear, iMonth, iDay)
        Return retval
    End Function

    Private Shared Function readEmpNif(exs As List(Of Exception), oSegments As List(Of String)) As String
        Dim retval As String = ""
        Dim segments = TextHelper.MatchingSegments(oSegments, "NIF.")
        If segments.Count > 0 Then
            retval = segments.First().Replace("NIF.", "").Trim.Split(" ").First()
        Else
            exs.Add(New Exception("No s'ha trobat cap referencia al Nif de la empresa"))
        End If
        Return retval
    End Function

    Shared Function readStaff(exs As List(Of Exception), oSegments As List(Of String), oStaffs As List(Of DTOStaff)) As DTOStaff
        Dim retval As DTOStaff = Nothing
        Dim iHeaderIdx = HeaderIdx(exs, oSegments, Headers.Dades)
        If exs.Count = 0 Then
            Dim segment = oSegments(iHeaderIdx + 1).Trim
            Dim fields = segment.Split(" ")
            If fields.Count > 0 Then
                Dim sSegSocNum = fields.First
                retval = oStaffs.FirstOrDefault(Function(x) TextHelper.Match(x.numSs, sSegSocNum))
            End If
        End If
        Return retval
    End Function

    Private Shared Function readItems(exs As List(Of Exception), oSegments As List(Of String)) As List(Of DTONomina.Item)
        Dim retval As New List(Of DTONomina.Item)
        Dim iHeaderIdx = HeaderIdx(exs, oSegments, Headers.Detall)
        If exs.Count = 0 Then
            Dim startIdx = iHeaderIdx + 1
            For idx As Integer = startIdx To startIdx + 19
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
        Else
            exs.Add(New Exception("S'ha trobat cap o mes d'una capçalera de detall"))
        End If
        Return retval
    End Function

    Shared Function GetTotalDevengat(exs As List(Of Exception), oSegments As List(Of String)) As DTOAmt
        Dim retval As DTOAmt = DTOAmt.Factory()
        Dim oSegmentTotals = SegmentTotals(exs, oSegments)
        If exs.Count = 0 Then
            Dim fields = TextHelper.splitByLength(oSegmentTotals, 14)
            Dim field = fields(Totals.TotalDevengado).Trim
            retval = ParseAmt(field)
        End If
        Return retval
    End Function

    Shared Function GetIrpfBase(exs As List(Of Exception), oSegments As List(Of String)) As DTOAmt
        Dim retval As DTOAmt = DTOAmt.Factory()
        Dim oSegmentTotals = SegmentTotals(exs, oSegments)
        If exs.Count = 0 Then
            Dim fields = TextHelper.splitByLength(oSegmentTotals, 14)
            Dim field = fields(Totals.BaseIrpf).Trim
            retval = ParseAmt(field)
        End If
        Return retval
    End Function

    Shared Function GetLiquid(exs As List(Of Exception), oSegments As List(Of String)) As DTOAmt
        Dim oCaptionsSegmentIdx = HeaderIdx(exs, oSegments, Headers.Liquid)
        Dim segment = oSegments(oCaptionsSegmentIdx + 1).Trim
        Dim retval = ParseAmt(segment)
        Return retval
    End Function

    Shared Function SegmentTotals(exs As List(Of Exception), oSegments As List(Of String)) As String
        Dim iHeaderIdx = HeaderIdx(exs, oSegments, Headers.Totals)
        Dim retval = oSegments(iHeaderIdx + 1)
        Return retval
    End Function


    Shared Function HeaderIdx(exs As List(Of Exception), oSegments As List(Of String), Header As Headers) As Integer
        'Busca l'index del segment a partir del qual trobar les dades
        Dim retval As Integer
        Dim sHeaderCaption = HeaderCaption(Header)
        Dim oMatchingSegments = TextHelper.MatchingSegments(oSegments, sHeaderCaption)
        Select Case oMatchingSegments.Count
            Case 0
                exs.Add(New Exception("no s'ha trobat la capçalera"))
            Case 1
                retval = oSegments.IndexOf(oMatchingSegments.First())
            Case Else
                exs.Add(New Exception("s'han trobat " & oMatchingSegments.Count & " capçaleres duplicades"))
        End Select
        Return retval
    End Function

    Shared Function HeaderCaption(Header As Headers) As String
        Dim retval As String = ""
        Select Case Header
            Case Headers.Dades
                retval = "Nº AFILIACION. S.S. TARIFA COD.CT SECCION PERIODO NRO. TOT. DIAS"
            Case Headers.Detall
                retval = "CONCEPTO DEVENGOS DEDUCCIONES"
            Case Headers.Totals
                retval = "REM. TOTAL P.P.EXTRAS BASE I.R.P.F. T. DEVENGADO BASE A.T. Y DES." '"BASE S.S. T.  A DEDUCIR REM. TOTAL P.P.EXTRAS BASE I.R.P.F. T. DEVENGADO BASE A.T. Y DES."
            Case Headers.Liquid
                retval = "LIQUIDO A PERCIBIR"
        End Select
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
        Dim textStream = LegacyHelper.iTextPdfHelper.readText(filename, exs)
        Dim retval = textStream.Split(vbLf).ToList
        Return retval
    End Function

    Shared Function segments(exs As List(Of Exception), byteArray As Byte()) As List(Of String)
        Dim textStream = LegacyHelper.iTextPdfHelper.readText(byteArray, exs)
        Dim retval = textStream.Split(vbLf).ToList
        Return retval
    End Function

End Class
