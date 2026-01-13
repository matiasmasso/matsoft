Public Class BancTransferPool
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBancTransferPool)
        Return Await Api.Fetch(Of DTOBancTransferPool)(exs, "BancTransferPool", oGuid.ToString())
    End Function

    Shared Async Function FromCca(oCca As DTOCca, exs As List(Of Exception)) As Task(Of DTOBancTransferPool)
        Return Await Api.Fetch(Of DTOBancTransferPool)(exs, "BancTransferPool/FromCca", oCca.Guid.ToString())
    End Function

    Shared Function Load(ByRef oBancTransferPool As DTOBancTransferPool, exs As List(Of Exception)) As Boolean
        If Not oBancTransferPool.IsLoaded And Not oBancTransferPool.IsNew Then
            Dim pBancTransferPool = Api.FetchSync(Of DTOBancTransferPool)(exs, "BancTransferPool", oBancTransferPool.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBancTransferPool)(pBancTransferPool, oBancTransferPool, exs)
                For Each oBeneficiari In oBancTransferPool.Beneficiaris
                    oBeneficiari.Parent = oBancTransferPool
                Next
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oBancTransferPool As DTOBancTransferPool, exs As List(Of Exception)) As Task(Of Integer)
        Dim retval = Await Api.Update(Of DTOBancTransferPool, Integer)(oBancTransferPool, exs, "BancTransferPool")
        oBancTransferPool.IsNew = False
        Return retval
    End Function

    Shared Async Function Save(exs As List(Of Exception), oBancTransferPool As DTOBancTransferPool) As Task(Of Integer)
        Dim oCtaCredit = Await PgcCta.FromCod(DTOPgcPlan.Ctas.bancs, oBancTransferPool.User.Emp, exs)
        Dim oBeneficiaris As List(Of DTOBancTransferBeneficiari) = oBancTransferPool.Beneficiaris
        Dim oBancEmissor As DTOBanc = oBancTransferPool.BancEmissor
        Dim oExpenses As DTOAmt = oBancTransferPool.Expenses

        Dim oCca As DTOCca = DTOCca.Factory(oBancTransferPool.Fch, oBancTransferPool.User, DTOCca.CcdEnum.TransferNorma34)
        If oBancTransferPool.Beneficiaris.Count = 1 Then
            oCca.Concept = String.Format("{0} - transferencia a {1}", oBancEmissor.abr, oBeneficiaris.First.Contact.FullNom)
        Else
            oCca.Concept = String.Format("{0} - transferencies", oBancEmissor.abr)
        End If

        oCca.Ref = oBancTransferPool
        oBancTransferPool.Cca = oCca

        Dim oPnds As New List(Of DTOPnd)
        For Each oBeneficiari As DTOBancTransferBeneficiari In oBancTransferPool.Beneficiaris
            With oBeneficiari
                If .Pnds.Count > 0 Then
                    oPnds.AddRange(.Pnds)
                    For Each oPnd As DTOPnd In .Pnds
                        Select Case oPnd.Cod
                            Case DTOPnd.Codis.Creditor
                                oCca.AddDebit(oPnd.Amt, oPnd.Cta, oPnd.Contact, oPnd)
                            Case DTOPnd.Codis.Deutor
                                oCca.AddCredit(oPnd.Amt, oPnd.Cta, oPnd.Contact, oPnd)
                        End Select
                    Next
                Else
                    oCca.AddDebit(.Amt, .Cta, .Contact)
                End If
            End With
        Next

        If oExpenses IsNot Nothing Then
            If oExpenses.IsNotZero Then
                Dim oCtaExpenses = Await PgcCta.FromCod(DTOPgcPlan.Ctas.despesesPagament, oBancTransferPool.User.Emp, exs)
                oCca.AddDebit(oExpenses, oCtaExpenses, oBancEmissor)
            End If

        End If

        oCca.AddSaldo(oCtaCredit, oBancEmissor)
        oBancTransferPool.Pnds = oPnds

        Dim retval As Integer = Await BancTransferPool.Update(oBancTransferPool, exs)
        Return retval

    End Function

    Shared Async Function SaveRef(oBancTransferPool As DTOBancTransferPool, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOBancTransferPool)(oBancTransferPool, exs, "BancTransferPool/saveref")
    End Function

    Shared Async Function Delete(oBancTransferPool As DTOBancTransferPool, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBancTransferPool)(oBancTransferPool, exs, "BancTransferPool")
    End Function

    Shared Async Function FromReps(oUser As DTOUser, exs As List(Of Exception)) As Task(Of DTOBancTransferPool)
        Dim oExercici As DTOExercici = DTOExercici.Current(oUser.Emp)
        Dim oCta = Await PgcCta.FromCod(DTOPgcPlan.Ctas.ServeisEur, oUser.Emp, exs)
        Dim oSaldos = Await Reps.Saldos(oExercici, exs)
        Dim oIbans = Await Reps.Ibans(oUser.Emp, exs)
        Dim retval As DTOBancTransferPool = DTOBancTransferPool.Factory(oUser)
        For Each oSaldo As DTOPgcSaldo In oSaldos
            Dim oIban As DTOIban = oIbans.Find(Function(x) x.Titular.Equals(oSaldo.Contact))
            Dim oDeb As DTOAmt = oSaldo.Debe.Clone
            Dim oHab As DTOAmt = oSaldo.Haber.Clone
            Dim oAmt As DTOAmt = oHab.Substract(oDeb)
            If oAmt.IsPositive Then
                DTOBancTransferPool.AddBeneficiari(retval, oCta, oSaldo.Contact, oIban, oAmt, "Comisiones M+O")
            End If
        Next
        Return retval
    End Function

    Shared Async Function FromStaff(oEmp As DTOEmp, oUser As DTOUser, exs As List(Of Exception)) As Task(Of DTOBancTransferPool)
        Dim oExercici As DTOExercici = DTOExercici.Current(oEmp)
        Dim oCta = Await PgcCta.FromCod(DTOPgcPlan.Ctas.PagasTreballadors, oEmp, exs)
        Dim oSaldos = Await Staffs.Saldos(oExercici, exs)
        Dim oIbans = Await Staffs.Ibans(oEmp, exs)
        Dim retval As DTOBancTransferPool = DTOBancTransferPool.Factory(oUser)
        For Each oSaldo As DTOPgcSaldo In oSaldos
            Dim oIban As DTOIban = oIbans.Find(Function(x) x.Titular.Equals(oSaldo.Contact))
            Dim oDeb As DTOAmt = oSaldo.Debe.Clone
            Dim oHab As DTOAmt = oSaldo.Haber.Clone
            Dim oAmt As DTOAmt = oHab.Substract(oDeb)
            DTOBancTransferPool.AddBeneficiari(retval, oCta, oSaldo.Contact, oIban, oAmt, "Nómina " & oEmp.Nom)
        Next
        Return retval
    End Function


    Shared Async Function Traspas(oUser As DTOUser, oBancEmissor As DTOBanc, oBancReceptor As DTOBanc, oAmt As DTOAmt, oExpenses As DTOAmt, DtFch As Date, exs As List(Of Exception)) As Task(Of DTOBancTransferPool)
        Banc.Load(oBancEmissor, exs)
        Banc.Load(oBancReceptor, exs)
        oBancReceptor.FullNom = oBancReceptor.Abr 'necessari per omplir el concepte de l'assentament
        Dim oIbanReceptor = Await Iban.FromContact(exs, oBancReceptor, DTOIban.Cods.Banc)
        Dim sConcepte As String = String.Format("traspas de {0}", oBancEmissor.Abr)

        Dim retval As DTOBancTransferPool = DTOBancTransferPool.Factory(
        oUser,
        DtFch,
        oBancEmissor,
        oExpenses)

        Dim oCta = Await PgcCta.FromCod(DTOPgcPlan.Ctas.bancs, oUser.Emp, exs)
        DTOBancTransferPool.AddBeneficiari(
            retval,
            oCta,
            oBancReceptor,
            oIbanReceptor.BankBranch,
            oIbanReceptor.Digits,
            oAmt,
            sConcepte
            )
        Return retval
    End Function


End Class

Public Class BancTransferPools
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, Optional oBanc As DTOBanc = Nothing) As Task(Of List(Of DTOBancTransferPool))
        Dim retval = Await Api.Fetch(Of List(Of DTOBancTransferPool))(exs, "BancTransferPools", oEmp.Id, OpcionalGuid(oBanc))
        For Each value In retval
            For Each oBeneficiari In value.Beneficiaris
                oBeneficiari.Parent = value
            Next
        Next
        Return retval
    End Function

End Class

