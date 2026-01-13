

Public Class Xl_FormaDePago
    Private mFormaDePago As FormaDePago
    Private mContact As Contact
    Private mTipus As Contact.Tipus

    Private mAllowEvents As Boolean
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub LoadFromContact(ByVal oTipus As Contact.Tipus, ByVal oContact As Object, ByVal oFormadePago As FormaDePago)
        mTipus = oTipus
        mContact = oContact
        mFormaDePago = oFormadePago
        refresca(mFormaDePago, EventArgs.Empty)
        mAllowEvents = True
    End Sub

    Public ReadOnly Property FormaDePago() As FormaDePago
        Get
            Return mFormaDePago
        End Get
    End Property

    Private Sub refresca(sender As Object, e As System.EventArgs)
        If TypeOf (sender) Is FormaDePago And sender IsNot Nothing Then
            mFormaDePago = sender
        End If

        LabelText.Text = mFormaDePago.Text(BLL.BLLSession.Current.Lang)
        If mFormaDePago.Iban Is Nothing Then
            BankToolStripMenuItem.Enabled = False
            BranchToolStripMenuItem.Enabled = False
            MandatoToolStripMenuItem.Enabled = False
        Else
            Xl_Iban1.Load(mFormaDePago.Iban)
            BankToolStripMenuItem.Enabled = True
            BranchToolStripMenuItem.Enabled = True
            MandatoToolStripMenuItem.Enabled = True

            Dim BlMissingMandatoVisible As Boolean = False
            If mTipus = Contact.Tipus.Client Then
                BlMissingMandatoVisible = BLL.BLLIban.IsMissingMandato(mFormaDePago.Iban)
                'If Not mFormaDePago.CustomerIban.HasMandatoVigent Then
                ' Select Case mFormaDePago.Cod
                '     Case MaxiSrvr.DTOCustomer.FormasDePagament.DomiciliacioBancaria, MaxiSrvr.DTOCustomer.FormasDePagament.EfteAndorra
                ' BlMissingMandatoVisible = True
                'End Select
                'End If
            End If

            LabelMissingMandato.Visible = BlMissingMandatoVisible
            PictureBoxMissingMandato.Visible = BlMissingMandatoVisible
        End If
    End Sub

    Private Sub ButtonEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEdit.Click
        'root.ShowFormaDePago(mFormaDePago)
        Dim oFrm As New Frm_FormaDePago(mTipus, mContact, mFormaDePago)
        AddHandler oFrm.AfterUpdate, AddressOf AfterUpdateFormaDePago
        oFrm.Show()
    End Sub

    Private Sub AfterUpdateFormaDePago(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mFormaDePago = CType(sender, FormaDePago)
        refresca(mFormaDePago, System.EventArgs.Empty)
        RaiseEvent AfterUpdate(mFormaDePago, EventArgs.Empty)
    End Sub


    Private Sub MandatoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MandatoToolStripMenuItem.Click
        Dim oFrm As New Frm_Iban(mFormaDePago.Iban)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub BankToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BankToolStripMenuItem.Click
        If mFormaDePago.Iban IsNot Nothing Then
            Dim oBank As DTOBank = BLL.BLLIbanStructure.GetBank(mFormaDePago.Iban.Digits)
            Dim oFrm As New Frm_Bank(oBank)
            AddHandler oFrm.AfterUpdate, AddressOf refresca
            oFrm.Show()
        End If
    End Sub

    Private Sub BranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BranchToolStripMenuItem.Click
        If mFormaDePago.Iban IsNot Nothing Then
            Dim oBranch As DTOBankBranch = BLL.BLLIbanStructure.GetBankBranch(mFormaDePago.Iban.Digits)
            If oBranch Is Nothing Then
                Dim oBank As DTOBank = BLL.BLLIbanStructure.GetBank(mFormaDePago.Iban.Digits)
                oBranch = BLL.BLLBankBranch.NewBranch(oBank)
                oBranch.Id = BLL.BLLIban.BranchId(mFormaDePago.Iban)
            End If
            Dim oFrm As New Frm_BankBranch(oBranch)
            AddHandler oFrm.AfterUpdate, AddressOf refresca
            oFrm.Show()
        End If
    End Sub

    Private Sub Xl_Iban1_RequestToChange(sender As Object, e As MatEventArgs) Handles Xl_Iban1.RequestToChange
        Dim oContact As DTOContact = New DTOContact(mContact.Guid)
        Dim oFrm As New Frm_Contact_Ibans(oContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
        oFrm.Show()
    End Sub

    Private Sub RefreshIban(sender As Object, e As MatEventArgs)
        Dim oContact As DTOContact = New DTOContact(mContact.Guid)
        Dim oIban As DTOIban = BLL.BLLIban.FromContact(oContact)
        Xl_Iban1.Load(oIban)
    End Sub

    Private Sub Xl_Iban1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Iban1.RequestToRefresh
        RefreshIban(sender, e)
    End Sub
End Class
