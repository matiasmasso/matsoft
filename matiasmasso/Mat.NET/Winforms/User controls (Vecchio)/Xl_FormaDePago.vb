

Public Class Xl_FormaDePago
    Private _PaymentTerms As DTOPaymentTerms
    Private _Contact As DTOContact

    Private mTipus As DTOIban.Cods

    Private mAllowEvents As Boolean
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Async Function Load(oTipus As DTOIban.Cods, oContact As DTOContact, oPaymentTerms As DTOPaymentTerms) As Task
        mTipus = oTipus
        _Contact = oContact
        _PaymentTerms = oPaymentTerms
        Await Refresca()
        mAllowEvents = True
    End Function

    Public ReadOnly Property PaymentTerms() As DTOPaymentTerms
        Get
            Return _PaymentTerms
        End Get
    End Property


    Private Async Sub Refresca(sender As Object, e As MatEventArgs)
        Await Refresca()
    End Sub

    Private Async Function Refresca() As Task

        LabelText.Text = FEB2.PaymentTerms.Text(_PaymentTerms, Current.Session.Lang)
        If _PaymentTerms IsNot Nothing Then
            If _PaymentTerms.Iban Is Nothing Then
                BankToolStripMenuItem.Enabled = False
                BranchToolStripMenuItem.Enabled = False
                MandatoToolStripMenuItem.Enabled = False
            Else
                Await Xl_Iban1.Load(_PaymentTerms.Iban)
                BankToolStripMenuItem.Enabled = True
                BranchToolStripMenuItem.Enabled = True
                MandatoToolStripMenuItem.Enabled = True

                Dim BlMissingMandatoVisible As Boolean = False
                If mTipus = DTOIban.Cods.Client Then
                    BlMissingMandatoVisible = DTOIban.IsMissingMandato(_PaymentTerms.Iban)
                    'If Not mFormaDePago.CustomerIban.HasMandatoVigent Then
                    ' Select Case mFormaDePago.Cod
                    '     Case MaxiSrvr.DTOPaymentTerms.CodsFormaDePago.DomiciliacioBancaria, MaxiSrvr.DTOPaymentTerms.CodsFormaDePago.EfteAndorra
                    ' BlMissingMandatoVisible = True
                    'End Select
                    'End If
                End If

                LabelMissingMandato.Visible = BlMissingMandatoVisible
                PictureBoxMissingMandato.Visible = BlMissingMandatoVisible
            End If

        End If
    End Function

    Private Sub ButtonEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEdit.Click
        'root.ShowFormaDePago(mFormaDePago)
        Dim oFrm As New Frm_FormaDePago(mTipus, _Contact, _PaymentTerms)
        AddHandler oFrm.AfterUpdate, AddressOf AfterUpdateFormaDePago
        oFrm.Show()
    End Sub

    Private Async Sub AfterUpdateFormaDePago(ByVal sender As System.Object, ByVal e As MatEventArgs)
        _PaymentTerms = e.Argument
        Await Refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub


    Private Sub MandatoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MandatoToolStripMenuItem.Click
        Dim oFrm As New Frm_Iban(_PaymentTerms.Iban)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub BankToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BankToolStripMenuItem.Click
        If _PaymentTerms.Iban IsNot Nothing Then
            Dim exs As New List(Of Exception)
            Dim oBank = Await FEB2.IbanStructure.GetBank(_PaymentTerms.Iban.Digits, exs)
            If exs.Count = 0 Then
                Dim oFrm As New Frm_Bank(oBank)
                AddHandler oFrm.AfterUpdate, AddressOf Refresca
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub BranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BranchToolStripMenuItem.Click
        If _PaymentTerms.Iban IsNot Nothing Then
            Dim exs As New List(Of Exception)
            Dim oBranch = Await FEB2.IbanStructure.GetBankBranch(_PaymentTerms.Iban.Digits, exs)
            If exs.Count = 0 Then
                If oBranch Is Nothing Then
                    Dim oBank = Await FEB2.IbanStructure.GetBank(_PaymentTerms.Iban.Digits, exs)
                    oBranch = DTOBankBranch.Factory(oBank)
                    oBranch.Id = DTOIban.BranchId(_PaymentTerms.Iban)
                End If
                Dim oFrm As New Frm_BankBranch(oBranch)
                AddHandler oFrm.AfterUpdate, AddressOf Refresca
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Xl_Iban1_RequestToChange(sender As Object, e As MatEventArgs) Handles Xl_Iban1.RequestToChange
        Dim oPreviousIban As DTOIban = e.Argument
        Select Case mTipus
            Case DTOIban.Cods.Client
                Dim oFrm As New Frm_Contact_Ibans(_Contact)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
                oFrm.Show()
            Case Else
                Dim oNextIban As New DTOIban()
                With oNextIban
                    .Cod = mTipus
                    .Titular = _Contact
                    .FchFrom = Today
                End With
                Dim oFrm As New Frm_IbanCcc(oNextIban, oPreviousIban)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
                oFrm.Show()
        End Select
    End Sub

    Private Async Sub RefreshIban(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oIban = Await FEB2.Iban.FromContact(exs, _Contact, mTipus)
        If exs.Count = 0 Then
            Await Xl_Iban1.Load(oIban)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Iban1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Iban1.RequestToRefresh
        RefreshIban(sender, e)
    End Sub

    Private Sub Xl_Iban1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Iban1.RequestToAddNew
        Dim oIban As New DTOIban
        With oIban
            .Cod = mTipus
            .Titular = _Contact
            .FchFrom = Today
        End With
        Select Case mTipus
            Case DTOIban.Cods.Client
                Dim oFrm As New Frm_Iban(oIban)
                AddHandler oFrm.AfterUpdate, AddressOf RefrescaIban
                oFrm.Show()
            Case Else
                Dim oFrm As New Frm_IbanCcc(oIban)
                AddHandler oFrm.AfterUpdate, AddressOf RefrescaIban
                oFrm.Show()
        End Select
    End Sub

    Private Async Sub RefrescaIban(sender As Object, e As MatEventArgs)
        Await Xl_Iban1.Load(e.Argument)
    End Sub
End Class
