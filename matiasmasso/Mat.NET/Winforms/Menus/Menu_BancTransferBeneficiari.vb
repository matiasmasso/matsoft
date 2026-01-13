Public Class Menu_BancTransferBeneficiari

    Inherits Menu_Base

    Private _BancTransferBeneficiaris As List(Of DTOBancTransferBeneficiari)
    Private _BancTransferBeneficiari As DTOBancTransferBeneficiari


    Public Sub New(ByVal oBancTransferBeneficiaris As List(Of DTOBancTransferBeneficiari))
        MyBase.New()
        _BancTransferBeneficiaris = oBancTransferBeneficiaris
        If _BancTransferBeneficiaris IsNot Nothing Then
            If _BancTransferBeneficiaris.Count > 0 Then
                _BancTransferBeneficiari = _BancTransferBeneficiaris.First
            End If
        End If
    End Sub

    Public Sub New(ByVal oBancTransferBeneficiari As DTOBancTransferBeneficiari)
        MyBase.New()
        _BancTransferBeneficiari = oBancTransferBeneficiari
        _BancTransferBeneficiaris = New List(Of DTOBancTransferBeneficiari)
        If _BancTransferBeneficiari IsNot Nothing Then
            _BancTransferBeneficiaris.Add(_BancTransferBeneficiari)
        End If
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
            MenuItem_Error(),
        MenuItem_Zoom(),
        MenuItem_Pool(),
        MenuItem_Extracte()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _BancTransferBeneficiaris.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Pool() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pool"
        oMenuItem.Enabled = _BancTransferBeneficiaris.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Pool
        Return oMenuItem
    End Function

    Private Function MenuItem_Outlook() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Notificar"
        oMenuItem.Enabled = _BancTransferBeneficiaris.Count = 1
        oMenuItem.Image = My.Resources.MailSobreGroc
        AddHandler oMenuItem.Click, AddressOf Do_Outlook
        Return oMenuItem
    End Function

    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Extracte"
        oMenuItem.Enabled = _BancTransferBeneficiaris.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function

    Private Function MenuItem_Error() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Error"
        oMenuItem.ForeColor = Color.Red

        Dim exs As New List(Of DTOIban.Exceptions)
        If FEB2.Iban.ValidateBankBranch(_BancTransferBeneficiari.BankBranch, exs) Then
            oMenuItem.Visible = False
        Else
            For Each ex As DTOIban.Exceptions In exs
                Select Case ex
                    Case DTOIban.Exceptions.MissingBankBranch
                        oMenuItem.DropDownItems.Add("seleccionar oficina bancaria", Nothing, AddressOf Do_SelectBankBranch)
                    Case DTOIban.Exceptions.MissingBankNom
                        oMenuItem.DropDownItems.Add("registrar el nom de la entitat bancaria", Nothing, AddressOf Do_ShowBanc)
                    Case DTOIban.Exceptions.MissingBIC
                        oMenuItem.DropDownItems.Add("registrar BIC", Nothing, AddressOf Do_ShowBanc)
                    Case DTOIban.Exceptions.MissingMandateDocument
                        oMenuItem.DropDownItems.Add("pujar mandat signat", Nothing, AddressOf Do_Zoom)
                    Case DTOIban.Exceptions.MissingMandateFch
                        oMenuItem.DropDownItems.Add("registrar data signatura mandat", Nothing, AddressOf Do_Zoom)
                    Case DTOIban.Exceptions.MissingBranchLocation
                        oMenuItem.DropDownItems.Add("registrar la població de la oficina bancaria", Nothing, AddressOf Do_ShowBranch)
                    Case DTOIban.Exceptions.MissingBranchAddress
                        oMenuItem.DropDownItems.Add("registrar la adreça de la oficina bancaria", Nothing, AddressOf Do_ShowBranch)
                    Case DTOIban.Exceptions.WrongDigits
                        oMenuItem.DropDownItems.Add("verificar els digits del compte", Nothing, AddressOf Do_ShowCcc)

                End Select
            Next

        End If

        Return oMenuItem
    End Function

    Private Sub Do_ShowCcc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oIban = DTOIban.Factory(_BancTransferBeneficiari.Account)
        Dim oFrm As New Frm_IbanCcc(oIban)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ShowBanc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Bank(_BancTransferBeneficiari.BankBranch.Bank)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ShowBranch(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BankBranch(_BancTransferBeneficiari.BankBranch)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_SelectBankBranch()
        Dim exs As New List(Of Exception)
        Dim oBankBranch = Await FEB2.BankBranch.FromIban(exs, _BancTransferBeneficiari.Account)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Banks(oBankBranch, Frm_Banks.Modes.SelectBranch)
            AddHandler oFrm.OnItemSelected, AddressOf OnBankBranchSelected
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub OnBankBranchSelected(sender As Object, e As MatEventArgs)
        _BancTransferBeneficiari.BankBranch = e.Argument
        RefreshRequest(Me, New MatEventArgs(_BancTransferBeneficiari))
    End Sub


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_BancTransferPool(_BancTransferBeneficiari.Parent)
        Dim oFrm As New Frm_BancTransferBeneficiari(_BancTransferBeneficiari)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Pool(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BancTransferPool(_BancTransferBeneficiari.Parent)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Outlook(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not Await MatOutlook.RemittanceAdvice(_BancTransferBeneficiari, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Extracte(_BancTransferBeneficiari.Contact, _BancTransferBeneficiari.Cta)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub




End Class

