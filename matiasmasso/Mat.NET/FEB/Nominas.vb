Public Class Nomina
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTONomina)
        Return Await Api.Fetch(Of DTONomina)(exs, "nomina", oGuid.ToString())
    End Function

    Shared Function Load(ByRef onomina As DTONomina, exs As List(Of Exception)) As Boolean
        If Not onomina.IsLoaded And Not onomina.IsNew Then
            Dim pnomina = Api.FetchSync(Of DTONomina)(exs, "nomina", onomina.Cca.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTONomina)(pnomina, onomina, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTONomina, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.Cca.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.Cca.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.Cca.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "nomina")
        End If
        Return retval
    End Function
    Shared Async Function Delete(oNomina As DTONomina, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTONomina)(oNomina, exs, "Nomina")
    End Function

    Shared Function Cca(ByRef oNomina As DTONomina, DtFch As Date, oCtas As List(Of DTOPgcCta), oUser As DTOUser, exs As List(Of Exception)) As DTOCca
        Dim retval As DTOCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.Nomina)
        With retval
            .Concept = DTONomina.CcaConcepte(oNomina)

            If oNomina.Devengat IsNot Nothing Then
                If oNomina.Devengat.IsNotZero Then
                    Dim oDevengatMenysDietes As DTOAmt = oNomina.Devengat.Clone
                    oDevengatMenysDietes.Substract(oNomina.Dietes)
                    .AddDebit(oDevengatMenysDietes, Cta(oCtas, DTOPgcPlan.Ctas.Nomina, exs), oNomina.Staff)
                End If
            End If

            If oNomina.Dietes.IsNotZero Then
                .AddDebit(oNomina.Dietes, Cta(oCtas, DTOPgcPlan.Ctas.Dietas, exs), oNomina.Staff)
            End If

            If oNomina.SegSocial.IsNotZero Then
                .AddCredit(oNomina.SegSocial, Cta(oCtas, DTOPgcPlan.Ctas.SegSocialDevengo, exs), oNomina.Staff)
            End If

            If oNomina.Irpf.IsNotZero Then
                .AddCredit(oNomina.Irpf, Cta(oCtas, DTOPgcPlan.Ctas.IrpfTreballadors, exs), oNomina.Staff)
            End If

            If oNomina.Embargos.IsNotZero Then
                .AddCredit(oNomina.Embargos, Cta(oCtas, DTOPgcPlan.Ctas.NominaEmbargos, exs), oNomina.Staff)
            End If

            If oNomina.Deutes.IsNotZero Then
                .AddCredit(oNomina.Deutes, Cta(oCtas, DTOPgcPlan.Ctas.NominaDeutes, exs), oNomina.Staff)
            End If

            If oNomina.Anticips.IsNotZero Then
                .AddCredit(oNomina.Anticips, Cta(oCtas, DTOPgcPlan.Ctas.AnticipsTreballadors, exs), oNomina.Staff)
            End If

            If oNomina.Liquid.IsNotZero Then
                .AddCredit(oNomina.Liquid, Cta(oCtas, DTOPgcPlan.Ctas.PagasTreballadors, exs), oNomina.Staff)
            End If

        End With
        Return retval
    End Function

    Shared Function Cta(oAllCtas As List(Of DTOPgcCta), oCodi As DTOPgcPlan.Ctas, exs As List(Of Exception)) As DTOPgcCta
        Dim retval As DTOPgcCta = oAllCtas.FirstOrDefault(Function(x) x.Codi = oCodi)
        If retval Is Nothing Then exs.Add(New Exception(String.Format("No s'ha trobat el compte {0}", oCodi.ToString())))
        Return retval
    End Function
End Class

Public Class Nominas
    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTONomina))
        Dim retval = Await Api.Fetch(Of List(Of DTONomina))(exs, "Nominas/fromuser", oUser.Guid.ToString())
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oStaff As DTOStaff) As Task(Of List(Of DTONomina))
        Return Await Api.Fetch(Of List(Of DTONomina))(exs, "Nominas", oStaff.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTONomina))
        Return Await Api.Fetch(Of List(Of DTONomina))(exs, "Nominas", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function Llibre(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of MatHelper.Excel.Sheet)
        Dim retval As New MatHelper.Excel.Sheet(oExercici.Year, "M+O Nomines")
        With retval
            .DisplayTotals = True
            .AddColumn("assentament", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("treballador", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("devengat", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("dietes", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("seg.social", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Irpf", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("altres", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("liquid", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        Dim oNominas = Await Nominas.All(exs, oExercici)
        If exs.Count = 0 Then
            oNominas = oNominas.
            OrderBy(Function(x) x.Staff.Nom).OrderBy(Function(y) y.Cca.Fch).ToList
            For Each oNomina In oNominas
                Dim oRow As MatHelper.Excel.Row = retval.AddRow
                With oNomina
                    oRow.AddCell(.Cca.Id, DocFile.DownloadUrl(.Cca.DocFile, True))
                    oRow.AddCell(.Cca.Fch)
                    oRow.AddCell(.Staff.Nom)
                    oRow.AddCellAmt(.Devengat.Substract(.Dietes))
                    oRow.AddCellAmt(.Dietes)
                    oRow.AddCellAmt(.SegSocial)
                    oRow.AddCellAmt(.Irpf)
                    oRow.AddFormula("RC[-4]+RC[-3]-RC[-2]-RC[-1]-RC[1]")
                    oRow.AddCellAmt(.Liquid)
                End With
            Next
        End If

        Return retval
    End Function

End Class
