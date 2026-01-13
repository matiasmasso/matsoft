Public Class ComarcaLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOComarca
        Dim retval As DTOComarca = Nothing
        Dim oComarca As New DTOComarca(oGuid)
        If Load(oComarca) Then
            retval = oComarca
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oComarca As DTOComarca) As Boolean
        If Not oComarca.IsLoaded And Not oComarca.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Comarca.Nom AS ComarcaNom ")
            sb.AppendLine(", Comarca.Zona, Zona.Country, Zona.Nom AS ZonaNom ")
            sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por, Country.ISO ")
            sb.AppendLine("FROM Comarca ")
            sb.AppendLine("INNER JOIN Zona ON Comarca.Zona = Zona.Guid ")
            sb.AppendLine("INNER JOIN Country ON Zona.Country = Country.Guid ")
            sb.AppendLine("WHERE Comarca.Guid='" & oComarca.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oCountry As New DTOCountry(oDrd("Country"))
                With oCountry
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                    .ISO = oDrd("ISO")
                End With

                Dim oZona As New DTOZona(oDrd("Zona"))
                oZona.Nom = oDrd("ZonaNom")
                oZona.Country = oCountry

                With oComarca
                    .Nom = oDrd("Nom")
                    .Zona = oZona
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oComarca.IsLoaded
        Return retval
    End Function

    Shared Function Update(oComarca As DTOComarca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oComarca, oTrans)
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


    Shared Sub Update(oComarca As DTOComarca, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Comarca ")
        sb.AppendLine("WHERE Guid='" & oComarca.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oComarca.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oComarca
            oRow("Nom") = .Nom
            oRow("Zona") = SQLHelper.NullableBaseGuid(.Zona)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oComarca As DTOComarca, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oComarca, oTrans)
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


    Shared Sub Delete(oComarca As DTOComarca, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Comarca WHERE Guid='" & oComarca.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ComarcasLoader

    Shared Function All(Optional oZona As DTOZona = Nothing) As List(Of DTOComarca)
        Dim retval As New List(Of DTOComarca)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Comarca.Guid, Comarca.Nom AS ComarcaNom ")
        sb.AppendLine(", Comarca.Zona, Zona.Country, Zona.Nom AS ZonaNom ")
        sb.AppendLine(", Country.Nom_Esp, Country.Nom_Cat, Country.Nom_Eng, Country.Nom_Por, Country.ISO ")
        sb.AppendLine("FROM Comarca ")
        sb.AppendLine("INNER JOIN Zona ON Comarca.Zona = Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country = Country.Guid ")

        If oZona IsNot Nothing Then
            sb.AppendLine("WHERE Comarca.Zona = '" & oZona.Guid.ToString & "' ")
        End If
        sb.AppendLine("ORDER BY Zona.Nom, Comarca.Nom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oCountry As New DTOCountry
        oZona = New DTOZona
        Do While oDrd.Read
            If Not oZona.Guid.Equals(oDrd("Zona")) Then
                If Not oCountry.Guid.Equals(oDrd("Country")) Then
                    oCountry = New DTOCountry(oDrd("Country"))
                    With oCountry
                        .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "Nom_Esp", "Nom_Cat", "Nom_Eng", "Nom_Por")
                        .ISO = oDrd("ISO")
                    End With
                End If
                oZona = New DTOZona(oDrd("Zona"))
                oZona.Nom = oDrd("ZonaNom")
                oZona.Country = oCountry
            End If

            Dim item As New DTOComarca(oDrd("Guid"))
            With item
                .Nom = oDrd("ComarcaNom")
                .Zona = oZona
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
