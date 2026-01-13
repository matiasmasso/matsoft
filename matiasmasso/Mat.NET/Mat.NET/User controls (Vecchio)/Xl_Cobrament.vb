

Public Class Xl_Cobrament
    Private mPagadorNom As String
    Private mDsBancs As DataSet
    Private mDsVISAs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean

    Public Enum Fpgs
        Cash
        Transfer
        Xec
        Pagare
    End Enum

    Public Event AfterUpdate()

    Public WriteOnly Property PagadorNom() As String
        Set(ByVal value As String)
            mPagadorNom = value
            Xl_IbanDigits1.Load("") 'dispara AllowEvents
        End Set
    End Property

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

    Public ReadOnly Property Cta() As PgcCta
        Get
            Dim oCta As PgcCta = Nothing
            Dim oPlan As PgcPlan = PgcPlan.FromToday
            Select Case CodiFpg
                Case Fpgs.Cash
                    oCta = oPlan.Cta(DTOPgcPlan.ctas.caixa)
                Case Fpgs.Transfer, Fpgs.Xec, Fpgs.Pagare
                    oCta = oPlan.Cta(DTOPgcPlan.ctas.bancs)
            End Select
            Return oCta
        End Get
    End Property

    Public ReadOnly Property SubCta() As Contact
        Get
            Dim oContact As Contact = Nothing
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
            Xl_IbanDigits1.Load(value.Digits)
        End Set
    End Property


    Public ReadOnly Property XecVto() As Date
        Get
            Return DateTimePickerVto.Value.ToShortDateString
        End Get
    End Property

    Public ReadOnly Property CheckComplete() As Boolean
        Get
            Dim BlOk As Boolean = False
            Select Case CodiFpg
                Case Fpgs.Cash
                    BlOk = True
                Case Fpgs.Transfer
                    BlOk = (CurrentBanc() IsNot Nothing)
                Case Fpgs.Xec
                    BlOk = TextBoxXecNum.Text > ""
                    Dim sDigits As String = Xl_IbanDigits1.Value
                    If Not Bll.BllIban.Validated(sDigits) Then BlOk = False
                Case Fpgs.Pagare
                    BlOk = TextBoxXecNum.Text > ""
                    Dim sDigits As String = Xl_IbanDigits1.Value
                    If Not Bll.BllIban.Validated(sDigits) Then BlOk = False
            End Select
            Return BlOk
        End Get
    End Property

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


    Private Sub RadioButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonPagare.Click, RadioButtonXec.Click, RadioButtonTransfer.Click, RadioButtonCash.Click
        refresca()
        RaiseEvent AfterUpdate()
    End Sub


    Private Sub LoadBancs()
        Static BlDone As Boolean
        If BlDone Then Exit Sub
        BlDone = True

        Dim SQL As String = "SELECT CLI, ABR " _
        & "FROM CliBnc " _
        & "WHERE emp=" & App.Current.Emp.Id & " AND " _
        & "ACTIU = 1 " _
        & "ORDER BY ABR"

        mDsBancs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsBancs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow(0) = 0
        oRow(1) = "(seleccionar banc)"
        oTb.Rows.Add(oRow)

        With ComboBoxBancs
            .DisplayMember = "ABR"
            .ValueMember = "CLI"
            .DataSource = oTb
            .SelectedIndex = oTb.Rows.Count - 1
        End With
    End Sub


    Private Sub refresca()
        Select Case CodiFpg
            Case Fpgs.Cash
                GroupBoxBanc.Visible = False
                GroupBoxXec.Visible = False
                LabelConcepte.Visible = True
                TextBoxConcepte.Visible = True
                TextBoxConcepte.Text = mPagadorNom & "-efectiu"

            Case Fpgs.Transfer
                LoadBancs()
                Dim oBanc As Banc = CurrentBanc()
                GroupBoxBanc.Visible = True
                GroupBoxBanc.Top = GroupBoxXec.Top
                GroupBoxXec.Visible = False
                If oBanc Is Nothing Then
                    LabelConcepte.Visible = False
                    TextBoxConcepte.Visible = False
                Else
                    PictureBoxBanc.Image = BLL.BLLIban.Img(oBanc.Iban.Digits)
                    LabelConcepte.Visible = True
                    TextBoxConcepte.Visible = True
                    TextBoxConcepte.Text = oBanc.Abr & "-transferència " & mPagadorNom
                End If

            Case Fpgs.Xec
                GroupBoxBanc.Visible = False
                GroupBoxXec.Visible = True
                LabelVto.Enabled = False
                DateTimePickerVto.Enabled = False
                LabelConcepte.Visible = False
                TextBoxConcepte.Visible = False

            Case Fpgs.Pagare
                GroupBoxBanc.Visible = False
                GroupBoxXec.Visible = True
                LabelVto.Enabled = True
                DateTimePickerVto.Enabled = True
                LabelConcepte.Visible = False
                TextBoxConcepte.Visible = False
        End Select

    End Sub

    Private Function CurrentBanc() As Banc
        Dim oBanc As Banc = Nothing
        Dim LngId As Long = ComboBoxBancs.SelectedValue
        If LngId > 0 Then
            oBanc = MaxiSrvr.Banc.FromNum(mEmp, LngId)
        End If
        Return oBanc
    End Function


    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 _
     TextBoxXecNum.TextChanged
        refresca()
        RaiseEvent AfterUpdate()
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxBancs.SelectedIndexChanged
        refresca()
        RaiseEvent AfterUpdate()
    End Sub

    Private Sub Xl_IbanDigits1_AfterUpdate() Handles Xl_IbanDigits1.AfterUpdate
        refresca()
        RaiseEvent AfterUpdate()
    End Sub

    Private Sub Xl_Cobrament_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Height = TextBoxConcepte.Top + TextBoxConcepte.Height
        Me.Width = GroupBoxBanc.Left + GroupBoxBanc.Width
    End Sub


End Class
