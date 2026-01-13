

Public Class Menu_Proveidor
    Private mProveidor As Proveidor
    Private _Proveidor As DTOProveidor

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oProveidor As Proveidor)
        MyBase.New()
        mProveidor = oProveidor
        _Proveidor = BLL_Proveidor.FromOldProveidor(oProveidor)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Pdcs(), _
        MenuItem_Albs(), _
        MenuItem_Pncs(), _
        MenuItem_Confirmacions(), _
        MenuItem_Previsions(), _
        MenuItem_AvisCamion_Import(), _
        MenuItem_AvisCamion_NoImport(), _
        MenuItem_LastRemesas(), _
        MenuItem_NewRemesaImportacio(), _
        MenuItem_SellOut(), _
        MenuItem_Credencials(), _
        MenuItem_Fra(), _
        MenuItem_Pagos(), _
        MenuItem_PriceLists(), _
        MenuItem_ForecastTemplate() _
        })
    End Function

    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Pdcs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.LogisticManager, Rol.Ids.Accounts
                oMenuItem.Text = "Comandes..."
                oMenuItem.DropDownItems.Add(SubMenuItem_NewPdc)
                oMenuItem.DropDownItems.Add(SubMenuItem_LastPdcs)
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_Albs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.LogisticManager
                oMenuItem.Text = "Entrades..."
                oMenuItem.DropDownItems.Add(SubMenuItem_NewAlb)
                oMenuItem.DropDownItems.Add(SubMenuItem_LastAlbs)
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_Pncs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.LogisticManager
                AddHandler oMenuItem.Click, AddressOf Do_Pncs
                oMenuItem.Image = My.Resources.bell
                oMenuItem.Text = "Comandes pendents"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_Confirmacions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.Operadora, Rol.Ids.LogisticManager, Rol.Ids.Accounts
                AddHandler oMenuItem.Click, AddressOf Do_Confirmacions
                oMenuItem.Text = "Confirmacions"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_Previsions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.Operadora, Rol.Ids.LogisticManager, Rol.Ids.Accounts
                AddHandler oMenuItem.Click, AddressOf Do_Previsions
                oMenuItem.Text = "Previsions"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_AvisCamion_Import() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.LogisticManager
                AddHandler oMenuItem.Click, AddressOf Do_AvisCamion_Import
                oMenuItem.Text = "Avis Camion (import)"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_AvisCamion_NoImport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.LogisticManager
                AddHandler oMenuItem.Click, AddressOf Do_AvisCamion_NoImport
                oMenuItem.Text = "Avis Camion (nacional)"
            Case Else
                oMenuItem.Enabled = False
        End Select
        Return oMenuItem
    End Function

    Private Function MenuItem_LastRemesas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, Rol.Ids.LogisticManager, Rol.Ids.Accounts
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
        'oMenuItem.Enabled = mProveidor.ex(DTOPurchaseOrder.Codis.proveidor)
        Return oMenuItem
    End Function

    Private Function MenuItem_Credencials() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Credencials
        oMenuItem.Text = "Credencials"
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
        oMenuItem.Enabled = mProveidor.ExistPdcs(DTOPurchaseOrder.Codis.proveidor)
        Return oMenuItem
    End Function

    Private Function SubMenuItem_NewAlb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_NewAlb
        oMenuItem.Text = "Nova entrada"
        oMenuItem.Enabled = mProveidor.ExistPncs(DTOPurchaseOrder.Codis.proveidor)
        Return oMenuItem
    End Function

    Private Function SubMenuItem_LastAlbs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastAlbs
        oMenuItem.Text = "Ultimes entrades"
        'oMenuItem.Enabled = mProveidor.ex(DTOPurchaseOrder.Codis.proveidor)
        Return oMenuItem
    End Function



    Private Function MenuItem_PriceLists() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_PriceLists
        oMenuItem.Text = "Tarifes de preus"
        'oMenuItem.Enabled = mProveidor.ex(DTOPurchaseOrder.Codis.proveidor)
        Return oMenuItem
    End Function

    Private Function MenuItem_ForecastTemplate() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ForecastTemplate
        oMenuItem.Text = "Plantilla Forecast"
        oMenuItem.Visible = mProveidor.Guid.Equals((New Roemer).Guid)
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_LastPdcs(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowProveidorPdcs(mProveidor)
    End Sub

    Private Sub Do_NewPdc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPdc As Pdc = mProveidor.NewPdc(Today, "")
        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, New DTOContact(oPdc.Client.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oFrm As New Frm_Pdc_Proveidor(oPdc)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_LastAlbs(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowProveidorAlbs(mProveidor)
    End Sub

    Private Sub Do_NewAlb(ByVal sender As Object, ByVal e As System.EventArgs)
        root.NewPrvEntrada(mProveidor)
        'MsgBox("no implementat encara")
        'root.ShowProveidorAlbNew(mProveidor)
    End Sub

    Private Sub Do_Pncs(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowProveidorPncs(mProveidor)
    End Sub

    Private Sub Do_Confirmacions(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_OrderConfirmations(mProveidor)
        oFrm.Show()
    End Sub

    Private Sub Do_Previsions(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowPrevisions(mProveidor)
    End Sub

    Private Sub Do_AvisCamion_Import(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Importacio(mProveidor.NewImportacio)
        oFrm.Show()
    End Sub

    Private Sub Do_AvisCamion_NoImport(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Mgz_AvisCamion
        With oFrm
            .Proveidor = mProveidor
            .Show()
        End With
    End Sub

    Private Sub Do_LastRemesas(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowImportacions(mProveidor)
    End Sub

    Private Sub Do_SellOut(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oStat As DTOStat = BLL.BLLStat.SellOut(_Proveidor)
        Dim oFrm As New Frm_Stats(oStat)
        oFrm.Show()
    End Sub

    Private Sub Do_NewFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Wz_Proveidor_NewFra(mProveidor, Today)
        oFrm.Show()
    End Sub

    Private Sub Do_NewImport(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oImportacio As New Importacio(mProveidor, Today)
        Dim oFrm As New Frm_Importacio(oImportacio)
        oFrm.Show()
    End Sub

    Private Sub Do_Pagos(ByVal sender As Object, ByVal e As System.EventArgs)
        WzPagament(mProveidor)
    End Sub

    Private Sub Do_PriceLists(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Proveidor_PriceLists(_Proveidor)
        oFrm.Show()
    End Sub

    Private Sub Do_ForecastTemplate(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar plantilla Forecast de Römer"
            .InitialDirectory = Roemer.PathToSuplierForecastTemplateFolder
            .FileName = "FC template Masso.xltx"
            .Filter = "Plantillas Excel (*.xltx)|*.xltx|tots els arxius|*.*"
            If .ShowDialog Then
                Dim sFolder As String = System.IO.Path.GetDirectoryName(.FileName)
                Roemer.PathToSuplierForecastTemplateFolder = sFolder
                Dim exs as New List(Of exception)
                If Not MatExcel.FillRoemerTemplate(.FileName, exs) Then
                    UIHelper.WarnError( exs, "error al importar la plantilla")
                End If
            End If
        End With
    End Sub

    Private Sub Do_Credencials()
        Dim oContact As New DTOContact(mProveidor.Guid)

        Dim oFrm As New Frm_Credencials(oContact)
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Try
            RaiseEvent AfterUpdate(sender, e)
        Catch ex As Exception
        End Try
    End Sub
End Class
