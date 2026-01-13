

Public Class Frm_Rebut

    Private mRebut As rebut
    Private mAllowEvents As Boolean
    Private mLang As DTOLang

    Public WriteOnly Property Rebut() As Rebut
        Set(ByVal value As Rebut)
            mRebut = value

            With mRebut
                mLang = .Lang
                TextBoxId.Text = .Id
                NumBox1.Amt = .Amt
                DateTimePickerFch.Value = .Fch
                DateTimePickerVto.Value = .Vto
                TextBoxConcepte.Text = .Concepte
                TextBoxIBAN.Text = .IbanDigits
                TextBoxNom.Text = .Nom
                TextBoxAdr.Text = .Adr
                TextBoxCit.Text = .Cit
            End With
            mAllowEvents = True
        End Set
    End Property

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mRebut
            .Lang = mLang
            .Id = TextBoxId.Text
            .Amt = NumBox1.Amt
            .Fch = DateTimePickerFch.Value
            .Vto = DateTimePickerVto.Value
            .Concepte = TextBoxConcepte.Text
            .IbanDigits = TextBoxIBAN.Text
            .Nom = TextBoxNom.Text
            .Adr = TextBoxAdr.Text
            .Cit = TextBoxCit.Text
        End With

        Dim oPdf As New PdfRebut(mRebut)
        root.ShowPdf(oPdf.Stream)
    End Sub

   
    Private Sub TextBoxNom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxNom.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim oContact As Contact = Finder.FindContact(BLL.BLLApp.Emp, TextBoxNom.Text)
            If Not oContact Is Nothing Then
                mLang = oContact.Lang
                Dim oAdr As Adr = oContact.Adr
                TextBoxNom.Text = oContact.Nom
                TextBoxAdr.Text = oAdr.Text
                TextBoxCit.Text = oAdr.Zip.ZipyCit
                Dim oIban As DTOIban = New Client(oContact.Guid).FormaDePago.Iban
                If oIban IsNot Nothing Then
                    TextBoxIBAN.Text = oIban.Digits
                End If
            End If
        End If
    End Sub
End Class