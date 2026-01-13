Public Class Frm_ElCorteIngles
    Private _Holding As DTOHolding
    Private _TabLoaded(10) As Boolean
    Private _EciCatalog As List(Of DTO.Integracions.ElCorteIngles.Cataleg)

    Public Enum Tabs
        Companies
        Depts
        Centres
        Platforms
        Orders
        Stocks
        Cataleg
        Logs
    End Enum


    Private Async Sub Frm_ElCorteIngles_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _Holding = Await FEB.Holding.Find(exs, DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles).Guid)
        If exs.Count = 0 Then
            Await Xl_ContactsCompanies.Load(_Holding.companies)
            Await LoadCentres()
            Xl_ProgressBarEnhanced1.Visible = False
        Else
            Xl_ProgressBarEnhanced1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadCentres() As Task
        Dim oCustomers = _Holding.clusters.SelectMany(Function(x) x.Customers).ToList()
        Await Xl_Contacts1.Load(oCustomers)
    End Function

    Private Async Sub ExhauritsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExhauritsToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Xl_ProgressBarEnhanced1.Visible = True
        Dim oSheet = Await FEB.ElCorteIngles.PlantillaExhaurits(exs)
        If UIHelper.ShowExcel(oSheet, exs) Then
            Xl_ProgressBarEnhanced1.Visible = False
        Else
            Xl_ProgressBarEnhanced1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub DescatalogatsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DescatalogatsToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Xl_ProgressBarEnhanced1.Visible = True
        Dim oSheet = Await FEB.ElCorteIngles.PlantillaDescatalogats(exs)
        If UIHelper.ShowExcel(oSheet, exs) Then
            Xl_ProgressBarEnhanced1.Visible = False
        Else
            Xl_ProgressBarEnhanced1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim oTab = TabControl1.SelectedIndex
        If Not _TabLoaded(oTab) Then
            Select Case oTab
                Case Tabs.Depts
                    Await LoadDepts()
                Case Tabs.Centres
                Case Tabs.Platforms
                Case Tabs.Orders
                    Dim years As New List(Of Integer)
                    For i = DTO.GlobalVariables.Today().Year To DTO.GlobalVariables.Today().Year - 10 Step -1
                        years.Add(i)
                    Next
                    Xl_Years1.Load(years)
                    Await LoadOrders()
                Case Tabs.Stocks
                    Await LoadStocks()
                Case Tabs.Cataleg
                    Await LoadCataleg()
                Case Tabs.Logs
                    Await LoadLogs()
            End Select
            _TabLoaded(oTab) = True
        End If
    End Sub


    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await LoadOrders()
    End Sub


    Private Async Function LoadStocks() As Task
        Dim exs As New List(Of Exception)
        Xl_ProgressBarEnhanced1.Visible = True
        Dim oHolding = DTOHolding.Wellknown(DTOHolding.Wellknowns.elCorteIngles)
        Dim oInvrpt = Await FEB.InvRpts.Model(exs, oHolding, Current.Session.User, Xl_HoldingInvrpt1.Fch)
        If exs.Count = 0 Then
            Xl_HoldingInvrpt1.Load(oInvrpt, Xl_HoldingInvrpt1.Fch)
            Xl_ProgressBarEnhanced1.Visible = False
        Else
            Xl_ProgressBarEnhanced1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_HoldingInvrpt1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_HoldingInvrpt1.RequestToRefresh
        Await LoadStocks()
    End Sub


