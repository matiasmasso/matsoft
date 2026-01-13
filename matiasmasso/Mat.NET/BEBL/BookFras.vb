Public Class BookFra
    Shared Function Find(oGuid As Guid) As DTOBookFra
        Dim oCca As New DTOCca(oGuid)
        Dim retval As DTOBookFra = BookFraLoader.Find(oCca)
        Return retval
    End Function

    Shared Function Update(oBookFra As DTOBookFra, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BookFraLoader.Update(oBookFra, exs)
        Return retval
    End Function

    Shared Function Delete(exs As List(Of Exception), oBookFra As DTOBookFra) As Boolean
        Dim retval As Boolean = BookFraLoader.Delete(oBookFra.cca, exs)
        Return retval
    End Function

    Shared Function LogSii(oBookFra As DTOBookFra, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If oBookFra.SiiLog Is Nothing Then
            exs.Add(New Exception("SiiLog is nothig at BEBL.BookFra.LogSii()"))
        Else
            retval = BookFraLoader.LogSii(oBookFra, exs)
        End If
        Return retval
    End Function
End Class

Public Class BookFras

    Shared Function All(oMode As DTOBookFra.Modes, oExercici As DTOExercici, Optional iMes As Integer = 0, Optional oContact As DTOContact = Nothing) As List(Of DTOBookFra)
        Dim retval As List(Of DTOBookFra) = BookFrasLoader.All(mode:=oMode, exercici:=oExercici, mes:=iMes, contact:=oContact)
        Return retval
    End Function

    Shared Function SiiPending(oEmp As DTOEmp) As List(Of DTOBookFra)
        Dim retval As List(Of DTOBookFra) = BookFrasLoader.SiiPending(oEmp)
        Return retval
    End Function

    Shared Function SendToSii(entorno As DTO.Defaults.Entornos, oOrg As DTOContact, oBookFras As List(Of DTOBookFra), exs As List(Of Exception), Optional ShowProgress As ProgressBarHandler = Nothing) As Boolean
        Dim BlCancelRequest As Boolean
        Dim sLabel As String = ""

        Try
            For Each oBookFra As DTOBookFra In oBookFras
                BookFraLoader.Load(oBookFra)
                sLabel = String.Format("Pas 1 de 3 passos: carregant fra.{0} del {1:dd/MM/yyy} a {2}", oBookFra.FraNum, oBookFra.Cca.fch, oBookFra.Contact.nom)
                If ShowProgress IsNot Nothing Then
                    ShowProgress(0, oBookFras.Count - 1, oBookFras.IndexOf(oBookFra), sLabel, BlCancelRequest)
                End If
                If BlCancelRequest Then
                    Dim idx As Integer = oBookFras.IndexOf(oBookFra)
                    oBookFras = oBookFras.Take(idx + 1)
                    Exit For
                End If
            Next
            sLabel = String.Format("Pas 2 de 3 passos: enviant {0} factures rebudes a Hisenda", oBookFras.Count)
            If ShowProgress IsNot Nothing Then
                ShowProgress(0, oBookFras.Count - 1, 0, sLabel, BlCancelRequest)
            End If

            If AeatSii.FacturasRecibidas.Send(entorno, oBookFras, DTOTax.Closest, BEBL.Cert.X509Certificate2(oOrg), exs) Then
                For Each oBookFra As DTOBookFra In oBookFras
                    BookFraLoader.LogSii(oBookFra, exs)
                    If ShowProgress IsNot Nothing Then
                        sLabel = String.Format("Pas 3 de 3 passos: desant fra.{0} del {1:dd/MM/yyy} a {2}", oBookFra.FraNum, oBookFra.Cca.fch, oBookFra.Contact.nom)
                        ShowProgress(0, oBookFras.Count - 1, oBookFras.IndexOf(oBookFra), sLabel, BlCancelRequest)
                    End If
                Next

            End If


        Catch ex As Exception
            exs.Add(New Exception("BEBL.BookFras.SendToSii: Error al enviar les factures rebudes"))
            exs.Add(ex)
        End Try

        Return exs.Count = 0
    End Function

End Class
