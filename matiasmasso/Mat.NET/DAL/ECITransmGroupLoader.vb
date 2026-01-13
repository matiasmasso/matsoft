Public Class ECITransmGroupLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOECITransmGroup
        Dim retval As DTOECITransmGroup = Nothing
        Dim oECITransmGroup As New DTOECITransmGroup(oGuid)
        If Load(oECITransmGroup) Then
            retval = oECITransmGroup
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oECITransmGroup As DTOECITransmGroup) As Boolean
        If Not oECITransmGroup.IsLoaded And Not oECITransmGroup.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT ECITransmGroup.Nom, ECITransmGroup.Ord ")
            sb.AppendLine(", ECITransmGroup.Platform, Platform.FullNom AS PlatformNom ")
            sb.AppendLine(", ECITransmCentre.Guid AS Child, ECITransmCentre.Centre, Centre.FullNom AS CentreNom ")
            sb.AppendLine("FROM ECITransmGroup ")
            sb.AppendLine("LEFT OUTER JOIN CliGral AS Platform ON ECITransmGroup.Platform=Platform.Guid ")
            sb.AppendLine("INNER JOIN CliGral AS Centre ON ECITransmGroup.Centre=Centre.Guid ")
            sb.AppendLine("WHERE ECITransmGroup.Guid='" & oECITransmGroup.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                If Not oECITransmGroup.IsLoaded Then
                    With oECITransmGroup
                        .Nom = oDrd("Nom")
                        .Ord = oDrd("Ord")
                        If Not IsDBNull(oDrd("Platform")) Then
                            .Platform = New DTOContact(oDrd("Platform"))
                            .Platform.FullNom = SQLHelper.GetStringFromDataReader(oDrd("PlatformNom"))
                        End If
                        .Items = New List(Of DTOECITransmCentre)
                        .IsLoaded = True
                    End With
                End If
                If Not IsDBNull(oDrd("Child")) Then
                    Dim item As New DTOECITransmCentre(oDrd("Child"))
                    With item
                        .Centre = New DTOContact(oDrd("Centre"))
                        .Centre.FullNom = oDrd("CentreNom")
                    End With
                    oECITransmGroup.Items.Add(item)
                End If
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oECITransmGroup.IsLoaded
        Return retval
    End Function

    Shared Function Update(oECITransmGroup As DTOECITransmGroup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oECITransmGroup, oTrans)
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

    Shared Sub Update(oECITransmGroup As DTOECITransmGroup, ByRef oTrans As SqlTransaction)
        UpdateHeader(oECITransmGroup, oTrans)
        UpdateItems(oECITransmGroup, oTrans)
    End Sub


    Shared Sub UpdateHeader(oECITransmGroup As DTOECITransmGroup, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ECITransmGroup ")
        sb.AppendLine("WHERE Guid='" & oECITransmGroup.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oECITransmGroup.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oECITransmGroup
            oRow("Nom") = .Nom
            oRow("Ord") = .Ord
            oRow("Platform") = SQLHelper.NullableBaseGuid(.Platform)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateItems(oECITransmGroup As DTOECITransmGroup, ByRef oTrans As SqlTransaction)
        DeleteItems(oECITransmGroup, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ECITransmCentre ")
        sb.AppendLine("WHERE Parent='" & oECITransmGroup.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item As DTOECITransmCentre In oECITransmGroup.Items
            Dim oRow As DataRow = oTb.Rows.Add
            oRow("Guid") = item.Guid
            oRow("Parent") = oECITransmGroup.Guid
            oRow("Centre") = item.Centre.Guid
        Next

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oECITransmGroup As DTOECITransmGroup, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oECITransmGroup, oTrans)
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


    Shared Sub Delete(oECITransmGroup As DTOECITransmGroup, ByRef oTrans As SqlTransaction)
        DeleteItems(oECITransmGroup, oTrans)
        DeleteHeader(oECITransmGroup, oTrans)
    End Sub

    Shared Sub DeleteHeader(oECITransmGroup As DTOECITransmGroup, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ECITransmGroup WHERE Guid='" & oECITransmGroup.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteItems(oECITransmGroup As DTOECITransmGroup, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE ECITransmCentre WHERE Parent='" & oECITransmGroup.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

#End Region

End Class

Public Class ECITransmGroupsLoader

    Shared Function All() As List(Of DTOECITransmGroup)
        Dim retval As New List(Of DTOECITransmGroup)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ECITransmGroup.Guid, ECITransmGroup.Nom, ECITransmGroup.Ord ")
        sb.AppendLine(", ECITransmGroup.Platform, Platform.FullNom AS PlatformNom ")
        sb.AppendLine(", ECITransmCentre.Guid AS Child, ECITransmCentre.Centre, Centre.FullNom AS CentreNom ")
        sb.AppendLine("FROM ECITransmGroup ")
        sb.AppendLine("LEFT OUTER JOIN ECITransmCentre ON ECITransmGroup.Guid=ECITransmCentre.Parent ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Platform ON ECITransmGroup.Platform=Platform.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral AS Centre ON ECITransmCentre.Centre=Centre.Guid ")
        sb.AppendLine("ORDER BY ECITransmGroup.Ord, CentreNom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oParent As New DTOECITransmGroup
        Do While oDrd.Read
            If Not oParent.Guid.Equals(oDrd("Guid")) Then
                oParent = New DTOECITransmGroup(oDrd("Guid"))
                With oParent
                    .Nom = oDrd("Nom")
                    .Ord = oDrd("Ord")
                    If Not IsDBNull(oDrd("Platform")) Then
                        .Platform = New DTOContact(oDrd("Platform"))
                        .Platform.FullNom = SQLHelper.GetStringFromDataReader(oDrd("PlatformNom"))
                    End If
                    .Items = New List(Of DTOECITransmCentre)
                    .IsLoaded = True
                End With
                retval.Add(oParent)
            End If
            If Not IsDBNull(oDrd("Child")) Then
                Dim item As New DTOECITransmCentre(oDrd("Child"))
                With item
                    .Centre = New DTOContact(oDrd("Centre"))
                    .Centre.FullNom = oDrd("CentreNom")
                End With
                oParent.Items.Add(item)
            End If
        Loop

        oDrd.Close()
        Return retval
    End Function


End Class
