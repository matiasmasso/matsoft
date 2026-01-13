

Public Class Menu_Client
    Private mClient As Client
    Private _Customer As DTOCustomer

    Public Sub New(ByVal oClient As Client)
        MyBase.New()
        mClient = oClient
        _Customer = New DTOCustomer(oClient.Guid)
    End Sub

    Public Sub New(ByVal oCustomer As DTOCustomer)
        MyBase.New()
        _Customer = oCustomer
        mClient = New Client(_Customer.Guid)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Group(), _
        MenuItem_NewPdc(), _
        MenuItem_NewPdcOld(), _
        MenuItem_NewAlb(), _
        MenuItem_NewFra(), _
        MenuItem_Pncs(), _
        MenuItem_Hist(), _
        MenuItem_LastPdcs(), _
        MenuItem_LastAlbs(), _
        MenuItem_LastFras(), _
        MenuItem_Spvs(), _
        MenuItem_Credit(), _
        MenuItem_Stat(), _
        MenuItem_CitClis(), _
        MenuItem_Tarifas(), _
        MenuItem_ArtCustRefs(), _
        MenuItem_ExcelCataleg()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================


    Private Function MenuItem_Group() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Grup..."
        If mClient.IsGroup Then
            oMenuItem.Image = My.Resources.People_Blue
            oMenuItem.DropDownItems.AddRange(New Menu_ClientGroup(mClient.CcxOrMe).Range)
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_NewPdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewPdc
        oMenuItem.Text = "nova comanda"

        oMenuItem.ShortcutKeys = Shortcut.CtrlP ' Keys.Control | Keys.P 'root.ShortCutControlKey("P")
        Return oMenuItem
    End Function

    Private Function MenuItem_NewPdcOld() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewPdcOld
        oMenuItem.Text = "nova comanda (vell)"

        Return oMenuItem
    End Function


    Private Function MenuItem_NewAlb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewAlb
        oMenuItem.Text = "nou albará"
        oMenuItem.Enabled = mClient.ExistPncs()
        oMenuItem.ShortcutKeys = Shortcut.CtrlA
        Return oMenuItem
    End Function

    Private Function MenuItem_NewFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewFra
        oMenuItem.Text = "facturar"
        Return oMenuItem
    End Function

    Private Function MenuItem_Pncs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Pncs
        oMenuItem.Text = "pendents d'entrega"
        Return oMenuItem
    End Function

    Private Function MenuItem_Hist() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Hist
        oMenuItem.Text = "historial"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastPdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastPdcs
        oMenuItem.Text = "ultimes comandes"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastAlbs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastAlbs
        oMenuItem.Text = "ultims albarans"
        Return oMenuItem
    End Function

    Private Function MenuItem_LastFras() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastFras
        oMenuItem.Text = "ultimes factures"
        Return oMenuItem
    End Function

    Private Function MenuItem_Spvs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "reparacions"
        oMenuItem.DropDownItems.Add(MenuItem_LastSpvs)
        oMenuItem.DropDownItems.Add(MenuItem_NewSpv)
        Return oMenuItem
    End Function

    Private Function MenuItem_LastSpvs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastSpvs
        oMenuItem.Text = "reparacions"
        oMenuItem.Image = My.Resources.Spv
        oMenuItem.Enabled = mClient.ExistSpvs
        Return oMenuItem
    End Function

    Private Function MenuItem_NewSpv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewSpv
        oMenuItem.ShortcutKeys = Shortcut.CtrlR
        oMenuItem.Text = "nova reparacio"
        oMenuItem.Image = My.Resources.clip
        Return oMenuItem
    End Function

    Private Function MenuItem_Credit() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Credit
        oMenuItem.Text = "crèdit"
        Return oMenuItem
    End Function

    Private Function MenuItem_Stat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Stat
        oMenuItem.Text = "estadística"
        Return oMenuItem
    End Function


    Private Function MenuItem_CitClis() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CitClis
        oMenuItem.Text = "altres clients a la poblacio"
        Return oMenuItem
    End Function

    Private Function MenuItem_ArtCustRefs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ArtCustRefs
        oMenuItem.Text = "cataleg personalitzat"
        Return oMenuItem
    End Function

    Private Function MenuItem_ExcelCataleg() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ExcelCataleg
        oMenuItem.Text = "cataleg"
        oMenuItem.Image = My.Resources.Excel
        Return oMenuItem
    End Function

    Private Function MenuItem_Tarifas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Tarifas
        oMenuItem.Text = "tarifas"
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================


    Private Sub Do_NewPdc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.NewCustomerOrder(_Customer)
        With oOrder
            .UsrCreated = BLL.BLLSession.Current.User
        End With

        If oOrder.Customer.UnEquals(_Customer) Then
            BLL.BLLContact.Load(_Customer)
            MsgBox("Client canviat a central:" & vbCrLf & _Customer.FullNom)
        End If

        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oFrm As New Frm_PurchaseOrder(oOrder)
            oFrm.Show()
        End If

    End Sub

    Private Sub Do_NewPdcOld(ByVal sender As Object, ByVal e As System.EventArgs)
        root.NewCliPdc(mClient)
    End Sub



    Private Sub Do_NewAlb(ByVal sender As Object, ByVal e As System.EventArgs)
        root.NewCliAlbNew(mClient)
    End Sub

    Private Sub Do_NewFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAlbs As Albs = Albs.PendentsDeFacturar(mClient)
        root.ExeFacturacio(oAlbs)
    End Sub

    Private Sub Do_Pncs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact_Pncs(_Customer, DTOPurchaseOrder.Codis.Client)
        oFrm.Show()
    End Sub

    Private Sub Do_Hist(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowClientHistorial(mClient)
    End Sub

    Private Sub Do_LastPdcs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContact As DTOContact = BLL.BLLContact.Find(mClient.Guid)
        Dim oFrm As New Frm_Customer_PurchaseOrders(oContact)
        oFrm.Show()
    End Sub

    Private Sub Do_LastAlbs(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowClientAlbs(mClient)
    End Sub

    Private Sub Do_LastFras(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowClientFras(mClient)
    End Sub

    Private Sub Do_LastSpvs(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowLastSpvs(mClient)
    End Sub

    Private Sub Do_Tarifas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CustomerTarifa(_Customer)
        oFrm.Show()
    End Sub

    Private Sub Do_NewSpv(ByVal sender As Object, ByVal e As System.EventArgs)
        'root.NewSpv(mClient)
        Dim oSpv As New spv(mClient)
        oSpv.Fch = Today
        Dim oFrm As New Frm_Spv(oSpv)
        oFrm.Show()
    End Sub

    Private Sub Do_Credit(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Client_Risc(mClient)
        oFrm.Show()
    End Sub

    Private Sub Do_Stat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContact As DTOContact = BLL.BLLContact.Find(mClient.Guid)
        Dim oStat As New DTOStat
        With oStat
            .Lang = BLL.BLLApp.Lang
            .Area = oContact
            .ConceptType = DTOStat.ConceptTypes.Product
            .ExpandToLevel = 1
            .Format = DTOStat.Formats.Amounts
        End With
        Dim oFrm As New Frm_Stats(oStat)
        oFrm.Show()
        'root.ShowClientStatStps(mClient)
    End Sub

    Private Sub Do_CitClis(ByVal sender As Object, ByVal e As System.EventArgs)
        BLL.BLLContact.Load(_Customer)
        Dim oFrm As New Frm_Location_Clis(_Customer.Address.Zip.Location)
        oFrm.Show()
    End Sub

    Private Sub Do_ArtCustRefs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New frm_ArtCustRefs(mClient.CcxOrMe)
        oFrm.Show()
    End Sub

    Private Sub Do_ExcelCataleg(ByVal sender As Object, ByVal e As System.EventArgs)
        'UIHelper.ShowHtml(mClient.UrlPro & "/tarifas")
        Dim oCsv As New CsvCataleg(mClient, Today)
        Dim sFilename As String = oCsv.FileName

        Dim oDlg As New SaveFileDialog
        With oDlg
            .AddExtension = True
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            If .ShowDialog Then
                Dim oByteArray As Byte() = oCsv.toByteArray

                Dim exs as New List(Of exception)
                If Not BLL.FileSystemHelper.SaveStream(oByteArray, exs, .FileName) Then
                    UIHelper.WarnError( exs, "error al desar el document")
                End If

            End If
        End With
    End Sub



End Class
