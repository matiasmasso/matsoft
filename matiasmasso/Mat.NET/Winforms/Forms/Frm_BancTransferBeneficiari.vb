Public Class Frm_BancTransferBeneficiari

    Private _BancTransferBeneficiari As DTOBancTransferBeneficiari
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBancTransferBeneficiari)
        MyBase.New()
        Me.InitializeComponent()
        _BancTransferBeneficiari = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _BancTransferBeneficiari
            If .Contact IsNot Nothing Then
                If .Contact.FullNom = "" Then .Contact.FullNom = .Contact.nom
                Xl_Contact21.Contact = .Contact
                Xl_LookupPgcCta1.PgcCta = .Cta
                Xl_Eur1.Amt = .Amt
                TextBoxConcept.Text = .Concepte
                Await Xl_IbanDigits1.Load(.Account, .BankBranch)

            End If
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxConcept.TextChanged,
        Xl_Eur1.AfterUpdate,
         Xl_LookupPgcCta1.AfterUpdate,
          Xl_IbanDigits1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _BancTransferBeneficiari
            .Contact = Xl_Contact21.Contact
            .Cta = Xl_LookupPgcCta1.PgcCta
            .Amt = Xl_Eur1.Amt
            .Concepte = TextBoxConcept.Text
            .Account = Xl_IbanDigits1.Value
            .BankBranch = Xl_IbanDigits1.BankBranch
        End With

        RaiseEvent AfterUpdate(Me, New MatEventArgs(_BancTransferBeneficiari))
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub Control_Changed(sender As Object, e As MatEventArgs) Handles _
        Xl_Contact21.AfterUpdate
        Dim exs As New List(Of Exception)
        Dim oIban As DTOIban = Nothing
        Dim oCtaCod As DTOPgcPlan.Ctas
        Dim oContact As DTOContact = e.Argument
        FEB2.Contact.Load(oContact, exs)
        If exs.Count = 0 Then
            Dim oCtas = Await FEB2.PgcCtas.All(exs)
            If exs.Count = 0 Then
                If oContact.rol IsNot Nothing Then
                    If oContact.rol.IsStaff Then
                        oIban = Await FEB2.Iban.FromContact(exs, oContact, DTOIban.Cods.staff)
                        oCtaCod = DTOPgcPlan.Ctas.PagasTreballadors
                    Else
                        Select Case oContact.rol.id
                            Case DTORol.Ids.Banc
                                oIban = Await FEB2.Iban.FromContact(exs, oContact, DTOIban.Cods.banc)
                                oCtaCod = DTOPgcPlan.Ctas.bancs
                            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                                oIban = Await FEB2.Iban.FromContact(exs, oContact, DTOIban.Cods.client)
                                oCtaCod = DTOPgcPlan.Ctas.Clients
                            Case Else
                                oIban = Await FEB2.Iban.FromContact(exs, oContact, DTOIban.Cods.proveidor)
                                oCtaCod = DTOPgcPlan.Ctas.ProveidorsEur
                        End Select
                    End If
                    Xl_LookupPgcCta1.PgcCta = oCtas.FirstOrDefault(Function(x) x.codi = oCtaCod)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If


        If exs.Count = 0 Then
            If oIban IsNot Nothing Then
                Await Xl_IbanDigits1.Load(oIban)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class


