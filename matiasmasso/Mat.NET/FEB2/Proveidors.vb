Public Class Proveidor
    Inherits _FeblBase


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOProveidor)
        Return Await Api.Fetch(Of DTOProveidor)(exs, "Proveidor", oGuid.ToString())
    End Function

    Shared Function Exists(oProveidor As DTOContact, exs As List(Of Exception)) As Boolean
        Dim oPrv = Api.FetchSync(Of DTOProveidor)(exs, "Proveidor", oProveidor.Guid.ToString())
        Return oPrv IsNot Nothing
    End Function

    Shared Function Load(ByRef oProveidor As DTOProveidor, exs As List(Of Exception)) As Boolean
        If Not oProveidor.IsLoaded And Not oProveidor.IsNew Then
            Dim pProveidor = Api.FetchSync(Of DTOProveidor)(exs, "Proveidor", oProveidor.Guid.ToString())
            If exs.Count = 0 And pProveidor IsNot Nothing Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProveidor)(pProveidor, oProveidor, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oProveidor As DTOProveidor, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOProveidor)(oProveidor, exs, "Proveidor")
        oProveidor.IsNew = False
    End Function


    Shared Async Function Delete(oProveidor As DTOProveidor, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProveidor)(oProveidor, exs, "Proveidor")
    End Function

    Shared Async Function CheckFacturaAlreadyExists(exs As List(Of Exception), oProveidor As DTOContact, ByVal oExercici As DTOExercici, ByVal sFraNum As String) As Task(Of DTOCca)
        Return Await Api.Execute(Of String, DTOCca)(sFraNum, exs, "Proveidor/CheckFacturaAlreadyExists", oProveidor.Guid.ToString, oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function SaveFactura(exs As List(Of Exception), oCca As DTOCca, oPnds As IEnumerable(Of DTOPnd), Optional oImportacio As DTOImportacio = Nothing) As Task(Of DTOCca)
        Dim retval As DTOCca = Nothing
        exs = New List(Of Exception)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized_Cca", Api.Serialize(oCca, exs))
            If oCca.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", oCca.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", oCca.DocFile.Stream)
            End If
            If oPnds IsNot Nothing Then
                oMultipart.AddStringContent("Serialized_Pnds", Api.Serialize(oPnds, exs))
            End If
            If oImportacio IsNot Nothing Then
                oMultipart.AddStringContent("Serialized_Importacio", Api.Serialize(oImportacio, exs))
            End If
            retval = Await Api.Upload(Of DTOCca)(oMultipart, exs, "Proveidor/SaveFactura")
        End If
        Return retval

    End Function
End Class
