Imports System.Data.SqlClient

Public Class Zip
    Shared Function Find(oGuid As Guid) As DTOZip
        Dim retval As DTOZip = ZipLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oZip As DTOZip) As Boolean
        Dim retval As Boolean = ZipLoader.Load(oZip)
        Return retval
    End Function

    Shared Function FromZipCod(oCountry As DTOCountry, sZipCod As String) As DTOZip
        Return ZipLoader.FromZipCod(sZipCod, oCountry)
    End Function

    Shared Function Update(oZip As DTOZip, exs As List(Of Exception)) As Boolean
        Return ZipLoader.Update(oZip, exs)
    End Function

    Shared Function Delete(oZip As DTOZip, exs As List(Of Exception)) As Boolean
        Return ZipLoader.Delete(oZip, exs)
    End Function

End Class
Public Class Zips

    Shared Function All(Optional oLang As DTOLang = Nothing) As List(Of DTOCountry)
        Dim retval = ZipsLoader.All(oLang)
        Return retval
    End Function

    Shared Function All(oCountry As DTOCountry, sZipCod As String) As List(Of DTOZip)
        Return ZipsLoader.All(oCountry, sZipCod)
    End Function

    Shared Function Tree(Optional oLang As DTOLang = Nothing) As List(Of DTOCountry)
        Dim retval As List(Of DTOCountry) = ZipsLoader.Tree(oLang)
        Return retval
    End Function

    Shared Function Merge(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Dim retval = ZipsLoader.Merge(exs, oGuids)
        Return retval
    End Function
End Class
