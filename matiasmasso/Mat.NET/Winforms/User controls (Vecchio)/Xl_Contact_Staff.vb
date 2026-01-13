

Public Class Xl_Contact_Staff
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSegSoc As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerBorn As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerAlta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerBaixa As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxBaixa As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxAlias As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Sex1 As Xl_Sex
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelVisaCards As System.Windows.Forms.Label
    Friend WithEvents Xl_Iban1 As Xl_Iban
    Friend WithEvents Label4 As Label
    Friend WithEvents Xl_LookupStaffPos1 As Xl_LookupStaffPos
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents Xl_LookupStaffCategory1 As Xl_LookupStaffCategory
    Friend WithEvents Xl_NumSS1 As Xl_NumSS

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxSegSoc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DateTimePickerBorn = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerAlta = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DateTimePickerBaixa = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxBaixa = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Xl_Iban1 = New Winforms.Xl_Iban()
        Me.TextBoxAlias = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelVisaCards = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_LookupStaffCategory1 = New Winforms.Xl_LookupStaffCategory()
        Me.Xl_Image1 = New Winforms.Xl_Image()
        Me.Xl_LookupStaffPos1 = New Winforms.Xl_LookupStaffPos()
        Me.Xl_Sex1 = New Winforms.Xl_Sex()
        Me.Xl_NumSS1 = New Winforms.Xl_NumSS()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "S.Social:"
        '
        'TextBoxSegSoc
        '
        Me.TextBoxSegSoc.Location = New System.Drawing.Point(69, 38)
        Me.TextBoxSegSoc.Name = "TextBoxSegSoc"
        Me.TextBoxSegSoc.Size = New System.Drawing.Size(167, 20)
        Me.TextBoxSegSoc.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 157)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "neixament:"
        '
        'DateTimePickerBorn
        '
        Me.DateTimePickerBorn.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBorn.Location = New System.Drawing.Point(115, 158)
        Me.DateTimePickerBorn.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.DateTimePickerBorn.Name = "DateTimePickerBorn"
        Me.DateTimePickerBorn.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePickerBorn.TabIndex = 7
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(115, 178)
        Me.DateTimePickerAlta.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePickerAlta.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 178)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "alta:"
        '
        'DateTimePickerBaixa
        '
        Me.DateTimePickerBaixa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaixa.Location = New System.Drawing.Point(115, 199)
        Me.DateTimePickerBaixa.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.DateTimePickerBaixa.Name = "DateTimePickerBaixa"
        Me.DateTimePickerBaixa.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePickerBaixa.TabIndex = 11
        '
        'CheckBoxBaixa
        '
        Me.CheckBoxBaixa.AutoSize = True
        Me.CheckBoxBaixa.Location = New System.Drawing.Point(17, 199)
        Me.CheckBoxBaixa.Name = "CheckBoxBaixa"
        Me.CheckBoxBaixa.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxBaixa.TabIndex = 10
        Me.CheckBoxBaixa.Text = "baixa"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Xl_Iban1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 268)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(276, 101)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Transferencies"
        '
        'Xl_Iban1
        '
        Me.Xl_Iban1.Location = New System.Drawing.Point(17, 33)
        Me.Xl_Iban1.Name = "Xl_Iban1"
        Me.Xl_Iban1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_Iban1.TabIndex = 0
        '
        'TextBoxAlias
        '
        Me.TextBoxAlias.Location = New System.Drawing.Point(69, 12)
        Me.TextBoxAlias.Name = "TextBoxAlias"
        Me.TextBoxAlias.Size = New System.Drawing.Size(167, 20)
        Me.TextBoxAlias.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "alias"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "categoría"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 385)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Tarja de crèdit"
        '
        'LabelVisaCards
        '
        Me.LabelVisaCards.AutoSize = True
        Me.LabelVisaCards.Location = New System.Drawing.Point(26, 403)
        Me.LabelVisaCards.Name = "LabelVisaCards"
        Me.LabelVisaCards.Size = New System.Drawing.Size(176, 13)
        Me.LabelVisaCards.TabIndex = 17
        Me.LabelVisaCards.Text = "(no té assignada cap tarja de crèdit)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 121)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "carrec"
        '
        'Xl_LookupStaffCategory1
        '
        Me.Xl_LookupStaffCategory1.IsDirty = False
        Me.Xl_LookupStaffCategory1.Location = New System.Drawing.Point(69, 96)
        Me.Xl_LookupStaffCategory1.Name = "Xl_LookupStaffCategory1"
        Me.Xl_LookupStaffCategory1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupStaffCategory1.ReadOnlyLookup = False
        Me.Xl_LookupStaffCategory1.Size = New System.Drawing.Size(167, 20)
        Me.Xl_LookupStaffCategory1.TabIndex = 21
        Me.Xl_LookupStaffCategory1.Value = Nothing
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(252, 0)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(222, 262)
        Me.Xl_Image1.TabIndex = 20
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Xl_LookupStaffPos1
        '
        Me.Xl_LookupStaffPos1.IsDirty = False
        Me.Xl_LookupStaffPos1.Location = New System.Drawing.Point(69, 121)
        Me.Xl_LookupStaffPos1.Name = "Xl_LookupStaffPos1"
        Me.Xl_LookupStaffPos1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupStaffPos1.ReadOnlyLookup = False
        Me.Xl_LookupStaffPos1.Size = New System.Drawing.Size(167, 20)
        Me.Xl_LookupStaffPos1.StaffPos = Nothing
        Me.Xl_LookupStaffPos1.TabIndex = 19
        Me.Xl_LookupStaffPos1.Value = Nothing
        '
        'Xl_Sex1
        '
        Me.Xl_Sex1.Location = New System.Drawing.Point(191, 223)
        Me.Xl_Sex1.Name = "Xl_Sex1"
        Me.Xl_Sex1.Sex = DTOUser.Sexs.NotSet
        Me.Xl_Sex1.Size = New System.Drawing.Size(45, 45)
        Me.Xl_Sex1.TabIndex = 14
        '
        'Xl_NumSS1
        '
        Me.Xl_NumSS1.Location = New System.Drawing.Point(69, 64)
        Me.Xl_NumSS1.Name = "Xl_NumSS1"
        Me.Xl_NumSS1.Size = New System.Drawing.Size(167, 20)
        Me.Xl_NumSS1.TabIndex = 22
        '
        'Xl_Contact_Staff
        '
        Me.Controls.Add(Me.Xl_NumSS1)
        Me.Controls.Add(Me.Xl_LookupStaffCategory1)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.Xl_LookupStaffPos1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LabelVisaCards)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Sex1)
        Me.Controls.Add(Me.TextBoxAlias)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.CheckBoxBaixa)
        Me.Controls.Add(Me.DateTimePickerBaixa)
        Me.Controls.Add(Me.DateTimePickerAlta)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.DateTimePickerBorn)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxSegSoc)
        Me.Controls.Add(Me.Label6)
        Me.Name = "Xl_Contact_Staff"
        Me.Size = New System.Drawing.Size(477, 435)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _Staff As DTOStaff = Nothing
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Async Function Load(value As DTOStaff) As Task
        Dim exs As New List(Of Exception)
        _Staff = value
        With _Staff
            TextBoxAlias.Text = .Abr
            TextBoxSegSoc.Text = .NumSs
            Xl_NumSS1.Load(.NumSs)
            Xl_Sex1.Sex = .Sex
            Xl_LookupStaffCategory1.Load(.Category)
            Xl_LookupStaffPos1.StaffPos = .StaffPos
            Xl_Image1.Bitmap = LegacyHelper.ImageHelper.Converter(.avatar)

            If .Birth >= DateTimePickerBorn.MinDate Then
                DateTimePickerBorn.Value = .Birth
            Else
                DateTimePickerBorn.Value = DateTimePickerBorn.MinDate
            End If

            If .Alta >= DateTimePickerAlta.MinDate Then
                DateTimePickerAlta.Value = .Alta
            Else
                DateTimePickerAlta.Value = DateTimePickerAlta.MinDate
            End If

            If .Baixa > DateTimePickerBaixa.MinDate And .Baixa < DateTimePickerBaixa.MaxDate Then
                CheckBoxBaixa.Checked = True
                DateTimePickerBaixa.Value = .Baixa
            Else
                CheckBoxBaixa.Checked = False
                DateTimePickerBaixa.Visible = False
                DateTimePickerBaixa.Value = DateTimePickerBaixa.MinDate
            End If

            If FEB2.Iban.Load(.Iban, exs) Then
                Await Xl_Iban1.Load(.Iban)
            Else
                UIHelper.WarnError(exs)
            End If

            Await refrescaVisas()

            _AllowEvents = True
        End With
    End Function

    Public ReadOnly Property Staff As DTOStaff
        Get
            With _Staff
                .Abr = TextBoxAlias.Text
                .NumSs = TextBoxSegSoc.Text
                .Category = Xl_LookupStaffCategory1.StaffCategory
                .StaffPos = Xl_LookupStaffPos1.StaffPos
                .avatar = LegacyHelper.ImageHelper.Converter(Xl_Image1.Bitmap)
                .Sex = Xl_Sex1.Sex
                .Birth = DateTimePickerBorn.Value
                .Alta = DateTimePickerAlta.Value
                If CheckBoxBaixa.Checked Then
                    .Baixa = DateTimePickerBaixa.Value
                Else
                    .Baixa = Nothing
                End If
                .Iban = Xl_Iban1.Value
            End With
            Return _Staff
        End Get
    End Property


    Private Async Sub refrescaVisas(sender As Object, e As MatEventArgs)
        Await refrescaVisas()
    End Sub
    Private Async Function refrescaVisas() As Task
        Dim exs As New List(Of Exception)
        Dim oVisas = Await FEB2.VisaCards.All(exs, Current.Session.Emp, _Staff)
        If exs.Count = 0 Then
            If oVisas.Count = 0 Then
                LabelVisaCards.Text = "(no te cap tarja de crèdit assignada)"
            Else
                Dim sb As New System.Text.StringBuilder
                For Each oVisa As DTOVisaCard In oVisas
                    sb.AppendLine(oVisa.Emisor.Nom & " " & oVisa.Digits)
                Next
                LabelVisaCards.Text = sb.ToString
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub CheckBoxBaixa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBaixa.CheckedChanged
        DateTimePickerBaixa.Visible = CheckBoxBaixa.Checked
        If CheckBoxBaixa.Checked Then
            If DateTimePickerBaixa.Value = DateTimePicker.MinimumDateTime Then
                DateTimePickerBaixa.Value = Today
            End If
        End If

        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub ControlChanged() Handles _
        TextBoxAlias.TextChanged,
        TextBoxSegSoc.TextChanged,
        Xl_LookupStaffCategory1.AfterUpdate,
        DateTimePickerBorn.ValueChanged,
        DateTimePickerAlta.ValueChanged,
        DateTimePickerBaixa.ValueChanged,
        Xl_LookupStaffPos1.AfterUpdate,
         Xl_Image1.AfterUpdate,
          Xl_Sex1.AfterUpdate

        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub LabelVisaCards_DoubleClick(sender As Object, e As EventArgs) Handles LabelVisaCards.DoubleClick
        Dim oFrm As New Frm_VisaCards
        AddHandler oFrm.AfterUpdate, AddressOf refrescaVisas
        oFrm.Show()
    End Sub

    Private Sub Xl_Iban1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Iban1.RequestToAddNew
        Dim oIban = DTOIban.Factory(GlobalVariables.Emp, _Staff, DTOIban.Cods.Staff)
        Dim oFrm As New Frm_IbanCcc(oIban)
        AddHandler oFrm.AfterUpdate, AddressOf onIbanUpdated
        oFrm.Show()
    End Sub

    Private Async Sub onIbanUpdated(sender As Object, e As MatEventArgs)
        Await Xl_Iban1.Load(e.Argument)
    End Sub


    Private Sub Xl_Iban1_RequestToChange(sender As Object, e As MatEventArgs) Handles Xl_Iban1.RequestToChange
        Dim oIban As DTOIban = e.Argument
        If oIban Is Nothing Then
            Xl_Iban1_RequestToAddNew(sender, e)
        Else
            Select Case oIban.Cod
                Case DTOIban.Cods.Client
                    Dim oFrm As New Frm_CustomerIbans(oIban.Titular)
                    oFrm.Show()
                Case Else
                    Dim oFrm As New Frm_IbanCcc(oIban)
                    AddHandler oFrm.AfterUpdate, AddressOf onIbanUpdated
                    oFrm.Show()
            End Select
        End If
    End Sub


End Class
