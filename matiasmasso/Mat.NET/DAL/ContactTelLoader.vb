Public Class ContactTelLoader
    Shared Function Delete(oContactTel As DTOContactTel, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oContactTel, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Delete(oContactTel As DTOContactTel, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE CliTel WHERE Guid='" & oContactTel.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class

Public Class ContactTelsLoader
    Shared Function All(oContact As DTOContact, Optional oCod As DTOContactTel.Cods = DTOContactTel.Cods.NotSet) As List(Of DTOContactTel)
        Dim retval As New List(Of DTOContactTel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Clitel.Cod, CliTel.Num, PrefixeTelefonic, CliTel.Obs, CliTel.Privat ")
        sb.AppendLine(", Country.Guid as CountryGuid, Country.ISO ")
        sb.AppendLine(", Country.Nom_Esp AS CountryEsp, Country.Nom_Cat AS CountryCat, Country.Nom_Eng AS CountryEng, Country.Nom_Por AS CountryPor ")
        sb.AppendLine("FROM CliTel ")
        sb.AppendLine("INNER JOIN Country ON CliTel.Country = Country.Guid ")
        sb.AppendLine("WHERE CliGuid='" & oContact.Guid.ToString & "' ")
        If oCod <> DTOContactTel.Cods.NotSet Then
            sb.AppendLine("AND Cod=" & CInt(oCod) & " ")
        End If
        sb.AppendLine("AND Privat=0 ")
        sb.AppendLine("ORDER BY Cod, Ord, Id ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContactTel
            With item
                .Cod = oDrd("Cod")
                .Value = oDrd("Num")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                .Privat = oDrd("Privat")
                .Country = New DTOCountry(oDrd("CountryGuid"))
                With .Country
                    .ISO = oDrd("ISO")
                    .prefixeTelefonic = SQLHelper.GetStringFromDataReader(oDrd("PrefixeTelefonic"))
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor")
                End With
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
