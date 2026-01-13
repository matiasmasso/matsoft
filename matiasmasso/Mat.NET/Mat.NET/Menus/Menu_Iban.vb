Public Class Menu_Iban
    Private _Iban As DTOIban

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oIban As DTOIban)
        MyBase.New()
        _Iban = oIban
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
                                         MenuItem_BrowseImg(), _
                                         MenuItem_BlankToSign(), _
                                         MenuItem_CopyRef(), _
                                         MenuItem_IbanStructure(), _
        MenuItem_Del()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.Lupa
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_BrowseImg() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Imatge"
        AddHandler oMenuItem.Click, AddressOf Do_BrowseImg
        Return oMenuItem
    End Function

    Private Function MenuItem_BlankToSign() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Mandato per signar"
        AddHandler oMenuItem.Click, AddressOf Do_BlankToSign
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyRef() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar referencia"
        AddHandler oMenuItem.Click, AddressOf Do_CopyRef
        Return oMenuItem
    End Function

    Private Function MenuItem_IbanStructure() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Format Iban"
        AddHandler oMenuItem.Click, AddressOf Do_Structure
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eliminar"
        oMenuItem.Image = My.Resources.aspa
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function





    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Iban(_Iban)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_BrowseImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = BLL.BLLIban.ImgUrl(_Iban.Digits, True)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_BlankToSign()
        If _Iban.BankBranch Is Nothing Then
            MsgBox("entitat bancaria no registrada. Codi Swift desconegut", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            if BLL.BLLIban.Swift(_Iban.BankBranch) Is Nothing Then
                MsgBox("Codi Swift no registrat", MsgBoxStyle.Exclamation, "MAT.NET")
            Else
                Dim oMandato As New SEPA_Mandato_B2B(_Iban)
                root.ShowPdf(oMandato.Pdf)
            End If
        End If
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs as New List(Of exception)
        if BLL.BLLIban.delete(_Iban, exs) Then
            RaiseEvent AfterUpdate(sender, e)
        Else
            UIHelper.WarnError( exs, "error al eliminar domiciliació " & BLL.BLLIban.Formated(_Iban))
        End If
    End Sub

    Private Sub Do_CopyRef(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(_Iban.Guid.ToString, True)
        MsgBox("referencia del mandat copiada")
    End Sub

    Private Sub Do_Structure(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCountry As DTOCountry = BLL.BLLIban.Country(_Iban)
        Dim oIbanStructure As DTOIbanStructure = BLL.BLLIbanStructure.Find(oCountry)
        Dim oFrm As New Frm_IbanStructure(oIbanStructure)
        oFrm.Show()
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class



