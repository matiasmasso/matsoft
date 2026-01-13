Public Class EdiversaFile
    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOEdiversaFile)
        Return Await Api.Fetch(Of DTOEdiversaFile)(exs, "EdiversaFile", oGuid.ToString())
    End Function

    Shared Async Function FromResultGuid(exs As List(Of Exception), oResultGuid As Guid) As Task(Of DTOEdiversaFile)
        Return Await Api.Fetch(Of DTOEdiversaFile)(exs, "EdiversaFile/FromResultGuid", oResultGuid.ToString())
    End Function

    Shared Async Function FromNumComanda(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, numComanda As String) As Task(Of DTOEdiversaFile)
        Return Await Api.Fetch(Of DTOEdiversaFile)(exs, "ediversa/FromNumComanda", oEmp.Id, year, numComanda)
    End Function

    Shared Function Load(ByRef oEdiversaFile As DTOEdiversaFile, exs As List(Of Exception)) As Boolean
        If Not oEdiversaFile.IsLoaded And Not oEdiversaFile.IsNew Then
            Dim pEdiversaFile = Api.FetchSync(Of DTOEdiversaFile)(exs, "EdiversaFile", oEdiversaFile.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEdiversaFile)(pEdiversaFile, oEdiversaFile, exs)
                oEdiversaFile.loadSegments()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Procesa(exs As List(Of Exception), oEdiversaFile As DTOEdiversaFile) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Ediversa/procesa", oEdiversaFile.Guid.ToString)
    End Function

    Shared Async Function Update(exs As List(Of Exception), oEdiversaFile As DTOEdiversaFile) As Task(Of Boolean)
        Return Await Api.Update(Of DTOEdiversaFile)(oEdiversaFile, exs, "EdiversaFile")
        oEdiversaFile.IsNew = False
    End Function

    Shared Async Function Delete(oEdiversaFile As DTOEdiversaFile, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEdiversaFile)(oEdiversaFile, exs, "EdiversaFile")
        oEdiversaFile.IsNew = False
    End Function

    Shared Async Function Restore(exs As List(Of Exception), oEmp As DTOEmp, oEdiversaFile As DTOEdiversaFile) As Task(Of DTOEdiversaFile)
        Return Await Api.Execute(Of DTOEdiversaFile, DTOEdiversaFile)(oEdiversaFile, exs, "EdiversaFile/restore", oEmp.Id)
    End Function

    Shared Async Function QueueInvoice(exs As List(Of Exception), oLog As DTOInvoicePrintLog) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOInvoicePrintLog, Boolean)(oLog, exs, "EdiversaFile/QueueInvoice")
    End Function

    Shared Async Function GetInterlocutor(oFile As DTOEdiversaFile, NadTag As DTOEdiversaContact.Cods, exs As List(Of DTOEdiversaException)) As Task(Of DTOContact)
        Dim ex2 As New List(Of Exception)
        Dim retval As DTOContact = Nothing
        Dim sRegexPattern As String = "(?<=" & NadTag.ToString & "\|)\d*"
        Dim sEan As String = TextHelper.RegexValue(oFile.Stream, sRegexPattern)
        sEan = sEan.Replace(vbCr, "")
        Dim oGln = DTOEan.Factory(sEan)
        If DTOEan.isValid(oGln) Then
            retval = Await FEB2.Contact.FromGln(oGln.Value, ex2)
            If retval IsNot Nothing Then FEB2.Contact.Load(retval, ex2)
        Else
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.InterlocutorNotFound, oFile, NadTag.ToString & " no es un Ean valid"))
        End If
        Return retval
    End Function
End Class

Public Class EdiversaFiles

    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaFile))
        Return Await Api.Fetch(Of List(Of DTOEdiversaFile))(exs, "ediversafiles", oEmp.Id)
    End Function


    Shared Async Function Tags(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, IOCod As DTOEdiversaFile.IOcods) As Task(Of List(Of String))
        Return Await Api.Fetch(Of List(Of String))(exs, "ediversafiles/tags", oEmp.Id, year, IOCod)
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, IOCod As DTOEdiversaFile.IOcods, Tag As String) As Task(Of List(Of DTOEdiversaFile))
        Return Await Api.Fetch(Of List(Of DTOEdiversaFile))(exs, "ediversafiles", oEmp.Id, year, IOCod, Tag)
    End Function

    Shared Async Function OpenFiles(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaFile))
        'llegeix els fitxers pendents de processar
        Return Await Api.Fetch(Of List(Of DTOEdiversaFile))(exs, "ediversafiles/openFiles", oEmp.Id)
    End Function

    Shared Async Function Delete(values As List(Of DTOEdiversaFile), exs As List(Of Exception)) As Task(Of Boolean)
        'llegeix els fitxers pendents de processar
        Return Await Api.Delete(Of List(Of DTOEdiversaFile))(values, exs, "ediversafiles")
    End Function

    Shared Async Function Restore(exs As List(Of Exception), oEmp As DTOEmp, oEdiversaFiles As List(Of DTOEdiversaFile)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOEdiversaFile), Boolean)(oEdiversaFiles, exs, "EdiversaFiles/restore", oEmp.Id)
    End Function

    Shared Async Function Descarta(exs As List(Of Exception), oEdiversaFiles As List(Of DTOEdiversaFile)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOEdiversaFile), Boolean)(oEdiversaFiles, exs, "EdiversaFiles/descarta")
    End Function

End Class
