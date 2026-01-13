Imports DTO.Models.Min

Public Class ArtCustRefsLoader

    Shared Function All(oUser As DTOUser) As List(Of DTOGuidNom.Compact)
        Dim retval As New List(Of DTOGuidNom.Compact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwUsrArtCustRef.Sku, VwUsrArtCustRef.Ref ")
        sb.AppendLine("FROM VwUsrArtCustRef ")
        sb.AppendLine("WHERE VwUsrArtCustRef.Email = '" & oUser.Guid.ToString() & "'")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item = DTOGuidNom.Compact.Factory(oDrd("Sku"), oDrd("Ref"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function ElCorteIngles() As List(Of DTO.Integracions.ElCorteIngles.Cataleg)
        Dim retval As New List(Of DTO.Integracions.ElCorteIngles.Cataleg)
        Dim oHolding = DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT ArtCustRef.Guid, ArtCustRef.ArtGuid, ArtCustRef.Ref, ArtCustRef.CustomerDept, EciDept.Id AS DeptId, ArtCustRef.FchDescatalogado ")
        sb.AppendLine(", VwSkuNom.* ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON ArtCustRef.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN ECIDept ON ArtCustRef.CustomerDept=ECIDept.Guid ")
        sb.AppendLine("WHERE ArtCustRef.CliGuid = '" & oHolding.Guid.ToString() & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTO.Integracions.ElCorteIngles.Cataleg
            With item
                .Guid = oDrd("Guid")
                .Sku = SQLHelper.GetProductFromDataReader(oDrd)
                .Ref = oDrd("Ref")
                If Not IsDBNull(oDrd("FchDescatalogado")) Then
                    .FchDescatalogado = oDrd("FchDescatalogado")
                End If
                If Not IsDBNull(oDrd("CustomerDept")) Then
                    .Dept = New Models.Base.GuidNom(oDrd("CustomerDept"), SQLHelper.GetStringFromDataReader(oDrd("DeptId")))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Append2(exs As List(Of Exception), items As List(Of DTO.Integracions.ElCorteIngles.Cataleg))
        Dim sb As New System.Text.StringBuilder
        Dim Eci = DTO.DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
        sb.AppendLine("INSERT INTO ArtCustRef (CliGuid, ArtGuid, Ref, CustomerDept) ")
        For Each item In items
            sb.AppendLine(IIf(items.IndexOf(item) = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}','{1}','{2}','{3}') ", Eci.Guid.ToString(), item.Sku.Guid.ToString(), item.Ref, item.Dept.Guid.ToString())
        Next
        Dim SQL = sb.ToString()
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function

    Shared Function Append(exs As List(Of Exception), items As List(Of DTO.Integracions.ElCorteIngles.Cataleg))
        Dim Eci = DTO.DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      CliGuid uniqueidentifier NOT NULL ")
        sb.AppendLine("	    , ArtGuid uniqueidentifier NOT NULL ")
        sb.AppendLine("	    , CustomerDept uniqueidentifier NOT NULL ")
        sb.AppendLine("	    , Ref varchar(50) NOT NULL")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(CliGuid, ArtGuid, CustomerDept, Ref) ")

        Dim idx As Integer = 0
        For Each item In items
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}','{1}','{2}','{3}') ", Eci.Guid.ToString(), item.Sku.Guid.ToString(), item.Dept.Guid.ToString(), item.Ref)
            idx += 1
        Next
        Dim SQLX = sb.ToString()

        'delete matching rows
        sb = New Text.StringBuilder(SQLX)
        sb.AppendLine()
        sb.AppendLine("DELETE ArtCustRef FROM ArtCustRef INNER JOIN @Table X ON ArtCustRef.CliGuid = X.CliGuid AND ArtCustRef.Ref = X.Ref ")
        Dim SQL1 = sb.ToString()
        Dim i1 = SQLHelper.ExecuteNonQuery(SQL1, exs)

        'append rows
        sb = New Text.StringBuilder(SQLX)
        sb.AppendLine("INSERT INTO ArtCustRef (CliGuid, ArtGuid, Ref, CustomerDept) ")
        sb.AppendLine("SELECT X.CliGuid, X.ArtGuid, X.Ref, X.CustomerDept ")
        sb.AppendLine("FROM @Table X ")
        sb.AppendLine("LEFT OUTER JOIN ArtCustRef ON ArtCustRef.CliGuid = X.CliGuid AND ArtCustRef.Ref = X.Ref AND ArtCustRef.ArtGuid = NULL ")

        Dim SQL2 = sb.ToString()
        Dim i2 = SQLHelper.ExecuteNonQuery(SQL2, exs)
        Return exs.Count = 0

    End Function


End Class