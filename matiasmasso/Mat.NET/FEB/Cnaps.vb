Public Class Cnap
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCnap)
        Return Await Api.Fetch(Of DTOCnap)(exs, "Cnap", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCnap As DTOCnap, exs As List(Of Exception)) As Boolean
        If Not oCnap.IsLoaded And Not oCnap.IsNew Then
            Dim pCnap = Api.FetchSync(Of DTOCnap)(exs, "Cnap", oCnap.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCnap)(pCnap, oCnap, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCnap As DTOCnap, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCnap)(oCnap, exs, "Cnap")
        oCnap.IsNew = False
    End Function

    Shared Async Function Delete(oCnap As DTOCnap, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCnap)(oCnap, exs, "Cnap")
    End Function

    Shared Async Function ProductSearch(searchKey As String, exs As List(Of Exception)) As Task(Of List(Of DTOProduct))
        Dim retval As New List(Of DTOProduct)
        Dim oCnaps = Await Cnaps.All(exs, searchKey)
        For Each oCnap As DTOCnap In oCnaps
            retval.AddRange(Products(oCnap))
        Next
        Return retval
    End Function

    Shared Function Products(oCnap As DTOCnap) As List(Of DTOProduct)
        Dim retval As List(Of DTOProduct) = oCnap.Products
        If oCnap.Children IsNot Nothing Then
            For Each child As DTOCnap In oCnap.Children
                retval.AddRange(Products(child))
            Next
        End If
        Return retval
    End Function
End Class

Public Class Cnaps

    Shared Async Function All(exs As List(Of Exception), Optional searchKey As String = "") As Task(Of List(Of DTOCnap))
        Return Await Api.Fetch(Of List(Of DTOCnap))(exs, "Cnaps", searchKey)
    End Function

    Shared Async Function Tree(exs As List(Of Exception), Optional searchKey As String = "") As Task(Of List(Of DTOCnap))
        Dim retval As New List(Of DTOCnap)
        Dim oCnaps = Await All(exs, searchKey)
        If exs.Count = 0 Then
            For Each item As DTOCnap In oCnaps
                Dim oParent As DTOCnap = item.Parent
                If oParent Is Nothing Then
                    retval.Add(item)
                Else
                    oParent = oCnaps.FirstOrDefault(Function(x) x.Guid.Equals(oParent.Guid))
                    If oParent Is Nothing Then
                        If Debugger.IsAttached Then Stop
                    Else
                        If oParent.Children Is Nothing Then oParent.Children = New List(Of DTOCnap)
                        oParent.Children.Add(item)
                    End If
                End If
            Next
        End If
        Return retval
    End Function

End Class

