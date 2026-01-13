

Public Class Menu_Proveidor
    Private _Proveidor As DTOProveidor
    Private _Proveidors As List(Of DTOProveidor)

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oProveidor As DTOProveidor)
        MyBase.New()
        _Proveidor = oProveidor
        _Proveidors = New List(Of DTOProveidor)
        _Proveidors.Add(_Proveidor)
    End Sub

    Public Sub New(ByVal oProveidors As List(Of DTOProveidor))
        MyBase.New()
        _Proveidors = oProveidors
        _Proveidor = _Proveidors.First
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Pdcs(),
        MenuItem_Albs(),
        MenuItem_AvisCamion_Import(),
        MenuItem_AvisCamion_NoImport(),
        MenuItem_LastRemesas(),
        MenuItem_NewRemesaImportacio(),
        MenuItem_SellOut(),
        MenuItem_Fra(),
        MenuItem_Pagos(),
        MenuItem_PriceLists(),
        MenuItem_Margins(),
        MenuItem_Forecast(),
        MenuItem_FiresLocals(),
        MenuItem_SkuDataImport(),
        MenuItem_QualityDistribution()
        })
    End Function

    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Pdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.LogisticManager, DTORol.Ids.Accounts
                oMenuItem.Text = "Comandes..."
                oMenuItem.DropDownItems.Add(SubMenuItem_NewPdc)
                oMenuItem.DropDownItems.Add(SubMenuItem_LastPdcs)
                oMenuItem.DropDownItems.Add(SubMenuItem_Pncs)
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_Albs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case Current.Session.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.LogisticManager, DTORol.Ids.Accounts
                oMenuItem.Text = "Entrades..."
                oMenuItem.DropDownItems.Add(SubMenuItem_NewAlb)
                oMenuItem.DropDownItems.Add(SubMenuItem_LastAlbs)
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function



    Private Function MenuItem_AvisCamion_Import() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.LogisticManager
                AddHandler oMenuItem.Click, AddressOf Do_AvisCamion_Import
                oMenuItem.Text = "Avis Camion (import)"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_AvisCamion_NoImport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.LogisticManager
                AddHandler oMenuItem.Click, AddressOf Do_AvisCamion_NoImport
                oMenuItem.Text = "Avis Camion (nacional)"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_LastRemesas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case Current.Session.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.LogisticManager, DTORol.Ids.Accounts
                AddHandler oMenuItem.Click, AddressOf Do_LastRemesas
                oMenuItem.Text = "remeses importacio"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_NewRemesaImportacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewImport
        oMenuItem.Text = "Nova remesa de importació"
        Return oMenuItem
    End Function

    Private Function MenuItem_SellOut() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SellOut
        oMenuItem.Text = "sell-out"
        Return oMenuItem
    End Function

    Private Function MenuItem_Fra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewFra
        oMenuItem.Text = "Entrar factura"
        oMenuItem.ShowShortcutKeys = True
        oMenuItem.ShortcutKeys = Shortcut.CtrlF
        Return oMenuItem
    End Function

    Private Function MenuItem_Pagos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Pagos
        oMenuItem.Text = "Pagar factura"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NewPdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewPdc
        oMenuItem.Text = "Nova comanda"
        Return oMenuItem
    End Function


    Private Function SubMenuItem_LastPdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastPdcs
        oMenuItem.Text = "Ultimes comandes"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Pncs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case Current.Session.User.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.LogisticManager
                AddHandler oMenuItem.Click, AddressOf Do_Pncs
                oMenuItem.Image = My.Resources.bell
                oMenuItem.Text = "Comandes pendents"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NewAlb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewAlb
        oMenuItem.Text = "Nova entrada"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_LastAlbs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastAlbs
        oMenuItem.Text = "Ultimes entrades"
        Return oMenuItem
    End Function



    Private Function MenuItem_PriceLists() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_PriceLists
        oMenuItem.Text = "Tarifes de preus"
        Return oMenuItem
    End Function


    Private Function MenuItem_Margins() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Margins
        oMenuItem.Text = "Marges comercials"
        Return oMenuItem
    End Function

    Private Function MenuItem_Forecast() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Forecast"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Forecast
        Return oMenuItem
    End Function

    Private Function MenuItem_FiresLocals() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel Fires Locals"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_ExcelFiresLocals
        Return oMenuItem
    End Function

    Private Function MenuItem_SkuDataImport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SkuDataImport
        oMenuItem.Text = "Importar dades productes"
        Return oMenuItem
    End Function

    Private Function MenuItem_QualityDistribution() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_QualityDistribution
        oMenuItem.Text = "QualityDistribution"
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_LastPdcs(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oPurchaseOrders =await FEB2.PurchaseOrders.Headers(exs, Cod:=DTOPurchaseOrder.Codis.Proveidor, Contact:=_Proveidor)
        'Dim sCaption As String = String.Format("Comandes de {0}", _Proveidor.FullNom)
        Dim oFrm As New Frm_PurchaseOrders(_Proveidor)
        oFrm.Show()
    End Sub

    Private Async Sub Do_NewPdc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, _Proveidor, DTOAlbBloqueig.Codis.PDC, exs) Then
            If FEB2.Proveidor.Load(_Proveidor, exs) Then
                Dim oPurchaseOrder = DTOPurchaseOrder.Factory(GlobalVariables.Emp, _Proveidor, Current.Session.User)
                Dim oFrm As New Frm_PurchaseOrder_Proveidor(oPurchaseOrder)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_LastAlbs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Proveidor_Entrades(_Proveidor)
        oFrm.Show()
    End Sub

    Private Async Sub Do_NewAlb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oDelivery = FEB2.Delivery.Factory(exs, _Proveidor, Current.Session.User, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oDelivery.Contact, DTOAlbBloqueig.Codis.ALB, exs) Then
                Dim oFrm As New Frm_AlbNew2(oDelivery)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Pncs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProveidorPncs(_Proveidor)
        oFrm.Show()
    End Sub


    Private Sub Do_AvisCamion_Import(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.Proveidor.Load(_Proveidor, exs) Then
            Dim oImportacio = DTOImportacio.Factory(GlobalVariables.Emp, _Proveidor)
            Dim oFrm As New Frm_Importacio(oImportacio)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Do_AvisCamion_NoImport(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Mgz_AvisCamion
        With oFrm
            .Proveidor = _Proveidor
            .Show()
        End With
    End Sub

    Private Sub Do_LastRemesas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Importacions(_Proveidor)
        oFrm.Show()
    End Sub

    Private Async Sub Do_SellOut(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB2.SellOut.Factory(exs, Current.Session.User,  , DTOSellOut.ConceptTypes.Yeas, DTOStat.Formats.Units)
        If exs.Count = 0 Then
            FEB2.SellOut.AddFilterValues(oSellout, DTOSellOut.Filter.Cods.Provider, _Proveidors.ToArray)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_NewFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Wz_Proveidor_NewFra(_Proveidor, Today)
        oFrm.Show()
    End Sub

    Private Sub Do_NewImport(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oImportacio = DTOImportacio.Factory(Current.Session.Emp, _Proveidor)
        Dim oFrm As New Frm_Importacio(oImportacio)
        oFrm.Show()
    End Sub

    Private Sub Do_Pagos(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Pagament(_Proveidor)
        oFrm.Show()
    End Sub

    Private Sub Do_PriceLists(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Proveidor_PriceLists(_Proveidor)
        oFrm.Show()
    End Sub


    Private Sub Do_Margins()
        Dim oFrm As New Frm_Margins(_Proveidor)
        oFrm.Show()
    End Sub

    Private Sub Do_Forecast(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ProductSkuForecast(_Proveidor)
        oFrm.Show()
    End Sub

    Private Sub Do_SkuDataImport()
        Dim oFrm As New Frm_SkuDetailsImport(_Proveidor)
        oFrm.Show()

        'Dim oDlg As New OpenFileDialog
        'With oDlg
        '.Title = "Importar dades de productes"
        '.Filter = "Excel (*.xlsx)|*.xlsx|tots els fitxers|*.*"
        'If .ShowDialog = DialogResult.OK Then
        'Dim oFrm As New Frm_ExcelColumsMapping({"Referencia proveidor",
        '                                      "ISO Pais",
        '                                      "Codi duaner"},
        '.FileName)
        'AddHandler oFrm.AfterUpdate, AddressOf onColumnsMapped
        'oFrm.Show()

        'End If
        'End With

    End Sub

    Private Async Sub Do_ExcelFiresLocals()
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB2.Incentiu.ExcelFiresLocals(exs, _Proveidor, Current.Session.User)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub onColumnsMapped(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = e.Argument
        Dim oCountries As List(Of DTOCountry) = Await FEB2.Countries.All(DTOLang.ESP, exs)
        Dim iNotFound As Integer
        Dim iUpdateErrors As Integer
        For Each oRow In oSheet.Rows
            Dim oSku = Await FEB2.ProductSku.FromProveidor(exs, _Proveidor, oRow.Cells.First.Content)
            If exs.Count = 0 Then
                If oSku Is Nothing Then
                    iNotFound += 1
                Else
                    If FEB2.ProductSku.Load(oSku, exs) Then
                        Dim oCountry = DTOCountry.Parse(oRow.Cells(1).Content, oCountries)
                        Dim BlProcede As Boolean
                        If oCountry IsNot Nothing Then
                            If oCountry.UnEquals(oSku.MadeIn) Then
                                oSku.MadeIn = oCountry
                                BlProcede = True
                            End If
                        End If
                        If IsNumeric(oRow.Cells(2).Content) Then
                            If oSku.CodiMercancia Is Nothing Then
                                oSku.CodiMercancia = New DTOCodiMercancia(oRow.Cells(2).Content)
                                BlProcede = True
                            ElseIf oSku.CodiMercancia.Id <> oRow.Cells(2).Content Then
                                oSku.CodiMercancia = New DTOCodiMercancia(oRow.Cells(2).Content)
                                BlProcede = True
                            End If
                        End If
                        If BlProcede Then
                            If Not Await FEB2.ProductSku.Update(oSku, exs) Then
                                iUpdateErrors += 1
                            End If
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Next

        Dim s As String = String.Format("{0} productes. {1} no trobats. {2} errors al desar.", oSheet.Rows.Count, iNotFound, iUpdateErrors)
        MsgBox(s, MsgBoxStyle.Information)
    End Sub

    Private Sub Do_QualityDistribution()
        Dim oFrm As New Frm_QualityDistribution(_Proveidor)
        oFrm.Show()
    End Sub
    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Try
            RaiseEvent AfterUpdate(sender, e)
        Catch ex As Exception
        End Try
    End Sub
End Class
