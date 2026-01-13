Public Class AlineamientoDeDisponibilidadLoader
    Shared Function Find(oGuid As Guid) As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad
        Dim retval As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Fch, Text ")
        sb.AppendLine("FROM ElCorteInglesAlineamientoStocks ")
        sb.AppendLine("WHERE Guid = '" & oGuid.ToString() & "'")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad(oDrd("Guid"))
            retval.Fch = oDrd("Fch")
            retval.Text = oDrd("Text")
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Factory() As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad
        Dim retval As New DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad
        Dim oEci = DTOHolding.Wellknown(DTOCustomer.Wellknowns.elCorteIngles)
        Dim oEmp = EmpLoader.Find(DTOEmp.Ids.MatiasMasso)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT ECIDept.Id AS Uneco ")
        sb.AppendLine(", ArtCustRef.Ref AS RefECI ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.EAN13, VwSkuNom.Obsoleto ")
        sb.AppendLine(", VwSkuNom.BrandNomEsp, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuNomEsp ")
        sb.AppendLine(", VwSkuAndBundleStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine(", VwRetail.Retail ")
        sb.AppendLine("FROM ECIDept ")
        sb.AppendLine("INNER JOIN ArtCustRef ON ECIDept.Guid = ArtCustRef.CustomerDept AND ArtCustRef.CliGuid = '" & oEci.Guid.ToString() & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom ON ArtCustRef.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail ON ArtCustRef.ArtGuid = VwRetail.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks ON ArtCustRef.ArtGuid = VwSkuAndBundleStocks.SkuGuid AND VwSkuAndBundleStocks.MgzGuid = '" & oEmp.Mgz.Guid.ToString() & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON ArtCustRef.ArtGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("WHERE ArtCustRef.FchTo IS NULL AND VwSkuNom.CategoryCodi < 2 ")
        sb.AppendLine("ORDER BY ECIDept.Id, VwSkuNom.BrandNomEsp, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuNomEsp ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("SkuGuid")) And Not IsDBNull(oDrd("EAN13")) Then
                Dim iStock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                Dim iClients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                Dim iClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                Dim iClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                Dim iAvailability = iStock - (iClients - iClientsAlPot - iClientsEnProgramacio)
                If iAvailability < 0 Then iAvailability = 0
                Dim item As New DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item
                With item
                    .SkuGuid = oDrd("SkuGuid")
                    .BrandNom = SQLHelper.GetStringFromDataReader(oDrd("BrandNomEsp"))
                    .CategoryNom = SQLHelper.GetStringFromDataReader(oDrd("CategoryNomEsp"))
                    .SkuNom = SQLHelper.GetStringFromDataReader(oDrd("SkuNomEsp"))
                    .Ean = SQLHelper.GetStringFromDataReader(oDrd("Ean13"))
                    .RefEci = SQLHelper.GetStringFromDataReader(oDrd("RefECI"))
                    .Stock = iAvailability
                    .Price = SQLHelper.GetDecimalFromDataReader(oDrd("Retail"))
                    .Obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto"))
                    .Uneco = SQLHelper.GetStringFromDataReader(oDrd("Uneco"))
                End With
                retval.Items.Add(item)
            End If
        Loop
        oDrd.Close()

        sb = New Text.StringBuilder
        For Each item In retval.Items
            sb.AppendLine(item.Line(retval.Fch))
        Next
        retval.Text = sb.ToString

        Return retval
    End Function


    Shared Function Insert(exs As List(Of Exception), oLog As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Insert(oLog, oTrans)
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


    Shared Sub Insert(oLog As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ElCorteInglesAlineamientoStocks ")
        sb.AppendLine("WHERE Guid='" & Guid.NewGuid().ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        oRow = oTb.NewRow
        oTb.Rows.Add(oRow)
        oRow("Fch") = oLog.Fch
        oRow("Text") = oLog.Text
        oDA.Update(oDs)
    End Sub
End Class


Public Class AlineamientosDeDisponibilidadLoader
    Shared Function All() As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        Dim retval As New List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Fch ")
        sb.AppendLine("FROM ElCorteInglesAlineamientoStocks ")
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad
            With item
                .Guid = oDrd("Guid")
                .Fch = oDrd("Fch")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class