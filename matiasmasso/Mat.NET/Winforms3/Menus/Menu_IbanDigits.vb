Public Class Menu_IbanDigits
    Private _IbanDigits As String

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal sIbanDigits As String)
        MyBase.New()
        _IbanDigits = sIbanDigits
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Bank(), _
                                         MenuItem_Branch(), _
                                         MenuItem_CopyDigits(), _
                                         MenuItem_CopyFullText(), _
                                         MenuItem_CopyImg(), _
                                         MenuItem_IbanStructure() _
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Bank() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Entitat"
        oMenuItem.Enabled = _IbanDigits.Length >= 8
        AddHandler oMenuItem.Click, AddressOf Do_Bank
        Return oMenuItem
    End Function

    Private Function MenuItem_Branch() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Oficina bancària"
        oMenuItem.Enabled = _IbanDigits.Length >= 12
        AddHandler oMenuItem.Click, AddressOf Do_Branch
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyDigits() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar digits Iban"
        oMenuItem.Enabled = _IbanDigits.Length >= 12
        AddHandler oMenuItem.Click, AddressOf Do_CopyDigits
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyFullText() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar Iban complet"
        oMenuItem.Enabled = _IbanDigits.Length >= 12
        AddHandler oMenuItem.Click, AddressOf Do_CopyFullText
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyImg() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar Imatge"
        oMenuItem.Enabled = _IbanDigits.Length >= 12
        AddHandler oMenuItem.Click, AddressOf Do_CopyImg
        Return oMenuItem
    End Function

    Private Function MenuItem_IbanStructure() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Format Iban"
        AddHandler oMenuItem.Click, AddressOf Do_Structure
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Async Sub Do_Bank(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oBank As DTOBank = Await FEB.Iban.BankOrNew(_IbanDigits, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Bank(oBank)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Branch(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oBranch = Await FEB.Iban.BranchOrNew(_IbanDigits)
        Dim oFrm As New Frm_BankBranch(oBranch)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyDigits(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(_IbanDigits, True)
    End Sub

    Private Async Sub Do_CopyFullText(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oIban = DTOIban.Factory(_IbanDigits)
        Dim sText As String = Await FEB.Iban.ToMultilineString(oIban)
        Clipboard.SetDataObject(sText, True)
    End Sub

    Private Async Sub Do_CopyImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oImg = Await FEB.Iban.Img(exs, _IbanDigits, Current.Session.Lang)
        If exs.Count = 0 Then
            Clipboard.SetDataObject(oImg, True)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Structure(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCountry = Await FEB.Iban.Country(_IbanDigits, exs)
        If exs.Count = 0 Then
            Dim oIbanStructure = Await FEB.IbanStructure.Find(oCountry, exs)
            If exs.Count = 0 Then
                Dim oFrm As New Frm_IbanStructure(oIbanStructure)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
