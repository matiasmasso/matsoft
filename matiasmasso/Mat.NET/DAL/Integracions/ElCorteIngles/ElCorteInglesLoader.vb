Public Class ElCorteInglesLoader

    Shared Function OrdersModel(year As Integer) As List(Of DTO.Models.ElCorteInglesOrderModel)
        Dim retval As New List(Of DTO.Models.ElCorteInglesOrderModel)
        Dim oHolding = DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Pdc.Guid, Pdc.Pdc, Pdc.Fch, Pdc.Pdd, Pdc.Eur, Client.Ref, COUNT(Pnc.Guid) AS Items, SUM(Pnc.Qty) AS Qty, SUM(Pnc.Pn2) AS Pending ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN CliClient Client ON Pdc.CliGuid = Client.Guid ")
        sb.AppendLine("INNER JOIN CliClient Ccx ON Ccx.Guid = (CASE WHEN Client.CcxGuid IS NULL THEN Client.Guid ELSE Client.CcxGuid END) ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("LEFT OUTER JOIN Arc ON Pnc.Guid = Arc.PncGuid ")
        sb.AppendLine("WHERE Ccx.Holding='" & oHolding.Guid.ToString & "' ")
        sb.AppendLine("AND Pdc.Yea=" & year & " ")
        sb.AppendLine("GROUP BY Pdc.Guid, Pdc.Pdc, Pdc.Fch, Pdc.Pdd, Pdc.Eur, Client.Ref ")
        sb.AppendLine("ORDER BY Pdc.Fch DESC, Pdc.Pdd ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Dim pdd = oDrd("Pdd").ToString()
            Dim item As New DTO.Models.ElCorteInglesOrderModel
            With item
                .Guid = oDrd("Guid")
                .Id = oDrd("Pdc")
                .Fch = oDrd("Fch")
                .Eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
                .Centre = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .ECINum = pdd.Substring(0, If(pdd.IndexOf("/") > 0, pdd.IndexOf("/"), pdd.Length))
                Dim idx = pdd.IndexOf("dep.")
                If idx >= 0 And pdd.Length > idx + 4 Then
                    idx += 4
                    Dim len = pdd.IndexOf("/", idx) - idx
                    .Depto = pdd.Substring(idx, len)
                End If
                If SQLHelper.GetIntegerFromDataReader(oDrd("Items")) = 0 Then
                    .ShippingStatus = DTOPurchaseOrder.ShippingStatusCods.emptyOrder
                Else
                    Dim qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                    Dim pending = SQLHelper.GetIntegerFromDataReader(oDrd("Pending"))
                    If pending = 0 Then
                        .ShippingStatus = DTOPurchaseOrder.ShippingStatusCods.fullyShipped
                    ElseIf pending = qty Then
                        .ShippingStatus = DTOPurchaseOrder.ShippingStatusCods.unShipped
                    Else
                        .ShippingStatus = DTOPurchaseOrder.ShippingStatusCods.halfShipped
                    End If
                End If

            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
    Shared Function ComandesDeTransmisions(oTransmisions As List(Of DTOTransmisio)) As List(Of DTOTransmisio)
        Dim retval As New List(Of DTOTransmisio)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("select Alb.TransmGuid, Transm.Transm, Alb.PlatformGuid, Plataforma.ref AS Plataforma, Centro.Ref AS Centro, X.AlbGuid, Alb.Alb, Alb.CliGuid, X.PdcGuid, X.Pdd ")
        sb.AppendLine("From Alb ")
        sb.AppendLine("INNER JOIN Transm on Alb.TransmGuid = Transm.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliClient AS Centro  ON Alb.CliGuid=Centro.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliClient AS Plataforma  ON Alb.PlatformGuid=Plataforma.Guid ")
        sb.AppendLine("INNER JOIN (Select Arc.AlbGuid, Arc.PdcGuid, Pdc.Pdd FROM Arc INNER JOIN Pdc On Arc.PdcGuid = Pdc.Guid GROUP BY Arc.AlbGuid, Arc.PdcGuid, Pdc.Pdd) X On Alb.Guid = X.AlbGuid ")
        sb.AppendLine("WHERE (")
        For Each item As DTOTransmisio In oTransmisions
            If Not item.Equals(oTransmisions.First) Then
                sb.AppendLine("OR ")
            End If
            sb.AppendLine("Alb.TransmGuid = '" & item.Guid.ToString & "' ")
        Next
        sb.AppendLine(") ")
        sb.AppendLine("ORDER BY Transm.Transm, Alb.Alb ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oTransm As New DTOTransmisio
        Dim oDelivery As New DTODelivery
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Do While oDrd.Read
            If Not oTransm.Guid.Equals(oDrd("TransmGuid")) Then
                oTransm = New DTOTransmisio(oDrd("TransmGuid"))
                oTransm.Id = oDrd("Transm")
                oTransm.Deliveries = New List(Of DTODelivery)
                retval.Add(oTransm)
            End If
            If Not oDelivery.Guid.Equals(oDrd("AlbGuid")) Then
                oDelivery = New DTODelivery(oDrd("AlbGuid"))
                With oDelivery
                    .Transmisio = oTransm
                    .Id = oDrd("Alb")
                    .Customer = New DTOCustomer(oDrd("CliGuid"))
                    .Customer.Ref = oDrd("Centro")
                    If IsDBNull(oDrd("PlatformGuid")) Then
                    Else
                        .Platform = New DTOCustomerPlatform(oDrd("PlatformGuid"))
                        .Platform.Nom = oDrd("Plataforma")
                    End If
                    .PurchaseOrders = New List(Of DTOPurchaseOrder)
                End With
                oTransm.Deliveries.Add(oDelivery)
            End If
            If Not oPurchaseOrder.Guid.Equals(oDrd("PdcGuid")) Then
                oPurchaseOrder = New DTOPurchaseOrder(oDrd("PdcGuid"))
                With oPurchaseOrder
                    .Concept = oDrd("Pdd")
                End With
            End If
            oDelivery.PurchaseOrders.Add(oPurchaseOrder)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PlantillaDescatalogats() As MatHelper.Excel.Sheet
        Dim retval As MatHelper.Excel.Sheet = PlantillaProducte("Descatalogados El Corte Ingles")
        Dim oCustomer = CustomerLoader.Find(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles).Guid)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Art.FchObsoleto, VwSkuNom.SkuId, ArtCustRef.Ref, VwSkuNom.SkuNomLlarg, VwSkuNom.BrandNom, VwSkuNom.EAN13 ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("INNER JOIN Art ON ArtCustRef.ArtGuid = Art.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("where ArtCustRef.cliguid='" & oCustomer.Guid.ToString() & "'")
        sb.AppendLine("AND Art.Obsoleto <>0 ")
        sb.AppendLine("ORDER BY Art.FchObsoleto DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oTransm As New DTOTransmisio
        Dim oDelivery As New DTODelivery
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Do While oDrd.Read
            Dim oRow = retval.AddRow()
            oRow.AddCell(SQLHelper.GetFchFromDataReader(oDrd("FchObsoleto")))
            oRow.AddCell(oDrd("SkuId"))
            oRow.AddCell() 'Depto
            oRow.AddCell(Left(SQLHelper.GetStringFromDataReader(oDrd("Ref")), 3))
            oRow.AddCell(Mid(SQLHelper.GetStringFromDataReader(oDrd("Ref")), 4))
            oRow.AddCell() 'Talla
            oRow.AddCell() 'Material
            oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("SkuNomLlarg")))
            oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("BrandNom")))
            oRow.AddCell(oCustomer.SuProveedorNum)
            oRow.AddCell() 'Num.Fabricante
            oRow.AddCell() 'Serie
            oRow.AddCell() 'Mod.Fab.
            oRow.AddCell() 'Color
            oRow.AddCell() 'Tamahor
            oRow.AddCell() 'Dibujo
            oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("EAN13")))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function PlantillaExhaurits() As MatHelper.Excel.Sheet
        Dim retval As MatHelper.Excel.Sheet = PlantillaProducte("Temporalmente agotados")
        retval.Columns.RemoveRange(0, 2) 'remove columns obsolet date and SkuId since there may be different SkuIds for each custom ref
        Dim oCustomer = CustomerLoader.Find(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.elCorteIngles).Guid)
        EmpLoader.Load(oCustomer.Emp)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT ArtCustRef.Ref, VwSkuNom.SkuNomLlarg, VwSkuNom.BrandNom, VwSkuNom.EAN13 ")
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("INNER JOIN Art ON ArtCustRef.ArtGuid = Art.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oCustomer.Emp.Mgz.Guid.ToString() & "' ")
        sb.AppendLine("INNER JOIN VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("WHERE ArtCustRef.CliGuid='" & oCustomer.Guid.ToString() & "'")
        sb.AppendLine("AND ")
        sb.AppendLine("((CASE WHEN VwSkuStocks.Stock IS NULL THEN 0 ELSE VwSkuStocks.Stock END) ")
        sb.AppendLine("-(CASE WHEN VwSkuPncs.Clients IS NULL THEN 0 ELSE VwSkuPncs.Clients END) ")
        sb.AppendLine("+(CASE WHEN VwSkuPncs.ClientsAlPot IS NULL THEN 0 ELSE VwSkuPncs.ClientsAlPot END) ")
        sb.AppendLine("+(CASE WHEN VwSkuPncs.ClientsEnProgramacio IS NULL THEN 0 ELSE VwSkuPncs.ClientsEnProgramacio END) ")
        sb.AppendLine(") <= 0 ")
        sb.AppendLine("AND Art.Obsoleto = 0 ")
        sb.AppendLine("AND VwSkuNom.BrandGuid <> '" & DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios).Guid.ToString() & "' ")
        sb.AppendLine("GROUP BY ArtCustRef.Ref, VwSkuNom.SkuNomLlarg, VwSkuNom.BrandNom, VwSkuNom.EAN13 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandNom, VwSkuNom.SkuNomLlarg ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oTransm As New DTOTransmisio
        Dim oDelivery As New DTODelivery
        Dim oPurchaseOrder As New DTOPurchaseOrder
        Do While oDrd.Read

            Dim oRow = retval.AddRow()
            oRow.AddCell() 'Depto
            oRow.AddCell(Left(SQLHelper.GetStringFromDataReader(oDrd("Ref")), 3))
            oRow.AddCell(Mid(SQLHelper.GetStringFromDataReader(oDrd("Ref")), 4))
            oRow.AddCell() 'Talla
            oRow.AddCell() 'Material
            oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("SkuNomLlarg")))
            oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("BrandNom")))
            oRow.AddCell(oCustomer.SuProveedorNum)
            oRow.AddCell() 'Num.Fabricante
            oRow.AddCell() 'Serie
            oRow.AddCell() 'Mod.Fab.
            oRow.AddCell() 'Color
            oRow.AddCell() 'Tamahor
            oRow.AddCell() 'Dibujo
            oRow.AddCell(SQLHelper.GetStringFromDataReader(oDrd("EAN13")))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Descataloga(exs As List(Of Exception), oGuids As List(Of Guid), Optional reverse As Boolean = False) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	    Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("('{0}') ", oGuid.ToString())
            idx += 1
        Next
        sb.AppendLine()
        sb.AppendLine("UPDATE ArtCustRef ")
        If reverse Then
            sb.AppendLine("SET FchDescatalogado = NULL ")
        Else
            sb.AppendLine("SET FchDescatalogado = GETDATE() ")
        End If
        sb.AppendLine("FROM ArtCustRef ")
        sb.AppendLine("INNER JOIN @Table X ON ArtCustRef.Guid = X.Guid ")
        Dim SQL = sb.ToString()
        Dim i = SQLHelper.ExecuteNonQuery(SQL, exs)
        Return exs.Count = 0
    End Function

    Shared Function PlantillaProducte(title As String) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet(title)
        With retval
            .AddColumn("Fecha", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Ref M+O", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("DEPTO", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("FAMILIA", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("BARRA", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("TALLA", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("MATERIAL", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("DESCRIPCION", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("MARCA", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Nº PROVEEDOR", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("Nº FABRICANTE", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("SERIE", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("MOD.FAB.", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("COLOR", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("TAMAHOR", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("DIBUJO", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("EAN", MatHelper.Excel.Cell.NumberFormats.W50)
            .HeaderRowStyle = MatHelper.Excel.Column.HeaderRowStyles.ElCorteIngles
        End With
        Return retval
    End Function


End Class
