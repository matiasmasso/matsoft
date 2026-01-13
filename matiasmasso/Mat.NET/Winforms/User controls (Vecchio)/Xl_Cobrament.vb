

Public Class Xl_Cobrament
    Private mPagadorNom As String
    Private mDsBancs As DataSet
    Private mDsVISAs As DataSet
    Private mEmp as DTOEmp = Current.session.emp
    Private mAllowEvents As Boolean

    Public Enum Fpgs
        Cash
        Transfer
        Xec
        Pagare
    End Enum

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Async Sub SetPagadorNom(value As String)
        mPagadorNom = value
        Await Xl_IbanDigits1.Load("", Nothing) 'dispara AllowEvents
    End Sub

    Public Sub LoadPagare(sDocNum As String, DtVto As Date)
        Me.CodiFpg = Fpgs.Pagare
        TextBoxXecNum.Text = sDocNum
        DateTimePickerVto.Value = dtvto
    End Sub

    Public ReadOnly Property Concepte() As String
        Get
            Return TextBoxConcepte.Text
        End Get
    End Property

    Public ReadOnly Property Cta() As DTOPgcCta
        Get
            Dim retval As DTOPgcCta = Nothing
            Dim exs As New List(Of Exception)
            Select Case CodiFpg
                Case Fpgs.Cash
                    retval = FEB2.PgcCta.FromCodSync(DTOPgcPlan.Ctas.caixa, Current.Session.Emp, exs)
                Case Fpgs.Transfer, Fpgs.Xec, Fpgs.Pagare
                    retval = FEB2.PgcCta.FromCodSync(DTOPgcPlan.Ctas.bancs, Current.Session.Emp, exs)
            End Select
            Return retval
        End Get
    End Property

    Public ReadOnly Property SubCta() As DTOContact
        Get
            Dim oContact As DTOContact = Nothing
            Select Case CodiFpg
                Case Fpgs.Transfer, Fpgs.Xec, Fpgs.Pagare
                    oContact = CurrentBanc()
            End Select
            Return oContact
        End Get
    End Property



    Public ReadOnly Property XecNum() As String
        Get
            Return TextBoxXecNum.Text
        End Get
    End Property

    Public Property XecIBAN() As DTOIban
        Get
            Dim retval As New DTOIban
            retval.Digits = Xl_IbanDigits1.Value
            Return retval
        End Get
        Set(ByVal value As DTOIban)
            If value IsNot Nothing Then
                Xl_IbanDigits1.Load(value.digits, value.bankBranch)
            End If
        End Set
    End Property


    Public ReadOnly Property XecVto() As Date
        Get
            Return DateTimePickerVto.Value.ToShortDateString
        End Get
    End Property

    Public Function CheckComplete() As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean = False
        Select Case CodiFpg
            Case Fpgs.Cash
                retval = True
            Case Fpgs.Transfer
                retval = (CurrentBanc() IsNot Nothing)
            Case Fpgs.Xec
                Dim sDigits As String = Xl_IbanDigits1.Value
                retval = DTOIban.ValidateDigits(sDigits)
            Case Fpgs.Pagare
                retval = TextBoxXecNum.Text > ""
                Dim sDigits As String = Xl_IbanDigits1.Value
                If Not DTOIban.ValidateDigits(sDigits) Then retval = False
        End Select
        Return retval
    End Function

    Public Property CodiFpg() As Fpgs
        Get
            Dim retval As Fpgs = Fpgs.Cash
            If RadioButtonCash.Checked Then retval = Fpgs.Cash
            If RadioButtonTransfer.Checked Then retval = Fpgs.Transfer
            If RadioButtonXec.Checked Then retval = Fpgs.Xec
            If RadioButtonPagare.Checked Then retval = Fpgs.Pagare
            Return retval
        End Get
        Set(value As Fpgs)
            Select Case value
                Case Fpgs.Cash
                    RadioButtonCash.Checked = True
                Case Fpgs.Transfer
                    RadioButtonTransfer.Checked = True
                Case Fpgs.Xec
                    RadioButtonXec.Checked = True
                Case Fpgs.Pagare
                    RadioButtonPagare.Checked = True
            End Select
        End Set
    End Property


    Private Async Sub RadioButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonPagare.Click, RadioButtonXec.Click, RadioButtonTransfer.Click, RadioButtonCash.Click
        Dim exs As New List(Of Exception)
        If Await refresca(exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Function LoadBancs(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oBancs = Await FEB2.Bancs.AllActive(Current.Session.Emp, exs)
        If exs.Count = 0 Then
            With ComboBoxBancs
                .DisplayMember = "Abr"
                .DataSource = oBancs
            End With

            mAllowEvents = True
        End If
        Return (exs.Count = 0)
    End Function


    Private Async Function refresca(exs As List(Of Exception)) As Task(Of Boolean)
        Select Case CodiFpg
            Case Fpgs.Cash
                GroupBoxTransfer.Visible = False
                GroupBoxXec.Visible = False
                LabelConcepte.Visible = True
                TextBoxConcepte.Visible = True
                TextBoxConcepte.Text = mPagadorNom & "-efectiu"
            Case Fpgs.Transfer
                If ComboBoxBancs.DataSource Is Nothing Then
                    Await LoadBancs(exs)
                End If
                If exs.Count = 0 Then
                    Dim oBanc As DTOBanc = CurrentBanc()
                    GroupBoxTransfer.Visible = True
                    GroupBoxTransfer.Top = GroupBoxXec.Top
                    GroupBoxXec.Visible = False
                    If oBanc Is Nothing Then
                        LabelConcepte.Visible = False
                        TextBoxConcepte.Visible = False
                    Else
                        LabelConcepte.Visible = True
                        TextBoxConcepte.Visible = True
                        TextBoxConcepte.Text = oBanc.Abr & "-transferència " & mPagadorNom
                    End If
                End If

            Case Fpgs.Xec
                GroupBoxTransfer.Visible = False
                GroupBoxXec.Visible = True
                LabelVto.Enabled = False
                DateTimePickerVto.Enabled = False
                LabelConcepte.Visible = False
                TextBoxConcepte.Visible = False

            Case Fpgs.Pagare
                GroupBoxTransfer.Visible = False
                GroupBoxXec.Visible = True
                LabelVto.Enabled = True
                DateTimePickerVto.Enabled = True
                LabelConcepte.Visible = False
                TextBoxConcepte.Visible = False
        End Select
        Return (exs.Count = 0)
    End Function

    Private Function CurrentBanc() As DTOBanc
        Dim retval As DTOBanc = ComboBoxBancs.SelectedItem
        Return retval
    End Function


    Private Async Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     TextBoxXecNum.TextChanged
        Dim exs As New List(Of Exception)
        If Await refresca(exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxBancs.SelectedIndexChanged
        If mAllowEvents Then
            Dim exs As New List(Of Exception)
            If Await refresca(exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Xl_Cobrament_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Height = TextBoxConcepte.Top + TextBoxConcepte.Height
        Me.Width = GroupBoxTransfer.Left + GroupBoxTransfer.Width
    End Sub

    Private Sub ControlChanged(sender As Object, e As MatEventArgs) Handles _
        Xl_IbanDigits1.AfterUpdate,
         Xl_IbanDigitsTransfer.AfterUpdate,
          TextBoxConcepteBeneficiari.TextChanged

        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
