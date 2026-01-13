

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
    Friend WithEvents Xl_Laboral_Categoria1 As Xl_Laboral_Categoria
    Friend WithEvents Xl_IbanDigits1 As Mat.NET.Xl_IbanDigits
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelVisaCards As System.Windows.Forms.Label

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
        Me.Xl_IbanDigits1 = New Mat.NET.Xl_IbanDigits()
        Me.TextBoxAlias = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Sex1 = New Mat.NET.Xl_Sex()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Laboral_Categoria1 = New Mat.NET.Xl_Laboral_Categoria()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelVisaCards = New System.Windows.Forms.Label()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Num.Seg.Social:"
        '
        'TextBoxSegSoc
        '
        Me.TextBoxSegSoc.Location = New System.Drawing.Point(113, 38)
        Me.TextBoxSegSoc.Name = "TextBoxSegSoc"
        Me.TextBoxSegSoc.Size = New System.Drawing.Size(167, 20)
        Me.TextBoxSegSoc.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 109)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "neixament:"
        '
        'DateTimePickerBorn
        '
        Me.DateTimePickerBorn.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBorn.Location = New System.Drawing.Point(113, 109)
        Me.DateTimePickerBorn.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.DateTimePickerBorn.Name = "DateTimePickerBorn"
        Me.DateTimePickerBorn.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePickerBorn.TabIndex = 7
        '
        'DateTimePickerAlta
        '
        Me.DateTimePickerAlta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerAlta.Location = New System.Drawing.Point(113, 129)
        Me.DateTimePickerAlta.Margin = New System.Windows.Forms.Padding(3, 0, 3, 1)
        Me.DateTimePickerAlta.Name = "DateTimePickerAlta"
        Me.DateTimePickerAlta.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePickerAlta.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 130)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "alta:"
        '
        'DateTimePickerBaixa
        '
        Me.DateTimePickerBaixa.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerBaixa.Location = New System.Drawing.Point(113, 150)
        Me.DateTimePickerBaixa.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.DateTimePickerBaixa.Name = "DateTimePickerBaixa"
        Me.DateTimePickerBaixa.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePickerBaixa.TabIndex = 11
        '
        'CheckBoxBaixa
        '
        Me.CheckBoxBaixa.AutoSize = True
        Me.CheckBoxBaixa.Location = New System.Drawing.Point(17, 151)
        Me.CheckBoxBaixa.Name = "CheckBoxBaixa"
        Me.CheckBoxBaixa.Size = New System.Drawing.Size(51, 17)
        Me.CheckBoxBaixa.TabIndex = 10
        Me.CheckBoxBaixa.Text = "baixa"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Xl_IbanDigits1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 189)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(276, 101)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Transferencies"
        '
        'Xl_IbanDigits1
        '
        Me.Xl_IbanDigits1.Location = New System.Drawing.Point(17, 19)
        Me.Xl_IbanDigits1.Name = "Xl_IbanDigits1"
        Me.Xl_IbanDigits1.Size = New System.Drawing.Size(251, 71)
        Me.Xl_IbanDigits1.TabIndex = 0
        '
        'TextBoxAlias
        '
        Me.TextBoxAlias.Location = New System.Drawing.Point(113, 12)
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
        'Xl_Sex1
        '
        Me.Xl_Sex1.Location = New System.Drawing.Point(235, 109)
        Me.Xl_Sex1.Name = "Xl_Sex1"
        Me.Xl_Sex1.Sex = MaxiSrvr.Contact.Sexs.NotSet
        Me.Xl_Sex1.Size = New System.Drawing.Size(45, 45)
        Me.Xl_Sex1.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "categoría"
        '
        'Xl_Laboral_Categoria1
        '
        Me.Xl_Laboral_Categoria1.LaboralCategoria = Nothing
        Me.Xl_Laboral_Categoria1.Location = New System.Drawing.Point(113, 64)
        Me.Xl_Laboral_Categoria1.Name = "Xl_Laboral_Categoria1"
        Me.Xl_Laboral_Categoria1.Size = New System.Drawing.Size(197, 20)
        Me.Xl_Laboral_Categoria1.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 310)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Tarja de crèdit"
        '
        'LabelVisaCards
        '
        Me.LabelVisaCards.AutoSize = True
        Me.LabelVisaCards.Location = New System.Drawing.Point(26, 328)
        Me.LabelVisaCards.Name = "LabelVisaCards"
        Me.LabelVisaCards.Size = New System.Drawing.Size(176, 13)
        Me.LabelVisaCards.TabIndex = 17
        Me.LabelVisaCards.Text = "(no té assignada cap tarja de crèdit)"
        '
        'Xl_Contact_Staff
        '
        Me.Controls.Add(Me.LabelVisaCards)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Laboral_Categoria1)
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
        Me.Size = New System.Drawing.Size(359, 435)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mStaff As Staff = Nothing
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public WriteOnly Property Staff() As MaxiSrvr.Staff
        Set(ByVal value As MaxiSrvr.Staff)
            mStaff = value
            With mStaff
                TextBoxAlias.Text = .NomAlias
                TextBoxSegSoc.Text = .NumSS
                Xl_Sex1.Sex = .Sex
                Xl_Laboral_Categoria1.LaboralCategoria = .LaboralCategoria

                If .Born >= DateTimePickerBorn.MinDate Then
                    DateTimePickerBorn.Value = .Born
                Else
                    DateTimePickerBorn.Value = DateTimePickerBorn.MinDate
                End If

                If .Alta >= DateTimePickerAlta.MinDate Then
                    DateTimePickerAlta.Value = .Alta
                Else
                    DateTimePickerAlta.Value = DateTimePickerAlta.MinDate
                End If

                'DateTimePickerAlta.Value = .Alta
                If .Baixa > DateTimePickerBaixa.MinDate And .Baixa < DateTimePickerBaixa.MaxDate Then
                    CheckBoxBaixa.Checked = True
                    DateTimePickerBaixa.Value = .Baixa
                Else
                    CheckBoxBaixa.Checked = False
                    DateTimePickerBaixa.Visible = False
                    DateTimePickerBaixa.Value = DateTimePickerBaixa.MinDate
                End If

                'If .Iban IsNot Nothing Then
                Xl_IbanDigits1.Load(.Iban)
                'End If

                refrescaVisas()

                _AllowEvents = True
            End With
        End Set
    End Property

    Private Sub refrescaVisas()
        Dim oVisas As List(Of DTOVisaCard) = BLL.BLLVisaCards.All(New DTOContact(mStaff.Guid))
        If oVisas.Count = 0 Then
            LabelVisaCards.Text = "(no te cap tarja de crèdit assignada)"
        Else
            Dim sb As New System.Text.StringBuilder
            For Each oVisa As DTOVisaCard In oVisas
                sb.AppendLine(oVisa.Emisor.Nom & " " & oVisa.Digits)
            Next
            LabelVisaCards.Text = sb.ToString
        End If
    End Sub

    Public ReadOnly Property NumSS() As String
        Get
            Return TextBoxSegSoc.Text
        End Get
    End Property

    Public ReadOnly Property LaboralCategoria() As LaboralCategoria
        Get
            Return Xl_Laboral_Categoria1.LaboralCategoria
        End Get
    End Property

    Public ReadOnly Property Sex() As Contact.Sexs
        Get
            Return Xl_Sex1.Sex
        End Get
    End Property

    Public ReadOnly Property Born() As Date
        Get
            Return DateTimePickerBorn.Value
        End Get
    End Property

    Public ReadOnly Property Alta() As Date
        Get
            Return DateTimePickerAlta.Value
        End Get
    End Property

    Public ReadOnly Property Baixa() As Date
        Get
            If CheckBoxBaixa.Checked Then
                Return DateTimePickerBaixa.Value
            Else
                Return Date.MinValue
            End If
        End Get
    End Property

    Public ReadOnly Property IBAN() As DTOIban
        Get
            Dim retval As DTOIban = Nothing
            Dim sDigits As String = Xl_IbanDigits1.Value
            If BLL.BLLIban.Validated(sDigits) Then
                retval = New DTOIban
                retval.Digits = Xl_IbanDigits1.Value
                Dim oTitular As New DTOContact(mStaff.Guid)
                oTitular.Emp = BLL.BLLApp.Emp
                retval.Titular = oTitular
            End If
            Return retval
        End Get
    End Property

    Public ReadOnly Property NomAlias() As String
        Get
            Return TextBoxAlias.Text
        End Get
    End Property



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

    Private Sub RefreshIban(sender As Object, e As MatEventArgs)
        Dim oContact As DTOContact = New DTOContact(mStaff.Guid)
        Dim oIban As DTOIban = BLL.BLLIban.FromContact(oContact)
        If oIban IsNot Nothing Then
            Xl_IbanDigits1.Load(oIban.Digits)
        End If
    End Sub

    Private Sub ControlChanged() Handles TextBoxAlias.TextChanged, TextBoxSegSoc.TextChanged, Xl_Laboral_Categoria1.AfterUpdate, DateTimePickerBorn.ValueChanged, DateTimePickerAlta.ValueChanged, DateTimePickerBaixa.ValueChanged, Xl_Sex1.AfterUpdate, Xl_IbanDigits1.AfterUpdate
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Private Sub LabelVisaCards_DoubleClick(sender As Object, e As EventArgs) Handles LabelVisaCards.DoubleClick
        Dim oFrm As New Frm_VisaCards
        AddHandler oFrm.afterupdate, AddressOf refrescaVisas
        oFrm.Show()
    End Sub


End Class
