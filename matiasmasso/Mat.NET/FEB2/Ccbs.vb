Public Class Ccb

    Shared Function LinkToFactura(oCcb As DTOCcb, Optional AbsoluteUrl As Boolean = False) As String
        'Per Remittance advice
        Dim retval As String = ""
        If oCcb IsNot Nothing Then
            If oCcb.Pnd IsNot Nothing Then
                If oCcb.Pnd.Cca IsNot Nothing Then
                    If oCcb.Pnd.Cca.DocFile IsNot Nothing Then
                        retval = FEB2.DocFile.DownloadUrl(oCcb.Pnd.Cca.DocFile, AbsoluteUrl)
                    End If
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function FacturaText(oCcb As DTOCcb, oLang As DTOLang) As String
        Dim sFraNum As String = ""
        If oCcb IsNot Nothing Then
            If oCcb.Pnd IsNot Nothing Then sFraNum = oCcb.Pnd.FraNum
        End If
        Dim retval As String = String.Format("{0} {1}", oLang.Tradueix("Factura", "Factura", "Invoice"), sFraNum)
        Return retval
    End Function


End Class

Public Class Ccbs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici, oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing, Optional FchTo As Date = Nothing) As Task(Of List(Of DTOCcb))
        Return Await Api.Fetch(Of List(Of DTOCcb))(exs, "Ccbs", oExercici.Emp.Id, oExercici.Year, oCta.Guid.ToString, OpcionalGuid(oContact), FormatFch(FchTo))
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oYearMonth As DTOYearMonth) As Task(Of List(Of DTOCcb))
        Return Await Api.Fetch(Of List(Of DTOCcb))(exs, "Ccbs", oEmp.Id, oYearMonth.Year, oYearMonth.Month)
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, oCta As DTOPgcCta) As Task(Of List(Of DTOCcb))
        Return Await Api.Fetch(Of List(Of DTOCcb))(exs, "Ccbs/FromCta", oEmp.Id, year, oCta.Guid.ToString())
    End Function


    Shared Async Function All(exs As List(Of Exception), oCcd As DTOCcd) As Task(Of List(Of DTOCcb))
        Dim retval As List(Of DTOCcb) = Await All(exs, oCcd.Exercici, oCcd.Cta, oCcd.Contact, oCcd.Fch)
        Return retval
    End Function

    Shared Async Function LlibreMajor(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOCcb))
        Return Await Api.Fetch(Of List(Of DTOCcb))(exs, "Ccbs/LlibreMajor", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Function LlibreMajorSync(exs As List(Of Exception), oExercici As DTOExercici) As List(Of DTOCcb)
        Return Api.FetchSync(Of List(Of DTOCcb))(exs, "Ccbs/LlibreMajor", oExercici.Emp.Id, oExercici.Year)
    End Function


End Class
