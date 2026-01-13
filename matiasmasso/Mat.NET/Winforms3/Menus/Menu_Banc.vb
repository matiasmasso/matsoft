

Public Class Menu_Banc
    Inherits Menu_Base

    Private _Banc As DTOBanc


    Public Sub New(ByVal oBanc As DTOBanc)
        MyBase.New()
        _Banc = oBanc
    End Sub



    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(),
        MenuItem_Web(),
        MenuItem_Condicions(),
        MenuItem_Extracte(),
        MenuItem_Csas(),
        MenuItem_Saldos(),
        MenuItem_Xecs(),
        MenuItem_Pagaments(),
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
        'oMenuItem.DropDownItems.Add(SubMenuItem_Csa19New)
        oMenuItem.DropDownItems.Add(SubMenuItem_CsaSepaCore)
        If _Banc.Equals(DTOBanc.Wellknown(DTOBanc.Wellknowns.CaixaBank)) Then
            oMenuItem.DropDownItems.Add(SubMenuItem_CsaRMENew)
        End If
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



    Private Function SubMenuItem_CsaSepaCore() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_SepaCore
        oMenuItem.Text = "nova remesa al cobro (SEPA CORE)"
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


    Private Function SubMenuItem_Rebuts() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Rebuts
        oMenuItem.Text = "Rebuts"
        'oMenuItem.ShortcutKeys = Shortcut.CtrlC
        Return oMenuItem
    End Function

    Private Function MenuItem_SEPA() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "SEPA"
        With oMenuItem.DropDownItems
            .Add(SubMenuItem_MandatosTramesos)
            .Add(SubMenuItem_Traspas)
        End With
        Return oMenuItem
    End Function


    Private Function SubMenuItem_Traspas() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Traspas
        oMenuItem.Text = "Traspas entre comptes"
        Return oMenuItem
    End Function

    Private Function SubMenuItem_MandatosTramesos() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_MandatosTramesos
        oMenuItem.Text = "Mandats signats a tercers"
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Contact(_Banc)
        oFrm.Show()
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Extracte(_Banc)
        oFrm.Show()
    End Sub

    Private Sub Do_Saldos()
        Dim oFrm As New Frm_BancSdos
        oFrm.Show()
    End Sub

    Private Sub Do_Condicions()
        Dim oFrm As New Frm_BancTerms
        oFrm.Show()
    End Sub

    Private Sub Do_LastCsas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Csas(_Banc)
        oFrm.Show()
    End Sub

    Private Sub Do_Vtos(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BancVtos(_Banc)
        oFrm.Show()
    End Sub


    Private Sub Do_Traspas()
        Dim oFrm As New Frm_BancTraspas(_Banc)
        oFrm.Show()
    End Sub

    Private Sub Do_SepaCore(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CsaSepaCoreFactory(_Banc, DTOCsa.FileFormats.SepaCore)
        oFrm.Show()
    End Sub


    Private Sub Do_CsaRMENew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CsaSepaCoreFactory(_Banc, DTOCsa.FileFormats.RemesesExportacioLaCaixa)
        oFrm.Show()
    End Sub


    Private Sub Do_IngresXecs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_XecsEnCartera(_Banc)
        oFrm.Show()
    End Sub


    Private Sub Do_Rebuts(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Do_Web(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB.Contact.Load(_Banc, exs) Then
            Dim sUrl As String = _Banc.Website()
            Process.Start(sUrl)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Do_MandatosTramesos()
        Dim oFrm As New Frm_Banc_SepaMes(_Banc)
        oFrm.show()
    End Sub

End Class


