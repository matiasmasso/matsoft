Public Class Repliq

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTORepLiq)
        Return Await Api.Fetch(Of DTORepLiq)(exs, "Repliq", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oRepliq As DTORepLiq, exs As List(Of Exception)) As Boolean
        If Not oRepliq.IsLoaded And Not oRepliq.IsNew Then
            Dim pRepliq = Api.FetchSync(Of DTORepLiq)(exs, "Repliq", oRepliq.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORepLiq)(pRepliq, oRepliq, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oRepliq As DTORepLiq) As Task(Of DTORepLiq)
        Return Await Api.Execute(Of DTORepLiq, DTORepLiq)(oRepliq, exs, "Repliq")
    End Function

    Public Shared Async Function RegeneraPdf(exs As List(Of Exception), ByVal oRepLiq As DTORepLiq, oEmp As DTOEmp) As Task(Of Boolean)
        If Not oRepLiq.IsLoaded Then oRepLiq = Await Repliq.Find(oRepLiq.Guid, exs)
        If exs.Count = 0 Then
            Dim oPdfRepLiq As New LegacyHelper.PdfRepLiq(oRepLiq)
            Dim oCert = Await Cert.Find(oEmp.Org, exs)
            Dim oStream = oPdfRepLiq.Stream(exs, oCert)
            If exs.Count = 0 Then
                Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oStream, MimeCods.Pdf)
                If exs.Count = 0 Then
                    Dim oCca As DTOCca = oRepLiq.Cca
                    If Not oCca.IsLoaded Then oCca = Await Cca.Find(oCca.Guid, exs)
                    If exs.Count = 0 Then
                        oCca.DocFile = oDocFile
                        oCca.Id = Await Cca.Update(exs, oCca)
                    End If
                End If
            End If
        End If
        Return exs.Count = 0
    End Function

    Shared Async Function Delete(oRepliq As DTORepLiq, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORepLiq)(oRepliq, exs, "Repliq")
    End Function


    Shared Function DownloadUrl(oRepLiq As DTORepLiq, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oRepLiq IsNot Nothing Then
            retval = Cca.DownloadUrl(oRepLiq.Cca, AbsoluteUrl)
        End If
        Return retval
    End Function

    Shared Async Function GetCca(exs As List(Of Exception), oRepLiq As DTORepLiq, oUser As DTOUser) As Task(Of DTOCca)
        Dim retval As DTOCca = DTOCca.Factory(oRepLiq.Fch, oUser, DTOCca.CcdEnum.RepComisions, oRepLiq.formattedId())
        With retval
            .Concept = oRepLiq.Rep.NickName & "-liq.{0} de comisions (" & DTOLang.CAT.Mes(.Fch.Month) & ")"
            .Items = New List(Of DTOCcb)

            Dim oCtas = Await PgcCtas.All(exs)
            Dim oRaoSocial As DTOProveidor = Rep.RaoSocialFacturacio(oRepLiq.Rep)
            Dim oCtaComisions = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.ComisionsRepresentants)
            Dim oCtaIvaSoportatNacional = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaSoportatNacional)
            Dim oCtaIrpfProfessionals = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IrpfProfessionals)
            Dim oCtaIvaSoportatIntracomunitari = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaSoportatIntracomunitari)
            Dim oCtaIvaRepercutitIntracomunitari = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaRepercutitIntracomunitari)
            Dim oCtaServeisEur = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.ServeisEur)

            retval.AddDebit(oRepLiq.BaseImponible, oCtaComisions, oRaoSocial)
            If oRepLiq.IvaPct <> 0 Then retval.AddDebit(oRepLiq.IvaAmt, oCtaIvaSoportatNacional)
            If oRepLiq.IrpfPct <> 0 Then retval.AddCredit(oRepLiq.IrpfAmt, oCtaIrpfProfessionals, oRaoSocial)

            If DTOAddress.ExportCod(oRaoSocial.Address) = DTOInvoice.ExportCods.intracomunitari Then

                Dim oTaxIva = DTOApp.Current.Taxes.FirstOrDefault(Function(x) x.codi = DTOTax.Codis.iva_Standard)
                If oTaxIva IsNot Nothing Then
                    If oRepLiq.BaseImponible IsNot Nothing AndAlso oRepLiq.BaseImponible.IsNotZero Then
                        Dim oQuota As DTOAmt = oRepLiq.BaseImponible.Percent(oTaxIva.tipus)
                        retval.AddDebit(oQuota, oCtaIvaSoportatIntracomunitari)
                        retval.AddCredit(oQuota, oCtaIvaRepercutitIntracomunitari)
                    End If
                End If
            End If

            retval.AddSaldo(oCtaServeisEur, oRaoSocial)

            .DocFile = oRepLiq.DocFile
            .BookFra = New DTOBookFra(retval)
            With .BookFra
                If oRepLiq.IvaPct = 0 Then
                    Dim oExenta As New DTOBaseQuota(oRepLiq.BaseImponible)
                    .ivaBaseQuotas.Add(oExenta)
                Else
                    Dim oSujeta As New DTOBaseQuota(oRepLiq.BaseImponible, oRepLiq.IvaPct)
                    .ivaBaseQuotas.Add(oSujeta)
                End If

                If oRepLiq.IrpfPct <> 0 Then
                    .irpfBaseQuota = New DTOBaseQuota(oRepLiq.BaseImponible, oRepLiq.IrpfPct)
                End If
                .contact = oRaoSocial
                .cta = oCtaComisions
                .fraNum = oRepLiq.Id
                .claveRegimenEspecialOTrascendencia = DTOContact.claveRegimenEspecialOTrascendencia(.contact)
                .claveExenta = DTOContact.claveCausaExempcio(.contact)
            End With
        End With
        Return retval
    End Function

    Shared Function HeaderSync(exs As List(Of Exception), oEmp As DTOEmp, oInvoice As DTOInvoice, oRep As DTORep) As DTORepLiq
        Return Api.FetchSync(Of DTORepLiq)(exs, "Repliq/Header", oEmp.Id, oInvoice.Guid.ToString, oRep.Guid.ToString())
    End Function

