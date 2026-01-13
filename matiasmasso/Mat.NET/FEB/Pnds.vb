Public Class Pnd
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOPnd)
        Return Await Api.Fetch(Of DTOPnd)(exs, "Pnd", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oPnd As DTOPnd, exs As List(Of Exception)) As Boolean
        If Not oPnd.IsLoaded And Not oPnd.IsNew Then
            Dim pPnd = Api.FetchSync(Of DTOPnd)(exs, "Pnd", oPnd.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPnd)(pPnd, oPnd, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oPnd As DTOPnd, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPnd)(oPnd, exs, "Pnd")
        oPnd.IsNew = False
    End Function

    Shared Async Function Delete(oPnd As DTOPnd, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPnd)(oPnd, exs, "Pnd")
    End Function

    Shared Async Function Rebut(exs As List(Of Exception), oPnd As DTOPnd) As Task(Of DTORebut)
        Dim retval As DTORebut = Nothing
        If Pnd.Load(oPnd, exs) Then
            If Contact.Load(oPnd.Contact, exs) Then
                Dim oLang As DTOLang = oPnd.Contact.Lang
                retval = New DTORebut(oLang)
                With retval
                    .Amt = oPnd.Amt
                    .Fch = oPnd.Fch
                    .Vto = oPnd.Vto
                    If oPnd.Invoice IsNot Nothing Then
                        .Concepte = String.Format("fra.{0}", oPnd.Invoice.Num)
                    End If
                    If oPnd.Cod = DTOPnd.Codis.Deutor Then
                        Dim oIban = Await Iban.FromContact(exs, oPnd.Contact, oPnd.Cod)
                        If oIban IsNot Nothing Then
                            .IbanDigits = oIban.Digits
                        End If
                    End If
                    .Nom = oPnd.Contact.Nom
                    .Adr = oPnd.Contact.Address.Text
                    .Cit = DTOAddress.ZipyCit(oPnd.Contact.Address)
                End With
            End If
        End If
        Return retval
    End Function
End Class

Public Class Pnds
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oContact As DTOContact, sFraNum As String, DtFch As Date) As Task(Of List(Of DTOPnd))
        Return Await Api.Fetch(Of List(Of DTOPnd))(exs, "Pnds/FromFra", OpcionalGuid(oContact), sFraNum, FormatFch(DtFch))
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oContact As DTOContact, Optional cod As DTOPnd.Codis = DTOPnd.Codis.NotSet, Optional onlyPendents As Boolean = True) As Task(Of List(Of DTOPnd))
        Return Await Api.Fetch(Of List(Of DTOPnd))(exs, "Pnds", oEmp.Id, OpcionalGuid(oContact), cod, OpcionalBool(onlyPendents))
    End Function

    Shared Async Function Pending(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOPnd))
        Return Await Api.Fetch(Of List(Of DTOPnd))(exs, "Pnds/pending", oEmp.Id)
    End Function

    Shared Async Function Cartera(exs As List(Of Exception), oEmp As DTOEmp, DtFch As Date) As Task(Of List(Of DTOPnd))
        Return Await Api.Fetch(Of List(Of DTOPnd))(exs, "Pnds/cartera", oEmp.Id, FormatFch(DtFch))
    End Function

    Shared Async Function CarteraDeDeutorsYCreditors(exs As List(Of Exception), oEmp As DTOEmp, oAD As DTOPnd.Codis, DtFch As Date) As Task(Of MatHelper.Excel.Sheet)
        Dim sNom As String = "cartera de " & If(oAD = DTOPnd.Codis.Deutor, "deutors", "creditors")
        Dim oPnds = Await Cartera(exs, oEmp, DtFch)
        oPnds = oPnds.Where(Function(x) x.Cod = oAD).ToList
        Dim retval As New MatHelper.Excel.Sheet(sNom)
        With retval
            .addColumn(If(oAD = DTOPnd.Codis.Creditor, "Creditor", "Deutor"))
            .AddColumn("Compte")
            .AddColumn("Factura")
            .AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Import", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Venciment", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
        End With
        For Each oPnd In oPnds
            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            oRow.AddCell(oPnd.Contact.Nom)
            oRow.AddCell(DTOPgcCta.FormatAccountId(oPnd.Cta, oPnd.Contact))
            oRow.AddCell(oPnd.FraNum)
            oRow.AddCell(oPnd.Fch)
            oRow.AddCellAmt(oPnd.Amt)
            oRow.AddCell(oPnd.Vto)
        Next
        Return retval
    End Function

    Shared Async Function Descuadres(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOPgcSaldo))
        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "Pnds/descuadres", oExercici.Emp.Id, oExercici.Year)
    End Function

End Class