#Region "Cataleg"
    Private Async Function LoadCataleg() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _EciCatalog = Await FEB.ArtCustRefs.ElCorteIngles(exs)
        If exs.Count = 0 Then
            Dim catalogItems = _EciCatalog.OrderBy(Function(x) x.Ref).ToList()
            If CheckBoxHideObsolets.Checked Then
                catalogItems = _EciCatalog.Where(Function(x) x.FchDescatalogado Is Nothing).ToList()
            End If
            Xl_ElCorteInglesCataleg1.Load(catalogItems)
            LabelCatalegCount.Text = String.Format("Refs: {0:N0}", catalogItems.Count)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearchCataleg_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearchCataleg.AfterUpdate
        Xl_ElCorteInglesCataleg1.Filter = e.Argument
    End Sub

    Private Sub ButtonCatalegDownload_Click(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Xl_ProgressBarEnhanced1.Visible = True
        Dim oSheet As New MatHelper.Excel.Sheet("Catalogo El Corte Ingles")
        With oSheet
            .AddColumn("Guid")
            .AddColumn("Ref.ECI")
            .AddColumn("Dept")
            .AddColumn("Ref.M+O")
            .AddColumn("Ref.Fabrica")
            .AddColumn("Ean")
            .AddColumn("Marca")
            .AddColumn("Categoria")
            .AddColumn("Producto")
        End With
        For Each item In Xl_ElCorteInglesCataleg1.FilteredValues
            Dim oRow = oSheet.AddRow
            oRow.AddCell(item.Guid.ToString)
            oRow.AddCell(item.Ref)
            oRow.AddCell(item.Dept?.Nom)
            If item.Sku IsNot Nothing Then
                oRow.AddCell(item.Sku.Id)
                oRow.AddCell(item.Sku.RefProveidor)
                oRow.AddCell(DTOEan.eanValue(item.Sku.Ean13))
                oRow.AddCell(DTOProduct.BrandNom(item.Sku))
                oRow.AddCell(DTOProduct.CategoryNom(item.Sku))
                oRow.AddCell(DTOProduct.SkuNom(item.Sku))
            End If
        Next
        If UIHelper.ShowExcel(oSheet, exs) Then
            Xl_ProgressBarEnhanced1.Visible = False

        Else
            Xl_ProgressBarEnhanced1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonCatalegUpload_Click(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet As New MatHelper.Excel.Sheet
        If UIHelper.LoadExcelSheetDialog(oSheet, "Importar Excel amb el cataleg de El Corte Ingles", True) Then
            Xl_ProgressBarEnhanced1.Visible = True
            'Dim ourCatalog = Await FEB.Cache.Catalog(exs)
            If exs.Count = 0 Then
                Dim oDepts = Await FEB.ElCorteInglesDepts.All(exs)
                If exs.Count = 0 Then
                    Dim oCache = Await FEB.Cache.Fetch(exs, Current.Session.User)
                    If exs.Count = 0 Then
                        Dim oCatalegEci = CatalegEci(exs, oSheet, oCache, oDepts)
                        For Each item In oCatalegEci
                            Dim msg = String.Format("Pujant ref. '{0}'", item.Ref)
                            Xl_ProgressBarEnhanced1.ShowProgress(1, oCatalegEci.Count, oCatalegEci.IndexOf(item), msg, False)
                            If Not String.IsNullOrEmpty(item.Ref) AndAlso item.Sku IsNot Nothing Then
                                Await FEB.CustomerProduct.UpdateElCorteIngles(exs, item)
                            End If
                        Next
                        exs = New List(Of Exception)
                        Await LoadCataleg()

                        If UIHelper.ShowExcel(oSheet, exs) Then
                            Xl_ProgressBarEnhanced1.Visible = False
                        Else
                            Xl_ProgressBarEnhanced1.Visible = False
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    Xl_ProgressBarEnhanced1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Else
                Xl_ProgressBarEnhanced1.Visible = False
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Function CatalegEci(exs As List(Of Exception), oSheet As MatHelper.Excel.Sheet, oCache As Models.ClientCache, oDepts As List(Of DTO.Integracions.ElCorteIngles.Dept)) As List(Of DTO.Integracions.ElCorteIngles.Cataleg)
        Dim retval As New List(Of DTO.Integracions.ElCorteIngles.Cataleg)
        If Not oSheet.Columns.Any(Function(x) x.Header = "Guid") Then
            oSheet.Columns.Add(New MatHelper.Excel.Column("Guid", MatHelper.Excel.Cell.NumberFormats.PlainText))
            For Each oRow In oSheet.Rows
                oRow.AddCell()
            Next
        End If
        If Not oSheet.Columns.Any(Function(x) x.Header = "Errors") Then
            oSheet.Columns.Add(New MatHelper.Excel.Column("Errors", MatHelper.Excel.Cell.NumberFormats.PlainText))
            For Each oRow In oSheet.Rows
                oRow.AddCell()
            Next
        End If
        For Each oRow In oSheet.Rows
            Dim oGuid = oRow.CellGuid("Guid")
            Dim item As New DTO.Integracions.ElCorteIngles.Cataleg()
            With item
                If oGuid Is Nothing Then
                    oGuid = System.Guid.NewGuid()
                    oRow.Cells(oRow.CellIdx("Guid")).Content = oGuid.ToString()
                End If
                .Guid = oGuid
                If DTOEan.isValid(oRow.CellString("Ean").ToString()) Then
                    .Sku = oCache.FindSku(DTOEan.Factory(oRow.CellString("Ean")))
                End If
                If .Sku Is Nothing Then
                    .Sku = oCache.FindSku(oRow.CellInt("Ref.M+O").ToString().toInteger())
                End If
                If .Sku Is Nothing Then
                    .Sku = oCache.FindSkuByRefPrv(oRow.CellString("Ref.Fabrica"))
                End If
                If .Sku Is Nothing Then
                    Dim err = String.Format("producte '{0}' no trobat per l'Ean '{1}'", oRow.CellString("Ref.ECI"), oRow.CellString("Ean").ToString())
                    oRow.Cells(oRow.CellIdx("Errors")).Content = err
                Else
                    .Ref = oRow.CellString("Ref.ECI")
                    If String.IsNullOrEmpty(.Ref) Then
                        Dim err = String.Format("no s'ha trobat la columna Ref.ECI o la columna es buida")
                        oRow.Cells(oRow.CellIdx("Errors")).Content = err
                    End If
                    Dim sDept = oRow.CellString("Dept").Trim()
                    If Not String.IsNullOrEmpty(sDept) Then
                        Dim oDept = oDepts.FirstOrDefault(Function(x) x.Id = sDept)
                        If oDept IsNot Nothing Then
                            .Dept = New DTO.Models.Base.GuidNom(oDept.Guid, oDept.Nom)
                        End If
                    End If
                    retval.Add(item)
                End If
            End With
        Next
        Return retval
    End Function


    Private Sub ButtonUploadCatalogUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUploadCatalogUpdate.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar Excel Ean/dept/EciRef"
            .Filter = "fitxers Excel|*.xlsx"
            If .ShowDialog Then
                If .FileName > "" Then
                    Dim exs As New List(Of Exception)
                    If FileSystemHelper.IsFileLocked(.FileName, IO.FileMode.Open, IO.FileAccess.Read, exs) Then
                        UIHelper.WarnError(exs)
                    Else
                        Dim sFields() As String = {"Ean", "Ref.ECI", "Depto"}
                        Dim oFrm As New Frm_ExcelColumsMapping(sFields, .FileName)
                        AddHandler oFrm.AfterUpdate, AddressOf Do_ImportExcelCatalog
                        oFrm.Show()
                    End If
                End If
            End If
        End With
    End Sub

    Private Async Sub Do_ImportExcelCatalog(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oCache = Await FEB.Cache.Fetch(exs, GlobalVariables.Emp)
        Dim oDepts = Await FEB.ElCorteInglesDepts.All(exs)

        Dim oReadExceptions As New List(Of MatHelper.Excel.ReadException)
        Dim oSheet As MatHelper.Excel.Sheet = e.Argument
        Dim oExcelCatalog As New List(Of DTO.Integracions.ElCorteIngles.Cataleg)
        Dim iRow As Integer = 1
        For Each oRow As MatHelper.Excel.Row In oSheet.Rows
            Dim sEan = TextHelper.LeaveJustNumericDigits(oRow.Cells(0).Content)
            Dim sRef = TextHelper.LeaveJustNumericDigits(oRow.Cells(1).Content)
            Dim sDpt = TextHelper.LeaveJustNumericDigits(oRow.Cells(2).Content)
            If DTOEan.isValid(sEan) Then
                Dim oEan = DTOEan.Factory(sEan)
                Dim oSku = oCache.Skus.FirstOrDefault(Function(x) x.Ean13 IsNot Nothing AndAlso x.Ean13.Equals(oEan))
                If oSku Is Nothing Then
                    oReadExceptions.Add(MatHelper.Excel.ReadException.Factory(iRow, String.Format("No s'ha trobat cap article per l'Ean '{0}'", sEan)))
                Else
                    Dim item As New DTO.Integracions.ElCorteIngles.Cataleg()
                    item.Sku = New DTOProductSku(oSku.Guid)
                    item.Ref = sRef

                    Dim oDept = oDepts.FirstOrDefault(Function(x) x.Id = sDpt)
                    If oDept Is Nothing Then
                        oReadExceptions.Add(MatHelper.Excel.ReadException.Factory(iRow, String.Format("No hi ha cap departament registrat per '{0}'", sDpt)))
                    Else
                        item.Dept = New Models.Base.GuidNom(oDept.Guid, sDpt)
                    End If

                    Dim ecis = _EciCatalog.Where(Function(x) x.Ref = sRef)
                    If ecis.Count = 0 Then
                        oExcelCatalog.Add(item)
                    Else
                        oExcelCatalog.Add(item)
                    End If


                End If
            Else
                If Not String.IsNullOrEmpty(sEan) Then oReadExceptions.Add(MatHelper.Excel.ReadException.Factory(iRow, String.Format("Ean invalid '{0}'", sEan)))
            End If
            iRow += 1
        Next

        If oExcelCatalog.Count = 0 Then
            UIHelper.WarnError(New Exception("No hi ha cap article nou per afegir"))
        Else
            Dim oErrSheet As New MatHelper.Excel.Sheet("Resultats de pujar referencies del cataleg de El Corte Ingles")

            If Await FEB.ElCorteIngles.AppendToCataleg(exs, oExcelCatalog) Then
                oErrSheet.AddRowWithCells(String.Format("afegits {0} articles satisfactoriament", oExcelCatalog.Count))
            Else
                oErrSheet.AddRowWithCells("S'han trobat els següents errors:")
            End If

            For Each ex In exs
                oErrSheet.AddRowWithCells("", ex.Message)
            Next

            oErrSheet.AddRow()

            oErrSheet.AddRowWithCells("fila", "error")
            For Each Err As MatHelper.Excel.ReadException In oReadExceptions.OrderBy(Function(x) x.Row)
                oErrSheet.AddRowWithCells(Err.Row, Err.Msg)
            Next
            UIHelper.ShowExcel(oErrSheet, exs)

            Await LoadCataleg()
        End If
    End Sub

    Private Async Sub Xl_ElCorteInglesCataleg1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ElCorteInglesCataleg1.RequestToRefresh
        Await LoadCataleg()
    End Sub

    Private Sub CheckBoxHideObsolets_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHideObsolets.CheckedChanged

        If _EciCatalog IsNot Nothing Then
            Dim catalogItems = _EciCatalog.OrderBy(Function(x) x.Ref).ToList()
            If CheckBoxHideObsolets.Checked Then
                catalogItems = _EciCatalog.Where(Function(x) x.FchDescatalogado Is Nothing).ToList()
            End If
            LabelCatalegCount.Text = String.Format("Refs: {0:N0}", catalogItems.Count)
            Xl_ElCorteInglesCataleg1.Load(catalogItems)

        End If

    End Sub


#End Region


#Region "Alineamiento"

    Private Async Function LoadLogs() As Task
        Dim exs As New List(Of Exception)
        Xl_ProgressBarEnhanced1.Visible = True
        Dim logs = Await FEB.ElCorteIngles.AlineamientosDeDisponibilidad(exs)
        If exs.Count = 0 Then
            Xl_ElCorteInglesAlineamientoDisponibilidadLogs1.Load(logs)
            Xl_ProgressBarEnhanced1.Visible = False
        Else
            Xl_ProgressBarEnhanced1.Visible = False
            UIHelper.WarnError(exs)
        End If

    End Function

#End Region

    Private Async Function LoadOrders() As Task
        Dim exs As New List(Of Exception)
        Xl_ProgressBarEnhanced1.Visible = True
        Dim orders = Await FEB.ElCorteIngles.Orders(exs, Xl_Years1.Value)
        If exs.Count = 0 Then
            Dim deptos As List(Of String) = orders.Where(Function(z) Not String.IsNullOrEmpty(z.Depto)).GroupBy(Function(x) x.Depto).Select(Function(y) y.First.Depto).OrderBy(Function(o) o).ToList()
            deptos.Insert(0, "(tots)")
            ComboBoxDepts.DataSource = deptos
            Xl_ECIPurchaseOrders1.Load(orders)
            Xl_ProgressBarEnhanced1.Visible = False
        Else
            Xl_ProgressBarEnhanced1.Visible = False
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Async Sub RefrescaDepts(sender As Object, e As MatEventArgs)
        Await LoadDepts()
    End Sub

    Private Async Function LoadDepts() As Task
        Dim exs As New List(Of Exception)
        Xl_ProgressBarEnhanced1.Visible = True
        Dim oDepts = Await FEB.ElCorteInglesDepts.All(exs)
        If exs.Count = 0 Then
            Xl_ElCorteInglesDepts1.Load(oDepts)
            Xl_ProgressBarEnhanced1.Visible = False
        Else
            Xl_ProgressBarEnhanced1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub SaveFileStocksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveFileStocksToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "Fitxer de Alineamiento de Disponibilidad per El Corte Ingles"
            .FileName = "STOCK.TXT"
            .Filter = "fitxers de text|*.txt|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                Xl_ProgressBarEnhanced1.Visible = True

                Dim value = Await FEB.ElCorteIngles.AlineamientoDeDisponibilidad(exs)
                If FileSystemHelper.SaveTextToFile(value.Text, oDlg.FileName, exs) Then
                    Xl_ProgressBarEnhanced1.Visible = False
                Else
                    Xl_ProgressBarEnhanced1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Async Sub Xl_ElCorteInglesDepts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ElCorteInglesDepts1.RequestToRefresh
        Await LoadDepts()
    End Sub

    Private Sub Xl_ElCorteInglesDepts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ElCorteInglesDepts1.RequestToAddNew
        Dim value As New DTO.Integracions.ElCorteIngles.Dept
        value.Nom = "(nou departament de El Corte Ingles)"
        Dim oFrm As New Frm_ElCorteInglesDept(value)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaDepts
        oFrm.Show()
    End Sub

    Private Sub Xl_TextboxSearchOrders_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearchOrders.AfterUpdate
        Xl_ECIPurchaseOrders1.Filter = e.Argument
    End Sub

    Private Sub ComboBoxDepts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxDepts.SelectedIndexChanged
        If ComboBoxDepts.SelectedIndex = 0 Then
            Xl_ECIPurchaseOrders1.Depto = ""
        Else
            Xl_ECIPurchaseOrders1.Depto = ComboBoxDepts.SelectedItem
        End If
    End Sub
End Class