Public Class ProductAtlasLoader

    Shared Function GetCountries(oProduct As DTOProduct, oLang As DTOLang) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        Dim sFieldNom As String = oLang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng")
        Dim SQL As String = "SELECT Country.Guid, " & sFieldNom & " FROM Country " _
                            & "INNER JOIN Zona ON Country.Guid = Zona.Country " _
                            & "INNER JOIN Location ON Zona.Guid = Location.Zona " _
                            & "INNER JOIN Web ON Location.Guid = Web.Location " _
                            & "INNER JOIN ProductParent ON Web.Category = ProductParent.ChildGuid " _
                            & "WHERE ProductParent.ParentGuid = '" & oProduct.Guid.ToString & "' " _
                            & "AND Web.Impagat = 0 " _
                            & "AND Web.Botiga = 1 " _
                            & "AND Web.Obsoleto = 0 " _
                            & "AND Web.Blocked = 0 " _
                            & "GROUP BY Country.Guid, " & sFieldNom & " " _
                            & "ORDER BY " & sFieldNom & " "

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oCountry As New DTOCountry(oGuid)
            Select Case oLang.Id
                Case DTOLang.Ids.ESP
                    oCountry.Nom.Esp = oDrd("Nom_Esp")
                Case DTOLang.Ids.CAT
                    oCountry.Nom.Cat = oDrd("Nom_Cat")
                Case DTOLang.Ids.ESP
                    oCountry.Nom.Eng = oDrd("Nom_Eng")
            End Select
            retval.Add(oCountry)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function GetZonas(oProduct As DTOProduct, oCountry As DTOCountry) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Zona.Guid, Zona.Nom FROM Zona ")
        sb.AppendLine("INNER JOIN Location ON Zona.Guid = Location.Zona ")
        sb.AppendLine("INNER JOIN Web ON Location.Guid = Web.Location ")

        oProduct = ProductLoader.Find(oProduct.Guid)
        Select Case oProduct.SourceCod
            Case DTOProduct.SourceCods.Brand
                sb.AppendLine("INNER JOIN ProductParent ON Web.Category = ProductParent.ChildGuid ")
                sb.AppendLine("WHERE ProductParent.ParentGuid = '" & oProduct.Guid.ToString & "' ")
            Case DTOProduct.SourceCods.Category
                sb.AppendLine("WHERE Web.Category = '" & oProduct.Guid.ToString & "' ")
            Case DTOProduct.SourceCods.SKU
                sb.AppendLine("INNER JOIN ProductParent ON Web.Category = ProductParent.ParentGuid ")
                sb.AppendLine("WHERE ProductParent.ChildGuid = '" & oProduct.Guid.ToString & "' ")
        End Select

        sb.AppendLine("AND Zona.Country = '" & oCountry.Guid.ToString & "' ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Botiga = 1 ")
        sb.AppendLine("AND Web.Obsoleto = 0 ")
        sb.AppendLine("AND Web.Blocked = 0 ")
        sb.AppendLine("GROUP BY Zona.Guid, Zona.Nom ")
        sb.AppendLine("ORDER BY Zona.Nom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oZona As New DTOZona(oDrd("Guid"))
            With oZona
                .Country = oCountry
                .Nom = oDrd("Nom")
            End With
            retval.Add(oZona)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function GetLocations(oProduct As DTOProduct, oZona As DTOZona) As List(Of DTOLocation)
        Dim retval As New List(Of DTOLocation)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Location.Guid, Location.Nom ")
        sb.AppendLine("FROM Location ")
        sb.AppendLine("INNER JOIN Web ON Location.Guid = Web.Location ")

        oProduct = ProductLoader.Find(oProduct.Guid)
        If TypeOf oProduct Is DTOProductBrand Then
            sb.AppendLine("INNER JOIN ProductParent ON Web.Category = ProductParent.ChildGuid ")
            sb.AppendLine("WHERE ProductParent.ParentGuid = '" & oProduct.Guid.ToString & "' ")
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            sb.AppendLine("WHERE Web.Category = '" & oProduct.Guid.ToString & "' ")
        ElseIf TypeOf oProduct Is DTOProductSku Then
            sb.AppendLine("INNER JOIN ProductParent ON Web.Category = ProductParent.ParentGuid ")
            sb.AppendLine("WHERE ProductParent.ChildGuid = '" & oProduct.Guid.ToString & "' ")
        End If

        ''Dim oBrand As DTOProductBrand = BLL.BLLProduct.Brand(oProduct)
        's 'b.AppendLine("INNER JOIN ProductParent ON Web.Category = ProductParent.ChildGuid ")
        'sb.AppendLine("WHERE ProductParent.ParentGuid = '" & oBrand.Guid.ToString & "' ")



        sb.AppendLine("AND Web.AreaGuid = '" & oZona.Guid.ToString & "' ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Botiga = 1 ")
        sb.AppendLine("AND Web.Obsoleto = 0 ")
        sb.AppendLine("AND Web.Blocked = 0 ")
        sb.AppendLine("GROUP BY Location.Guid, Location.Nom ")
        sb.AppendLine("ORDER BY Location.Nom")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oLocation As New DTOLocation(oDrd("Guid"))
            With oLocation
                .Zona = oZona
                .Nom = oDrd("Nom")
            End With
            retval.Add(oLocation)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function BestZona(oProductGuid As Guid, oCountry As DTOCountry) As DTOZona
        Dim retval As DTOZona = Nothing
        Dim SQL As String = "SELECT TOP 1 Zona.Guid, Zona.Nom FROM Zona " _
                            & "INNER JOIN Location ON Zona.Guid = Location.Zona " _
                            & "INNER JOIN Web ON Location.Guid = Web.Location " _
                            & "INNER JOIN ProductParent ON Web.Category = ProductParent.ChildGuid " _
                            & "WHERE Zona.Country = '" & oCountry.Guid.ToString & "' " _
                            & "AND ProductParent.ParentGuid = '" & oProductGuid.ToString & "' " _
                            & "AND Web.Impagat = 0 " _
                            & "AND Web.Botiga = 1 " _
                            & "AND Web.Obsoleto = 0 " _
                            & "AND Web.Blocked = 0 " _
                            & "GROUP BY Zona.Guid, Zona.Nom " _
                            & "ORDER BY SUM(CASE WHEN SalePointsCount>0 THEN SumCcxVal/SalePointsCount ELSE 0 END) DESC"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOZona(oDrd("Guid"))
            retval.Nom = oDrd("Nom")
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function BestLocation(oProductGuid As Guid) As DTOLocation
        Dim retval As DTOLocation = Nothing
        Dim SQL As String = "SELECT TOP 1 Location.Guid as LocationGuid, Location.Nom as LocationNom, " _
                            & "Zona.Guid as ZonaGuid, Zona.Nom as ZonaNom, Zona.Country " _
                            & "FROM Location " _
                            & "INNER JOIN Zona ON Location.Zona = Zona.Guid " _
                            & "INNER JOIN Web ON Location.Guid = Web.Location " _
                            & "INNER JOIN ProductParent ON Web.Category = ProductParent.ChildGuid " _
                            & "WHERE ProductParent.ParentGuid = @Product " _
                            & "AND Web.Impagat = 0 " _
                            & "AND Web.Botiga = 1 " _
                            & "AND Web.Obsoleto = 0 " _
                            & "AND Web.Blocked = 0 " _
                            & "GROUP BY Location.Guid, Location.Nom, Zona.Guid, Zona.Nom, Zona.Country " _
                            & "ORDER BY SUM(SumCcxVal) DESC"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Product", oProductGuid.ToString)
        If oDrd.Read Then
            Dim oCountry As New DTOCountry(CType(oDrd("Country"), Guid))

            Dim oZona As New DTOZona(oDrd("ZonaGuid"))
            oZona.Nom = oDrd("ZonaNom")
            oZona.Country = oCountry

            retval = New DTOLocation(CType(oDrd("LocationGuid"), Guid))
            retval.Nom = oDrd("LocationNom")
            retval.Zona = oZona
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function BestLocation(oProductGuid As Guid, oZona As DTOZona) As DTOLocation
        'TYO DEPRECATE
        Dim retval As DTOLocation = Nothing
        Dim SQL As String = "SELECT TOP 1 Location.Guid, Location.Nom FROM Location " _
                            & "INNER JOIN Web ON Location.Guid = Web.Location " _
                            & "INNER JOIN ProductParent ON Web.Category = ProductParent.ChildGuid " _
                            & "WHERE Location.Zona = @Zona " _
                            & "AND ProductParent.ParentGuid = @Product " _
                            & "AND Web.Impagat = 0 " _
                            & "AND Web.Botiga = 1 " _
                            & "AND Web.Obsoleto = 0 " _
                            & "AND Web.Blocked = 0 " _
                            & "GROUP BY Location.Guid, Location.Nom " _
                            & "ORDER BY SUM(SumCcxVal) DESC"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Product", oProductGuid.ToString, "@Zona", oZona.Guid.ToString)
        If oDrd.Read Then
            Dim oGuid As Guid = oDrd("Guid")
            retval = New DTOLocation(oGuid)
            retval.Nom = oDrd("Nom")
        End If
        oDrd.Close()
        Return retval
    End Function
End Class
