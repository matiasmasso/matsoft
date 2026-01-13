Imports Microsoft.Office.Interop

Public Class Menu_ProductBrand
    Inherits Menu_Base

    Private _Brands As List(Of DTOProductBrand)

#Region "Constructors"

    Public Sub New(ByVal oBrand As DTOProductBrand, Optional ByVal oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Brands = New List(Of DTOProductBrand)
        _Brands.Add(oBrand)
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oBrands As List(Of DTOProductBrand))
        MyBase.New()
        _Brands = oBrands
        AddMenuItems()
    End Sub


#End Region



    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_LangText())
        MyBase.AddMenuItem(MenuItem_Atlas())
        MyBase.AddMenuItem(MenuItem_Wtbols())
        MyBase.AddMenuItem(MenuItem_Delete())
        MyBase.AddMenuItem(MenuItem_Sellout())
        MyBase.AddMenuItem(MenuItem_FcastFollowUp())
        MyBase.AddMenuItem(MenuItem_Tarifes())
        MyBase.AddMenuItem(MenuItem_Repeticions())
        MyBase.AddMenuItem(MenuItem_RankingCustomers())
        MyBase.AddMenuItem(MenuItem_MailReps())
        MyBase.AddMenuItem(MenuItem_RepAdrs())
        MyBase.AddMenuItem(MenuItem_Sorteos())
        MyBase.AddMenuItem(MenuItem_emailsDistribuidorsOficials())
        MyBase.AddMenuItem(MenuItem_Incidencies())
        MyBase.AddMenuItem(MenuItem_Advanced())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================


    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_LangText() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = Current.Session.Tradueix("nombres y descripciones", "noms i descripcions", "names & descriptions")
        AddHandler oMenuItem.Click, AddressOf Do_LangText
        Return oMenuItem
    End Function

    Private Function MenuItem_Atlas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Atlas"
        AddHandler oMenuItem.Click, AddressOf Do_Atlas
        Return oMenuItem
    End Function

    Private Function MenuItem_Wtbols() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "botigues online"
        oMenuItem.Enabled = _Brands.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Wtbols
        Return oMenuItem
    End Function

    Private Function MenuItem_Sellout() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sellout"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Sellout
        Return oMenuItem
    End Function

    Private Function MenuItem_FcastFollowUp() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Forecast follow Up"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_FcastFollowUp
        Return oMenuItem
    End Function

    Private Function MenuItem_Repeticions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Repeticions"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Repeticions
        Return oMenuItem
    End Function

    Private Function MenuItem_RankingCustomers() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ranking clients"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_RankingCustomers
        Return oMenuItem
    End Function

    Private Function MenuItem_Tarifes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Tarifes"
        oMenuItem.DropDownItems.Add("cost (Excel)", Nothing, AddressOf Do_ExcelTarifaCost)
        oMenuItem.DropDownItems.Add("PVP (Excel)", Nothing, AddressOf Do_ExcelTarifaPvp)
        oMenuItem.DropDownItems.Add("a una data. copiar link", Nothing, AddressOf Do_CopyTarifas)

        Return oMenuItem
    End Function



    Private Function MenuItem_MailReps() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "circular a reps"
        'oMenuItem.Image = My.Resources.SquareArrowTurquesa
        AddHandler oMenuItem.Click, AddressOf Do_MailReps
        Return oMenuItem
    End Function

    Private Function MenuItem_RepAdrs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "adreces reps"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_RepAdrs
        Return oMenuItem
    End Function

    Private Function MenuItem_Sorteos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "sortejos"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Sorteos
        Return oMenuItem
    End Function



    Private Function MenuItem_emailsDistribuidorsOficials() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "emails Distr.Oficials"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_CsvEmailsOficialDistributors
        Return oMenuItem
    End Function

    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "avanzado"
        oMenuItem.DropDownItems.Add("Url Json distribuidors", Nothing, AddressOf Do_CopyLinkUrlJsonSalePoints)
        oMenuItem.DropDownItems.Add("Url Xml distribuidors", Nothing, AddressOf Do_CopyLinkUrlXMLSalePoints)
        Return oMenuItem
    End Function


    Private Async Sub Do_ExcelTarifaCost(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oBrand = _Brands.First
        MyBase.ToggleProgressBarRequest(True)
        If oBrand.proveidor Is Nothing Then
            If FEB2.ProductBrand.Load(oBrand, exs) Then
                If oBrand.proveidor Is Nothing Then
                    UIHelper.WarnError("Aquesta marca no te assignat cap proveidor")
                    Exit Sub
                End If
            Else
                UIHelper.WarnError(exs)
                Exit Sub
            End If


            Dim otarifaVigent = Await FEB2.PriceListItemsSupplier.Vigent(exs, oBrand.proveidor)
            Dim oFilteredItems = otarifaVigent.Where(Function(x) DTOProductSku.Brand(x.Sku).Equals(oBrand)).ToList
            If exs.Count = 0 Then
                Dim oSheet = DTOPriceListSupplier.ExcelTarifaVigent(oFilteredItems)
                If Not UIHelper.ShowExcel(oSheet, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If

    End Sub

    Private Async Sub Do_ExcelTarifaPvp(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oTarifa = Await FEB2.CustomerTarifa.Load(exs, Current.Session.User)
        'TODO: extrau nomes la de la marca
        If exs.Count = 0 Then
            Dim oExcelSheet As MatHelperStd.ExcelHelper.Sheet = oTarifa.Excel(Current.Session.Lang)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyTarifas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DtFch As Date = Today
        Dim sFch As String = InputBox("tarifes a una data posterior:", "COPIAR ENLLAÇ A PAGINA DE TARIFES", DtFch.ToShortDateString)
        If IsDate(sFch) Then
            Dim sUrl = FEB2.CustomerTarifa.TarifaUrl(True, CDate(sFch))
            Clipboard.SetDataObject(sUrl)
            MsgBox("copiat " & sUrl, MsgBoxStyle.Information, "MAT.NET")
        Else
            MsgBox("format de data & '" & sFch & "' incorrecte (dd/mm/aa)", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Async Sub Do_RankingCustomers()
        Dim exs As New List(Of Exception)
        Dim oRanking = Await FEB2.Ranking.CustomerRanking(exs, Current.Session.User, _Brands.First)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Ranking(oRanking)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Function MenuItem_Incidencies() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Incidencies"
        oMenuItem.Image = My.Resources.Spv
        AddHandler oMenuItem.Click, AddressOf Do_Incidencies
        Return oMenuItem
    End Function


    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_ZoomNew(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_ProductBrand(_ProductBrand)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _Brands(0).nom.Tradueix(Current.Session.Lang) & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.ProductBrand.Delete(_Brands(0), exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar la marca")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Tpa(_Brands.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Atlas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB2.SellOut.Factory(exs, Current.Session.User, , DTOSellOut.ConceptTypes.Geo, DTOSellOut.Formats.Units)
        If exs.Count = 0 Then
            oSellout.AddFilter(DTOSellOut.Filter.Cods.Product, _Brands.ToArray)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Wtbols(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductWtbols(_Brands.First)
        oFrm.Show()
    End Sub


    Private Async Sub Do_Sellout(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB2.SellOut.Factory(exs, Current.Session.User, , DTOSellOut.ConceptTypes.Geo)
        If exs.Count = 0 Then
            Dim oValues = DTOProductBrand.ToGuidNoms(_Brands)
            oSellout.AddFilter(DTOSellOut.Filter.Cods.Product, oValues)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_FcastFollowUp()
        Dim oFrm As New Frm_ForecastFollowUp(_Brands.First)
        oFrm.Show()
    End Sub

    Private Sub Do_Incidencies()
        Dim oQuery = DTOIncidenciaQuery.Factory(Current.Session.User)
        With oQuery
            .Src = DTOIncidencia.Srcs.Producte
            .Product = _Brands.First
        End With
        Dim oFrm As New Frm_Last_Incidencies(oQuery)
        oFrm.Show()
    End Sub

    Private Sub Do_Repeticions()
        Dim oFrm As New Frm_ProductRepeticions(_Brands.First)
        oFrm.Show()
    End Sub

    Private Sub Do_Sorteos()
        'Dim oFrm As New Frm_Raffles(mTpas(0))
        'oFrm.Show()
    End Sub



    Private Async Sub Do_CsvEmailsOficialDistributors()
        Dim exs As New List(Of Exception)
        Dim oBrand As DTOProductBrand = _Brands(0)
        If FEB2.ProductBrand.Load(oBrand, exs) Then
            Dim oEmails = Await FEB2.CliProductsBlocked.DistribuidorsOficialsActiveEmails(exs, oBrand)
            If exs.Count = 0 Then
                Dim oCsv As New DTOCsv("Destinataris circular distribuidors.csv")
                For Each item As DTOEmail In oEmails
                    Dim oRow = oCsv.AddRow()
                    oRow.AddCell(item.Guid.ToString())
                    oRow.AddCell(item.EmailAddress)
                Next
                UIHelper.SaveCsvDialog(oCsv, "emails distribuidors oficials " & oBrand.nom.Tradueix(Current.Session.Lang))
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_MailReps()
        Dim exs As New List(Of Exception)
        Dim oProduct As DTOProduct = _Brands.First
        Dim oRepEmails = Await FEB2.Reps.Emails(exs, oProduct)
        If exs.Count = 0 Then
            Dim oMailMessage = DTOMailMessage.Factory("info@matiasmasso.es")
            With oMailMessage
                .Bcc = oRepEmails.Select(Function(x) x.EmailAddress).ToList
                .Subject = String.Format("Circular a la red comercial de {0}", oProduct.FullNom())
            End With

            If exs.Count = 0 Then
                If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_RepAdrs()
        Dim sb As New System.Text.StringBuilder
        Dim exs As New List(Of Exception)
        If _Brands.Count > 0 Then
            Dim oReps = Await FEB2.Reps.All(exs, _Brands.First)
            For Each oRep As DTORep In oReps
                Dim sMovil = Await FEB2.Contact.Movil(oRep, exs)
                sb.Append(FEB2.Rep.RaoSocialFacturacio(oRep))
                sb.Append(";")
                sb.Append(oRep.Address.Text)
                sb.Append(";")
                sb.Append(DTOAddress.ZipyCit(oRep.Address))
                sb.Append(";")
                sb.Append(sMovil)
                sb.AppendLine()
            Next

        End If

        If exs.Count = 0 Then
            Dim oDlg As New SaveFileDialog
            With oDlg
                .DefaultExt = "csv"
                .AddExtension = True
                .FileName = "adreces comercials " & _Brands.First.nom.Tradueix(Current.Session.Lang)
                .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                .Title = _Brands.First.nom.Tradueix(Current.Session.Lang) & ": guardar relació de noms i adreces dels comercials"
                Dim Rc As DialogResult = .ShowDialog()
                If Rc = System.Windows.Forms.DialogResult.OK Then
                    Dim sr As New IO.StreamWriter(.FileName, False, System.Text.Encoding.Default)
                    sr.WriteLine(sb.ToString())
                    sr.Close()
                End If
            End With
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Do_LangText()
        Dim oFrm As New Frm_ProductDescription(_Brands.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLinkUrlJsonSalePoints()
        Dim sUrl As String = FEB2.ProductDistributors.UrlJSON(_Brands.First, True)
        UIHelper.CopyLink(sUrl)
    End Sub

    Private Sub Do_CopyLinkUrlXMLSalePoints()
        Dim sUrl As String = FEB2.ProductDistributors.UrlXML(_Brands.First, True)
        UIHelper.CopyLink(sUrl)
    End Sub
End Class