End Class

Public Class Repliqs

    Shared Async Function Model(exs As List(Of Exception), oUser As DTOUser) As Task(Of Models.RepLiqsModel)
        Return Await Api.Fetch(Of Models.RepLiqsModel)(exs, "Repliqs", oUser.Guid.ToString)
    End Function

    Shared Async Function Factory(oRepComsLiquidables As List(Of DTORepComLiquidable), DtFch As Date, oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTORepLiq))
        Dim retval As New List(Of DTORepLiq)

        Dim oReps = oRepComsLiquidables.Select(Function(x) x.Rep).Distinct.ToList
        For Each oRep In oReps
            If Rep.Load(exs, oRep) Then
                Dim oRepLiq = DTORepLiq.Factory(oRep, DtFch)
                With oRepLiq
                    .Items.AddRange(oRepComsLiquidables.Where(Function(x) x.Rep.Equals(oRep)))
                    .BaseFras = DTORepLiq.GetBaseFacturas(oRepLiq)
                    .BaseImponible = DTORepLiq.GetTotalComisions(oRepLiq)
                    .IvaAmt = DTORepLiq.GetIVAAmt(oRepLiq)
                    .IrpfAmt = DTORepLiq.GetIRPFAmt(oRepLiq)
                    .Total = DTORepLiq.GetTotalComisions(oRepLiq)
                    .Cca = Await Repliq.GetCca(exs, oRepLiq, oUser)
                End With
                retval.Add(oRepLiq)
            End If
        Next

        Return retval
    End Function

    Shared Async Function Headers(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTORepLiq))
        Return Await Api.Fetch(Of List(Of DTORepLiq))(exs, "Repliqs/headers", oEmp.Id)
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oRep As DTORep) As Task(Of List(Of DTORepLiq))
        Return Await Api.Fetch(Of List(Of DTORepLiq))(exs, "Repliqs/headers/FromRep", oRep.Guid.ToString())
    End Function

    Shared Async Function Update(exs As List(Of Exception), oRepliqs As List(Of DTORepLiq), oUser As DTOUser, ShowProgress As ProgressBarHandler) As Task(Of Boolean)
        Dim cancelRequest As Boolean
        Dim retval As New List(Of Repliq)

        Dim msg As String = oUser.Lang.Tradueix("Preparando el registro de las liquidaciones...", "Preparant el registre de les liquidacions...", "Getting ready to register the commission statements...")
        ShowProgress(0, oRepliqs.Count * 2, 0, msg, cancelRequest)
        Dim oCert = Await Cert.Find(oUser.Emp.Org, exs)
        Dim idx = 0
        If Not cancelRequest Then
            Dim globalPrefix = String.Format("{0} ", oUser.Lang.Tradueix("Liquidacion de", "Liquidacio de", "Statement from"))
            For Each oRepLiq In oRepliqs
                Dim msgPrefix = String.Format("{0} {1}. ", globalPrefix, oRepLiq.Rep.NicknameOrNom())

                'a first step saves the repliq and returns the updated one with the new Id number
                Dim exs2 As New List(Of Exception)
                msg = String.Format("{0} {1}. ", msgPrefix, oUser.Lang.Tradueix("Paso 1: Registrando", ". Pas 1: Registrant", ". Step 1: Logging"))
                ShowProgress(0, oRepliqs.Count * 2, 2 * idx + 1, msg, cancelRequest)
                Dim oRepLiqResult = Await Repliq.Update(exs2, oRepLiq)

                If exs2.Count = 0 Then
                    'a second step uploads the Pdf to the account entry (pdf cannot be generated on the server since Azure Web Api cannot host Ghostscript executable) 
                    msg = String.Format("{0} {1}. ", msgPrefix, oUser.Lang.Tradueix("Paso 2: Subiendo el Pdf", ". Pas 2: Pujant el Pdf", ". Step 2: Uploading the Pdf document"))
                    ShowProgress(0, oRepliqs.Count * 2, 2 * idx + 2, msg, cancelRequest)

                    If Not Await Repliq.RegeneraPdf(exs2, oRepLiqResult, oUser.Emp) Then
                        msg = oUser.Lang.Tradueix("Error al subir el Pdf de la liquidación de ", "Error al pujar el Pdf de la liquidació de ", "Error on uploading statement Pdf from ") & oRepLiqResult.Rep.NicknameOrNom()
                        exs.Add(New Exception(msg))
                        exs.AddRange(exs2)
                    End If
                Else
                    msg = oUser.Lang.Tradueix("Error al grabar la liquidación de ", "Error al desar la liquidació de ", "Error on saving the statement from ") & oRepLiqResult.Rep.NicknameOrNom()
                    exs.Add(New Exception(msg))
                    exs.AddRange(exs2)
                End If

                idx += 1
            Next
        End If
        Return exs.Count = 0
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oRepLiqs As List(Of DTORepLiq)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTORepLiq))(oRepLiqs, exs, "Repliqs")
    End Function

    Shared Async Function GetRepLiqsFromQuarter(exs As List(Of Exception), oRep As DTORep, ByVal iYea As Integer, ByVal iQuarter As Integer) As Task(Of List(Of DTORepLiq))
        'Dim retval As List(Of DTORepLiq) = BLLRepLiqs.All(oEmp, oRep).Where(Function(x) Year(x.Fch) = iYea And GetQuarterFromFch(x.Fch) = iQuarter)
        Dim retval = Await Repliqs.Headers(exs, oRep)
        retval = retval.Where(Function(x) x.fch.Year = iYea And GetQuarterFromFch(x.fch) = iQuarter)
        Return retval.ToList
    End Function

    Shared Function GetQuarterFromFch(Optional ByVal DtFch As Date = Nothing) As Integer
        If DtFch = Nothing Then DtFch = Date.MinValue
        Dim iMes As Integer = DtFch.Month
        Dim iQuarter As Integer = CInt(1 + (iMes - 1) / 4)
        Return iQuarter
    End Function


    Shared Function Excel(oRep As DTORep, oRepLiqs As List(Of DTORepLiq)) As MatHelper.Excel.Sheet
        Dim sCaption = String.Format("M+O {0} {1}", oRep.Lang.Tradueix("Liquidaciones", "Liquidacions", "Commission Statements"), oRep.NickName)
        Dim retval As New MatHelper.Excel.Sheet(sCaption)
        With retval
            .AddColumn(oRep.Lang.Tradueix("Liquidación", "Liquidació", "Statement Id"))
            .AddColumn(oRep.Lang.Tradueix("Fecha", "Data", "Date"), MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn(oRep.Lang.Tradueix("Base", "Base", "Base"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oRep.Lang.Tradueix("Devengado", "Devengat", "Commission"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oRep.Lang.Tradueix("IVA", "IVA", "VAT"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oRep.Lang.Tradueix("IRPF", "IRPF", "IRPF"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oRep.Lang.Tradueix("Liquido", "Liquid", "Cash"), MatHelper.Excel.Cell.NumberFormats.Euro)
        End With
        For Each item In oRepLiqs
            Dim oRow = retval.AddRow
            oRow.AddCell(item.FormattedId, Repliq.DownloadUrl(item, True))
            oRow.AddCell(item.Fch)
            oRow.AddCellAmt(item.BaseFras)
            oRow.AddCellAmt(item.BaseImponible)
            oRow.AddCellAmt(DTORepLiq.GetIVAAmt(item))
            oRow.AddCellAmt(DTORepLiq.GetIRPFAmt(item))
            oRow.AddFormula("RC[-3]+RC[-2]-RC[-1]")
        Next
        Return retval
    End Function

End Class
