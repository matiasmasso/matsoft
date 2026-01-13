Public Class Exercici
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOExercici)
        Return Await Api.Fetch(Of DTOExercici)(exs, "Exercici", oGuid.ToString())
    End Function

    Shared Async Function Saldos(exs As List(Of Exception), oExercici As DTOExercici, Optional SkipTancament As Boolean = True) As Task(Of List(Of DTOPgcSaldo))
        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "Exercici/saldos", oExercici.Emp.Id, oExercici.Year, OpcionalBool(SkipTancament))
    End Function

    Shared Async Function RenumeraAssentaments(exs As List(Of Exception), ByVal oExercici As DTOExercici) As Task(Of Integer)
        Return Await Api.Fetch(Of Integer)(exs, "Exercici/RenumeraAssentaments", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function RetrocedeixAssentamentsApertura(exs As List(Of Exception), ByVal oExercici As DTOExercici) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Exercici/RetrocedeixAssentamentsApertura", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function EliminaTancaments(exs As List(Of Exception), ByVal oExercici As DTOExercici) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Exercici/EliminaTancaments", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function Apertura(exs As List(Of Exception), oExercici As DTOExercici, oUser As DTOUser, ShowProgress As ProgressBarHandler) As Task(Of Boolean)
        Dim CancelRequest As Boolean
        Dim oCcas As New List(Of DTOCca)
        Dim oSaldosDetall = Await FEB2.Exercici.Saldos(exs, oExercici.Previous)
        If exs.Count = 0 Then
            Dim oAllCtas = Await FEB2.PgcCtas.All(exs, oExercici.Year)
            If exs.Count = 0 Then
                For Each oSaldo In oSaldosDetall
                    Dim oSaldoCta = oAllCtas.FirstOrDefault(Function(x) x.Guid.Equals(oSaldo.Epg.Guid))
                    If oSaldoCta IsNot Nothing Then
                        oSaldo.Epg = oSaldoCta
                    End If
                Next

                Dim oSaldosResum = DTOPgcSaldo.ResumTotsDigits(oSaldosDetall)

                'Assentament de apertura
                Dim oSource As List(Of DTOPgcSaldo) = oSaldosResum.Where(Function(x) DirectCast(x.Epg, DTOPgcCta).id < "600").ToList
                Dim oCcaApertura As DTOCca = DTOCca.Factory(oExercici.FirstFch, oUser, DTOCca.CcdEnum.AperturaExercisi)
                oCcaApertura.Concept = "Balanç d'apertura"
                For Each oSaldo As DTOPgcSaldo In oSource
                    If oSaldo.IsDeutor Then
                        oCcaApertura.AddDebit(oSaldo.SdoDeudor, oSaldo.Epg)
                    ElseIf oSaldo.IsCreditor Then
                        oCcaApertura.AddCredit(oSaldo.SdoCreditor, oSaldo.Epg)
                    End If
                Next
                Dim oCtaResultats = DTOPgcCta.FromCodi(oAllCtas, DTOPgcPlan.Ctas.resultatsAnyAnterior)
                oCcaApertura.AddSaldo(oCtaResultats)
                oCcas.Add(oCcaApertura)

                'Apertura de comptes
                Dim oCca As DTOCca = Nothing
                Dim oLang As DTOLang = DTOApp.current.lang
                oSource = oSaldosDetall.Where(Function(x) DirectCast(x.Epg, DTOPgcCta).id < "600").
                                Where(Function(x) DirectCast(x.Epg, DTOPgcCta).id <> "21").
                                Where(Function(x) x.Contact IsNot Nothing).
                                ToList

                Dim oCta As New DTOPgcCta
                For Each oSaldo As DTOPgcSaldo In oSource
                    Try

                        If oSaldo.IsNotZero Then
                            If oCta.UnEquals(oSaldo.Epg) Then
                                If oCca IsNot Nothing Then
                                    oCca.AddSaldo(oCta)
                                End If
                                oCta = oSaldo.Epg
                                oCca = DTOCca.Factory(oExercici.FirstFch, oUser, DTOCca.CcdEnum.AperturaExercisi, oCta.Id)
                                oCca.Concept = DTOPgcCta.FullNom(oSaldo.Epg, oLang) & "-apertura compte"
                                oCcas.Add(oCca)
                            End If
                            If oSaldo.IsDeutor Then
                                oCca.AddDebit(oSaldo.SdoDeudor, oSaldo.Epg, oSaldo.Contact)
                            ElseIf oSaldo.IsCreditor Then
                                oCca.AddCredit(oSaldo.SdoCreditor, oSaldo.Epg, oSaldo.Contact)
                            Else
                                exs.Add(New Exception("saldo ni deutor ni creditor"))
                            End If
                        End If
                    Catch ex As Exception
                        exs.Add(ex)
                    End Try
                Next
                If oCca IsNot Nothing Then
                    oCca.AddSaldo(oCta)
                End If

                If Await FEB2.Exercici.RetrocedeixAssentamentsApertura(exs, oExercici) Then
                    For Each oCca In oCcas
                        Dim exs2 As New List(Of Exception)
                        oCca.Id = Await FEB2.Cca.Update(exs2, oCca)
                        If exs2.Count = 0 Then
                            ShowProgress(0, oCcas.Count, oCcas.IndexOf(oCca), "Desant #" & oCca.Id & " " & oCca.Concept, CancelRequest)
                        Else
                            ShowProgress(0, oCcas.Count, oCcas.IndexOf(oCca), "Error al desar " & oCca.Concept, CancelRequest)
                            exs.Add(New Exception("Error al desar " & oCca.Concept))
                            exs.AddRange(exs2)
                        End If
                        If CancelRequest Then Exit For
                    Next
                End If
            End If
        End If

        Return exs.Count = 0
    End Function

    Shared Async Function Tancament(exs As List(Of Exception), oExercici As DTOExercici, oUser As DTOUser, ShowProgress As ProgressBarHandler) As Task(Of Boolean)
        Dim retval As Boolean
        If Await FEB2.Exercici.EliminaTancaments(exs, oExercici) Then
            If Await TancamentComptes(oExercici, oUser, ShowProgress, exs) Then
                If Await TancamentExplotacio(oExercici, oUser, ShowProgress, exs) Then
                    retval = Await TancamentBalanç(oExercici, oUser, ShowProgress, exs)
                End If
            End If
        End If
        Return retval
    End Function


    Shared Async Function TancamentComptes(oExercici As DTOExercici, oUser As DTOUser, ShowProgress As ProgressBarHandler, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim CancelRequest As Boolean
        Dim oAllCtas = Await FEB2.PgcCtas.All(exs, oExercici.Year)
        Dim oLang As DTOLang = DTOLang.CAT
        Dim DtFch As Date = oExercici.LastDayOrToday
        Dim oSaldos = Await FEB2.Exercici.Saldos(exs, oExercici)
        Dim items As List(Of DTOPgcSaldo) = oSaldos.Where(Function(x) x.Contact IsNot Nothing).ToList
        Dim oCta As New DTOPgcCta
        Dim oCca As DTOCca = Nothing
        For Each item As DTOPgcSaldo In items
            If item.IsNotZero Then
                If Not oCta.Equals(item.Epg) Then
                    If oCca IsNot Nothing Then
                        oCca.AddSaldo(oCta)
                        Dim ex2 As New List(Of Exception)
                        oCca.Id = Await FEB2.Cca.Update(ex2, oCca)
                        If ex2.Count > 0 Then
                            exs.Add(New Exception("error al desar " & oCca.Concept))
                            exs.AddRange(ex2)
                        End If
                    End If

                    oCta = oAllCtas.FirstOrDefault(Function(x) x.Equals(item.Epg))
                    If oCta Is Nothing Then
                        oCta = New DTOPgcCta(item.Epg.Guid)
                        oCta.nom = item.Epg.nom
                    End If
                    oCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.TancamentComptes)
                    oCca.Concept = String.Format("tancament compte {0}", DTOPgcCta.FullNom(oCta, oLang))
                End If
                If item.IsDeutor Then
                    oCca.AddCredit(item.SdoDeudor, oCta, item.Contact)
                Else
                    oCca.AddDebit(item.SdoCreditor, oCta, item.Contact)
                End If
            End If
            ShowProgress(0, items.Count, items.IndexOf(item), "Desant tancaments de compte", CancelRequest)
            If CancelRequest Then Exit For
        Next

        If oCca IsNot Nothing Then
            oCca.AddSaldo(oCta)
            Dim ex2 As New List(Of Exception)
            oCca.Id = Await FEB2.Cca.Update(ex2, oCca)
            If ex2.Count > 0 Then
                exs.Add(New Exception("error al desar " & oCca.Concept))
                exs.AddRange(ex2)
            End If
        End If
        retval = exs.Count = 0
        Return retval
    End Function


    Shared Async Function TancamentExplotacio(oExercici As DTOExercici, oUser As DTOUser, ShowProgress As ProgressBarHandler, exs As List(Of Exception)) As Task(Of Boolean)
        Dim CancelRequest As Boolean
        ShowProgress(0, 0, 0, "Tancant compte de explotació", CancelRequest)

        Dim retval As Boolean
        Dim oLang As DTOLang = DTOLang.CAT
        Dim DtFch As Date = oExercici.LastDayOrToday

        Dim oAllCtas = Await FEB2.PgcCtas.All(exs, oExercici.Year)

        Dim oCca As DTOCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.TancamentExplotacio)
        oCca.Concept = "compte de explotació"

        Dim oSaldos = Await FEB2.Exercici.Saldos(exs, oExercici, False)
        For Each oSaldo In oSaldos
            Dim oCta = oAllCtas.FirstOrDefault(Function(x) x.Equals(oSaldo.Epg))
            If oCta Is Nothing Then oCta = New DTOPgcCta(oSaldo.Epg.Guid)
            oSaldo.Epg = oCta
        Next
        Dim query = oSaldos.
            Where(Function(x) DTOPgcCta.IsExplotacio(DirectCast(x.Epg, DTOPgcCta))).
            GroupBy(Function(x) x.Epg.Guid).
            Select(Function(y) New With {.ctaGuid = y.Key, .debe = y.Sum(Function(z) z.Debe.Eur), .haber = y.Sum(Function(w) w.Haber.Eur)})

        For Each item In query
            If item.debe <> item.haber Then
                Dim oCta As New DTOPgcCta(item.ctaGuid)
                If item.debe > item.haber Then
                    Dim oSaldoDeutor As DTOAmt = DTOAmt.Factory(item.debe - item.haber)
                    oCca.AddCredit(oSaldoDeutor, oCta)
                Else
                    Dim oSaldoCreditor As DTOAmt = DTOAmt.Factory(item.haber - item.debe)
                    oCca.AddDebit(oSaldoCreditor, oCta)
                End If
            End If
        Next


        Dim oResultats = Await FEB2.PgcCta.FromCod(DTOPgcPlan.Ctas.resultatsAnyAnterior, oExercici, exs)
        oCca.AddSaldo(oResultats)
        Dim ex2 As New List(Of Exception)
        oCca.id = Await FEB2.Cca.Update(ex2, oCca)
        If ex2.Count > 0 Then
            exs.Add(New Exception("error al tancar el compte de explotació"))
            exs.AddRange(ex2)
        End If

        retval = exs.Count = 0
        Return retval
    End Function

    Shared Async Function TancamentBalanç(oExercici As DTOExercici, oUser As DTOUser, ShowProgress As ProgressBarHandler, exs As List(Of Exception)) As Task(Of Boolean)
        Dim CancelRequest As Boolean
        ShowProgress(0, 0, 0, "Tancant balanç", CancelRequest)

        Dim oLang As DTOLang = DTOLang.CAT
        Dim DtFch As Date = oExercici.LastDayOrToday

        Dim oCca As DTOCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.TancamentBalanç)
        oCca.Concept = "balanç de tancament"

        Dim oSaldos = Await FEB2.Exercici.Saldos(exs, oExercici, False)
        Dim query = oSaldos.
            GroupBy(Function(x) x.Epg.Guid).
            Select(Function(y) New With {.ctaGuid = y.Key, .debe = y.Sum(Function(z) z.Debe.Eur), .haber = y.Sum(Function(w) w.Haber.Eur)})

        For Each item In query
            If item.debe <> item.haber Then
                Dim oCta As New DTOPgcCta(item.ctaGuid)
                If item.debe > item.haber Then
                    Dim oSaldoDeutor As DTOAmt = DTOAmt.Factory(item.debe - item.haber)
                    oCca.AddCredit(oSaldoDeutor, oCta)
                Else
                    Dim oSaldoCreditor As DTOAmt = DTOAmt.Factory(item.haber - item.debe)
                    oCca.AddDebit(oSaldoCreditor, oCta)
                End If
            End If
        Next

        oCca.Id = Await FEB2.Cca.Update(exs, oCca)

        Return exs.Count = 0
    End Function

End Class

Public Class Exercicis
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional oCta As DTOPgcCta = Nothing) As Task(Of List(Of DTOExercici))
        Return Await Api.Fetch(Of List(Of DTOExercici))(exs, "Exercicis", oEmp.Id, OpcionalGuid(oContact), OpcionalGuid(oCta))
    End Function

    Shared Function AllSync(exs As List(Of Exception), oEmp As DTOEmp) As List(Of DTOExercici)
        Dim years = Api.FetchSync(Of List(Of Integer))(exs, "Exercicis/years", oEmp.Id)
        Dim retval As New List(Of DTOExercici)
        For Each year In years
            Dim oEsercici As New DTOExercici(oEmp, year)
        Next
        Return retval
    End Function

    Shared Async Function Years(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of Integer))
        Return Await Api.Fetch(Of List(Of Integer))(exs, "Exercicis/years", oEmp.Id)
    End Function

End Class
