

Public Class Frm_Rebut

    Private _Rebut As DTORebut
    Private _AllowEvents As Boolean
    Private _Lang As DTOLang

    Public Sub New(oRebut As DTORebut)
        MyBase.New
        Me.InitializeComponent()
        _Rebut = oRebut
        _Lang = Current.Session.Lang
    End Sub

    Private Sub Frm_Rebut_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Rebut
            .Lang = If(.Lang Is Nothing, _Lang, .Lang)
            TextBoxId.Text = .Id
            Xl_AmountCur1.Amt = If(.Amt Is Nothing, DTOAmt.Factory(), .Amt)
            If .Fch = Nothing Then
                DateTimePickerFch.Value = DTO.GlobalVariables.Today()
            Else
                DateTimePickerFch.Value = .Fch
            End If
            If .Vto <> Nothing Then
                DateTimePickerVto.Value = .Vto
            End If
            TextBoxConcepte.Text = .Concepte
            TextBoxIBAN.Text = .IbanDigits
            TextBoxNom.Text = .Nom
            TextBoxAdr.Text = .Adr
            TextBoxCit.Text = .Cit
        End With
        _AllowEvents = True
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Rebut
            .Lang = _Lang
            .Id = TextBoxId.Text
            .Amt = Xl_AmountCur1.Amt
            .Fch = DateTimePickerFch.Value
            .Vto = DateTimePickerVto.Value
            .Concepte = TextBoxConcepte.Text
            .IbanDigits = TextBoxIBAN.Text
            .Nom = TextBoxNom.Text
            .Adr = TextBoxAdr.Text
            .Cit = TextBoxCit.Text
        End With

        Dim exs As New List(Of Exception)
        Dim oPdf As New LegacyHelper.PdfRebut(_Rebut)
        Dim oStream = oPdf.Stream(exs)
        If exs.Count = 0 Then
            UIHelper.ShowPdf(oStream)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub TextBoxNom_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxNom.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim exs As New List(Of Exception)
            Dim oContact = Finder.FindContact(exs, Current.Session.User, TextBoxNom.Text)
            If exs.Count = 0 Then
                If Not oContact Is Nothing Then
                    Dim oCustomer As DTOCustomer = DTOCustomer.FromContact(oContact)
                    _Lang = oCustomer.Lang
                    Dim oAddress As DTOAddress = oCustomer.Address
                    TextBoxNom.Text = oCustomer.Nom
                    TextBoxAdr.Text = oAddress.Text
                    TextBoxCit.Text = DTOAddress.ZipyCit(oAddress)
                    Dim oIban As DTOIban = oCustomer.PaymentTerms.Iban
                    If oIban IsNot Nothing Then
                        TextBoxIBAN.Text = oIban.Digits
                    End If
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
End Class