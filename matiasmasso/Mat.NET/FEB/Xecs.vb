Public Class Xec
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOXec)
        Return Await Api.Fetch(Of DTOXec)(exs, "Xec", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oXec As DTOXec, exs As List(Of Exception)) As Boolean
        If Not oXec.IsLoaded And Not oXec.IsNew Then
            Dim pXec = Api.FetchSync(Of DTOXec)(exs, "Xec", oXec.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOXec)(pXec, oXec, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oXec As DTOXec, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOXec)(oXec, exs, "Xec")
        oXec.IsNew = False
    End Function


    Shared Async Function Delete(oXec As DTOXec, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOXec)(oXec, exs, "Xec")
    End Function


    Shared Async Function Cobrament(exs As List(Of Exception), oUser As DTOUser, oXec As DTOXec) As Task(Of Boolean)
        'Return Await Api.Execute(Of DTOXec)(oXec, exs, "Xec/Cobrament", oUser.Guid.ToString())
        Dim retval As Boolean
        Dim oBank As DTOBank = DTOIban.Bank(oXec.Iban)
        Dim sBankNom As String = ""
        If oBank IsNot Nothing Then
            sBankNom = DTOBank.NomComercialORaoSocial(oBank)
        End If
        Dim oCtaXecsEnCartera = Await PgcCta.FromCod(DTOPgcPlan.Ctas.ClientsXecsEnCartera, oUser.Emp, exs)
        If exs.Count = 0 Then
            Dim sText As String = String.Format("{0} {1} num.{2} rebut de {3}", If(oXec.Format = DTOXec.Formats.Pagare, "pagaré", "xec"), sBankNom, oXec.XecNum, oXec.Lliurador.nom)
            Dim oCca As DTOCca = DTOCca.Factory(oXec.FchRecepcio, oUser, DTOCca.CcdEnum.XecRebut)
            oCca.concept = TextHelper.VbLeft(sText, 60)

            Dim oSum = DTOAmt.Empty
            For Each oPnd As DTOPnd In oXec.Pnds
                Select Case oPnd.Cod
                    Case DTOPnd.Codis.Deutor
                        oCca.AddCredit(oPnd.Amt, oPnd.Cta, oPnd.Contact, oPnd)
                        oSum.Add(oPnd.Amt)
                    Case DTOPnd.Codis.Creditor
                        oCca.AddDebit(oPnd.Amt, oPnd.Cta, oPnd.Contact, oPnd)
                        oSum.Substract(oPnd.Amt)
                End Select
                oPnd.Status = DTOPnd.StatusCod.saldat
            Next

            If oXec.Impagats.Count > 0 Then
                Dim oCtaImpagats = Await PgcCta.FromCod(DTOPgcPlan.Ctas.impagats, oUser.Emp, exs)
                Dim oCtaRecuperacioGastos As DTOPgcCta = Await PgcCta.FromCod(DTOPgcPlan.Ctas.ImpagosRecuperacioDespeses, oUser.Emp, exs)
                For Each oImpagat As DTOImpagat In oXec.Impagats
                    oCca.AddCredit(oImpagat.Nominal, oCtaImpagats, oImpagat.Csb.Contact)
                    If oImpagat.Gastos IsNot Nothing Then
                        oCca.AddCredit(oImpagat.Gastos, oCtaRecuperacioGastos, oImpagat.Csb.Contact)
                    End If
                Next
            End If

            oCca.AddSaldo(oCtaXecsEnCartera, oXec.Lliurador)

            With oXec
                .CcaRebut = oCca
                .FchRecepcio = oCca.Fch
                .Amt = oSum
            End With

            retval = Await Api.Execute(Of DTOXec)(oXec, exs, "Xec/UpdateXecRebut")

        End If
        Return retval
    End Function

End Class

Public Class Xecs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oCca As DTOCca) As Task(Of List(Of DTOXec))
        Return Await Api.Fetch(Of List(Of DTOXec))(exs, "Xecs", oCca.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oStatusCod As DTOXec.StatusCods, Optional oCodPresentacio As DTOXec.ModalitatsPresentacio = DTOXec.ModalitatsPresentacio.NotSet) As Task(Of List(Of DTOXec))
        Return Await Api.Fetch(Of List(Of DTOXec))(exs, "Xecs", oEmp.Id, oStatusCod, oCodPresentacio)
    End Function

    Shared Async Function Headers(exs As List(Of Exception), Optional oLliurador As DTOContact = Nothing) As Task(Of List(Of DTOXec))
        Return Await Api.Fetch(Of List(Of DTOXec))(exs, "Xecs/Headers")
    End Function

End Class
