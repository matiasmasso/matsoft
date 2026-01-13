

Public Class Xl_Pagaments
    Private mPlan As PgcPlan = PgcPlan.FromToday
    Private mBeneficiariNom As String
    Private mDsBancs As DataSet
    Private mDsVISAs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean

    Public Enum Fpgs
        Cash
        Visa
        Transfer
        Xec
        Efecte
    End Enum

    Public Event AfterUpdate()

    Public WriteOnly Property BeneficiariNom() As String
        Set(ByVal value As String)
            mBeneficiariNom = value
        End Set
    End Property

    Public ReadOnly Property Concepte() As String
        Get
            Return TextBoxConcepte.Text
        End Get
    End Property

    Public ReadOnly Property Cta() As PgcCta
        Get
            Dim oCta As PgcCta = Nothing
            Select Case CodiFpg
                Case Fpgs.Cash
                    oCta = mPlan.Cta(DTOPgcPlan.ctas.caixa)
                Case Fpgs.Visa
                    oCta = mPlan.Cta(DTOPgcPlan.ctas.VisasPagadas)
                Case Fpgs.Transfer, Fpgs.Xec, Fpgs.Efecte
                    oCta = mPlan.Cta(DTOPgcPlan.ctas.bancs)
            End Select
            Return oCta
        End Get
    End Property

    Public ReadOnly Property SubCta() As DTOContact
        Get
            Dim retval As DTOContact = Nothing
            Select Case CodiFpg
                Case Fpgs.Visa
                    retval = New DTOContact(Xl_LookupVisaCard1.VisaCard.Titular.Guid)
                Case Fpgs.Transfer, Fpgs.Xec, Fpgs.Efecte
                    retval = CurrentBanc()
            End Select
            Return retval
        End Get
    End Property

    Public ReadOnly Property CheckComplete() As Boolean
        Get
            Dim retval As Boolean
            If RadioButtonCash.Checked Then
                retval = True
            ElseIf RadioButtonVISA.Checked Then
                If Xl_LookupVisaCard1.VisaCard IsNot Nothing Then retval = True
            Else
                If CurrentBanc() IsNot Nothing Then
                    If RadioButtonTransfer.Checked Then retval = True
                    If RadioButtonXec.Checked And TextBoxXecNum.Text > "" Then retval = True
                    If RadioButtonEfecte.Checked Then retval = True
                End If
            End If
            Return retval
        End Get
    End Property

    Public ReadOnly Property CodiFpg() As Fpgs
        Get
            Dim retval As Fpgs
            If RadioButtonCash.Checked Then retval = Fpgs.Cash
            If RadioButtonVISA.Checked Then retval = Fpgs.Visa
            If RadioButtonTransfer.Checked Then retval = Fpgs.Transfer
            If RadioButtonXec.Checked Then retval = Fpgs.Xec
            If RadioButtonEfecte.Checked Then retval = Fpgs.Efecte
            Return retval
        End Get
    End Property


    Private Sub RadioButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    RadioButtonVISA.Click, _
    RadioButtonEfecte.Click, _
    RadioButtonXec.Click, _
    RadioButtonTransfer.Click, _
    RadioButtonCash.Click
        refresca()
        RaiseEvent AfterUpdate()
    End Sub


    Private Sub LoadBancs()
        Static BlDone As Boolean
        If BlDone Then Exit Sub
        BlDone = True

        With ComboBoxBancs
            .DisplayMember = "ABR"
            .DataSource = BLLBancs.All()
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub refresca()
        Dim s As String = ""
        If RadioButtonCash.Checked Then
            GroupBoxBanc.Visible = False
            GroupBoxVisas.Visible = False
            GroupBoxTransfer.Visible = False
            LabelConcepte.Visible = True
            TextBoxConcepte.Visible = True
            s = mBeneficiariNom & "-efectiu"
        ElseIf RadioButtonVISA.Checked Then
            GroupBoxBanc.Visible = False
            GroupBoxVisas.Visible = True
            GroupBoxTransfer.Visible = False
            GroupBoxVisas.Top = GroupBoxBanc.Top
            If Xl_LookupVisaCard1.VisaCard IsNot Nothing Then
                s = mBeneficiariNom & "-carrec " & Xl_LookupVisaCard1.VisaCard.Nom
            End If
        ElseIf RadioButtonTransfer.Checked Then
            GroupBoxBanc.Visible = False
            GroupBoxVisas.Visible = False
            GroupBoxTransfer.Visible = True
            GroupBoxVisas.Top = GroupBoxBanc.Top
            LoadBancs()
            LabelConcepte.Visible = True
            TextBoxConcepte.Visible = True
            s = CType(ComboBoxBancs.SelectedItem, DTOBanc).Abr & "-transferencia a " & mBeneficiariNom
            'Xl_IbanDigits1.Load()
        Else
            LoadBancs()
            GroupBoxBanc.Visible = True
            GroupBoxVISAs.Visible = False
            Dim oBanc As DTOBanc = CurrentBanc()
            If oBanc Is Nothing Then
                TextBoxXecNum.Text = ""
                PictureBoxBanc.Image = Nothing
                LabelXecNum.Visible = False
                TextBoxXecNum.Visible = False
                LabelConcepte.Visible = False
                TextBoxConcepte.Visible = False
            Else
                PictureBoxBanc.Image = oBanc.Logo
                LabelXecNum.Visible = RadioButtonXec.Checked
                TextBoxXecNum.Visible = RadioButtonXec.Checked
                If RadioButtonTransfer.Checked Then
                End If
                LabelConcepte.Visible = True
                TextBoxConcepte.Visible = True
                If RadioButtonTransfer.Checked Then s = oBanc.Abr & "-transferencia a " & mBeneficiariNom
                If RadioButtonXec.Checked Then s = oBanc.Abr & "-xec " & TextBoxXecNum.Text & " a " & mBeneficiariNom
                If RadioButtonEfecte.Checked Then s = oBanc.Abr & "-efecte " & mBeneficiariNom
            End If
        End If
        TextBoxConcepte.Text = s
    End Sub

    Private Function CurrentBanc() As DTOBanc
        Dim retval As DTOBanc = ComboBoxBancs.SelectedItem
        Return retval
    End Function

    Private Sub TextBoxXecNum_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxXecNum.TextChanged
        refresca()
        If CheckComplete Then RaiseEvent AfterUpdate()
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ComboBoxBancs.SelectedIndexChanged
        refresca()
        RaiseEvent AfterUpdate()
    End Sub


End Class
