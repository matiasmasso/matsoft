Public Class IbanStructureLoader

    Shared Function Find(oCountry As DTOCountry) As DTOIban.Structure
        Dim retval As DTOIban.Structure = Nothing
        Dim oIbanStructure As New DTOIban.Structure
        oIbanStructure.Country = oCountry
        If Load(oIbanStructure) Then
            retval = oIbanStructure
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oIbanStructure As DTOIban.Structure) As Boolean
        If Not oIbanStructure.IsLoaded And Not oIbanStructure.IsNew And oIbanStructure.Country IsNot Nothing Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT IbanStructure.* ")
            sb.AppendLine("FROM IbanStructure ")

            If oIbanStructure.Country.ISO = "" Then
                sb.AppendLine("INNER JOIN Country ON IbanStructure.CountryISO=Country.ISO ")
                sb.AppendLine("WHERE Country.Guid='" & oIbanStructure.Country.Guid.ToString & "' ")
            Else
                sb.AppendLine("WHERE CountryISO='" & oIbanStructure.Country.ISO & "' ")
            End If

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oIbanStructure
                    .BankPosition = oDrd("BankPosition")
                    .BankLength = oDrd("BankLength")
                    .BankFormat = oDrd("BankFormat")
                    .BranchPosition = oDrd("BranchPosition")
                    .BranchLength = oDrd("BranchLength")
                    .BranchFormat = oDrd("BranchFormat")
                    .CheckDigitsPosition = oDrd("CheckDigitsPosition")
                    .CheckDigitsLength = oDrd("CheckDigitsLength")
                    .CheckDigitsFormat = oDrd("CheckDigitsFormat")
                    .AccountPosition = oDrd("AccountPosition")
                    .AccountLength = oDrd("AccountLength")
                    .AccountFormat = oDrd("AccountFormat")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oIbanStructure.IsLoaded
        Return retval
    End Function

    Shared Function Update(oIbanStructure As DTOIban.Structure, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oIbanStructure, oTrans)
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

    Shared Sub Update(oIbanStructure As DTOIban.Structure, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM IbanStructure ")
        sb.AppendLine("WHERE CountryISO=@ISO")

        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@ISO", oIbanStructure.Country.ISO)
        Dim oDs As New DataSet

        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("CountryISO") = oIbanStructure.Country.ISO
        Else
            oRow = oTb.Rows(0)
        End If

        With oIbanStructure
            oRow("BankPosition") = .BankPosition
            oRow("BankLength") = .BankLength
            oRow("BankFormat") = .BankFormat

            oRow("BranchPosition") = .BranchPosition
            oRow("BranchLength") = .BranchLength
            oRow("BranchFormat") = .BranchFormat

            oRow("CheckDigitsPosition") = .CheckDigitsPosition
            oRow("CheckDigitsLength") = .CheckDigitsLength
            oRow("CheckDigitsFormat") = .CheckDigitsFormat

            oRow("AccountPosition") = .AccountPosition
            oRow("AccountLength") = .AccountLength
            oRow("AccountFormat") = .AccountFormat

            oRow("OverallLength") = .OverallLength
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oIbanStructure As DTOIban.Structure, ByRef exs as list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oIbanStructure, oTrans)
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


    Shared Sub Delete(oIbanStructure As DTOIban.Structure, ByRef oTrans As SqlTransaction)
        With oIbanStructure
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE IbanStructure ")
            sb.AppendLine("WHERE CountryISO=@ISO")

            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@ISO", oIbanStructure.Country.ISO)
        End With
    End Sub

End Class

Public Class IbanStructuresLoader
    Shared Function All() As List(Of DTOIban.Structure)
        Dim retval As New List(Of DTOIban.Structure)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT IbanStructure.* ")
        sb.AppendLine(", Country.Guid AS CountryGuid ")
        sb.AppendLine("FROM IbanStructure ")
        sb.AppendLine("INNER JOIN Country ON IbanStructure.CountryISO = Country.ISO ")
        sb.AppendLine("ORDER BY IbanStructure.CountryISO")
        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim item As New DTOIban.Structure
            With item
                .Country = New DTOCountry(oDrd("CountryGuid"))
                .Country.ISO = oDrd("CountryISO")

                .BankPosition = oDrd("BankPosition")
                .BankLength = oDrd("BankLength")
                .BankFormat = oDrd("BankFormat")
                .BranchPosition = oDrd("BranchPosition")
                .BranchLength = oDrd("BranchLength")
                .BranchFormat = oDrd("BranchFormat")
                .CheckDigitsPosition = oDrd("CheckDigitsPosition")
                .CheckDigitsLength = oDrd("CheckDigitsLength")
                .CheckDigitsFormat = oDrd("CheckDigitsFormat")
                .AccountPosition = oDrd("AccountPosition")
                .AccountLength = oDrd("AccountLength")
                .AccountFormat = oDrd("AccountFormat")
                .IsLoaded = True
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class