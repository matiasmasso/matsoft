Public Class WebLogBrowse

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOWebLogBrowse
        Dim retval As DTOWebLogBrowse = WebLogBrowseLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oWebLogBrowse As DTOWebLogBrowse) As Boolean
        Dim retval As Boolean = WebLogBrowseLoader.Load(oWebLogBrowse)
        Return retval
    End Function

    Shared Function Update(oWebLogBrowse As DTOWebLogBrowse, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebLogBrowseLoader.Update(oWebLogBrowse, exs)
        Return retval
    End Function

    Shared Function Delete(oWebLogBrowse As DTOWebLogBrowse, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WebLogBrowseLoader.Delete(oWebLogBrowse, exs)
        Return retval
    End Function
#End Region

End Class

Public Class WebLogBrowses

    Shared Function All(oDoc As DTOBaseGuid) As List(Of DTOWebLogBrowse)
        Dim retval As List(Of DTOWebLogBrowse) = WebLogBrowsesLoader.All(oDoc)
        Return retval
    End Function

End Class

