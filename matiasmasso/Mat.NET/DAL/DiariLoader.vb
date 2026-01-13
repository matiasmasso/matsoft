Public Class DiariLoader
    Private Const MaxBrands As Integer = 6

    Public Enum Fields
        Id
        Fch
        FullNom
        Tpa
        Tot
    End Enum

    Public Enum FieldsDetail
        Guid
        Pdc
        Src
        FchCreated
        Cli
        FullNom
        Tot
    End Enum

    Shared Sub Load(ByRef oDiari As DTODiari)
        LoadYears(oDiari)
        LoadBrands(oDiari)
        LoadItems(oDiari)
        LoadSummaries(oDiari)
    End Sub

    Shared Function Years(oDiari As DTODiari) As List(Of DtoDiariItem)
        Dim retval As New List(Of DtoDiariItem)

        Dim sFchField As String = FchField(oDiari)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Year(" & sFchField & ") AS Source, GETDATE(), '' ")
        sb.AppendLine(SqlBrands2(oDiari))
        sb.AppendLine(SqlFrom(oDiari))
        sb.AppendLine(SqlWhere(oDiari, True))
        sb.AppendLine("GROUP BY Year(" & sFchField & ") ")
        sb.AppendLine("ORDER BY Year(" & sFchField & ") DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DtoDiariItem
            With oItem
                .Parent = oDiari
                .Source = oDrd("Source")
                .Level = DtoDiariItem.Levels.Yea
                .Text = oDrd("Source").ToString
                .Total = oDrd(Fields.Tot - 1)
                .Values = New List(Of Decimal)
                For i As Integer = 0 To oDiari.Brands.Count - 1
                    .Values.Add(oDrd(Fields.Tot + i))
                Next
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Months(oDiari As DTODiari) As List(Of DtoDiariItem)
        Dim retval As New List(Of DtoDiariItem)

        Dim sFchField As String = FchField(oDiari)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Month(" & sFchField & ") AS Source, GETDATE(), '' ")
        sb.AppendLine(SqlBrands2(oDiari))
        sb.AppendLine(SqlFrom(oDiari))
        sb.AppendLine(SqlWhere(oDiari, False))
        sb.AppendLine("GROUP BY Month(" & sFchField & ") ")
        sb.AppendLine("ORDER BY Month(" & sFchField & ") DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DtoDiariItem
            With oItem
                .Parent = oDiari
                .Source = oDrd("Source")
                .Level = DtoDiariItem.Levels.Mes
                .Text = oDiari.Year & " " & oDiari.Lang.Mes(CInt(oDrd("Source")))
                .Total = oDrd(Fields.Tot - 1)
                .Values = New List(Of Decimal)
                For i As Integer = 0 To oDiari.Brands.Count - 1
                    .Values.Add(oDrd(Fields.Tot + i))
                Next
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Days(oDiari As DTODiari) As List(Of DtoDiariItem)
        Dim retval As New List(Of DtoDiariItem)

        Dim sFchField As String = FchField(oDiari)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Day(" & sFchField & ") AS Source, GETDATE(), '' ")
        sb.AppendLine(SqlBrands2(oDiari))
        sb.AppendLine(SqlFrom(oDiari))
        sb.AppendLine(SqlWhere(oDiari, False, False))
        sb.AppendLine("GROUP BY Day(" & sFchField & ") ")
        sb.AppendLine("ORDER BY Day(" & sFchField & ") DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oItem As New DtoDiariItem
            With oItem
                .Parent = oDiari
                .Source = New Date(oDiari.Year, oDiari.Month, oDrd("Source"))
                .Level = DtoDiariItem.Levels.Dia
                .Text = Format(.Source.Day, "00") & " " & oDiari.Lang.WeekDay(.Source)
                .Total = oDrd(Fields.Tot - 1)
                .Values = New List(Of Decimal)
                For i As Integer = 0 To oDiari.Brands.Count - 1
                    .Values.Add(oDrd(Fields.Tot + i))
                Next
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Orders(oDiari As DTODiari) As List(Of DtoDiariItem)
        Dim retval As New List(Of DtoDiariItem)

        Dim sb As New System.Text.StringBuilder

        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
                sb.AppendLine("SELECT Pdc.Guid, Pdc.Pdc, Pdc.Src, Pdc.FchCreated, Pdc.CliGuid, CliGral.FullNom ")
            Case DTODiari.Modes.Albs
                sb.AppendLine("SELECT Alb.Guid, Alb.Alb, Alb.Fch, Alb.CliGuid, CliGral.FullNom ")
            Case DTODiari.Modes.Fras
                sb.AppendLine("SELECT Fra.Guid, Fra.Fra, Fra.Fch, Fra.CliGuid, CliGral.FullNom ")
        End Select


        sb.AppendLine(SqlBrands2(oDiari))
        sb.AppendLine(SqlFrom(oDiari))
        sb.AppendLine(SqlWhere(oDiari, False, False, False))
        sb.AppendLine("AND Month(" & FchField(oDiari) & ")=" & oDiari.Month & " ")

        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
                sb.AppendLine("GROUP BY Pdc.Guid, Pdc.Pdc, Pdc.Src, Pdc.FchCreated, Pdc.CliGuid, CliGral.FullNom ")
                sb.AppendLine("ORDER BY Pdc.FchCreated DESC, Pdc.Pdc DESC")
            Case DTODiari.Modes.Albs
                sb.AppendLine("GROUP BY Alb.Guid, Alb.Alb, Alb.Fch, Alb.CliGuid, CliGral.FullNom ")
                sb.AppendLine("ORDER BY Alb.Fch DESC, Alb.Alb DESC")
            Case DTODiari.Modes.Fras
                sb.AppendLine("GROUP BY Fra.Guid, Fra.Fra, Fra.Fch, Fra.CliGuid, CliGral.FullNom ")
                sb.AppendLine("ORDER BY Fra.Fch DESC, Fra.Fra DESC")
        End Select

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))
            With oCustomer
                .FullNom = oDrd("FullNom")
            End With

            Dim oSource As DTOBaseGuid = Nothing
            Select Case oDiari.Mode
                Case DTODiari.Modes.Pdcs
                    oSource = New DTOPurchaseOrder(oDrd("Guid"))
                    With DirectCast(oSource, DTOPurchaseOrder)
                        .Customer = oCustomer
                        .Num = oDrd("Pdc")
                        .Source = oDrd("Src")
                        .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    End With

                Case DTODiari.Modes.Albs
                    oSource = New DTOPurchaseOrder(oDrd("Guid"))
                    With DirectCast(oSource, DTODelivery)
                        .Customer = oCustomer
                        .Id = oDrd("Alb")
                        .Fch = oDrd("Fch")
                    End With
                Case DTODiari.Modes.Fras
                    oSource = New DTOInvoice(oDrd("Guid"))
                    With DirectCast(oSource, DTOInvoice)
                        .Customer = oCustomer
                        .Num = oDrd("Fra")
                        .Fch = oDrd("Fch")
                    End With
            End Select


            Dim oItem As New DtoDiariItem
            With oItem
                .Parent = oDiari
                .Source = oSource
                .Level = DtoDiariItem.Levels.Pdc
                .PurchaseOrder = oSource
                '.Text = Format(.Source.Day, "00") & " " & oDiari.Lang.WeekDay(.Source)
                .Total = oDrd(FieldsDetail.Tot)
                .Values = New List(Of Decimal)
                For i As Integer = 0 To oDiari.Brands.Count - 1
                    .Values.Add(oDrd(FieldsDetail.Tot + i + 1))
                Next
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FchField(oDiari As DTODiari) As String
        Dim retval As String = ""
        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
                retval = "Pdc.fchcreated"
            Case DTODiari.Modes.Albs, DTODiari.Modes.Fras
                retval = "Alb.Fch"
        End Select
        Return retval
    End Function

    Shared Sub LoadYears(ByRef oDiari As DTODiari)
        Dim sFchField As String = FchField(oDiari)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Year(" & sFchField & ") AS Year ")
        sb.AppendLine(SqlFrom(oDiari))
        sb.AppendLine(SqlWhere(oDiari, True))
        sb.AppendLine("GROUP BY Year(" & sFchField & ")")
        sb.AppendLine("ORDER BY Year(" & sFchField & ") DESC")

        oDiari.Years = New List(Of Integer)
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("Year")) Then
                Dim iYear As Integer = oDrd("Year")
                oDiari.Years.Add(iYear)
            End If
        Loop
        oDrd.Close()
    End Sub

    Shared Sub LoadTopBrands(ByRef oDiari As DTODiari)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP " & MaxBrands & " Tpa.Guid, BrandNom.Esp AS BrandNom ")
        sb.AppendLine(SqlFrom(oDiari))
        sb.AppendLine(SqlWhere(oDiari))
        sb.AppendLine("GROUP BY Tpa.Guid, BrandNom.Esp")

        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
                sb.AppendLine("ORDER BY SUM(Pnc.Qty * Pnc.Eur) DESC")
            Case DTODiari.Modes.Albs, DTODiari.Modes.Fras
                sb.AppendLine("ORDER BY SUM(Arc.Qty * Arc.Eur) DESC")
        End Select

        oDiari.Brands = New List(Of DTOProductBrand)
        Dim oDiversos As DTOProductBrand = Nothing
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBrand As New DTOProductBrand(DirectCast(oDrd("Guid"), Guid))
            SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
            If oBrand.Nom.Esp = "VARIOS" Then
                oDiversos = oBrand 'posposa-ho fins al final
            Else
                oDiari.Brands.Add(oBrand)
            End If
        Loop
        oDrd.Close()
        If oDiversos IsNot Nothing Then
            oDiari.Brands.Add(oDiversos)
        End If

    End Sub

    Shared Sub LoadBrands(ByRef oDiari As DTODiari)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid, BrandNom.Esp AS BrandNom")
        sb.AppendLine(SqlFrom(oDiari))
        sb.AppendLine(SqlWhere(oDiari))
        sb.AppendLine("GROUP BY Tpa.Guid, BrandNom.Esp")

        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
                sb.AppendLine("ORDER BY SUM(Pnc.Qty * Pnc.Eur) DESC")
            Case DTODiari.Modes.Albs, DTODiari.Modes.Fras
                sb.AppendLine("ORDER BY SUM(Arc.Qty * Arc.Eur) DESC")
        End Select

        oDiari.Brands = New List(Of DTOProductBrand)
        Dim oDiversos As DTOProductBrand = Nothing
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBrand As New DTOProductBrand(DirectCast(oDrd("Guid"), Guid))
            SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
            If oBrand.nom.Esp = "VARIOS" Then
                oDiversos = oBrand 'posposa-ho fins al final
            Else
                oDiari.Brands.Add(oBrand)
            End If
        Loop
        oDrd.Close()
        If oDiversos IsNot Nothing Then
            oDiari.Brands.Add(oDiversos)
        End If

    End Sub

    Shared Sub LoadItems(ByRef oDiari As DTODiari)
        oDiari.Items = New List(Of DtoDiariItem)
        Dim sSQL As String = Sql(oDiari)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(sSQL)
        Do While oDrd.Read
            Dim oItem As New DtoDiariItem
            With oItem
                .Level = DtoDiariItem.Levels.Pdc
                .Fch = oDrd("Fch")
                Select Case oDiari.Mode
                    Case DTODiari.Modes.Pdcs
                        .Text = Format(.Fch, "HH:mm") & " - " & oDrd("FullNom")
                    Case DTODiari.Modes.Albs
                        .Text = String.Format("Alb.{0:00000} {1}", oDrd("Id"), oDrd("FullNom"))
                    Case DTODiari.Modes.Fras
                        .Text = String.Format("Fra.{0:00000} {1}", oDrd("Id"), oDrd("FullNom"))
                End Select
                .Values = New List(Of Decimal)
                .Source = oDrd("Guid")
                For i As Integer = 0 To oDiari.Brands.Count - 1
                    .Values.Add(oDrd(Fields.Tpa + i + 1))
                Next
            End With
            oDiari.Items.Add(oItem)
        Loop
        oDrd.Close()
    End Sub

    Shared Sub LoadSummaries(ByRef oDiari As DTODiari)
        Dim oItems2 As New List(Of DtoDiariItem)

        Dim Index As Integer = 1
        Dim oYear As New DtoDiariItem
        oYear.Level = DtoDiariItem.Levels.Yea
        oYear.Text = oDiari.Year
        oYear.Index = Index
        oYear.Values = New List(Of Decimal)
        For Each oBrand As DTOProductBrand In oDiari.Brands
            oYear.Values.Add(0)
        Next

        Dim oMes As New DtoDiariItem
        Dim oDia As New DtoDiariItem

        oItems2.Add(oYear)
        For Each oItem As DtoDiariItem In oDiari.Items
            If oMes.Source <> oItem.Fch.Month Then
                Index += 1
                oMes = New DtoDiariItem
                oMes.Source = oItem.Fch.Month
                oMes.Level = DtoDiariItem.Levels.Mes
                oMes.Text = oDiari.Lang.Mes(oItem.Fch.Month)
                oMes.ParentIndex = oYear.Index
                oMes.Index = Index
                oMes.Values = New List(Of Decimal)
                For Each oBrand As DTOProductBrand In oDiari.Brands
                    oMes.Values.Add(0)
                Next
                oItems2.Add(oMes)
                oDia = New DtoDiariItem
            End If
            If oDia.Source <> oItem.Fch.Day Then
                Index += 1

                Dim sWeek = oDiari.Lang.WeekDay(oItem.Fch)

                oDia = New DtoDiariItem
                oDia.Source = oItem.Fch.Day
                oDia.Level = DtoDiariItem.Levels.Dia
                oDia.ParentIndex = oMes.Index
                oDia.Index = Index
                oDia.Text = Format(oItem.Fch.Day, "00") & " " & oDiari.Lang.WeekDay(oItem.Fch)
                oDia.Values = New List(Of Decimal)
                For Each oBrand As DTOProductBrand In oDiari.Brands
                    oDia.Values.Add(0)
                Next
                oItems2.Add(oDia)
            End If
            Index += 1
            oItem.ParentIndex = oDia.Index
            oItem.Index = Index
            oItems2.Add(oItem)
            For i As Integer = 0 To oDiari.Brands.Count - 1
                oYear.Values(i) += oItem.Values(i)
                oMes.Values(i) += oItem.Values(i)
                oDia.Values(i) += oItem.Values(i)
            Next
        Next

        oDiari.Items = oItems2
    End Sub

    Shared Function Sql(oDiari As DTODiari) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(SqlSelect(oDiari))
        sb.AppendLine(SqlFrom(oDiari))
        sb.AppendLine(SqlWhere(oDiari))
        sb.AppendLine(SqlGroup(oDiari))
        sb.AppendLine(SqlOrder)
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlBrands2(oDiari As DTODiari) As String
        Dim sb As New System.Text.StringBuilder

        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
                sb.AppendLine(", SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) ")
            Case DTODiari.Modes.Albs, DTODiari.Modes.Fras
                sb.AppendLine(", SUM(Arc.Qty*Arc.Eur*(100-Arc.Dto)/100) ")
        End Select

        For i As Integer = 0 To oDiari.Brands.Count - 1
            Dim oBrand As DTOProductBrand = oDiari.Brands(i)

            Select Case oDiari.Mode
                Case DTODiari.Modes.Pdcs
                    sb.AppendLine(", SUM(CASE WHEN Tpa.Guid = '" & oBrand.Guid.ToString & "' THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) ")
                Case DTODiari.Modes.Albs, DTODiari.Modes.Fras
                    sb.AppendLine(", SUM(CASE WHEN Tpa.Guid = '" & oBrand.Guid.ToString & "' THEN Arc.Qty*Arc.Eur*(100-Arc.Dto)/100 ELSE 0 END) ")
            End Select

        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlBrands(oDiari As DTODiari) As String
        Dim sb As New System.Text.StringBuilder
        For Each oBrand As DTOProductBrand In oDiari.Brands
            Select Case oDiari.Mode
                Case DTODiari.Modes.Pdcs
                    sb.AppendLine(", SUM(CASE WHEN Tpa.Guid = '" & oBrand.Guid.ToString & "' THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) ")
                Case DTODiari.Modes.Albs, DTODiari.Modes.Fras
                    sb.AppendLine(", SUM(CASE WHEN Tpa.Guid = '" & oBrand.Guid.ToString & "' THEN Arc.Qty*Arc.Eur*(100-Arc.Dto)/100 ELSE 0 END) ")
            End Select
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlSelect(oDiari As DTODiari) As String
        Dim sb As New System.Text.StringBuilder
        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
                sb.Append("SELECT Pdc.Guid, Pdc.Pdc AS Id, Pdc.FchCreated AS Fch, CliGral.FullNom ")
            Case DTODiari.Modes.Albs
                sb.Append("SELECT Alb.Guid, Alb.Alb AS Id, Alb.Fch, CliGral.FullNom ")
            Case DTODiari.Modes.Fras
                sb.Append("SELECT Fra.Guid, Fra.Fra AS Id, Fra.Fch, CliGral.FullNom ")
        End Select
        sb.Append(SqlBrands(oDiari))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlFrom(oDiari As DTODiari) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Art ON Pnc.ArtGuid = Art.Guid ")
        sb.AppendLine("INNER JOIN Stp ON Stp.Guid=Art.Category ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand=Tpa.Guid ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid=BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid=Pdc.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid=CliGral.Guid ")

        If oDiari.Channel IsNot Nothing Then
            sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
        End If

        Select Case oDiari.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Operadora
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                sb.AppendLine("INNER JOIN Email_Clis ON Pdc.CliGuid=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oDiari.User.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN Email_Clis ON Pnc.RepGuid=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oDiari.User.Guid.ToString & "' ")
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oDiari.User.Guid.ToString & "' AND Stp.Codi BETWEEN 0 AND 1 ")
            Case Else
                sb.AppendLine("AND 1=2 ")
        End Select

        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
            Case DTODiari.Modes.Albs
                sb.Append("INNER JOIN Arc ON Pnc.Guid = Arc.PncGuid ")
                sb.Append("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
            Case DTODiari.Modes.Fras
                sb.Append("INNER JOIN Arc ON Pnc.Guid = Arc.PncGuid ")
                sb.Append("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
                sb.Append("INNER JOIN Fra ON Alb.FraGuid = Fra.Guid ")
        End Select

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlWhere(oDiari As DTODiari, Optional AllYears As Boolean = False, Optional allMonths As Boolean = True, Optional AllDays As Boolean = True) As String
        Dim sFchField As String = FchField(oDiari)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("WHERE Pdc.Emp=" & oDiari.emp.Id & " ")
        sb.AppendLine("AND Pdc.Cod=" & CInt(DTOPurchaseOrder.Codis.client) & " ")
        sb.AppendLine("AND (Pnc.Bundle IS NULL OR Pnc.Bundle = Pnc.Guid) ")
        If Not AllYears Then
            sb.AppendLine("AND Year(" & sFchField & ") =" & oDiari.Year & " ")
        End If
        If Not allMonths Then
            sb.AppendLine("AND Month(" & sFchField & ") =" & oDiari.Month & " ")
        End If
        If Not AllDays Then
            sb.AppendLine("AND Day(" & sFchField & ") =" & oDiari.Day & " ")
        End If
        If oDiari.Channel IsNot Nothing Then
            sb.AppendLine("AND ContactClass.DistributionChannel = '" & oDiari.Channel.Guid.ToString & "' ")
        End If
        If oDiari.Rep IsNot Nothing Then
            sb.AppendLine("AND Pnc.RepGuid = '" & oDiari.Rep.Guid.ToString & "' ")
        End If

        Dim oRol As DTORol = Nothing
        If oDiari.Owner Is Nothing Then
            oRol = oDiari.User.Rol
            Select Case oRol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Operadora
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite, DTORol.Ids.Rep, DTORol.Ids.Comercial
                    'sb.AppendLine("AND Email_Clis.EmailGuid='" & oDiari.User.Guid.ToString & "' ")
                Case DTORol.Ids.Manufacturer
                    'sb.AppendLine("AND Email_Clis.EmailGuid='" & oDiari.User.Guid.ToString & "' ")
                    'sb.AppendLine("AND Stp.Codi BETWEEN 0 AND 1 ")
                Case Else
                    'sb.AppendLine("AND 1=2 ")
            End Select
        Else
            If oDiari.Owner IsNot Nothing Then oRol = oDiari.Owner.Rol
            If oRol Is Nothing Then oRol = oDiari.User.Rol
            Select Case oRol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Operadora
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    sb.AppendLine("AND Pdc.CliGuid='" & oDiari.Owner.Guid.ToString & "' ")
                Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                    sb.AppendLine("AND Pnc.RepGuid='" & oDiari.Owner.Guid.ToString & "' ")
                Case Else
                    sb.AppendLine("AND 1=2 ")
            End Select
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlGroup(oDiari As DTODiari) As String
        Dim sb As New System.Text.StringBuilder
        Select Case oDiari.Mode
            Case DTODiari.Modes.Pdcs
                sb.AppendLine("GROUP BY Pdc.Guid, Pdc.Pdc, Pdc.FchCreated, CliGral.FullNom ")
            Case DTODiari.Modes.Albs
                sb.AppendLine("GROUP BY Alb.Guid, Alb.Alb, Alb.Fch, CliGral.FullNom ")
            Case DTODiari.Modes.Fras
                sb.AppendLine("GROUP BY Fra.Guid, Fra.Fra, Fra.Fch, CliGral.FullNom ")
        End Select


        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SqlOrder() As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("ORDER BY Fch DESC, Id DESC")
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class
