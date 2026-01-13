Public Class TraspasDeliveryLoader


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTraspasDelivery
        Dim retval As DTOTraspasDelivery = Nothing
        Dim oTraspasDelivery As New DTOTraspasDelivery(oGuid)
        If Load(oTraspasDelivery) Then
            retval = oTraspasDelivery
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oTraspasDelivery As DTOTraspasDelivery) As Boolean
        If Not oTraspasDelivery.IsLoaded And Not oTraspasDelivery.IsNew Then
            oTraspasDelivery.Items = New List(Of DTODeliveryItem)
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Arc.Qty, Arc.ArtGuid, Arc.MgzGuid, Arc.Cod, Arc.Fch ")
            sb.AppendLine(", Product2.Cod as ProductCod, Product2.BrandGuid, Product2.BrandNom, Product2.CategoryGuid, Product2.CategoryNom, Product2.SkuNom ")
            sb.AppendLine(", Mgz.Nom AS MgzAbr ")
            sb.AppendLine(", Alb.Alb ")
            sb.AppendLine("FROM Arc ")
            sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
            sb.AppendLine("INNER JOIN Product2 ON Arc.ArtGuid = Product2.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Mgz ON Arc.MgzGuid = Mgz.Guid ")
            sb.AppendLine("WHERE Arc.AlbGuid='" & oTraspasDelivery.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY Product2.BrandNom, Product2.CategoryNom, Product2.SkuNom ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oMgz As New DTOMgz(oDrd("MgzGuid"))
                oMgz.Abr = SQLHelper.GetStringFromDataReader(oDrd("MgzAbr"))
                Dim item As New DTODeliveryItem
                With item
                    .Qty = oDrd("Qty")
                    .Sku = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), CType(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ArtGuid"), oDrd("SkuNom"))
                    Select Case oDrd("Cod")
                        Case DTODeliveryItem.Cods.TraspasEntrada
                            oTraspasDelivery.MgzTo = oMgz
                        Case DTODeliveryItem.Cods.TraspasSortida
                            With oTraspasDelivery
                                .MgzFrom = oMgz
                                .Fch = oDrd("Fch")
                                .Id = oDrd("Alb")
                                .Items.Add(item)
                            End With
                    End Select
                End With
            Loop

            oDrd.Close()
            oTraspasDelivery.IsLoaded = True

        End If

        Dim retval As Boolean = oTraspasDelivery.IsLoaded
        Return retval
    End Function

#End Region

End Class

