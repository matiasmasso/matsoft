Public Class BrandAreaLoader

    Shared Function Find(oGuid As Guid) As DTOBrandArea
        Dim retval As DTOBrandArea = Nothing
        Dim oBrandArea As New DTOBrandArea
        oBrandArea.Guid = oGuid
        If Load(oBrandArea) Then
            retval = oBrandArea
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBrandArea As DTOBrandArea) As Boolean
        If Not oBrandArea.IsLoaded And Not oBrandArea.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BrandArea.Brand, BrandArea.Area, BrandArea.FchFrom, BrandArea.FchTo ")
            sb.AppendLine(", BrandNom.Esp AS BrandNomEsp, VwArea.Cod as AreaCod, VwArea.Nom as AreaNom ")
            sb.AppendLine("FROM BrandArea ")
            sb.AppendLine("INNER JOIN VwArea ON BrandArea.Area = VwArea.Guid ")
            sb.AppendLine("INNER JOIN VwLangText AS BrandNom ON BrandArea.Brand = BrandNom.Guid AND BrandNom.Src = 28 ")
            sb.AppendLine("WHERE BrandArea.Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oBrandArea.Guid.ToString())
            If oDrd.Read Then
                With oBrandArea
                    .Brand = New DTOProductBrand(oDrd("Brand"))
                    SQLHelper.LoadLangTextFromDataReader(.Brand.nom, oDrd, "BrandNomEsp", "BrandNomEsp", "BrandNomEsp", "BrandNomEsp")
                    Select Case oDrd("AreaCod")
                        Case DTOArea.Cods.Country
                            .Area = New DTOCountry(oDrd("Area"))
                            DirectCast(.Area, DTOCountry).LangNom.Esp = oDrd("Nom")
                        Case DTOArea.Cods.Zona
                            .Area = New DTOZona(oDrd("Area"))
                            DirectCast(.Area, DTOZona).Nom = oDrd("Nom")
                        Case DTOArea.Cods.Location
                            .Area = New DTOLocation(oDrd("Area"))
                            DirectCast(.Area, DTOLocation).Nom = oDrd("Nom")
                    End Select
                    .FchFrom = oDrd("FchFrom")
                    If Not IsDBNull(oDrd("FchTo")) Then
                        .FchTo = oDrd("FchTo")
                    End If

                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oBrandArea.IsLoaded
        Return retval
    End Function


    Shared Function Update(oBrandArea As DTOBrandArea, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBrandArea, oTrans)
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


    Shared Sub Update(oBrandArea As DTOBrandArea, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM BrandArea ")
        sb.AppendLine("WHERE Guid=@Guid ")

        Dim SQL As String = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oBrandArea.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBrandArea.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        oRow("Brand") = oBrandArea.Brand.Guid
        oRow("Area") = oBrandArea.Area.Guid
        oRow("FchFrom") = oBrandArea.FchFrom
        If oBrandArea.FchTo = Nothing Then
            oRow("FchTo") = System.DBNull.Value
        Else
            oRow("FchTo") = oBrandArea.FchTo
        End If

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oBrandArea As DTOBrandArea, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBrandArea, oTrans)
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


    Shared Sub Delete(oBrandArea As DTOBrandArea, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE BrandArea WHERE Guid = @Guid ")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBrandArea.Guid.ToString())
    End Sub

End Class

Public Class BrandAreasLoader
    Shared Function FromBrand(oBrand As DTOProductBrand) As List(Of DTOBrandArea)
        Dim retval As New List(Of DTOBrandArea)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BrandArea.Guid, BrandArea.FchFrom, BrandArea.FchTo ")
        sb.AppendLine(", VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipGuid, VwAreaNom.ZipCod ")
        sb.AppendLine("FROM BrandArea ")
        sb.AppendLine("INNER JOIN VwAreaNom ON BrandArea.Area = VwAreaNom.Guid ")
        sb.AppendLine("WHERE BrandArea.Brand='" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY VwAreaNom.CountryNomEsp, VwAreaNom.ZonaNom, VwAreaNom.LocationNom, VwAreaNom.ZipCod")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOBrandArea
            With item
                .Guid = oDrd("Guid")
                .Brand = oBrand
                .Area = AreaLoader.NewArea(DirectCast(oDrd("AreaCod"), DTOArea.Cods), DirectCast(oDrd("CountryGuid"), Guid), oDrd("CountryNomEsp").ToString, oDrd("CountryNomCat").ToString, oDrd("CountryNomEng").ToString, oDrd("CountryISO").ToString, oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZipGuid"), oDrd("ZipCod"))
                .FchFrom = oDrd("FchFrom")
                If Not IsDBNull(oDrd("FchTo")) Then
                    .FchTo = oDrd("FchTo")
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oBrandAreas As List(Of DTOBrandArea), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBrandAreas, oTrans)
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


    Shared Sub Update(oBrandAreas As List(Of DTOBrandArea), ByRef oTrans As SqlTransaction)
        Dim iBrandCount As Integer = oBrandAreas.Select(Function(x) x.Brand.Guid).Distinct.Count

        If iBrandCount > 1 Then
            Throw New Exception("no es poden desar areas de diferents marques comercials a l'hora")
        Else
            Dim oBrand As DTOProductBrand = oBrandAreas(0).Brand
            Delete(oBrand, oTrans)

            Dim SQL As String = "SELECT * FROM BrandArea WHERE Brand=@Guid"
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oBrand.Guid.ToString())
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            For Each item In oBrandAreas
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Guid") = item.Guid
                oRow("Brand") = oBrand.Guid
                oRow("Area") = item.Area.Guid
                oRow("FchFrom") = item.FchFrom
                oRow("FchTo") = item.FchTo
            Next

            oDA.Update(oDs)
        End If

    End Sub


    Shared Function Delete(oBrand As DTOProductBrand, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBrand, oTrans)
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


    Shared Sub Delete(oBrand As DTOProductBrand, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BrandArea WHERE Brand=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBrand.Guid.ToString())
    End Sub
End Class
