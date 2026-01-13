Public Class Cca


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCca)
        Return Await Api.Fetch(Of DTOCca)(exs, "Cca", oGuid.ToString())
    End Function

    Shared Async Function FromNum(oEmp As DTOEmp, year As Integer, id As Integer, exs As List(Of Exception)) As Task(Of DTOCca)
        Return Await Api.Fetch(Of DTOCca)(exs, "Cca/fromNum", oEmp.Id, year, id)
    End Function


    Shared Function Load(ByRef oCca As DTOCca, exs As List(Of Exception)) As Boolean
        If Not oCca.IsLoaded And Not oCca.IsNew Then
            Dim pCca = Api.FetchSync(Of DTOCca)(exs, "Cca", oCca.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCca)(pCca, oCca, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOCca) As Task(Of Integer)
        Dim retval As Integer
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(Of Integer)(oMultipart, exs, "Cca")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oCca As DTOCca) As Task(Of Boolean)
        Dim retval As Boolean
        Select Case oCca.Ccd
            Case DTOCca.CcdEnum.TransferNorma34
                Dim oTransfer = Await FEB2.BancTransferPool.FromCca(oCca, exs)
                If oTransfer Is Nothing Then
                    oTransfer = New DTOBancTransferPool()
                    oTransfer.Cca = oCca
                End If

                If Await FEB2.BancTransferPool.Delete(oTransfer, exs) Then
                    retval = True
                Else
                    exs.Add(New Exception("No s'ha pogut eliminar la transferencia de l'assentament " & oCca.Id & " " & oCca.Concept & " "))
                    exs.AddRange(exs)
                End If
            Case Else
                If Await Api.Delete(Of DTOCca)(oCca, exs, "Cca") Then
                    retval = True
                Else
                    exs.Add(New Exception("L'assentament " & oCca.Id & " " & oCca.Concept & " no s'ha pogut eliminar:"))
                    exs.AddRange(exs)
                End If
        End Select

        Return retval
    End Function

    Shared Function Url(oCca As DTOCca, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "cca", oCca.Guid.ToString())
    End Function

    Shared Function DownloadUrl(oCca As DTOCca, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oCca IsNot Nothing Then
            retval = FEB2.DocFile.DownloadUrl(oCca.DocFile, AbsoluteUrl)
        End If
        Return retval
    End Function

    Shared Async Function LastBlockedYear(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of Integer)
        Dim iLastBlockedYear = Await FEB2.Default.EmpInteger(oEmp, DTODefault.Codis.LastBlockedCcaYea, exs)
        Dim retval = Math.Max(1985, iLastBlockedYear)
        Return retval
    End Function

    Shared Async Function IsBlockedYear(oEmp As DTOEmp, ByVal iYea As Integer, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim iLastBlockedYear = Await FEB2.Cca.LastBlockedYear(oEmp, exs)
        If exs.Count = 0 Then
            retval = (iYea <= iLastBlockedYear)
        End If
        Return retval
    End Function

    Shared Async Function IvaFchUltimaDeclaracio(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of Date)
        Return Await Api.Fetch(Of Date)(exs, "Cca/IvaFchUltimaDeclaracio", oEmp.Id)
    End Function

    Shared Function IsAllowedToBrowse(oCca As DTOCca, oBrowser As DTOUser) As Boolean
        Dim exs As New List(Of Exception)
        Dim oRol As DTORol = oBrowser.Rol
        Select Case oRol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Accounts
            Case Else
                'protetgir acces
                If FEB2.Cca.Load(oCca, exs) Then
                    For Each oCcb As DTOCcb In oCca.Items
                        If oCcb.Contact IsNot Nothing Then
                            If oCcb.Cta.Codi = DTOPgcPlan.Ctas.VisasPagadas Then
                                'permetem visualitzar factures publiques pagades amb visa d'administradors
                            Else
                                If Not FEB2.User.IsAllowedToRead(oBrowser, oCcb.Contact) Then
                                    'SendMail("matias@matiasmasso.es", "matias@matiasmasso.es", My.User.Name & " BROWSE ASSENTAMENT SENSIBLE " & oCca.yea & "/" & oCca.Id)
                                    'MsgBox("Operació no autoritzada", MsgBoxStyle.Exclamation, "MAT.NET")
                                    Return False
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                End If
        End Select
        Return True
    End Function

End Class

Public Class Ccas

    Shared Async Function Headers(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOCca))
        Return Await Api.Fetch(Of List(Of DTOCca))(exs, "Ccas/headers", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function Headers2(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOCca))
        Return Await Api.Fetch(Of List(Of DTOCca))(exs, "Ccas/headers2", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function All(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOCca))
        Dim retval = Await Api.Fetch(Of List(Of DTOCca))(exs, "Ccas", oExercici.Emp.Id, oExercici.Year)
        For Each oCca In retval
            For Each oCcb In oCca.Items
                oCcb.Cca = oCca
            Next
        Next
        Return retval
    End Function

    Shared Async Function Descuadres(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOCca))
        Dim retval = Await Api.Fetch(Of List(Of DTOCca))(exs, "Ccas/descuadres", oExercici.Emp.Id, oExercici.Year)
        For Each oCca In retval
            For Each oCcb In oCca.Items
                oCcb.Cca = oCca
            Next
        Next
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oCcas As List(Of DTOCca)) As Task(Of Boolean)
        'Ojo, nomes si no hi han documents adjunts
        Return Await Api.Update(Of List(Of DTOCca))(oCcas, exs, "Ccas")
    End Function

    Shared Async Function LlibreDiari(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOCca))
        Return Await Api.Fetch(Of List(Of DTOCca))(exs, "Ccas/LlibreDiari", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function LlibreMajor(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOCca))
        Return Await Api.Fetch(Of List(Of DTOCca))(exs, "Ccas/LlibreMajor", oExercici.Emp.Id, oExercici.Year)
    End Function
End Class

