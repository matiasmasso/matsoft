

Public Class Menu_Banc
    Inherits Menu_Base

    Private mBanc As Banc
    Public Event AfterImportQ43(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oBanc As Banc)
        MyBase.New()
        mBanc = oBanc
    End Sub

    Public Sub New(ByVal oBanc As DTOBanc)
        MyBase.New()
        mBanc = New Banc(oBanc.Guid)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
        MenuItem_Web(), _
        MenuItem_Condicions(), _
        MenuItem_Credencials(), _
        MenuItem_Extracte(), _
        SubMenuItem_Cuadern43(), _
        MenuItem_Csas(), _
        MenuItem_Saldos(), _
        MenuItem_Xecs(), _
        MenuItem_Pagaments(), _
        MenuItem_PoolBancari(), _
        MenuItem_SEPA()})
    End Function


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

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "web"
        oMenuItem.Image = My.Resources.iExplorer
        oMenuItem.ShortcutKeys = Shortcut.CtrlW
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_Condicions() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Condicions
        oMenuItem.Text = "Condicions"
        Return oMenuItem
    End Function

    Private Function MenuItem_Credencials() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Credencials
        oMenuItem.Text = "Credencials"
        Return oMenuItem
    End Function

    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Extracte"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function

    Private Function MenuItem_Csas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Remeses"
        oMenuItem.DropDownItems.Add(SubMenuItem_LastCsas)
        oMenuItem.DropDownItems.Add(SubMenuItem_Venciments)
        If BLL.BLLIbanStructure.GetCountryISO(mBanc.Iban.Digits) = "AD" Then
            oMenuItem.DropDownItems.Add(SubMenuItem_CsaANDNew)
        Else
            'oMenuItem.DropDownItems.Add(SubMenuItem_Csa19New)
            oMenuItem.DropDownItems.Add(SubMenuItem_Csa58New)
            If mBanc.Id = LaCaixa.Id Then
                oMenuItem.DropDownItems.Add(SubMenuItem_CsaRMENew)
            End If
        End If
        oMenuItem.DropDownItems.Add(SubMenuItem_SEPA_New)
        oMenuItem.DropDownItems.Add(SubMenuItem_Csa19Impago)
        Return oMenuItem
    End Function

    Private Function MenuItem_Saldos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "saldos"
        AddHandler oMenuItem.Click, AddressOf Do_Saldos
        Return oMenuItem
    End Function

    Private Function MenuItem_Xecs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_IngresXecs
        oMenuItem.Text = "Presentacio de xecs/pagarés"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function MenuItem_Pagaments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pagaments"
        oMenuItem.DropDownItems.Add(SubMenuItem_AutoritzarVenciments)
        oMenuItem.DropDownItems.Add(SubMenuItem_Rebuts)
        Return oMenuItem
    End Function

    Private Function SubMenuItem_LastCsas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_LastCsas
        oMenuItem.Text = "Ultimes remeses"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Venciments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Vtos
        oMenuItem.Text = "Venciments"
        oMenuItem.Image = My.Resources.bell
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Csa19New() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Csa19New
        oMenuItem.Text = "Nova Norma 19"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Csa58New() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Csa58New
        oMenuItem.Text = "nova remesa al cobro (norma 58)"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_SEPA_New() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SEPA_New
        oMenuItem.Text = "nova remesa al cobro (SEPA)"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_CsaRMENew() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CsaRMENew
        oMenuItem.Text = "Nova remesa de exportació"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_CsaANDNew() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_CsaANDNew
        oMenuItem.Text = "Nova remesa Andorra"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Csa19Impago() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Csa19Impago
        oMenuItem.Text = "Impagos Norma 19"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_AutoritzarVenciments() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_AutoritzarVenciments
        oMenuItem.Text = "Autoritzar venciments"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Rebuts() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Rebuts
        oMenuItem.Text = "Rebuts"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_Cuadern43() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ImportaCuadern43
        oMenuItem.Text = "Cuadern 43"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function MenuItem_PoolBancari() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "pool bancari"
        With oMenuItem.DropDownItems
            .Add(SubMenuItem_ShowPool)
            .Add(SubMenuItem_AddToPool)
        End With
        Return oMenuItem
    End Function

    Private Function SubMenuItem_ShowPool() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ShowPool
        oMenuItem.Text = "historic"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function SubMenuItem_AddToPool() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_AddToPool
        oMenuItem.Text = "afegir partida"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function



    Private Function MenuItem_SEPA() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "mandato SEPA"
        With oMenuItem.DropDownItems
            '.Add(SubMenuItem_MandatosTramesos)
            .Add(SubMenuItem_MandatoImportarTextes)
            .Add(SubMenuItem_MandatoExportarTextes)
            .Add(SubMenuItem_ShowSepaBancs)
        End With
        Return oMenuItem
    End Function
 

    Private Function SubMenuItem_MandatosTramesos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_MandatosTramesos
        oMenuItem.Text = "Tramesos"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_MandatoImportarTextes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Mandato_Importar_textes
        oMenuItem.Text = "Importar textes"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_MandatoExportarTextes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Mandato_Exportar_textes
        oMenuItem.Text = "exportar plantilla textes"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_ShowSepaBancs() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_ShowSepaBancs
        oMenuItem.Text = "bancs adherits a SEPA"
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowContact(mBanc)
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CliCtas(mBanc)
        oFrm.Show()
    End Sub

    Private Sub Do_Saldos()
        Dim oFrm As New frm_Banc_Saldos(mBanc.Emp)
        oFrm.show()
    End Sub

    Private Sub Do_Condicions()
        Dim oFrm As New Frm_BancTerms
        oFrm.Show()
    End Sub

    Private Sub Do_LastCsas(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCsas(mBanc)
    End Sub

    Private Sub Do_Vtos(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowBancVtos(mBanc)
    End Sub

    Private Sub Do_Csa19New(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCsa As New Csa(mBanc, DTOCsa.FileFormats.Norma19, DTO.DTOCsa.Types.AlCobro)
        Dim oFrm As New Frm_Banc_NovaRemesa(oCsa)
        oFrm.Show()
    End Sub

    Private Sub Do_Csa58New(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCsa As New Csa(mBanc, DTOCsa.FileFormats.Norma58, DTO.DTOCsa.Types.AlCobro)
        Dim oFrm As New Frm_Banc_NovaRemesa(oCsa)
        oFrm.Show()
    End Sub

    Private Sub Do_SEPA_New(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCsa As New Csa(mBanc, DTOCsa.FileFormats.SepaB2b, DTO.DTOCsa.Types.AlCobro)
        Dim oFrm As New Frm_Banc_NovaRemesa(oCsa)
        oFrm.Show()
    End Sub

    Private Sub Do_CsaRMENew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCsa As New Csa(mBanc, DTOCsa.FileFormats.RemesesExportacioLaCaixa, DTO.DTOCsa.Types.AlCobro)
        Dim oFrm As New Frm_Banc_NovaRemesa(oCsa)
        oFrm.Show()
    End Sub

    Private Sub Do_CsaANDNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCsa As New Csa(mBanc, DTOCsa.FileFormats.NormaAndorrana, DTO.DTOCsa.Types.AlCobro)
        Dim oFrm As New Frm_Banc_NovaRemesa(oCsa)
        oFrm.Show()
    End Sub

    Private Sub Do_Csa19Impago(ByVal sender As Object, ByVal e As System.EventArgs)
        root.NewBancAEB19Impag(mBanc)
    End Sub

    Private Sub Do_IngresXecs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_XecsEnCartera(mBanc)
        oFrm.Show()
    End Sub

    Private Sub Do_AutoritzarVenciments(ByVal sender As Object, ByVal e As System.EventArgs)
        ShowBancAutoritzVtos(mBanc)
    End Sub

    Private Sub Do_Rebuts(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = mBanc.WebSite()
        Process.Start("IExplore.exe", sUrl)
    End Sub

    Public Sub Do_ImportaCuadern43(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTACIO DE QUADERN 43 (EXTRACTE BANCARI)"
            .DefaultExt = ".TXT"
            .Multiselect = True
            If .ShowDialog = DialogResult.OK Then
                Dim iOk As Integer = 0
                Dim iKO As Integer = 0
                Dim iCount As Integer = 0
                Dim oAeb43 As AEB43 = Nothing
                For Each sFilename As String In .FileNames
                    iCount += 1
                    oAeb43 = New AEB43(sFilename)
                    If oAeb43.Update Then
                        iOk += 1
                    Else
                        iKO += 1
                    End If
                Next

                If iKO > 0 Then
                    MsgBox("Error al importar Quadern 43" & vbCrLf & iKO & " fitxers no importats de un total de " & iCount & " fitxers", MsgBoxStyle.Exclamation, "MAT.NET")
                Else
                    Select Case iOk
                        Case 0
                            MsgBox("No s'ha importat cap fitxer", MsgBoxStyle.Exclamation, "MAT.NET")
                        Case 1
                            MsgBox("fitxer Quadern 43 importat correctament", MsgBoxStyle.Information, "MAT-NET")
                            RaiseEvent AfterImportQ43(mBanc, EventArgs.Empty)
                        Case Else
                            MsgBox(iOk & " fitxers Quadern 43 importats correctament", MsgBoxStyle.Information, "MAT-NET")
                            RaiseEvent AfterImportQ43(mBanc, EventArgs.Empty)
                    End Select
                End If
            End If
        End With
    End Sub

    Private Sub Do_ShowPool(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BancPool
        oFrm.Show()
    End Sub

    Private Sub Do_AddToPool(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oBancPool As New BancPool
        With oBancPool
            .Banc = mBanc
            .FchFrom = Today
            .ProductCategory = BancPool.ProductCategories.NotSet
            .FchTo = Date.MinValue
        End With
        Dim oFrm As New Frm_BancPoolItem(oBancPool)
        oFrm.Show()
    End Sub

    Private Sub Do_MandatosTramesos()
        'Dim oFrm As New frm_Banc_MandatosTramesos(mBanc)
        'oFrm.show()
    End Sub

    Private Sub Do_Mandato_Importar_textes(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTACIO DE TEXTES MANDATO SEPA"
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            .DefaultExt = ".csv"
            .Multiselect = False
            If .ShowDialog = DialogResult.OK Then
                SEPA_Mandato_B2B.Importar_textos(.FileName)
            End If
        End With
    End Sub

    Private Sub Do_Mandato_Exportar_textes(ByVal sender As Object, ByVal e As System.EventArgs)
        SEPA_Mandato_B2B.Exportar_textos()
    End Sub

    Private Sub Do_ShowSepaBancs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Sepa_Bancs
        oFrm.Show()
    End Sub

    Private Sub Do_Credencials()
        Dim oBanc As New DTOBanc(mBanc.Guid)
        Dim oFrm As New Frm_Credencials(oBanc)
        oFrm.Show()
    End Sub
End Class


