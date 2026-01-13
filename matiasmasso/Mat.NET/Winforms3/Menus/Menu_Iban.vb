Public Class Menu_Iban
    Inherits Menu_Base

    Private _Iban As DTOIban

    Public Sub New(ByVal oIban As DTOIban)
        MyBase.New()
        _Iban = oIban
    End Sub


    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Error(),
                                         MenuItem_Titular(),
                                         MenuItem_Banc(),
                                         MenuItem_Mandat(),
                                         MenuItem_Del()})

    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Error() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Error"
        oMenuItem.ForeColor = Color.Red

        Dim exs As New List(Of DTOIban.Exceptions)
        If _Iban.Validate(exs) Then
            oMenuItem.Visible = False
        Else
            For Each ex As DTOIban.Exceptions In exs
                Select Case ex
                    Case DTOIban.Exceptions.missingBankBranch
                        oMenuItem.DropDownItems.Add("seleccionar oficina bancaria", Nothing, AddressOf Do_SelectBankBranch)
                    Case DTOIban.Exceptions.missingBankNom
                        oMenuItem.DropDownItems.Add("registrar el nom de la entitat bancaria", Nothing, AddressOf Do_ShowBanc)
                    Case DTOIban.Exceptions.missingBIC
                        oMenuItem.DropDownItems.Add("registrar BIC", Nothing, AddressOf Do_ShowBanc)
                    Case DTOIban.Exceptions.missingMandateDocument
                        oMenuItem.DropDownItems.Add("pujar mandat signat", Nothing, AddressOf Do_Zoom)
                    Case DTOIban.Exceptions.missingMandateFch
                        oMenuItem.DropDownItems.Add("registrar data signatura mandat", Nothing, AddressOf Do_Zoom)
                    Case DTOIban.Exceptions.missingBranchLocation
                        oMenuItem.DropDownItems.Add("registrar la població de la oficina bancaria", Nothing, AddressOf Do_ShowBranch)
                    Case DTOIban.Exceptions.missingBranchAddress
                        oMenuItem.DropDownItems.Add("registrar la adreça de la oficina bancaria", Nothing, AddressOf Do_ShowBranch)
                    Case DTOIban.Exceptions.wrongDigits
                        oMenuItem.DropDownItems.Add("verificar els digits del compte", Nothing, AddressOf Do_ShowCcc)

                End Select
            Next

        End If

        Return oMenuItem
    End Function

    Private Function MenuItem_Titular() As ToolStripMenuItem
        Dim exs As New List(Of Exception)

        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Titular"
        If _Iban.Titular IsNot Nothing Then
            Dim oContactMenu = FEB.ContactMenu.FindSync(exs, _Iban.Titular)
            Dim oMenuContact As New Menu_Contact(_Iban.Titular, oContactMenu)
            oMenuItem.DropDownItems.AddRange(oMenuContact.Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Banc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Compte"
        oMenuItem.DropDownItems.Add(MenuItem_Bank)
        oMenuItem.DropDownItems.Add(MenuItem_Branch)
        oMenuItem.DropDownItems.Add("Copiar digits", Nothing, AddressOf Do_CopyDigits)
        oMenuItem.DropDownItems.Add("Copiar dades bancaries complertes", Nothing, AddressOf Do_CopyIban)
        oMenuItem.DropDownItems.Add("Format Iban", Nothing, AddressOf Do_Structure)
        oMenuItem.DropDownItems.Add("Imatge", Nothing, AddressOf Do_BrowseImg)
        Return oMenuItem
    End Function

    Private Function MenuItem_Bank() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Entitat bancaria"
        If _Iban.BankBranch Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = _Iban.BankBranch.Bank IsNot Nothing
        End If
        AddHandler oMenuItem.Click, AddressOf Do_ShowBanc
        Return oMenuItem
    End Function

    Private Function MenuItem_Branch() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Oficina bancaria"
        oMenuItem.Enabled = _Iban.BankBranch IsNot Nothing
        AddHandler oMenuItem.Click, AddressOf Do_ShowBranch
        Return oMenuItem
    End Function


    Private Function MenuItem_Mandat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Mandat"
        oMenuItem.DropDownItems.Add("Zoom", Nothing, AddressOf Do_Zoom)
        oMenuItem.DropDownItems.Add("Mandato per signar", Nothing, AddressOf Do_BlankToSign)
        oMenuItem.DropDownItems.Add("Pujar mandato signat", Nothing, AddressOf Do_Upload)
        oMenuItem.DropDownItems.Add("Copiar referencia", Nothing, AddressOf Do_CopyRef)
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

    Private Sub Do_ShowCcc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_IbanCcc(_Iban)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub
    Private Sub Do_ShowBanc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Bank(_Iban.BankBranch.Bank)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ShowBranch(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BankBranch(_Iban.BankBranch)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Iban(_Iban)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_BrowseImg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl = FEB.Iban.ImgUrl(_Iban.Digits, True)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Async Sub Do_BlankToSign()
        Dim exs As New List(Of Exception)
        If FEB.Iban.Load(_Iban, exs) Then
            If _Iban.BankBranch Is Nothing Then
                MsgBox("entitat bancaria no registrada. Codi Swift desconegut", MsgBoxStyle.Exclamation, "MAT.NET")
            Else
                If FEB.Iban.Swift(_Iban.BankBranch, exs) Is Nothing Then
                    MsgBox("Codi Swift no registrat", MsgBoxStyle.Exclamation, "MAT.NET")
                Else
                    If FEB.Contact.Load(_Iban.titular, exs) Then
                        Dim sSwift = FEB.Iban.Swift(_Iban)
                        Dim oSepaTexts = Await FEB.SepaTexts.All(exs)
                        Dim oMandato = LegacyHelper.LegacyDivers.SepaMandato(exs, GlobalVariables.Emp, _Iban, sSwift, oSepaTexts, _Iban.titular.lang)
                        If Not Await UIHelper.ShowStreamAsync(exs, oMandato) Then
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Upload()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Pujar Mandato Bancari signat"
            .Filter = "Documents Pdf|*.pdf|Tots els arxius|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim exs As New List(Of Exception)
                Dim oDocfile = LegacyHelper.DocfileHelper.Factory(.FileName, exs)
                If oDocfile Is Nothing Then
                    UIHelper.WarnError(exs)
                Else
                    If Await FEB.Iban.UploadAndApprove(_Iban, oDocFile, Current.Session.User, exs) Then
                        MyBase.RefreshRequest(Me, New MatEventArgs(_Iban))
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End With
    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest Iban?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Iban.Delete(_Iban, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar domiciliació " & DTOIban.Formated(_Iban))
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_CopyRef(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(_Iban.Guid.ToString, True)
        MsgBox("referencia del mandat copiada")
    End Sub

    Private Async Sub Do_Structure(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCountry = Await FEB.Iban.Country(_Iban, exs)
        Dim oIbanStructure = Await FEB.IbanStructure.Find(oCountry, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_IbanStructure(oIbanStructure)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyDigits(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim src As String = _Iban.Digits
        UIHelper.CopyLink(src)
    End Sub

    Private Sub Do_CopyIban(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(DTOIban.Formated(_Iban.Digits))
        sb.AppendLine(DTOIban.BankNom(_Iban))
        If _Iban.BankBranch IsNot Nothing Then
            sb.AppendLine(_Iban.BankBranch.Address)
            If _Iban.BankBranch.Location IsNot Nothing Then
                sb.AppendLine(_Iban.BankBranch.Location.Nom)
            End If
        End If
        Dim src As String = sb.ToString
        UIHelper.CopyLink(src)
    End Sub

    Private Async Sub Do_SelectBankBranch()
        Dim exs As New List(Of Exception)
        Dim oFrm As Frm_Banks = Nothing

        If _Iban.IbanStructure Is Nothing OrElse _Iban.IbanStructure.BankLength = 0 Then
            _Iban.IbanStructure = Await FEB.IbanStructure.Find(Left(_Iban.Digits, 2), exs)
        End If

        If _Iban.IbanStructure IsNot Nothing Then
            Dim oBankBranch As DTOBankBranch = Await FEB.IbanStructure.GetBankBranch(_Iban, exs)
            If exs.Count = 0 Then
                If oBankBranch Is Nothing Then
                    Dim oBank = Await FEB.IbanStructure.GetBank(_Iban, exs)
                    If exs.Count = 0 Then
                        If oBank IsNot Nothing Then
                            oFrm = New Frm_Banks(oBank, Frm_Banks.Modes.SelectBranch)
                            AddHandler oFrm.OnItemSelected, AddressOf OnBankBranchSelected
                            oFrm.Show()
                            Exit Sub
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    oFrm = New Frm_Banks(oBankBranch, Frm_Banks.Modes.SelectBranch)
                    AddHandler oFrm.OnItemSelected, AddressOf OnBankBranchSelected
                    oFrm.Show()
                    Exit Sub
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If

        Dim oContact As DTOContact = _Iban.Titular
        If FEB.Contact.Load(oContact, exs) Then
            Dim oCountry As DTOCountry = DTOAddress.Country(oContact.Address)
            oFrm = New Frm_Banks(oCountry, Frm_Banks.Modes.SelectBranch)
            AddHandler oFrm.OnItemSelected, AddressOf OnBankBranchSelected
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub OnBankBranchSelected(sender As Object, e As MatEventArgs)
        _Iban.BankBranch = e.Argument
        Dim exs As New List(Of Exception)
        If Await FEB.Iban.Update(_Iban, exs) Then
            RefreshRequest(Me, New MatEventArgs(_Iban))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class



